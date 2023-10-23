using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyHook;
using System.Diagnostics;
using System.Windows.Forms;
using CcLib;
using System.Drawing;
using System.Threading;

namespace CcCore
{
    public class CCCoreApplication
    {
        /// <summary>
        /// Icono de la bandeja de windows
        /// </summary>
        private NotifyIcon trayIcon;


        /// <summary>
        /// Lista de exploradores de windows actualmente activas
        /// </summary>
        private List<Process> Explorers { get; set; }


        /// <summary>
        /// Almacena todas las
        /// </summary>
        private List<string> DestRootPath;


        static Dictionary<List<string>, string> filesToCopy = new Dictionary<List<string>, string>();
        static List<KeyValuePair<string, string>> filesource = new List<KeyValuePair<string, string>>();
        static List<string> files = new List<string>();
        static bool copyhookStart = false;
        delegate void CopyStart(List<KeyValuePair<string, string>> files);
        static event CopyStart OnCopyStart;


        public CCCoreApplication()
        {
            DestRootPath = new List<string>();

            IPCServer.OnFileHooked += IPCServer_OnFileHooked;
            // Initialize Tray Icon
            trayIcon = new NotifyIcon();

            trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            trayIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                    //new MenuItem("Configuraciones", (s, e) => {new ConfigForm().Show();}),
                    new MenuItem("Copia forzada", (s, e)=>{ }),
                    new MenuItem("Salir", (s, e) => { trayIcon.Visible = false; Application.Exit(); })
                });
            trayIcon.Visible = true;

            // Iniciar la busqueda de los procesos de windows explorer
            // e inyectar cada instancia activa y las que se levanten
            Task.Factory.StartNew(() => { ExplorerSeek(); });
        }



        private void IPCServer_OnFileHooked(string sources, string to, byte eventType)
        {
            CCEvent ccevent = (CCEvent)eventType;
            switch(ccevent)
            {
                case CCEvent.Copy:

                    break;
            }
        }



        /// <summary>
        /// Este metodo se mantiene buscando las instancias de Windows Explorer
        /// Para inyectarlas cuando se inicia
        /// </summary>
        public void ExplorerSeek()
        {
            try
            {
                while (true)
                {
                    Process[] allexplorers = Process.GetProcessesByName("explorer");
                    foreach (var exp in allexplorers)
                    {
                        //if (Explorers.FirstOrDefault(e => e.Id == exp.Id) == null)
                        //{
                        //    Explorers.Add(exp);

                        //    string channelName = null;
                        //    RemoteHooking.IpcCreateServer<IPCServer>(ref channelName, System.Runtime.Remoting.WellKnownObjectMode.Singleton);
                        //    RemoteHooking.Inject(exp.Id, "cclib.dll", "cclib.dll", channelName);
                        //}
                    }

                    List<Process> closedProcs = new List<Process>();
                    foreach (var sExp in Explorers)
                        try
                        {
                            Process.GetProcessById(sExp.Id);
                        }
                        catch
                        {
                            closedProcs.Add(sExp);
                        }

                    foreach (var cExp in closedProcs)
                        Explorers.Remove(cExp);

                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(null, e.Message, "CC Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
