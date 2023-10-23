using Base.Types;
using CopyCopy.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadSafeList;

namespace CopyCopy.Base
{
    public partial class CopyFileHandler
    {
        private bool                         _skipCurrentOperation;
        private FileStream                   _fsd;
        private FileStream                   _fss;
        private CancellationToken            _ctoken;
        private TaskCompletionSource<bool>   _stepTowardComplete;
        private CancellationTokenSource      _cts;
        private Delegate                     _callbackStatus;
        
        private OperationStatusResponse      _lastOperationResponse;
        private OperationStatusResponse      _lastSourceFailOperation;
        private OperationStatusResponse      _lastIoError;
        private OperationStatusResponse      _lastObjectDisposedError;
        private ulong                        _totalOperationVolume;
        private ulong                        _operationTotalBytes;
        private ulong                        _bytesReadedInCurrentOp;
        private CopyBackUp                   _backUpCopy;


        public ThreadSafeList<FileListItem> PendingFileOperations { get; set; }
        public int TotalOperations { get; set; }
        public int CountOperationsDone { get; set; }
        public bool IsOperationRunning { get; set; }
        public FileListItem ItemInOperation { get; private set; }


        public delegate OperationStatusResponse OperationStatus(OperationRisedStatus opStatus, object param);
        public delegate void                    OperationInPause(bool isPause);
        public delegate void                    TransferedVolumeHandler(ulong inCurrentOperation, ulong inTotal);
        public delegate void                    ItemInOperationChange(FileListItem item);
        public delegate void                    ItemListHandler(IEnumerable<FileListItem> itemsAdded);
        public delegate void                    TotalOperationVolumeChangeHandler(ulong totalVolOp);
        public delegate void                    TransferStatus(int currentPercent, int totalPercent, decimal transferRate);
        public delegate OperationStatusResponse FailSpaceRequirementHandler(ulong neededBytes, 
                                                                            ulong deviceVolume, 
                                                                            ulong operationVolume, 
                                                                            ulong deviceFreeSpace,
                                                                            string driveName,
                                                                            string driveLetter);


        public event TransferStatus                    OnTransferStatus;
        public event EventHandler                      SkipedOperation;
        public event EventHandler                      OperationTerminated;
        public event EventHandler                      OnCancelOperation;
        public event OperationInPause                  OnOperationPause;
        public event OperationStatus                   OperationRisedStatus;
        public event TotalOperationVolumeChangeHandler OnTotalVolumeChange;
        public event FailSpaceRequirementHandler       OnFailSpaceRequirements;
        public event ItemListHandler                   OnItemsAdded;
        public event ItemListHandler                   OnItemsRemove;
        public event ItemInOperationChange             OnItemInOperationChange;
        public event TransferedVolumeHandler           OnTransferVolumeChange;



        public CopyFileHandler(Delegate statuscallback)
        {
            _cts             = new CancellationTokenSource();
            _ctoken          = _cts.Token;
            _callbackStatus  = statuscallback;
        }



        public CopyFileHandler(bool doBackUp = true)
        {
            _cts                    = new CancellationTokenSource();
            _ctoken                 = _cts.Token;
            _totalOperationVolume   = 0;
            _bytesReadedInCurrentOp = 0;
            _operationTotalBytes    = 0;
            PendingFileOperations   = new ThreadSafeList<FileListItem>();

            if (doBackUp)
                _backUpCopy = new CopyBackUp();
        }

        

        public void PauseCopy() => IsOperationRunning = true;



        public void ResumeCopy() => IsOperationRunning = false;



        public void CancelCopy() => _cts.Cancel();



        public async Task StepToward()
        {
            _stepTowardComplete   = new TaskCompletionSource<bool>();
            _skipCurrentOperation = true;
            await _stepTowardComplete.Task;
            _skipCurrentOperation = false;
        }



        public void AddFileItems(IEnumerable<FileListItem> files)
        {
            var    destinationFolder = files.FirstOrDefault();
            bool   abort             = false;
            ulong  deviceFreeSpace   = 0;
            ulong  neededBytes       = 0;
            ulong  deviceVolume      = 0;
            ulong  totalOpVolume     = (ulong)files.Sum(x => x.Size) + 
                (ulong)(ItemInOperation != null ? ItemInOperation.Size : 0) + 
                (ulong)PendingFileOperations.Sum(z=>z.Size);


            while (!IsSpaceEnough(destinationFolder.GetDestinationDriveLetter(), totalOpVolume, ref deviceFreeSpace, ref neededBytes, ref deviceVolume))
            {
                var statusResponse = OnFailSpaceRequirements?.EndInvoke(
                    OnFailSpaceRequirements?.BeginInvoke(
                        neededBytes,
                        deviceVolume,
                        totalOpVolume,
                        deviceFreeSpace,
                        files.FirstOrDefault()?.GetDestinationDriveLetter(),
                        files.FirstOrDefault()?.GetDestinationDriveName(),
                        null,
                        null));

                if (statusResponse == OperationStatusResponse.Abort)
                {
                    abort = true;
                    break;
                }
                else if (statusResponse == OperationStatusResponse.Ignore)
                    break;
            }


            if (!abort)
            {
                _totalOperationVolume += (ulong)files.Sum(x => x.Size);
                PendingFileOperations.AddRange(files);
                TotalOperations += files.Count();
                OnItemsAdded?.Invoke(files);
                OnTotalVolumeChange?.Invoke(_totalOperationVolume);
                _backUpCopy?.SaveCopyList(PendingFileOperations.ToList());
            }
        }



        public void RemoveFileItems(IEnumerable<FileListItem> files)
        {
            foreach (FileListItem fileOps in files)
                PendingFileOperations.Remove(fileOps);
            
            _totalOperationVolume -= (ulong)files.Sum(x => x.Size);
            TotalOperations -= files.Count();
            OnItemsRemove?.Invoke(files);
            OnTotalVolumeChange?.Invoke(_totalOperationVolume);
            _backUpCopy?.SaveCopyList(PendingFileOperations.ToList());
        }



        public void RemoveFileItem(FileListItem file)
        {
            PendingFileOperations.Remove(file);

            _totalOperationVolume -= (ulong)file.Size;
            TotalOperations -= 1;
            OnItemsRemove?.Invoke(new List<FileListItem> { file });
            OnTotalVolumeChange?.Invoke(_totalOperationVolume);
            _backUpCopy?.SaveCopyList(PendingFileOperations.ToList());
        }



        public void DeleteCurrentDestinationFile()
        {
            if (ItemInOperation != null)
                if (File.Exists(ItemInOperation.Destination))
                    try
                    {
                        File.Delete(ItemInOperation.Destination);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
        }



        public string RenameNewFile(string destFile)
        {
            int    count       = 1;
            string filename    = Path.GetFileNameWithoutExtension(destFile);
            string extension   = Path.GetExtension(destFile);
            string path        = Path.GetDirectoryName(destFile);
            string newFullPath = destFile;

            while (File.Exists(newFullPath))
            {
                string tempFilename = string.Format("{0} ({1})", filename, count++);
                newFullPath         = Path.Combine(path, tempFilename + extension);
            }

            return newFullPath;
        }




        public bool IsSpaceEnough(string path, ulong operationTotalVolume, ref ulong deviceFreeSpace, ref ulong neededBytes, ref ulong deviceVolume)
        {
            long available = 0, total = 0, free = 0;
            GetDiskFreeSpaceEx(path, ref available, ref total, ref free);

            if ((ulong)available > operationTotalVolume)
                return true;
            else
            {
                deviceFreeSpace = (ulong)free;
                neededBytes     = operationTotalVolume - (ulong)available;
                deviceVolume    = (ulong)total;

                return false;
            }
        }




        public async void StartOperations()
        {
            docopy:
            ItemInOperation = PendingFileOperations.FirstOrDefault();
            if (ItemInOperation == null)
                goto end;
            
            PendingFileOperations.Remove(ItemInOperation);
            retryOperation:

            try
            {
                if (!Directory.Exists(ItemInOperation.DestinationDirectory))
                    Directory.CreateDirectory(ItemInOperation.DestinationDirectory);
            }
            catch { }


            if (!File.Exists(ItemInOperation.Source))
            {
                if ((_lastSourceFailOperation & OperationStatusResponse.DoAllways) != OperationStatusResponse.DoAllways)
                    _lastSourceFailOperation = OperationRisedStatus.EndInvoke(
                       OperationRisedStatus.BeginInvoke(
                           Base.OperationRisedStatus.SourceFileNoExists,
                           ItemInOperation.Source,
                           null,
                           null));


                if (_lastSourceFailOperation == OperationStatusResponse.Retry)
                    goto retryOperation;
                else if (_lastSourceFailOperation == OperationStatusResponse.Abort)
                    goto end;
                //else // Discard or else
                //    continue;
            }




            if (File.Exists(ItemInOperation.Destination))
            {
                if ((_lastOperationResponse & OperationStatusResponse.DoAllways) != OperationStatusResponse.DoAllways)
                    _lastOperationResponse = OperationRisedStatus.EndInvoke(
                        OperationRisedStatus.BeginInvoke(
                            Base.OperationRisedStatus.DestinationFileExists,
                            ItemInOperation.Destination,
                            null,
                            null));



                if ((_lastOperationResponse & OperationStatusResponse.Abort) == OperationStatusResponse.Abort)
                    goto end;
                else if ((_lastOperationResponse & OperationStatusResponse.DoRename) == OperationStatusResponse.DoRename)
                    ItemInOperation.Destination = RenameNewFile(ItemInOperation.Destination);
                else if ((_lastOperationResponse & OperationStatusResponse.Retry) == OperationStatusResponse.Retry)
                    goto retryOperation;
            }



            try
            {
                using (_fsd = new FileStream(ItemInOperation.Destination, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, 64*1024))
                {
                    using (_fss = new FileStream(ItemInOperation.Source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long filelen = _fss.Length;
                        int bufferlen = 0;

                        if (filelen < (8 * 1024))
                            bufferlen = 8 * 1024;
                        else if (filelen > (8 * 1024) && filelen < (16 * 1024))
                            bufferlen = 16 * 1024;
                        else if (filelen > 16 * 1024 && filelen < (32 * 1024))
                            bufferlen = (32 * 1024);
                        else if (filelen > (32 * 1024) && filelen < (64 * 1024))
                            bufferlen = (64 * 1024);
                        else
                            bufferlen = 1024 * 1024;

                        byte[] buffer       = new byte[bufferlen];
                        int breads          = 0;
                        decimal bxsecond    = 0;
                        Stopwatch sw        = new Stopwatch();

                        sw.Start();
                        OnItemInOperationChange?.BeginInvoke(ItemInOperation, null, null);

                        do
                        {

                            if (_skipCurrentOperation)
                            {
                                _fsd?.Close();
                                _fss?.Close();

                                if (File.Exists(_fsd.Name))
                                    try { File.Delete(_fsd.Name); } catch { }

                                _operationTotalBytes -= _bytesReadedInCurrentOp;
                                RemoveFileItem(ItemInOperation);
                                SkipedOperation?.Invoke(null, null);
                                _stepTowardComplete.SetResult(true);
                                goto stepTowardOperation;
                            }

                            
                            try
                            {
                                breads = await _fss.ReadAsync(buffer, 0, bufferlen);
                                _bytesReadedInCurrentOp += (ulong)breads;
                                _operationTotalBytes += (ulong)breads;

                                // Calculo de porciento de la copia del archivo
                                int percent = (int)(_bytesReadedInCurrentOp == 0 ? 1 : _bytesReadedInCurrentOp * 100 / (ulong)filelen);
                                int generalPercent = (int)(_operationTotalBytes * 100 / _totalOperationVolume);

                                await _fsd.WriteAsync(buffer, 0, breads);
                                
                                try { bxsecond = (decimal)(_bytesReadedInCurrentOp / sw.Elapsed.TotalSeconds); } catch { }

                                OnTransferVolumeChange?.BeginInvoke((ulong)_operationTotalBytes, _totalOperationVolume, null, null);
                                OnTransferStatus?.BeginInvoke(percent, generalPercent, bxsecond, null, null);
                            }
                            catch (IOException ex)
                            {
                                if ((_lastIoError & OperationStatusResponse.DoAllways) != OperationStatusResponse.DoAllways)
                                    _lastIoError = OperationRisedStatus.EndInvoke(
                                       OperationRisedStatus.BeginInvoke(
                                           Base.OperationRisedStatus.IoError,
                                           ex,
                                           null,
                                           null));


                                if (_lastIoError == OperationStatusResponse.Retry)
                                {
                                    _operationTotalBytes -= _bytesReadedInCurrentOp;
                                    _bytesReadedInCurrentOp = 0;
                                    goto retryOperation;
                                }
                                else if (_lastIoError == OperationStatusResponse.Abort)
                                    break;
                                //else // Discard or else
                                //    continue;
                            }
                            catch (ObjectDisposedException obj)
                            {
                                if ((_lastObjectDisposedError & OperationStatusResponse.DoAllways) != OperationStatusResponse.DoAllways)
                                    _lastObjectDisposedError = OperationRisedStatus.EndInvoke(
                                       OperationRisedStatus.BeginInvoke(
                                           Base.OperationRisedStatus.IoError,
                                           obj,
                                           null,
                                           null));


                                if (_lastObjectDisposedError == OperationStatusResponse.Retry)
                                {
                                    _operationTotalBytes -= _bytesReadedInCurrentOp;
                                    _bytesReadedInCurrentOp = 0;
                                    goto retryOperation;
                                }
                                else if (_lastObjectDisposedError == OperationStatusResponse.Abort)
                                    break;
                                //else // Discard or else
                                //    continue;
                            }

                            
                            if (IsOperationRunning)
                            {
                                sw.Stop();
                                OnOperationPause?.Invoke(true);
                                while (IsOperationRunning && !_cts.IsCancellationRequested) await Task.Delay(1000);
                                OnOperationPause?.Invoke(false);
                                sw.Start();
                            }
                        }
                        while (breads > 0 && !_cts.IsCancellationRequested);
                    }
                }
            }
            catch(SecurityException)
            {
                var r = RetryOnInvokeAccessException();
                if (r == 1)
                    goto retryOperation;
                else if(r == -1)
                    goto end;
                // else (2) -> Ignore
            }
            catch(UnauthorizedAccessException)
            {
                var r = RetryOnInvokeAccessException();
                if (r == 1)
                    goto retryOperation;
                else if (r == -1)
                    goto end;
                // else (2) -> Ignore
            }
            catch(IOException ex)
            {
                if ((_lastIoError & OperationStatusResponse.DoAllways) != OperationStatusResponse.DoAllways)
                    _lastIoError = OperationRisedStatus.EndInvoke(
                       OperationRisedStatus.BeginInvoke(
                           Base.OperationRisedStatus.IoError,
                           ex,
                           null,
                           null));


                if (_lastIoError == OperationStatusResponse.Retry)
                {
                    _operationTotalBytes -= _bytesReadedInCurrentOp;
                    _bytesReadedInCurrentOp = 0;
                    goto retryOperation;
                }
                else if (_lastIoError == OperationStatusResponse.Abort)
                    goto end;
            }

            if (_cts.IsCancellationRequested)
                goto end;

            CountOperationsDone++;

            stepTowardOperation:
            _bytesReadedInCurrentOp = 0;
           
            goto docopy;

            end:

            OnItemInOperationChange?.Invoke(null);
            OnTransferStatus?.Invoke(100, 100, 0);
            

            if (!_cts.IsCancellationRequested)
                OperationTerminated?.Invoke(null, null);
            else
                OnCancelOperation?.Invoke(null, null);
        }


        private int RetryOnInvokeAccessException()
        {
            var response = OperationRisedStatus?.Invoke(Base.OperationRisedStatus.AccessException, ItemInOperation);
            if (response == OperationStatusResponse.Retry)
            {
                _operationTotalBytes -= _bytesReadedInCurrentOp;
                return 1;
            }
            else if (response == OperationStatusResponse.Ignore)
                return 2;
            return -1; // ABORT
        }


        [DllImport("kernel32")]
        private static extern int GetDiskFreeSpaceEx(
            string   lpDirectoryName, 
            ref long lpFreeBytesAvailable,
            ref long lpTotalNumberOfBytes, 
            ref long lpTotalNumberOfFreeBytes);
    }




    public enum OperationRisedStatus:byte
    {
        SourceFileNoExists,
        DestinationFileExists,
        SpaceRequirementsFail,
        IoError,
        ObjectDisposedError,
        AccessException
    }


    [Flags]
    public enum OperationStatusResponse:byte
    {
        DoAllways   = 1,
        DoOverwrite = 2,
        DoRename    = 4,
        Discard     = 8,
        Retry       = 16,
        Abort       = 32,
        Ignore      = 64
    }
}