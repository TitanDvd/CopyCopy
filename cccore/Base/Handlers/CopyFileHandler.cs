using CcCore.Base.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CcCore.Base.Handlers
{
    public class CopyFileHandler
    {
        FileStream fsd;
        FileStream fss;
        CancellationToken ctoken;
        CancellationTokenSource cts;
        Action<int, int, ulong> _callbackStatus;
        TaskCompletionSource<bool> canceltransfer;
        Action<CopyStatus> _copystatecallback;

        bool copypause;
        bool skip;
        bool skipremove;

        public string DestFile
        {
            get
            {
                string dstfilepath = "";
                if (!DestPath.StartsWith("\\"))
                {
                    string destPathSubstr = DestPath.Substring(0, 3);
                    dstfilepath = DestPath.Replace(destPathSubstr, DestDrive.Letter);
                }
                else
                    dstfilepath = DestPath;

                if (!Directory.Exists(dstfilepath))
                    Directory.CreateDirectory(dstfilepath);
                
                return $"{dstfilepath}\\{FileInfo.Name}";
            }

            set
            {

            }
        }

        /// <summary>
        /// Archivo fuente el cual se va a copiar
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Directorio de destino, donde el usuario quiere colocar los archivos o directorios
        /// </summary>
        public string DestPath { get; set; }


        /// <summary>
        /// Si se especifica, define el directorio contenedor de los archivos de copia.
        /// O sea el contenedor para <see cref="SourceFile"/>
        /// </summary>
        public string DestDir { get; set; }


        /// <summary>
        /// Informacion sobre el dispositivo de almacenamniento de destino
        /// </summary>
        public DiskInfo DestDrive { get; set; }


        /// <summary>
        /// Informacion del archivo fuente
        /// </summary>
        public FileInfo FileInfo => new FileInfo(SourceFile);

        


        public CopyFileHandler(Action<int, int, ulong> statuscallback, Action<CopyStatus> copystatecallback)
        {
            cts = new CancellationTokenSource();
            ctoken = cts.Token;
            _callbackStatus = statuscallback;
            canceltransfer = new TaskCompletionSource<bool>();
            _copystatecallback = copystatecallback;
        }


        /// <summary>
        /// Comienza la copia de los archivos pasados al constructor
        /// </summary>
        /// <param name="statuscallback">Se invoca cada vez que se avanza en la pocision del stream. Contiene el porciento actual de la copia,
        /// la cantidad de MegaBytes leidos por segundo y un valor boleano que indica que el archivo ha sido omitido</param>
        /// <exception cref="IOException"></exception>
        public async Task CopyAsync()
        {
            using (fsd = new FileStream(DestFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (fss = new FileStream(SourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    long filelen = fss.Length;
                    long totalbytes = 0;
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

                    //if (DestDrive.FileSystem == "NTFS")
                    //    bufferlen = 1024 * 1024;

                    byte[] buffer = new byte[bufferlen];
                    int breads = -1;
                    //int totalReadsbxs = 0;
                    int bxsecond = 0;
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    
                    do
                    {
                        try
                        {
                            breads = await fss.ReadAsync(buffer, 0, bufferlen);
                        }
                        catch (IOException ex)
                        {
                            throw ex;
                        }
                        catch(ObjectDisposedException obj)
                        {
                            throw obj;
                        }

                        // Calculo de porciento de la copia del archivo
                        totalbytes += breads;
                        int percentage = (int)(totalbytes == 0 ? 1 : totalbytes * 100 / filelen);


                        if (copypause)
                        {
                            // [NOTA]* Esto puede ser inecesario ...
                            // probar quitarlo mas adelante
                            // _callbackStatus.BeginInvoke(percentage, 0, null, null);

                            // Interrumpe la copia
                            // Y espera hasta que copypause sea trues
                            while (copypause && !cts.IsCancellationRequested) await Task.Delay(1000);
                        }


                        // Calculando la velocidad de copia
                        // Si la cantidad de segundos total es mayor
                        // a 1. El metodo va a estar sumando la cantidad de datos
                        // que se leen del stream hasta tanto el total de segundos 
                        // sumen 1.
                        try
                        {
                            bxsecond = (int)(totalbytes / sw.Elapsed.TotalSeconds);
                        }
                        catch (DivideByZeroException) { }
                        //sw.Restart();
                        //if (sw.Elapsed.Seconds <= 1)
                        //    totalReadsbxs += breads;
                        //else
                        //{
                        //    bxsecond = totalReadsbxs;
                        //    totalReadsbxs = 0;
                        //    sw.Restart();
                        //}

                        // Si este valor es true
                        // El bucle omite el archivo
                        // Elimina el archivo de destino actual
                        // ... luego debe comenzar el siguiente en la cola
                        if (skip)
                        {
                            _callbackStatus.BeginInvoke(100, 0, (ulong)fss.Length - (ulong)totalbytes, null, null);
                            

                            fsd?.Close();
                            fss?.Close();
                            if (File.Exists(fsd.Name) && skipremove)
                                try
                                {
                                    File.Delete(fsd.Name);
                                }
                                catch
                                {

                                }
                            return;
                        }

                        try
                        {
                            await fsd.WriteAsync(buffer, 0, breads);
                            _callbackStatus.BeginInvoke(percentage, bxsecond, (ulong)breads, null, null);
                          
                        }
                        catch (IOException ex)
                        {
                            throw ex;
                        }
                        catch (ObjectDisposedException obj)
                        {
                            throw obj;
                        }
                    }
                    while (breads > 0 && !cts.IsCancellationRequested);
                }
            }

            if (cts.IsCancellationRequested)
                canceltransfer.SetResult(true);
        }


        public void PauseCopy()
        {
            copypause = true;
            _copystatecallback?.BeginInvoke(CopyStatus.Paused, null, null);
        }


        public void ResumeCopy()
        {
            copypause = false;
            _copystatecallback?.BeginInvoke(CopyStatus.Resumed, null, null);
        }


        async public Task CancelCopy()
        {
            cts.Cancel();
            bool result = await canceltransfer.Task;
            
            if (File.Exists(DestFile))
                try
                {
                    File.Delete(DestFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, $"No se pudo eliminar el archivo {DestFile}.\r\nError: {ex.Message}", "CopyCopy Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            _copystatecallback?.BeginInvoke(CopyStatus.Cancel, null, null);
        }


        public void StepFoward(bool removeSkipedFile = true)
        {
            skip = true;
            skipremove = removeSkipedFile;
            _copystatecallback?.BeginInvoke(CopyStatus.SkipFile, null, null);
        }


        public void RenameNewFile()
        {
            string newname = Guid.NewGuid().ToString();
            DestFile += newname;
        }


        public bool FileDestExist()
        {
            return File.Exists(DestFile);
        }
    }
}