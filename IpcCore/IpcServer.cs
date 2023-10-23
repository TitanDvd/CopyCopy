using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;

namespace IpcCore
{
    /// <summary>
    /// Intercambia informacion entre procesos.
    /// Esta implementacion solo permite una conexion IPC a la vez.
    /// Mientras exista una canalizacion el servidor no aceptara otra
    /// </summary>
    public class IpcServer
    {
        private bool _waitLoop = false;
        private Action<string> _cbChannel;
        public Dictionary<string, Ipc> ConnectedChannels = new Dictionary<string, Ipc>();
        private string _channel;


        public delegate void ClientConnected(Ipc channel);
        public event ClientConnected OnClientConnected;


        public delegate void ClientMessage(Ipc channel, IpcPacketWrapper payload);
        public event ClientMessage OnClientMeesage;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="TitanServerIPC"/>
        /// </summary>
        /// <param name="chanel"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public IpcServer(out string channelName)
        {
            _channel = 
             channelName = Guid.NewGuid().ToString();
            NamedPipeServerStream _ipc = new NamedPipeServerStream(channelName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            WaitConnection(_ipc);
        }



        public IpcServer(string channelName)
        {
            _channel = channelName;
            NamedPipeServerStream _ipc = new NamedPipeServerStream(channelName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            WaitConnection(_ipc);
        }


        public IpcServer(Action<string> cbChannelListening)
        {
            _cbChannel = cbChannelListening;
            _waitLoop = true;
            WaitConnection(null);
        }


        private void WaitConnection(NamedPipeServerStream ipc)
        {
            if(ipc == null)
            {
                _channel = Guid.NewGuid().ToString();
                _cbChannel?.Invoke(_channel);
                ipc = new NamedPipeServerStream(_channel, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            }

            Ipc pIpc = new Ipc(ipc);
            Tuple<Ipc, NamedPipeServerStream> parameters = new Tuple<Ipc, NamedPipeServerStream>(pIpc, ipc);
            ipc.BeginWaitForConnection(CallBack, parameters);
        }



        void CallBack(IAsyncResult res)
        {
            Tuple<Ipc, NamedPipeServerStream> parameters = (Tuple<Ipc, NamedPipeServerStream>)res.AsyncState;
            parameters.Item2.EndWaitForConnection(res);
            parameters.Item1.StartListen();
            lock(ConnectedChannels)
                ConnectedChannels.Add(_channel, parameters.Item1);

            

            parameters.Item1.ChannelDisconnected += (o, e) =>
            {
                lock (ConnectedChannels)
                {
                    var channelParams = ConnectedChannels.FirstOrDefault(x => x.Value.Equals(o));
                    if(channelParams.Key != null)
                        ConnectedChannels.Remove(channelParams.Key);
                }
            };


            parameters.Item1.OnMessageArrive += (p, ipc) =>
            {
                OnClientMeesage?.Invoke(ipc, p);
            };


            OnClientConnected?.Invoke(parameters.Item1);

            if (_waitLoop)
                WaitConnection(null);
        }



        public async void Send(string channel, object payload)
        {
            var ipcStreams = ConnectedChannels.FirstOrDefault();
            if (ipcStreams.Key == channel)
                await ipcStreams.Value.Send(payload);
        }
    }
}
