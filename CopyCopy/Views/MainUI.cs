using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpcCore;
using CopyCopyIpcPackets;
using System.IO;
using CopyCopy.Types;
using CopyCopy.Base;
using CopyCopy.Controls;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.WindowsAPICodePack.Taskbar;
using static CopyCopy.Base.Settings;
using System.Security.AccessControl;

namespace CopyCopy
{
    public partial class MainUI : Form
    {
        private bool                       _forceCopy;
        private bool                       _doSameOp;
        private bool                       _terminatedOperations;
        private TaskCompletionSource<bool> _completationSource;
        private string                     _ipcChannelName;
        private IpcClient                  _ipcClient;
        private bool                       _swSeeCopyList;
        private CopyFileHandler            _copyFileHandler;
        private TaskCompletionSource<bool> _isArgsSetted;
        private FileListItem               _itemInOp;

        

        public MainUI()
        {
            InitializeComponent();
        }


        public MainUI(string[] args)
        {

            AppSettings.LoadSettings();
            _isArgsSetted = new TaskCompletionSource<bool>();

            if(AppSettings.Options.ShowIconPerCopyInTaskBar)
                TaskbarManager.Instance.SetApplicationIdForSpecificWindow(Handle, Guid.NewGuid().ToString());

            InitializeComponent();
            
            for (int i = 0; i < args.Length; i++)
                if (args[i] == "ipc")
                    _ipcChannelName = args[++i];
                else if (args[i] == "forceCopy")
                    _forceCopy = true;

            if (string.IsNullOrEmpty(_ipcChannelName))
                throw new Exception("Channel name can't be null or empty");
            _isArgsSetted.SetResult(true);
            olvColumn3.Sortable = true;
            olv_queueList.AfterSorting += Olv_queueList_AfterSorting;
        }




        private void Olv_queueList_AfterSorting(object sender, BrightIdeasSoftware.AfterSortingEventArgs e)
        {
            var model = olv_queueList.Objects.Cast<FileListItem>();
            List<FileListItem> sorted = new List<FileListItem>();
            List<int> sIdx = new List<int>();
            
            foreach (var itm in model)
                sIdx.Add(olv_queueList.IndexOf(itm));
            sIdx.Sort();

            foreach (var idx in sIdx)
                sorted.Add((FileListItem)olv_queueList.GetModelObject(idx));
            
            _copyFileHandler.PendingFileOperations = sorted;
        }


        
        private void _ipcClient_OnMessageArrive(IpcPacketWrapper packet, Ipc stream)
        {
            
            switch(packet.Packet)
            {
                case List<FileOperationIpcPacket> operation:
                    Task.Factory.StartNew(() => { AddItemToListFromIPC(operation); });
                    break;
            }
        }



        
        private void AddItemToListFromIPC(List<FileOperationIpcPacket> packets)
        {
            var _totalOperationItems = new List<FileListItem>();
            
            foreach (var packet in packets)
                if (File.Exists(packet.Source))
                    _totalOperationItems.Add(new FileListItem(packet.Source, packet.Dest));
                else
                    // Element is treated as a directory entry
                    _totalOperationItems.AddRange(FileListItem.BuildFileListItems(packet.Source, packet.Dest));
            


            if (_copyFileHandler == null)
                InitializeCopyHandler(_totalOperationItems);
            else
                _copyFileHandler.AddFileItems(_totalOperationItems);
        }



        private void ForceCopy(IEnumerable<FileListItem> cItems) => InitializeCopyHandler(cItems, false);




        private void InitializeCopyHandler(IEnumerable<FileListItem> operations, bool doBackup = true)
        {
            _copyFileHandler = new CopyFileHandler(doBackup);

            _copyFileHandler.OperationRisedStatus    += _copyFileHandler_OperationRisedStatus;
            _copyFileHandler.OnTotalVolumeChange     += _copyFileHandler_OnTotalVolumeChange;
            _copyFileHandler.OnTransferStatus        += _copyFileHandler_OnTransferStatus;
            _copyFileHandler.OperationTerminated     += _copyFileHandler_OperationTerminated;
            _copyFileHandler.OnOperationPause        += _copyFileHandler_OnOperationPause;
            _copyFileHandler.OnFailSpaceRequirements += _copyFileHandler_OnFailSpaceRequirements;
            _copyFileHandler.OnItemsAdded            += _copyFileHandler_OnItemsAdded;
            _copyFileHandler.OnItemsRemove           += _copyFileHandler_OnItemsRemove;
            _copyFileHandler.OnItemInOperationChange += _copyFileHandler_OnItemInOperation;
            _copyFileHandler.OnTransferVolumeChange  += _copyFileHandler_OnTransferVolumeChange;
            _copyFileHandler.OnCancelOperation       += _copyFileHandler_OnCancelOperation;

            _copyFileHandler.AddFileItems(operations);
            _copyFileHandler.StartOperations();
            BeginInvoke(new Action(() => { btn_pauseResumeSw.Text = "Pausar"; }));
        }



        private void _copyFileHandler_OnCancelOperation(object sender, EventArgs e)
        {
            _ipcClient.Dispose();
            _terminatedOperations = true;
            _copyFileHandler.DeleteCurrentDestinationFile();
            _completationSource.SetResult(true);
        }



        private void _copyFileHandler_OnTransferVolumeChange(ulong inCurrentOperation, ulong inTotal)
        {
            EndInvoke(BeginInvoke(new Action(() => 
            {
                decimal trInCurrOp      = ConvertBytes(inCurrentOperation, out string messureInCurrOp);
                decimal trInAllOp       = ConvertBytes(inTotal, out string messureInAllOp);
                lbl_currentVolume.Text  = $"{trInCurrOp} {messureInCurrOp}";
                lbl_totalVolume.Text    = $"{trInAllOp} {messureInAllOp}";
            })));
        }



        private void _copyFileHandler_OnItemInOperation(FileListItem item)
        {
            EndInvoke(BeginInvoke(new Action(() =>
            {
                if (item != null)
                {
                    _itemInOp = item;
                    olv_queueList.RemoveObject(item);
                    lbl_copyingFrom.Text = item.Source;
                    lbl_copyingTo.Text = item.Destination;
                }
                lbl_totalFilesCount.Text = $"Elementos: {_copyFileHandler.CountOperationsDone}/{_copyFileHandler.TotalOperations.ToString()}";
            })));
        }




        private void _copyFileHandler_OnItemsRemove(IEnumerable<FileListItem> items)
        {
            EndInvoke(BeginInvoke(new Action(() =>
            {
                lbl_totalFilesCount.Text = $"Elementos: {_copyFileHandler.CountOperationsDone}/{_copyFileHandler.TotalOperations.ToString()}";
                olv_queueList.RemoveObjects(items.ToList());
            })));
        }




        private void _copyFileHandler_OnItemsAdded(IEnumerable<FileListItem> items)
        {
            EndInvoke(BeginInvoke(new Action(() =>
            {
                lbl_totalFilesCount.Text = $"Elementos: {_copyFileHandler.CountOperationsDone}/{_copyFileHandler.TotalOperations.ToString()}";
                olv_queueList.AddObjects(items.ToList());
            })));
        }




        private OperationStatusResponse _copyFileHandler_OnFailSpaceRequirements(
            ulong  neededBytes, 
            ulong  deviceVolume, 
            ulong  operationVolume, 
            ulong  deviceFreeSpace,
            string driveLetter,
            string drivename)
        {

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);

            string deviceDescrpt = "";
            if (string.IsNullOrEmpty(drivename))
                deviceDescrpt = driveLetter;
            else
                deviceDescrpt = $"{drivename} ({driveLetter})";

            decimal devVol   = ConvertBytes(deviceVolume, out string devMesUnit);
            decimal devNeeds = ConvertBytes(neededBytes, out string devNeedsMesUnit);
            decimal devFree  = ConvertBytes(deviceFreeSpace, out string devFreeMesUnit);
            decimal opVolm   = ConvertBytes(operationVolume, out string opVolmMesureUnt);

            string message = $"El dispositivo {deviceDescrpt} no tiene espacio suficiente\r\n" +
                $"\r\n\r\n" +
                    $"Dispositivo:                    {devVol} {devMesUnit}\r\n" +
                    $"Necesita:                       {devNeeds} {devNeedsMesUnit}\r\n" +
                    $"Libre:                             {devFree} {devFreeMesUnit}\r\n" +
                    $"Volumen de operacion: {opVolm} {opVolmMesureUnt}\r\n\r\n" +
                $"¿Que desea hacer?\r\n" +
                    $"\t\"Reintentar\": Verifica el espacio disponible\r\n" +
                    $"\t\"Abortar\": Cancela todas las operaciones y cierra la ventana\r\n" +
                    $"\t\"Ignorar\": Copia hasta donde de el espacio";

            DialogButtons buttons = DialogButtons.IgnoreButton | DialogButtons.RetryButton | DialogButtons.AbortButton;
            var dialogResult = DialogForm.Show(this, message, buttons);

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);

            if (dialogResult == DialogFormResult.Retry)
                return OperationStatusResponse.Retry;
            else if (dialogResult == DialogFormResult.Abort)
            {
                var msgB = MessageBox.Show(this, "¿Esta seguro de abortar?. " +
                    "Todas las operaciones pendientes seran canceladas " +
                    "y la ventana de copia va a cerrarse." +
                    "¿Esta seguro?", "CCUI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(msgB == DialogResult.Yes)
                    return OperationStatusResponse.Abort;
                else
                    return OperationStatusResponse.Retry;
            }
            else
                return OperationStatusResponse.Ignore;
        }




        private void _copyFileHandler_OnOperationPause(bool isPause)
        {
            if(isPause)
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            else
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }




        private void _copyFileHandler_OperationTerminated(object sender, EventArgs e)
        {
            BeginInvoke(new Action(()=>
            {
                if (AppSettings.Options.SoundWhenCopyFinish)
                {
                    try
                    {
                        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(AppSettings.Options.SoundPath);
                        sp.Play();
                    }
                    catch { }
                }

                // TODO
                // Send statistics to cccore

                _ipcClient.Dispose();

                _terminatedOperations     = true;
                btn_skipItem.Enabled      = false;
                btn_pauseResumeSw.Enabled = false;


                if (AppSettings.Options.CloseWindowWhenCopyFinish)
                    Close();
            }));
        }



        private void _copyFileHandler_OnTransferStatus(int currentPercent, int totalPercent, decimal transferRate)
        {
            var del = new Action(() =>
            {
                customProgressBar_cState.Value      = currentPercent;
                customProgressBar_gState.Value      = totalPercent;
                customProgressBar_gState.CustomText = $"{totalPercent}%";

                if (_itemInOp != null)
                {
                    decimal volume                      = ConvertBytes(_itemInOp.Size, out string meu);
                    customProgressBar_cState.CustomText = $"{currentPercent}% ({volume} {meu})";
                }



                if (currentPercent > 55)
                    customProgressBar_cState.TextColor = Color.White;
                else
                    customProgressBar_cState.TextColor = Color.Black;
                if (totalPercent > 50)
                    customProgressBar_gState.TextColor = Color.White;
                else
                    customProgressBar_gState.TextColor = Color.Black;

                if (AppSettings.Options.ShowUnitsRootInTitleBar)
                    Text = $"{_itemInOp?.GetSourceDriveLetter()} -> {_itemInOp?.GetDestinationDriveLetter()}";

                if (AppSettings.Options.ShowProgressOnTitleBar)
                    Text += $" {currentPercent}% - {totalPercent}%";
                
                decimal rate = ConvertBytes(transferRate, out string mu);
                lbl_bps.Text = $"{rate} {mu}/s";

                TaskbarManager.Instance.SetProgressValue(totalPercent, 100);
            });

            EndInvoke(BeginInvoke(del));
        }



        private void _copyFileHandler_OnTotalVolumeChange(ulong totalVolOp)
        {
            EndInvoke(BeginInvoke(new Action(() => {

                decimal opVolume = ConvertBytes(totalVolOp, out string totalVolMu);
                lbl_totalVolume.Text = $"{opVolume} {totalVolMu}";

            })));
        }



        private OperationStatusResponse _copyFileHandler_OperationRisedStatus(OperationRisedStatus rStatus, object param)
        {
            DialogButtons           buttons      = 0;
            OperationStatusResponse returnAction = 0;

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);

            switch (rStatus)
            {
                case OperationRisedStatus.DestinationFileExists:

                    buttons = DialogButtons.OverrideButton | DialogButtons.RenameButton | DialogButtons.AbortButton ;
                    var res = DialogForm.Show(this, "El archivo:\r\n" +
                                    $"\r\n{(string)param}\r\n\r\n" +
                                    "ya existe en el destino.\r\n¿Que desea hacer?\r\n" +
                                    "Abortar: Cancela las operaciones y cierra la ventana\r\n" +
                                    "Aplastar: Sobreescribe el archivo de destino\r\n" +
                                    "Renombrar: Renombra automaticamente el archivo nuevo", buttons, out _doSameOp);

                   

                    if (_doSameOp)
                        returnAction |= OperationStatusResponse.DoAllways;

                    if (res == DialogFormResult.Override)
                        returnAction |= OperationStatusResponse.DoOverwrite;
                    else if (res == DialogFormResult.Rename)
                        returnAction |= OperationStatusResponse.DoRename;
                    else
                        returnAction |= OperationStatusResponse.Abort;
                    break;




                case OperationRisedStatus.SourceFileNoExists:

                    buttons = DialogButtons.IgnoreButton | DialogButtons.RetryButton | DialogButtons.AbortButton;
                    var sourceNoExs = DialogForm.Show(this, "El archivo fuente\r\n" +
                                    $"\r\n{(string)param}\r\n\r\n" +
                                    "no existe.\r\n¿Que desea hacer?\r\n" +
                                    "Ignorar: Omite la operacion y pasa a la siguiente\r\n" +
                                    "Reintentar: Reintenta la operacion\r\n" +
                                    "Abortar: Cancela y termina todas las operaciones", buttons, out _doSameOp);
                    

                    if (_doSameOp)
                        returnAction |= OperationStatusResponse.DoAllways;

                    if (sourceNoExs == DialogFormResult.Retry)
                        returnAction |= OperationStatusResponse.Retry;
                    else if (sourceNoExs == DialogFormResult.Abort)
                        returnAction |= OperationStatusResponse.Abort;
                    else
                        returnAction |= OperationStatusResponse.Discard;
                    break;


                case OperationRisedStatus.IoError:

                    IOException ioExc = (IOException)param;

                    buttons = DialogButtons.RetryButton | DialogButtons.AbortButton;
                    var ioError = DialogForm.Show(this, $"Ocurrio un error en la operacion.\r\n" +
                                    $"\r\n{ioExc.Message}\r\n\r\n" +
                                    "¿Que desea hacer?:\r\n" +
                                    "Reintentar: Reintenta la operacion actual\r\n" +
                                    "Abortar: Cancela y termina todas las operaciones.", buttons);
                    

                    if (ioError == DialogFormResult.Retry)
                        returnAction |= OperationStatusResponse.Retry;
                    else if (ioError == DialogFormResult.Abort)
                        returnAction |= OperationStatusResponse.Abort;

                    break;


                case OperationRisedStatus.ObjectDisposedError:

                    ObjectDisposedException objDispExc = (ObjectDisposedException)param;

                    buttons = DialogButtons.IgnoreButton | DialogButtons.RetryButton | DialogButtons.AbortButton;
                    var objExc = DialogForm.Show(this, $"Ocurrio un error en la operacion.\r\n" +
                                    $"{objDispExc.Message}\r\n" +
                                    "¿Que desea hacer?:\r\n" +
                                    "\"Ignorar\": Ignora el error e intenta seguir copiando\r\n" +
                                    "\"Reintentar\": Reintenta la operacion actual\r\n" +
                                    "\"Abortar\": Cancela y termina todas las operaciones\r\n\r\n" +
                                    "!!!! No es recomendable la opcion de \"Hacer esto para todo\" ¡¡¡¡", buttons, out _doSameOp);


                    if (_doSameOp)
                        returnAction |= OperationStatusResponse.DoAllways;

                    if (objExc == DialogFormResult.Retry)
                        returnAction |= OperationStatusResponse.Retry;
                    else if (objExc == DialogFormResult.Abort)
                        returnAction |= OperationStatusResponse.Abort;
                    else
                        returnAction |= OperationStatusResponse.Discard;
                    break;


                case OperationRisedStatus.AccessException:

                    var message = "Hubo un problema de acceso en la operacion.\r\n" +
                        "¿Que desea hacer?\r\n" +
                        "Abortar: Cancelar todas las operaciones y cerrar la ventana\r\n" +
                        "Reintentar: Volver a probar la ultima operacion\r\n" +
                        "Ignorar: Ignora el problema actual e intenta seguir con la siguiente operacion";

                    buttons    = DialogButtons.AbortButton | DialogButtons.RetryButton | DialogButtons.IgnoreButton;
                    var result = DialogForm.Show(this, message, buttons);

                    if (result == DialogFormResult.Ignore)
                        returnAction |= OperationStatusResponse.Ignore;
                    else if (result == DialogFormResult.Abort)
                        returnAction |= OperationStatusResponse.Abort;
                    else
                        returnAction |= OperationStatusResponse.Retry;
                    break;
            }


            if((returnAction & OperationStatusResponse.Abort) == OperationStatusResponse.Abort)
            {
                var msgB = MessageBox.Show(this, "¿Esta seguro de abortar?. " +
                   "Todas las operaciones pendientes seran canceladas " +
                   "y la ventana de copia va a cerrarse." +
                   "¿Esta seguro?", "CCUI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (msgB == DialogResult.Yes)
                    return OperationStatusResponse.Abort;
                else
                    return OperationStatusResponse.Retry;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);

            return returnAction;
        }
        



        private void btn_seeCopyListSw_Click(object sender, EventArgs e)
        {
            if(_swSeeCopyList)
            {
                Height = MinimumSize.Height;
                _swSeeCopyList = false;
                btn_seeCopyListSw.Text = "Ver Lista";
            }
            else
            {
                if (Height > MinimumSize.Height)
                {
                    Height = MinimumSize.Height;
                    _swSeeCopyList = false;
                    btn_seeCopyListSw.Text = "Ver Lista";
                }
                else
                {
                    Height = 450;
                    _swSeeCopyList = true;
                    btn_seeCopyListSw.Text = "Ocultar Lista";
                }
            }
        }



        private void MainUI_Resize(object sender, EventArgs e)
        {
            if(Height > MinimumSize.Height)
            {
                _swSeeCopyList         = true;
                btn_seeCopyListSw.Text = "Ocultar Lista";
            }
            else
            {
                _swSeeCopyList         = false;
                btn_seeCopyListSw.Text = "Ver Lista";
            }
        }


        private void btn_pauseResumeSw_Click(object sender, EventArgs e)
        {
            if (_copyFileHandler.IsOperationRunning)
            {
                btn_pauseResumeSw.Text = "Pausar";
                _copyFileHandler.ResumeCopy();
            }
            else
            {
                btn_pauseResumeSw.Text = "Resumir";
                _copyFileHandler.PauseCopy();
            }
        }



        private void olv_queueList_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                var selectedElements = olv_queueList.SelectedObjects.Cast<FileListItem>();
                _copyFileHandler.RemoveFileItems(selectedElements);
            }
        }



        private async void btn_skipItem_Click(object sender, EventArgs e)
        {
            btn_skipItem.Enabled = false;
            await _copyFileHandler?.StepToward();
            btn_skipItem.Enabled = true;
        }



        private async void WaitForWindowsClosingEvent()
        {
            await _completationSource.Task;
            
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_terminatedOperations)
            {
                if (AppSettings.Options.AskWhenWindowIsForceClose)
                {
                    var res = MessageBox.Show(this, "¿Esta seguro de cancelar todas las tareas pendientes?", "Cerrar Copia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (res == DialogResult.OK)
                    {
                        e.Cancel = true;
                        _copyFileHandler?.CancelCopy();
                    }
                    else
                        e.Cancel = true;
                }
                else
                {
                    e.Cancel = true;
                    _copyFileHandler?.CancelCopy();
                }
            }
            else
                e.Cancel = false;
        }

        

        private decimal ConvertBytes(ulong volume, out string mesureUnit)
        {
            if (volume < 1024)
            {
                mesureUnit = "bytes";
                return volume;
            }
            else if(volume > 1024 && volume < (1024*1024))
            {
                mesureUnit = "kb";
                return Math.Round((decimal)volume / (decimal)1024, 2);
            }
            else if(volume > (1024*1024) && volume < (1024*1024*1024))
            {
                mesureUnit = "mb";
                return Math.Round((decimal)volume / (decimal)(1024 * 1024), 2);
            }
            else if(volume > (1024*1024*1024) && volume < (ulong)Math.Pow(1024,4))
            {
                mesureUnit = "gb";
                return Math.Round((decimal)volume / (decimal)(1024 * 1024 * 1024), 2);
            }
            else
            {
                mesureUnit = "tb";
                return Math.Round((decimal)volume / (decimal)Math.Pow(1024, 4),2);
            }
        }



        private decimal ConvertBytes(decimal volume, out string mesureUnit)
        {
            if (volume < 1024)
            {
                mesureUnit = "bytes";
                return volume;
            }
            else if (volume > 1024 && volume < (1024 * 1024))
            {
                mesureUnit = "kb";
                return Math.Round((decimal)volume / (decimal)1024, 2);
            }
            else if (volume > (1024 * 1024) && volume < (1024 * 1024 * 1024))
            {
                mesureUnit = "mb";
                return Math.Round((decimal)volume / (decimal)(1024 * 1024), 2);
            }
            else if (volume > (1024 * 1024 * 1024) && volume < (ulong)Math.Pow(1024, 4))
            {
                mesureUnit = "gb";
                return Math.Round((decimal)volume / (decimal)(1024 * 1024 * 1024), 2);
            }
            else
            {
                mesureUnit = "tb";
                return Math.Round((decimal)volume / (decimal)Math.Pow(1024, 4), 2);
            }
        }



        private async void MainUI_Shown(object sender, EventArgs e)
        {
            //AllocConsole();
            
            //AddItemToListFromIPC(new List<FileOperationIpcPacket>
            //{
            //    new FileOperationIpcPacket
            //    {
            //        Dest = @"D:\Peliculas",
            //        Source = @"\\10.40.36.18\i\PELICULAS\!SIN CONVERTIR\ACCION\[ACCION - ARTES MARCIALES] Attrition [2018] [SP]"
            //    }
            //});
            //return;
            await _isArgsSetted.Task;

            _completationSource         = new TaskCompletionSource<bool>();
            _ipcClient                  = new IpcClient(_ipcChannelName);
            _ipcClient.OnMessageArrive += _ipcClient_OnMessageArrive;

            WaitForWindowsClosingEvent();

            if (!string.IsNullOrEmpty(_ipcChannelName))
            {
                _ipcClient.Connect(out string error);
                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(this, $"No se pudo conectar con el servidor local. {error}", "IPC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ipcClient.StartListen();
                // await _ipcClient.Send(200);
            }

            SetForegroundWindow(Handle);
            TopMost = true;
            TopMost = false;
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);

            if (_forceCopy)
                ForceCopy();
        }



        private void ForceCopy()
        {
            OpenFileDialog filedialog = new OpenFileDialog()
            {
                Filter = "Json|*.json",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CopyCopyBackUp"
            };

            filedialog.ShowDialog(this);

            string fileccc = filedialog.FileName;

            if (File.Exists(fileccc))
            {
                retry:
                var backUpElements = CopyBackUp.LoadBackupFile(fileccc);
                string destinationSn = backUpElements.SerialNumber;
                string currentDestLetter = DiskDriveUtil.GetDriveLetterFromSn(destinationSn);

                if (currentDestLetter == null)
                {
                    DialogResult res = DialogResult.Cancel;

                    res = MessageBox.Show(this,
                        "No se pudo encontrar el dispositivo de destino para iniciar la copia desde un archivo de reespaldo.",
                        "CCUI",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Warning);

                    if (res == DialogResult.Retry)
                        goto retry;

                    _terminatedOperations = true;
                    Close();
                }
                else
                {
                    var fItem = backUpElements.CopyListItems.FirstOrDefault();
                    if (fItem != null)
                    {
                        string destLetter = fItem.GetDestinationDriveLetter();

                        if (destLetter != currentDestLetter)
                        {
                            foreach (var item in backUpElements.CopyListItems)
                            {
                                item.Destination = item.Destination.Replace(destLetter, currentDestLetter);
                                item.DestinationDirectory = item.DestinationDirectory.Replace(destLetter, currentDestLetter);
                            }
                        }
                        
                        Task.Factory.StartNew(()=> ForceCopy(backUpElements.CopyListItems));
                    }
                    else
                    {
                        MessageBox.Show(this,
                            "La lista de copias de respaldo esta vacia",
                            "CCUI",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        Close();
                        return;
                    }
                }


            }
            else
            {
                _terminatedOperations = true;
                Close();
            }
        }



        private void olv_queueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var elements = olv_queueList.SelectedObjects.Cast<FileListItem>();
            var selectionVolume = elements.Sum(x => x.Size);
            var size = ConvertBytes(selectionVolume, out string mu);
            lbl_selection.Text = $"Seleccion: {elements.Count()} ({size} {mu})";
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurityAttribute]
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr handle);
    }
}
