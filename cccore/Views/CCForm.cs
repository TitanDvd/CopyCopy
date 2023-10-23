using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using CcCore.Base.Types;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using CcCore.Base;
using CcCore.Base.Handlers;
using Newtonsoft.Json;
using static CcCore.Base.CcSettings;

namespace CcCore.Views
{
    public partial class CopyCopyMF : ProgressForm
    {
        private bool _queueContainerOpen = false;
        private bool _swStartResume = false;
        private bool forceclose = false;
        private bool sameActionForAllError = false;
        private bool isFormAboutClose = false;
        private bool ignoreSpaveReq = false;
        private bool _manualC = false;
        private string BackUpFile = null;
        TaskCompletionSource<bool> closeFormTask; 
        private Action<int, int, ulong> _callbackstatus;
        private Action<CopyStatus> _copystatecallback;
        
        private CopyOptionsForm.ActionResult actionRepeat;
        private List<CopyFileHandler> CopyQueued { get; set; }
        private ulong QueueTotalSize
        {
            get
            {
                ulong tsize = 0;
                CopyCopyQueue.ForEach((CopyFileHandler file) => { tsize += (ulong)file.FileInfo.Length; });
                return tsize;
            }
        }

        public List<CopyFileHandler> CopyCopyQueue { get; set; }
        public DiskInfo Driver { get; set; }
        public CopyFileHandler CurrentFileHandle { get; set; }


        public CopyCopyMF()
        {
            // Indica al sistema que va a realizarse una copia manual
            // por el usuario.
            _manualC = true;
            
            closeFormTask = new TaskCompletionSource<bool>();
            _callbackstatus = new Action<int, int, ulong>(CopyProgressCallback);
            CopyCopyQueue = new List<CopyFileHandler>();
            CopyQueued = new List<CopyFileHandler>();
            _copystatecallback = new Action<CopyStatus>(CopyStateCallback);
            State = ThumbnailProgressState.Normal;
            Shown += CopyCopyMF_Shown;
            InitializeComponent();
        }


        public CopyCopyMF(List<string> sourceEntry, string destFolder, DiskInfo drive)
        {
            Driver = drive;
            closeFormTask = new TaskCompletionSource<bool>();
            _callbackstatus = new Action<int, int, ulong>(CopyProgressCallback);
            _copystatecallback = new Action<CopyStatus>(CopyStateCallback);
            CopyCopyQueue = new List<CopyFileHandler>();
            CopyQueued = new List<CopyFileHandler>();
            State = ThumbnailProgressState.Normal;
            List<CopyFileHandler> handlers = new List<CopyFileHandler>();
            BuildFilesHandlerForCopy(sourceEntry, null, destFolder, drive, ref handlers);
            CopyCopyQueue.AddRange(handlers);

            // Guarda una copia de la lista actual de elementos a copiar
            SaveCopyList(sourceEntry, destFolder, drive.VolumeSerial);

            Shown += CopyCopyMF_Shown;

            InitializeComponent();
        }


        private async void CopyCopyMF_Shown(object sender, EventArgs e)
        {
            if (!_manualC)
            {
                // Trae el formulario al primer plano en orden Z
                // Si esta minizmiado igualmente
                Activate();

                // Actualiza el estado del texto del formulario
                SetFormText();

                // Actualiza en la UI la etiquera de elementos transferido y los que faltan
                UpdateTransferLabelCount(0, CopyCopyQueue.Count);

                // Actualiza en la UI el volumen de datos transferidos y por transferir
                UpdateTransferVolumeData();

                // Agrega los elementos al cola de la UI
                olv_queueList.SetObjects(CopyCopyQueue);

                // Comenzar a analizar la cola de archivos
                // Y espera a que todos los elementos de la copia se terminen
                await ParseQueueFiles();

                CloseCopyInterface(true);
            }
            else
            {

                OpenFileDialog cccfile = new OpenFileDialog
                {
                    InitialDirectory = AppSettings.CC_BackUp,
                    Multiselect = false,
                    Filter = "Archivos CpyCopyBackup (*.ccb)|*.ccb"
                };

                cccfile.Title = "Buscar archivo de CopyCopy (*.ccb)";
                cccfile.ShowDialog();

                if (!string.IsNullOrEmpty(cccfile.FileName))
                {
                    //string text = "Desea elegir una ruta alternativa para la copia?\r\n" +
                    //"Si elige \"No\" se usara la ruta con la que se guardo la copia.\r\n" +
                    //"Si elige \"Si\" puede elegir una ruta alternativa para una lista de copias.\r\n" +
                    //"ATENCION!: Si decide usar una ruta alternativa todos los elementos tendran la misma ruta alternativa. Por ejemplo:\r\n" +
                    //"Si se guardaron dos listas con dos rutas de destino diferente, ambas listas tendran la misma ruta alternativa si elije \"SI\".";
                    //string altPath = "";
                    //DialogResult resul = MessageBox.Show(this, text, "Copia Manual", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    //if (resul == DialogResult.Yes)
                    //{
                    //    WK.Libraries.BetterFolderBrowserNS.BetterFolderBrowser fbrowser = new WK.Libraries.BetterFolderBrowserNS.BetterFolderBrowser();
                    //    fbrowser.Multiselect = false;
                    //    fbrowser.RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    //    fbrowser.ShowDialog();

                    //    altPath = fbrowser.SelectedFolder;
                    //}


                    // Carga la lista de copias guardada previamente
                    CCBackUpList bclist = LoadCopyList(cccfile.FileName);

                    // Busca el dispositivo local segun el VolumeSerial guardado
                    Driver = DeviceHandlers.ActiveDisks.FirstOrDefault(d => d.VolumeSerial == bclist.VolumeSerial);

                    // Si el dispositivo no existe, el usuario puede establecer una ruta alternativa para la copia
                    if (Driver == null)
                    {
                        // string text = "Desea elegir una ruta alternativa para la copia?\r\n" +
                        //"Si elige \"No\" se usara la ruta con la que se guardo la copia.\r\n" +
                        //"Si elige \"Si\" puede elegir una ruta alternativa para una lista de copias.\r\n" +
                        //"ATENCION!: Si decide usar una ruta alternativa todos los elementos tendran la misma ruta alternativa. Por ejemplo:\r\n" +
                        //"Si se guardaron dos listas con dos rutas de destino diferente, ambas listas tendran la misma ruta alternativa si elije \"SI\".";
                        string text = "No existe un dispositivo en el equipo que cumpla con los requisitos para iniciar la copia. ¿Desea usar una ruta alternativa?";
                        string altPath = "";
                        DialogResult resul = MessageBox.Show(this, text, "Copia Manual", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (resul == DialogResult.Yes)
                        {
                            // Vuelve aqui para elegir una carpeta de destino alternativa
                            retryAltPathBrowser:

                            WK.Libraries.BetterFolderBrowserNS.BetterFolderBrowser fbrowser = new WK.Libraries.BetterFolderBrowserNS.BetterFolderBrowser();
                            fbrowser.Multiselect = false;
                            fbrowser.RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                            fbrowser.ShowDialog();

                            altPath = fbrowser.SelectedFolder;

                            if (string.IsNullOrEmpty(altPath))
                            {
                                DialogResult res = MessageBox.Show(this, "Debe seleccionar una carpeta como ruta alternativa", "Copia manual", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                if (res == DialogResult.Retry)
                                    goto retryAltPathBrowser;
                                else
                                {
                                    CloseCopyInterface(true);

                                    return;
                                }
                            }

                            // Obtener la letra del dispositivo
                            string letter = "";

                            // UNC
                            if (altPath.StartsWith("\\"))
                                letter = new DirectoryInfo(altPath).Root.FullName;
                            else
                                letter = new DirectoryInfo(altPath).Root.Name;

                            // Iterar sobre todos los elementos guardados y
                            // cambiar su dispositivo de destino
                            List<KeyValuePair<string, List<string>>> replace = new List<KeyValuePair<string, List<string>>>();
                            foreach (var elements in bclist.FilesAndDestFolder)
                            {
                                // Key es la ruta de destino
                                string elementLetter = elements.Key.Substring(0, 3);
                                string newDest = elements.Key.Replace(elementLetter, altPath + "\\");

                                replace.Add(new KeyValuePair<string, List<string>>(newDest, elements.Value));
                            }

                            bclist.FilesAndDestFolder = replace;

                            // Verificar que el dispositivo exista en la lista de discos locales
                            // Si no existe se le asignan valores por defecto a la nueva instanca DiskInfo
                            Driver = DeviceHandlers.ActiveDisks.FirstOrDefault(d => d.Letter == letter) ?? new DiskInfo(letter) {VolumeSerial = letter };
                            


                            // Luego de cambiado todas las carpetas de destino
                            // Se genera la lista de copias tal cual CC lo neecsita
                            foreach (var ccclist in bclist.FilesAndDestFolder)
                            {
                                List<CopyFileHandler> handlres = new List<CopyFileHandler>();
                                BuildFilesHandlerForCopy(ccclist.Value, null, ccclist.Key, Driver, ref handlres);
                                CopyCopyQueue.AddRange(handlres);
                            }
                        }
                        else
                        {
                            CloseCopyInterface(true);

                            return;
                        }
                    }
                    else
                    {
                        foreach (var ccclist in bclist.FilesAndDestFolder)
                        {
                            List<CopyFileHandler> handlres = new List<CopyFileHandler>();
                            BuildFilesHandlerForCopy(ccclist.Value, null, ccclist.Key, Driver, ref handlres);
                            CopyCopyQueue.AddRange(handlres);
                        }
                    }

                    // Trae el formulario al primer plano en orden Z
                    // Si esta minizmiado igualmente
                    Activate();

                    // Actualiza el estado del texto del formulario
                    SetFormText();

                    // Actualiza en la UI la etiquera de elementos transferido y los que faltan
                    UpdateTransferLabelCount(0, CopyCopyQueue.Count);

                    // Actualiza en la UI el volumen de datos transferidos y por transferir
                    UpdateTransferVolumeData();

                    // Agrega los elementos al cola de la UI
                    olv_queueList.SetObjects(CopyCopyQueue);

                    // Comenzar a analizar la cola de archivos
                    // Y espera a que todos los elementos de la copia se terminen
                    await ParseQueueFiles();

                    CloseCopyInterface(true);
                }
            }
        }

        private void CopyStateCallback(CopyStatus status)
        {
            Action<CopyStatus> beginvkcb = (CopyStatus state) =>
            {
                switch (state)
                {
                    case CopyStatus.Paused:
                        State = ThumbnailProgressState.Paused;
                        break;

                    case CopyStatus.Fail:
                    case CopyStatus.Cancel:
                        State = ThumbnailProgressState.Error;
                        break;

                    case CopyStatus.Resumed:
                    case CopyStatus.Completed:
                        State = ThumbnailProgressState.Normal;
                        break;


                    default:
                        State = ThumbnailProgressState.Indeterminate;
                        break;
                }
            };

            BeginInvoke(beginvkcb, status);
        }



        private async Task ParseQueueFiles()
        {
            for (int i = 0; i < CopyCopyQueue.Count; i++)
            {
                CurrentFileHandle = (CopyFileHandler)olv_queueList.GetModelObject(0);

                if (CurrentFileHandle != null)
                {
                    // Calcular que haya espacio suficiente.
                    // Si es insuficiente se notifica al usuario
                    // Si se ignora la notificacion el sistema intentara copiar
                    // hasta que no quede espacio
                    // El usuario puede cancelar la copia o reintentar la copia
                    // ya que cabe la posibilidad de que el usuario libere espacio en el 
                    // dispositivo de almacenamiento
                    if (!ignoreSpaveReq)
                    {
                        // Volver a este punto si no se cumple con los requisitos de espacio
                        SpaceReq:

                        // Muestra el dialogo de error y almacena el resultado en la variable spacereqResult
                        DialogResult spacereqResult = SpaceRequirements();

                        switch (spacereqResult)
                        {
                            case DialogResult.Retry:
                                goto SpaceReq;

                            case DialogResult.Abort:
                                CloseCopyInterface(true);
                                return;

                            case DialogResult.Ignore:
                                ignoreSpaveReq = true;
                                break;
                        }
                    }


                    // Al iniciar se retira el elemento actual de la cola
                    olv_queueList.RemoveObject(CurrentFileHandle);


                    // Decidir que hacer si el archivo ya existe
                    if (CurrentFileHandle.FileDestExist())
                    {
                        CopyOptionsForm.ActionResult result = DoActionFileExist(CurrentFileHandle.DestFile, CurrentFileHandle.FileInfo);
                        switch (result)
                        {
                            case CopyOptionsForm.ActionResult.Skip:
                                CurrentFileHandle.StepFoward(false);
                                break;

                            case CopyOptionsForm.ActionResult.OverwriteNewer:
                            case CopyOptionsForm.ActionResult.OverwriteOlder:
                                // DO NOTHING
                                break;

                            case CopyOptionsForm.ActionResult.RenameNewer:
                            case CopyOptionsForm.ActionResult.RenameOlder:
                                CurrentFileHandle.RenameNewFile();
                                break;

                            case CopyOptionsForm.ActionResult.CancelCopy:
                                CloseCopyInterface(true);
                                break;
                        }
                    }


                    // Actualizando la informacion de las etiquetas que contienen la informacion
                    // del archivo fuente que se esta copiando
                    // y el archivo de destino
                    lbl_copyingFrom.Text = CurrentFileHandle.SourceFile;
                    lbl_copyingTo.Text = CurrentFileHandle.DestFile;



                    // Si falla la copia por alguna razon de IO
                    // El sistema reintentara copiar si el usuario
                    // asi lo ha especificado
                    retrycopy:

                    try
                    {

                        // Realizando la copia
                        await CurrentFileHandle.CopyAsync();
                    }
                    catch (IOException)
                    {
                        // Reintenta encontrar el dispositivo de destino de la copia
                        IOErrorRetry:

                        // Muestra el dialgo de error y guarda el resultado de la accion en result
                        DialogResult result = IOException();

                        if (result == DialogResult.Retry)
                        {
                            DiskInfo disk = DeviceHandlers.ActiveDisks.FirstOrDefault(d => d.VolumeSerial == Driver.VolumeSerial);

                            if (disk == null)
                                goto IOErrorRetry;
                            else
                            {
                                // A partir de aqui se debe actualizar la informacion de los archivos
                                // de copia y la unidad de destino antes de reiniciar la copia debido a la falla
                                // Esto es solo cuestion visual ya que internamente, la referencia de los archivos
                                // de destino se cambian autmaticamente al cambiar el dispositivo de destino
                                Driver = disk;

                                // Cambiar la referencia en la cola
                                olv_queueList.RefreshObjects(CopyCopyQueue);

                                SetFormText();


                                goto retrycopy;
                            }
                        }
                        else
                            // Cerrando la copia
                            return;
                    }


                    if (!isFormAboutClose)
                    {
                        // Se agrega a elementos ya analizados
                        lock (CopyQueued)
                            CopyQueued.Add(CurrentFileHandle);

                        // Actualiza el conteo de archivos
                        BeginInvoke(new Action<int, int>(UpdateTransferLabelCount), CopyQueued.Count, CopyCopyQueue.Count);

                        // Actualiza el conteo de datos transferidos
                        BeginInvoke(new Action(UpdateTransferVolumeData));
                    }
                    else
                        closeFormTask.SetResult(true);
                }
                else
                    return;

            }

            PlayCopyEndNotification();
        }
        
        


        private void faButton1_Click(object sender, EventArgs e)
        {
            if (!_queueContainerOpen)
            {
                Height = 500;
                _queueContainerOpen = true;
            }
            else
            {
                Height = 197;
                _queueContainerOpen = false;
            }
        }



        private void btn_cancelCopy_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void faButton2_Click(object sender, EventArgs e)
        {
            if (!_swStartResume)
            {
                CurrentFileHandle.PauseCopy();
                btn_startResume.Button.Image = IconChar.Play.ToBitmap(btn_startResume.IconColor, btn_startResume.IconSize);
                _swStartResume = true;
            }
            else
            {
                CurrentFileHandle.ResumeCopy();
                btn_startResume.Button.Image = IconChar.Pause.ToBitmap(btn_startResume.IconColor, btn_startResume.IconSize);
                _swStartResume = false;
            }
        }


        public void UpdateCopyList(List<string> fse, string dest, DiskInfo drive)
        {
            Action newdelegate = () =>
            {
                // Guarda un respaldo de la lista de copia
                SaveCopyList(fse, dest, drive.VolumeSerial);

                // Pausa la copia para realizar la consulta de espacio
                CurrentFileHandle.PauseCopy();

                // Contendra la nueva lista de archivos que seran agregadas a la cola actual
                List<CopyFileHandler> newCopyList = new List<CopyFileHandler>();

                // Creando la nueva lista de copias en base al tipo CopyFileHandler
                BuildFilesHandlerForCopy(fse, null, dest, drive, ref newCopyList);
                
                // Calcular que haya espacio suficiente.
                // Si es insuficiente se notifica al usuario
                // Si se ignora la notificacion el sistema intentara copiar
                // hasta que no quede espacio
                // El usuario puede cancelar la copia o reintentar la copia
                // ya que cabe la posibilidad de que el usuario libere espacio en el 
                // dispositivo de almacenamiento
                SpaceReq:
                DialogResult spaceReqResult = SpaceRequirements(newCopyList);

                switch (spaceReqResult)
                {
                    case DialogResult.Abort:
                        CloseCopyInterface(true);
                        return;

                    case DialogResult.Retry:
                        goto SpaceReq;
                }

                // Agregando la nueva lista a la cola actual
                lock (CopyCopyQueue)
                    CopyCopyQueue.AddRange(newCopyList);

                // Agregando al control de colas
                olv_queueList.AddObjects(newCopyList);

                // Actualiza en la UI la etiquera de elementos transferido y los que faltan
                UpdateTransferLabelCount(CopyQueued.Count, CopyCopyQueue.Count);

                // Actualiza en la UI el volumen de datos transferidos y por transferir
                UpdateTransferVolumeData();

                // Reanudar la copia
                CurrentFileHandle.ResumeCopy();
            };


            EndInvoke(BeginInvoke(newdelegate));
        }
        


        void BuildFilesHandlerForCopy(List<string> fse, string rootdir, string destfolder, DiskInfo destdr, ref List<CopyFileHandler> copylist)
        {
            foreach (var entry in fse)
            {
                if (File.Exists(entry))
                {
                    copylist.Add(new CopyFileHandler(_callbackstatus, _copystatecallback)
                    {
                        DestDrive = destdr,
                        SourceFile = entry,
                        DestPath = destfolder,
                        DestDir = rootdir?.Split('\\').LastOrDefault()
                    });
                }
                else if (Directory.Exists(entry))
                {
                    List<string> fse2 = new List<string>(Directory.GetFileSystemEntries(entry));
                    BuildFilesHandlerForCopy(fse2, entry, destfolder + "\\" + entry.Split('\\').LastOrDefault(), destdr, ref copylist);
                }
            }
        }



        private async void CopyCopyMF_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (forceclose)
                e.Cancel = false;
            else
            {
                CurrentFileHandle?.PauseCopy();
                Activate();
                DialogResult res = MessageBox.Show(this, "¿Esta seguro de que desea cancelar la copia?", "Copy-Copy", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    await CurrentFileHandle?.CancelCopy();
                    isFormAboutClose = true;
                    await closeFormTask.Task;
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    CurrentFileHandle?.ResumeCopy();
                }
            }
        }


        /* Icon Update
        void UpdateNotifyIcon(int current, int maxvalue)
        {
            Action delegated = () =>
            {
                if (current == 0)
                {
                    int cval = 100;
                    int _iconH = 100;
                    int _iconW = 100;
                    int mval = _iconH;

                    Bitmap bmpt = new Bitmap(_iconW, mval);
                    Graphics g = Graphics.FromImage(bmpt);

                    Point baseRectPoint = new Point(0, 0);
                    Size baseSize = new Size(_iconW, mval);
                    Rectangle baseRect = new Rectangle(baseRectPoint, baseSize);

                    g.DrawRectangle(new Pen(Brushes.Red), baseRect);
                    g.FillRectangle(Brushes.Blue, new Rectangle(0, mval - cval, _iconW, cval));
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    g.DrawString("0", new Font("Arial", 50, FontStyle.Regular), Brushes.White, baseRect, sf);

                    IntPtr bmph = bmpt.GetHicon();
                    
                    copicon = noticon.Icon = System.Drawing.Icon.FromHandle(bmph);

                    bmpt.Dispose();
                    g.Dispose();

                    DestroyIcon(bmph);
                }
                else
                {

                    int cval = current * 100 / maxvalue;
                    int _iconH = 100;
                    int _iconW = 100;
                    int mval = _iconH;

                    Bitmap bmpt = new Bitmap(_iconW, mval);
                    Graphics g = Graphics.FromImage(bmpt);

                    Point baseRectPoint = new Point(0, 0);
                    Size baseSize = new Size(_iconW, mval);
                    Rectangle baseRect = new Rectangle(baseRectPoint, baseSize);

                    g.DrawRectangle(new Pen(Brushes.Red), baseRect);
                    g.FillRectangle(Brushes.Red, new Rectangle(0, mval - cval, _iconW, cval));
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    g.DrawString(cval.ToString(), new Font("Arial", 50, FontStyle.Regular), Brushes.White, baseRect, sf);

                    IntPtr bmph = bmpt.GetHicon();
                    System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(bmph);
                    bmpt.Dispose();
                    g.Dispose();

                    copicon = noticon.Icon = icon;

                    DestroyIcon(bmph);
                }
            };
            
            BeginInvoke(delegated);
        }
        */

       
        /// <summary>
        /// Cierra el stream de la copia en curso, y borra el archivo de destino.
        /// El proposito de esto es que el copiador omita el archivo de la copia actual
        /// y comienze con el siguiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stepFoward_Click(object sender, EventArgs e)
        {
            CurrentFileHandle.StepFoward();
        }
        


        void PlayCopyEndNotification()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer("Sounds\\0001.wav");
            sp.Play();
        }



        private void noticon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            Activate();
            BringFront();
            WindowState = FormWindowState.Normal;
            noticon.Visible = false;
        }


        private ulong CalculateSpaceOnDestDrive(DiskInfo destdrive, List<CopyFileHandler> nelements)
        {
            IEnumerable<CopyFileHandler> fileh = olv_queueList.Objects.Cast<CopyFileHandler>();
            ulong totalBytes = (ulong)fileh.Sum(s => s.FileInfo.Length);
            if(nelements != null)
                totalBytes += (ulong)nelements.Sum(e => e.FileInfo.Length);
            if (destdrive.FreeSpace < totalBytes)
                return totalBytes - destdrive.FreeSpace;
            return 0;
        }


        private ulong CalculateSpaceOnDestDrive(DiskInfo destdrive, List<CopyFileHandler> newfiles, List<CopyFileHandler> queueFiles, ref ulong asignatedSpaceInQueue)
        {
            ulong totalBytes = 0;
            ulong queueAsignation = 0;
            newfiles.ForEach((CopyFileHandler fse) => { totalBytes += (ulong)fse.FileInfo.Length; });
            queueFiles?.ForEach((CopyFileHandler fse) => { queueAsignation += (ulong)fse.FileInfo.Length; });
            asignatedSpaceInQueue = queueAsignation;
            totalBytes += queueAsignation;
            if (destdrive.FreeSpace < totalBytes)
                return totalBytes - destdrive.FreeSpace;
            return 0;
        }


        private double GetDataStr(ulong size, out string kbmborgb)
        {
            ulong okb = 1024;
            ulong omb = okb * 1024;
            ulong ogb = omb * 1024;

            if (size > okb && size < omb)
            {
                kbmborgb = "KB";
                return Math.Round((double)size / (double)okb, 2);
            }
            else if (size > omb && size < ogb)
            {
                kbmborgb = "MB";
                return Math.Round((double)size / (double)omb, 2);
            }
            else if (size > ogb)
            {
                kbmborgb = "GB";
                return Math.Round((double)size / (double)ogb, 2);
            }
            else
            {
                kbmborgb = "Bytes";
                return size;
            }
        }


        private void BringFront()
        {
            TopMost = true;
            TopMost = false;
        }




        double totalTransferedBytes = 0;
        public void CopyProgressCallback(int percent, int rate, ulong bytesreads)
        {
            BeginInvoke(new Action<int>(UpdateCurrentProgressBar), percent);
            BeginInvoke(new Action<int>(UpdateTransferRate), rate);
            totalTransferedBytes += bytesreads;
            double bytesTransferedPercent = (double)(totalTransferedBytes * 100 / QueueTotalSize);
            BeginInvoke(new Action<int>(UpdateGlobalProgressBar), (int)bytesTransferedPercent);
            BeginInvoke(new Action<int>(SetFormText), (int)bytesTransferedPercent);
        }
        

        private void UpdateTransferVolumeData()
        {
            ulong queueSize = 0;
            ulong queuedSize = 0;
            CopyQueued.ForEach((CopyFileHandler copyfile) =>
            {
                queuedSize += (ulong)copyfile.FileInfo.Length;
            });

            CopyCopyQueue.ForEach((CopyFileHandler fse) =>
            {
                queueSize += (ulong)fse.FileInfo.Length;
            });

            double currentQueued = GetDataStr(queuedSize, out string queuedUnit);
            double totalSize = GetDataStr(queueSize, out string totalUnit);

            lbl_volumeData.Text = $"Volumen de datos: {currentQueued} {queuedUnit} / {totalSize} {totalUnit}";
        }



        private void UpdateTransferLabelCount(int current, int total)
        {
            lbl_totalFilesCount.Text = $"Archivos copiados: {current}/{total}";
        }


        private void UpdateCurrentProgressBar(int percent)
        {
            progressBar_currentState.Value = percent;
            Value = percent;
        }


        private void UpdateTransferRate(int transferrate)
        {
            string txt = "";
            if (transferrate > 1048576)
                // Mas de 1mb/s
                txt = $"{transferrate / (1024 * 1024)} mb/s";
            else
                // Kb / s
                txt = $"{transferrate} kb/s";

            // Muestra la velocidad de la copia
            lbl_bps.Text = txt;
        }


        private void UpdateGlobalProgressBar(int currentvalue)
        {
            progressBar_globalState.Value = currentvalue;
        }

        


        private void SetCurrentCopyFilesInLabels(string from, string to)
        {
            lbl_copyingFrom.Text = from;
            lbl_copyingTo.Text = to;
        }


        private void UpdateFormState(int percent)
        {
            //Value = percent;
        }
        

        /// <summary>
        /// Dada la propiedad <see cref="Driver"/> este metdo determina si el espacio 
        /// disponible en el disco es suficiente para comenzar la copia
        /// </summary>
        /// <param name="spaceneeded"></param>
        /// <returns></returns>
        private DialogResult SpaceRequirements(List<CopyFileHandler> newElements = null)
        {
            ulong spaceneeded = CalculateSpaceOnDestDrive(Driver, newElements);

            if (spaceneeded != 0)
            {
                double spaceRequired = GetDataStr(spaceneeded, out string unitReq);
                double freeSpace = GetDataStr(Driver.FreeSpace, out string unitFree);
                double capacity = GetDataStr(Driver.Capacity, out string unitCap);

                string text = $"Espacio requerido:\t{spaceRequired} {unitReq}\r\n" +
                    $"Espacio libre:\t\t{freeSpace} {unitFree}\r\n" +
                    $"Capacidad:\t\t{capacity} {unitCap}\r\n" +
                    "\r\nOpciones:\r\n" +
                    $"\"Abortar\": Cancela toda la copia.\r\n" +
                    $"\"Reintentar\": Se volvera a calcular el espacio disponible.\r\n" +
                    $"\"Ignorar/Omitir\": La copia continuara hasta que no haya espacio disponible en la unidad\r\n";

                Activate();

                return MessageBox.Show(this, text, $"Espacio insuficiente en {Driver.Letter}({Driver.Name})", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            }
            return DialogResult.OK;
        }



        private CopyOptionsForm.ActionResult DoActionFileExist(string destfile, FileInfo sourceFileInfo)
        {
            string text = $"El archivo {destfile} ya existe en la ruta dada. Elija una de las opciones del cuadro de control para poder continuar. \r\n\r\n" +
                $"Opciones: \r\n" +
                "\"Cerrar la ventana\": Cancela esta copia completa\r\n" +
                "\"Sobreescribir\": Sobreescribe el archivo de destino\r\n" +
                "\"Renombrar\": Agrega un texto adicional al nombre del archivo\r\n" +
                "\"Saltar\": Omite el archivo de copia actual";
            
            if (!sameActionForAllError)
                actionRepeat = CopyOptionsForm.Show(MessageBoxIcon.Exclamation, text, CopyOptionsForm.CopyListControls.ShowRenameSkipOverwrite, out sameActionForAllError, this);
            
            return actionRepeat;
        }


        public void CloseCopyInterface(bool successclose)
        {
            forceclose = successclose;
            Close();
        }


        private void olv_queueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEnumerable<CopyFileHandler> selection = olv_queueList.SelectedObjects.Cast<CopyFileHandler>();
            Action updateselectionInfo = () => 
            {
                ulong sizeofselection = 0;
                foreach (var s in selection)
                    sizeofselection += (ulong)s.FileInfo.Length;
                double selectionSize = GetDataStr(sizeofselection, out string unitReq);
                lbl_selection.Text = $"Seleccion: {selection.Count()} / {selectionSize} {unitReq}";
            };
            BeginInvoke(updateselectionInfo);
        }


        private void SetFormText(int current = 0)
        {
            if (current == 0)
            {
                string volname = string.IsNullOrEmpty(Driver?.Name) ? "" : $"({Driver?.Name})";
                Text = $"Copia hacia {Driver?.Letter} {volname}";
            }
            else
            {
                string volname = string.IsNullOrEmpty(Driver?.Name) ? "" : $"({Driver?.Name})";
                Text = $"{current}% {Driver?.Letter} {volname}";
            }
        }


        private void olv_queueList_ModelCanDrop(object sender, BrightIdeasSoftware.ModelDropEventArgs e)
        {
            olv_queueList.MoveObjects(e.DropTargetIndex+1, e.SourceModels);
            e.Effect = DragDropEffects.Move;
        }


        private DialogResult IOException()
        {
            string text = "Hubo un error en la copia. Reinserte el dispositivo e intentelo nuevamente.\r\n" +
                $"Dispositivo: {Driver.Letter} - {Driver.Name}";

            Activate();
            return MessageBox.Show(null, text, $"{Driver.Letter} - IO Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }

        private void olv_queueList_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyData == Keys.Delete)
            {
                IEnumerable<CopyFileHandler> files = olv_queueList.SelectedObjects.Cast<CopyFileHandler>();
                olv_queueList.RemoveObjects(olv_queueList.SelectedObjects);

                lock (CopyCopyQueue)
                    foreach (var file in files)
                        CopyCopyQueue.Remove(file);
                

                UpdateTransferLabelCount(CopyQueued.Count, CopyCopyQueue.Count);

                UpdateTransferVolumeData();
            }
        }

        private void CopyCopyMF_SizeChanged(object sender, EventArgs e)
        {
            if (Height > 198 || WindowState == FormWindowState.Maximized)
                _queueContainerOpen = true;
            else
                _queueContainerOpen = false;
        }


        private void SaveCopyList(List<string> list, string destpath, string volumeserial)
        {
            KeyValuePair<string, List<string>> copylist = new KeyValuePair<string, List<string>>(destpath, list);
           
            string now = DateTime.Now.ToString("dd_MM_yyyy HH_mm_ss");
            if (BackUpFile == null)
            {
                string cccfile = $"ccc_{now}.ccb";
                BackUpFile = $"{AppSettings.CC_BackUp}\\{cccfile}";

                CCBackUpList bc = new CCBackUpList
                {
                    VolumeSerial = volumeserial,
                    FilesAndDestFolder = new List<KeyValuePair<string, List<string>>>() { copylist }
                };
                
                string serializedList = JsonConvert.SerializeObject(bc, Formatting.Indented);
                File.WriteAllText(BackUpFile, serializedList);
            }
            else
            {
                CCBackUpList currentbackup = LoadCopyList(BackUpFile);
                currentbackup.FilesAndDestFolder.Add(copylist);
                string serializedList = JsonConvert.SerializeObject(currentbackup, Formatting.Indented);
                File.WriteAllText(BackUpFile, serializedList);
            }
        }


        private CCBackUpList LoadCopyList(string file)
        {
            if(file == null) throw new ArgumentNullException();

            if (File.Exists(file))
                return (CCBackUpList)JsonConvert.DeserializeObject(File.ReadAllText(file), typeof(CCBackUpList));
            else
            {
                File.WriteAllText(file, "");
                return new CCBackUpList();
            }
        }
        

        new public void Activate()
        {
            Action deleg = () =>
            {
                if (WindowState != FormWindowState.Normal)
                    WindowState = FormWindowState.Normal;
                else
                    SetForegroundWindow(Handle);

                BringFront();
            };

            BeginInvoke(deleg);
        }


        [DllImport("User32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }
}