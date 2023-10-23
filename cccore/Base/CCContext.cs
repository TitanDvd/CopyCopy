using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CcCore.Base;
using System.Management;
using System.Drawing;
using EasyHook;
using CcLib;
using IpcCore;
using CopyCopyIpcPackets;

namespace CcCore
{
    public class CCContext : ApplicationContext
    {
        private NotifyIcon                   _trayIcon;
        private List<Tuple<Ipc, string>>     _ipcDestinations;
        private List<FileOperationIpcPacket> _destSourceFileTemp;
        private Stopwatch                    _stopWatch;
        private string                       _listeningChannel = "";
        private TaskCompletionSource<Ipc>    _allowTransfer;
        private List<int>                    _hookedProces;
        private IpcServer                    _server;


        private delegate void FileCopyEventHandler(string destination, List<FileOperationIpcPacket> fop);
        private event FileCopyEventHandler OnReadyToOperate;


        public static Settings Settings;


        public CCContext()
        {
            try
            {
                _hookedProces = new List<int>();
                Settings = new Settings();
                Settings.LoadSettings();

                ExplorerWatcher();

                _ipcDestinations    = new List<Tuple<Ipc, string>>();
                _destSourceFileTemp = new List<FileOperationIpcPacket>();
                _stopWatch          = new Stopwatch();


                OnReadyToOperate += CCContext_OnReadyToOperate;
                DetectItemsListReadyToWork();


                // From here with EasyHook, COM interface is hooked in EXPLORER.EXE
                HookExplorer();


                _server = new IpcServer(new Action<string>((channel) =>
                {
                    _listeningChannel = channel;
                }));


                _server.OnClientConnected += (ipc) =>
                {
                    // Client connection routine
                    ipc.ChannelDisconnected += (oIpc, e) =>
                    {
                        lock (_ipcDestinations)
                        {
                            var channelRelation = _ipcDestinations.FirstOrDefault(x => x.Item1.Equals(oIpc));
                            if (channelRelation != null)
                                _ipcDestinations.Remove(channelRelation);
                        }
                          
                        //lock (_ipcDestination)
                        //{
                        //    var channelToDelete = _ipcDestination.FirstOrDefault(c => c.Item1.Equals((Ipc)oIpc));
                        //    if (channelToDelete != null)
                        //        _ipcDestination.Remove(channelToDelete);
                        //}
                    };


                    if (_allowTransfer != null)
                        _allowTransfer.SetResult(ipc);
                };


                _server.OnClientMeesage += Server_OnMessageArrive;


                _trayIcon = new NotifyIcon
                {
                    Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                    ContextMenu = new ContextMenu(new MenuItem[] {
                        new MenuItem("CCCore Settings", (s, e) => {new Views.Configurations().Show();}),
                        new MenuItem("Delegate Settings", (s, e) => 
                        {
                            Process.Start(Settings.DelegatedExe, "options");
                        }),

                        new MenuItem("Force copy", (s, e)=>
                        {
                             if (!string.IsNullOrEmpty(Settings.DelegatedExe) && File.Exists(Settings.DelegatedExe))
                                Process.Start(Settings.DelegatedExe, $"ipc {_listeningChannel} forceCopy");
                        }),

                        new MenuItem("About CCC", (s, e) => { new Views.AboutCcc().Show(); }),
                        new MenuItem("Exit", (s, e) => { _trayIcon.Visible = false; Application.Exit(); })
                        }),

                    Visible = true
                };
            }

            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ExplorerWatcher()
        {


            ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startWatch.EventArrived += new EventArrivedEventHandler(ProcCreationEvent);
            startWatch.Start();
        }



        private void ProcCreationEvent(object sender, EventArrivedEventArgs e)
        {
            try
            {
                int pid = Convert.ToInt32(e.NewEvent.Properties["ProcessId"].Value);
                var proc = Process.GetProcessById(pid);
                if (proc.ProcessName.ToLower().Equals("explorer"))
                    HookExplorer(proc);
            }
            catch
            {

            }
        }




        private void HookExplorer()
        {
            var explorers = Process.GetProcessesByName("explorer");
            foreach (var exp in explorers)
            {
                if(!_hookedProces.Contains(exp.Id))
                {
                    _hookedProces.Add(exp.Id);

                    string channelName = null;
                    IPCServer.OnFileHooked += IPCServer_OnFileHooked1;

                    exp.EnableRaisingEvents = true;
                    exp.Exited += (o, e) =>
                    {
                        _hookedProces.Remove(exp.Id);
                        IPCServer.OnFileHooked -= IPCServer_OnFileHooked1;
                    };

                    RemoteHooking.IpcCreateServer<IPCServer>(ref channelName, System.Runtime.Remoting.WellKnownObjectMode.Singleton);

                    try
                    {
                        RemoteHooking.Inject(exp.Id, "cclib.dll", "cclib.dll", channelName);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Initializing error. " + ex.Message);
                        Environment.Exit(0);
                    }
                }
               
            }
        }



        private void HookExplorer(Process explorer)
        {
            if (!_hookedProces.Contains(explorer.Id))
            {
                explorer.EnableRaisingEvents = true;
                explorer.Exited += (o, e) =>
                {
                    _hookedProces.Remove(explorer.Id);
                    IPCServer.OnFileHooked -= IPCServer_OnFileHooked1;
                };
                string channelName = null;
                //IPCServer.OnFileHooked += IPCServer_OnFileHooked1;
                RemoteHooking.IpcCreateServer<IPCServer>(ref channelName, System.Runtime.Remoting.WellKnownObjectMode.Singleton);
                RemoteHooking.Inject(explorer.Id, "cclib.dll", "cclib.dll", channelName);
            }
        }



        private void Server_OnMessageArrive(Ipc stream, IpcPacketWrapper packet)
        {
            switch (packet.Packet)
            {
                case 200:
                    // Delegated client is conected and ready to receive the first file
                    // await stream.Send(fileOps);
                    //_allowTransfer.SetResult(new Tuple<IpcPacketWrapper, Ipc>(packet, stream));
                    break;


                case string destination:
                    lock (_ipcDestinations)
                        _ipcDestinations.Add(new Tuple<Ipc, string>(stream, destination));
                    break;
            }
        }




        private async void CCContext_OnReadyToOperate(string destination, List<FileOperationIpcPacket> fileOps)
        {
            fileOps.Sort(new FileNameComparer());
            FileInfo _dest = new FileInfo(destination);
            string __dest = _dest.Directory == null ? _dest.FullName : _dest.Directory.Root.Name;
            Tuple<Ipc,string> channel = null;
            lock (_ipcDestinations)
                channel = _ipcDestinations.FirstOrDefault(t => t.Item2 == __dest);

            if (channel == null)
            {
                if (!string.IsNullOrEmpty(Settings.DelegatedExe) && File.Exists(Settings.DelegatedExe))
                    Process.Start(Settings.DelegatedExe, $"ipc {_listeningChannel}");

                _allowTransfer = new TaskCompletionSource<Ipc>();
                var procResponse = await _allowTransfer.Task;
                lock(_ipcDestinations)
                    _ipcDestinations.Add(new Tuple<Ipc, string>(procResponse, __dest));
                try
                {
                    await procResponse.Send(fileOps);
                }
                catch { }

            }
            else
                await channel.Item1.Send(fileOps);
        }




        /// <summary>
        /// Trys to detect when the file operation list (<see cref="_destSourceFileTemp"/>) is ready to operate
        /// </summary>
        private void DetectItemsListReadyToWork()
        {
            Timer _t1 = new Timer
            {
                Interval = 10
            };
            _t1.Tick += (e, s) =>
            {
                if (_stopWatch.Elapsed.Seconds >= 1)
                {
                    var groupd = _destSourceFileTemp.GroupBy(x => x.Dest);
                    foreach (var group in groupd)
                        OnReadyToOperate?.Invoke(group.Key, group.ToList());

                    _destSourceFileTemp.Clear();
                    _stopWatch.Reset();
                }
            };
            _t1.Start();
        }




        /// <summary>
        /// This event is trigred when target process (EXPLORER.EXE) start a file copy operation
        /// </summary>
        /// <param name="sources">File sources</param>
        /// <param name="to">Destination path</param>
        /// <param name="eventType">Event Type: Copy (0) or Move (1)</param>
        private void IPCServer_OnFileHooked1(string source, string to, byte eventType)
        {
            _destSourceFileTemp.Add(new FileOperationIpcPacket
            {
                Dest = to,
                Operation = eventType,
                Source = source
            });
            _stopWatch.Restart();
        }
    }
}
