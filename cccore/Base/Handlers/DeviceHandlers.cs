using CcCore.Base.Types;
using CcCore.Usb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CcCore.Base
{
    public class DeviceHandlers
    {
        public static DiskInfoCollection ActiveDisks { get; set; }

        private List<Views.CopyCopyMF> CopyHandlers { get; set; }

        private Form _handle;
        

        public DeviceHandlers(ShadowForm handle)
        {
            _handle = handle;
            CopyHandlers = new List<Views.CopyCopyMF>();
            ActiveDisks = new DiskInfoCollection();

            // Cargar las memorias USB actualmente conectados
            ActiveDisks.AddRange(handle.GetAvailableDisks2());

            // Cargar los discos rigidos actualmente conectados
            // [NOTA]: Este metodo no carga los discos Nvme M2 o UM2
            //IEnumerable<System.IO.DriveInfo> dvsinfo = System.IO.DriveInfo.GetDrives().Where(d => d.DriveType != DriveType.Removable);
            //foreach (var dvinfo in dvsinfo)
            //{
            //    // Extrae el numeroSerial inmutable de los discos rigidos
            //    DiskInfo dvi = Win32_HddUtility.GetInfo(dvinfo.Name.Replace("\\", ""));

            //    // Se agrega la demas informacion disponible en el objeto System.IO.DriveInfo
            //    dvi.Capacity = (ulong)dvinfo.TotalSize;
            //    //dvi.FreeSpace = (ulong)dvinfo.AvailableFreeSpace;
            //    dvi.Name = dvinfo.VolumeLabel;
            //    dvi.Letter = dvinfo.RootDirectory.Name;
            //    ActiveDisks.Add(dvi);
            //}

            // Inicia la escucha de eventos USB
            handle.OnDeviceStateChange += Usbman_OnDeviceStateChange;
        }

        private void Usbman_OnDeviceStateChange(Usb.UsbStateChange devicechange, DiskInfo disk)
        {
            switch (devicechange)
            {
                case UsbStateChange.Added:
                    // Agrega un disco conectado a la pc a la lista de discos activos
                    lock (ActiveDisks)
                        ActiveDisks.Add(disk);
                    break;

                case UsbStateChange.Removed:
                    // ELimina de la lista de discos activos el USB removido
                    lock (ActiveDisks)
                        ActiveDisks.Remove(disk);
                    break;
            }

        }
        



        public void PerformAsyncCopyAction(List<string> sourceEntrys, string destpath)
        {
            Thread thread = new Thread(() =>
            {
                // 1. Buscar en la lista de copias abiertas un identificador que su destino
                //    coincida con el que se recibe en este metodo
                // 2. Si el identificador existe se entrega la nueva lista de elementos a copiar
                //    y su respectivo destino

                Views.CopyCopyMF activeCopyHandler = null;
                DirectoryInfo dirinfo = new DirectoryInfo(destpath);
                DiskInfo drive;
                if (destpath.StartsWith("\\"))
                    drive = ActiveDisks.FirstOrDefault(d => d.Letter == dirinfo.Root.FullName);
                else
                    drive = ActiveDisks.FirstOrDefault(d => d.Letter == dirinfo.Root.Name);

                // Si mas de una instancia del explorer es injectada, la lista de archivos se repetira * la cantidad de instancias
                // Si hay 3 instancias del explorer y se copian 5 archivos, copy copy intentara copiar 3*5 archivos
                // IEnumerable.Distinct parece resolver el problema
                List<string> dis = sourceEntrys.Distinct().ToList();

                lock (CopyHandlers)
                    activeCopyHandler = CopyHandlers.FirstOrDefault(c => c.Driver.Equals(drive));

                // Si activeCopyHandler es !null implica que la copia esta activa actualmente
                if (activeCopyHandler != null)
                {
                    activeCopyHandler.CurrentFileHandle?.PauseCopy();
                    string text = $"{activeCopyHandler.Driver.Letter} ({activeCopyHandler.Driver.Name}) Advierte:\r\n" +
                    "Deseas agregar los nuevos elementos a una lista ya existente o desea comenzar una nueva lista de copia?\r\n" +
                    "Si elige cerrar la ventana, la lista de nuevos elementos sera descartada.";
                    Views.CopyOptionsForm.ActionResult result = Views.CopyOptionsForm.Show(MessageBoxIcon.Warning, text, Views.CopyOptionsForm.CopyListControls.ShowAddOrNewList, activeCopyHandler);

                    if (result == Views.CopyOptionsForm.ActionResult.AddToCurrentList)
                        activeCopyHandler.UpdateCopyList(dis, destpath, drive);
                    else if (result == Views.CopyOptionsForm.ActionResult.CreateNewCopyForm)
                        ShowCopyForm(dis, destpath, drive);

                    activeCopyHandler.CurrentFileHandle?.ResumeCopy();
                }
                else
                {
                    // UNC
                    // Si es un disco compartido en la red o una carpeta
                    // ambos se van a tratar como un DiskInfo no local
                    // Para ello se genera un GUID y se escribe en la raiz
                    // del disco o carpeta compartida
                    if (drive == null && destpath.StartsWith("\\")) 
                    {
                        drive = new DiskInfo(dirinfo.Root.FullName);
                        //{
                        //    Name = "",
                        //    Tag = "",
                        //    VolumeSerial = dirinfo.Root.FullName
                        //};
                        ActiveDisks.Add(drive);
                    }
                    ShowCopyForm(dis, destpath, drive);
                }
            });

            thread.Start();
        }



        private void ShowCopyForm(List<string> dis, string destpath, DiskInfo drive)
        {
            Action delegatedForm = () => 
            {
                Views.CopyCopyMF copy = new Views.CopyCopyMF(dis, destpath, drive);
                lock (CopyHandlers)
                    CopyHandlers.Add(copy);
                copy.FormClosed += delegate
                {
                    lock (CopyHandlers)
                        CopyHandlers.Remove(copy);
                };
                copy.Show();
            };
            _handle.BeginInvoke(delegatedForm);
        }
    }
}
