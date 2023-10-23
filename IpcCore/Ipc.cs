using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Threading;
using static IpcCore.IpcSerializer.Serializer;

namespace IpcCore
{
    public class Ipc
    {
        /// <summary>
        /// Cliente Pipe para el IPC
        /// A traves de este objeto se haran las transferencias
        /// de mensajes entre los procesos
        /// </summary>
        protected PipeStream _ipc { get; set; }


        /// <summary>
        /// Lista de paquetes en cola
        /// </summary>
        private ThreadSafeList<IpcPacketWrapper> PacketList;


        public delegate void MessageArriveHandle(IpcPacketWrapper packet, Ipc stream);
        public event MessageArriveHandle OnMessageArrive;
        public event EventHandler ChannelDisconnected;
        private CancellationTokenSource cts;
        private CancellationToken ctsToken;


        /// <summary>
        /// Id actual de la cola de paquetes
        /// </summary>
        private long currentpacketId = 0;


        /// <summary>
        /// Indica que el objeto ha sido desechado
        /// </summary>
        private bool _disposed;


        public Ipc(PipeStream ipc) => _ipc = ipc;

        public Ipc() { }

        /// <summary>
        /// Inicia la escucha de la canalizacion
        /// </summary>
        public void StartListen()
        {
            cts = new CancellationTokenSource();
            ctsToken = cts.Token;
            PacketList = new ThreadSafeList<IpcPacketWrapper>();
            ReceiverLoop();
        }


        /// <summary>
        /// Detiene la escucha de la canalizacion
        /// </summary>
        public void StopListen()
        {
            cts.Cancel();
        }



        /// <summary>
        /// Envia un objeto al extremo remoto de la canalizacion
        /// Si <see cref="PacketConfiguration.WaitResponse"/> es false el metodo devolvera null
        /// </summary>
        /// <typeparam name="T">Tipo de objeto esperado como respuesta</typeparam>
        /// <param name="obj">Objeto serializable que se va a enviar</param>
        /// <param name="pconf">Configuracion del paquete</param>
        /// <exception cref="EmptyPacketException"></exception>
        /// <exception cref="TypeDifferException"></exception>
        /// <returns></returns>
        public async Task<T> Send<T>(object obj, PacketConfiguration pconf)
        {
            long cpack = ++currentpacketId;
            IpcPacketWrapper ipcPacket = new IpcPacketWrapper
            {
                PacketId = cpack,
                Packet = obj,
                PacketQueue = pconf.EndPointQueue
            };

            // Cuerpo del mensaje
            var serializedObj = Serialize(ipcPacket);

            // Fijar el tamano de la cabecera a 4 bytes
            // La cabecera indica el tamano del mensaje
            byte[] header = BitConverter.GetBytes(serializedObj.Length);
            
            // Mensaje completo
            var messeage = header.Concat(serializedObj).ToArray();
            
            // Enviando el mensaje
            await _ipc.WriteAsync(messeage, 0, messeage.Length);
            

            if (pconf.WaitResponse)
            {
                IpcPacketWrapper packet = await WaitPacket(++cpack);
                _= packet ?? throw new EmptyPacketException("Ipc has wait for a null packet due to a TimeOutException");
                if (typeof(T) != packet.Packet.GetType())
                    throw new TypeDifferException("Expected T type differ from type received from the remote channel");
                return (T)packet.Packet;
            }

            return default(T);
        }


        /// <summary>
        /// Envia un objeto como mensaje a traves de la canalizacion
        /// </summary>
        /// <param name="obj">Objeto serializable que se va a enviar</param>
        /// <param name="packetIdRespond">Id del paquete al que se le da respuesta</param>
        /// <returns></returns>
        public async Task Send(object obj, long packetIdRespond)
        {
            IpcPacketWrapper ipcPacketToRespond = new IpcPacketWrapper
            {
                PacketId = ++packetIdRespond,
                Packet = obj,
                PacketQueue = true
            };

            // Cuerpo del mensaje
            var serializedObj = Serialize(ipcPacketToRespond);

            // Fijar el tamano de la cabecera a 4 bytes
            // La cabecera indica el tamano del mensaje
            byte[] header = BitConverter.GetBytes(serializedObj.Length);

            // Mensaje completo
            var messeage = header.Concat(serializedObj).ToArray();

            // Enviando el mensaje
            await _ipc.WriteAsync(messeage, 0, messeage.Length);
        }


        /// <summary>
        /// Envia un objeto como mensaje a traves de la canalizacion
        /// </summary>
        /// <param name="obj">Objeto serializable que se va a enviar</param>
        /// <returns></returns>
        public async Task Send(object obj)
        {
            IpcPacketWrapper ipcPacketToRespond = new IpcPacketWrapper
            {
                PacketId = ++currentpacketId,
                Packet = obj,
                PacketQueue = false
            };

            // Cuerpo del mensaje
            var serializedObj = Serialize(ipcPacketToRespond);

            // Fijar el tamano de la cabecera a 4 bytes
            // La cabecera indica el tamano del mensaje
            byte[] header = BitConverter.GetBytes(serializedObj.Length);

            // Mensaje completo
            var messeage = header.Concat(serializedObj).ToArray();

            // Enviando el mensaje
            await _ipc.WriteAsync(messeage, 0, messeage.Length);
        }



        /// <summary>
        /// Recorrido sistemico que da el sistema para analzar el buffer de entrada
        /// </summary>
        private async void ReceiverLoop()
        {
            while (!cts.IsCancellationRequested)
            {
                // Header. Indicates the size of the buffer
                var hBuffer = new byte[4];

                try
                {
                    // Leyendo el buffer de la cabecera
                    // La cabecera indica el tamano del mensaje
                    await _ipc.ReadAsync(hBuffer, 0, hBuffer.Length);


                    // Entero que indica cuanto leer del mensaje
                    int msgLen = BitConverter.ToInt32(hBuffer, 0);

                    // Almacenar buffer para la lectura del mensaje
                    byte[] msgBuffer = new byte[msgLen];

                    // Leyendo el mensaje
                    await _ipc.ReadAsync(msgBuffer, 0, msgBuffer.Length);

                    // Deserializar el mensaje
                    try
                    {
                        // Devuelve el mensaje deserializado y convertido al 
                        // tipo especificado en <T>
                        IpcPacketWrapper ipc_packet = (IpcPacketWrapper)Deserialize(msgBuffer);
                        if (ipc_packet.PacketQueue)
                            PacketList.Add(ipc_packet);
                        else
                            OnMessageArrive?.Invoke(ipc_packet, this);

                    }
                    catch (Exception ex)
                    {
                        ChannelDisconnected?.Invoke(this, null);
                        Dispose();
                    }
                }
                catch (Exception xx)
                {
                    ChannelDisconnected?.Invoke(this, null);
                    Dispose();
                }

            }
        }



        /// <summary>
        /// Libera los recursos de <see cref="NamedPipeClientStream"/> usado
        /// para la canalizacion actual y cierra el canal 
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                cts.Cancel();
                _ipc.Flush();
                _ipc.Dispose();
                _ipc.Close();
            }
        }



        /// <summary>
        /// Si se usa, espera el id especifico en la lista de paquetes que han arrivado
        /// a la lista de paquetes en cola <see cref="PacketList"/>
        /// </summary>
        /// <param name="pakid">Id del paquete que se va a esperar su llegada</param>
        /// <param name="timeOut">Tiempo maximo de espera en segundos. Por defecto 60 segundos</param>
        /// <returns></returns>
        private async Task<IpcPacketWrapper> WaitPacket(long pakid, int timeOut = 60)
        {
            int segs = 0;
            var taskDelay = 90;
            var trys = timeOut * 10;
            while (segs < trys) 
            {
                IpcPacketWrapper packet = PacketList.FirstOrDefault(p => p.PacketId == pakid);
                if(packet != null)
                {
                    PacketList.Remove(packet);
                    return packet;
                }

                segs++;
                await Task.Delay(taskDelay);
            }

            return null;
        }
    }
}
