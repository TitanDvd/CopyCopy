using System;
namespace IpcCore
{
    /// <summary>
    /// Paquete para intercomunciacion de procesos
    /// </summary>
    [Serializable]
    public sealed class IpcPacketWrapper
    {
        /// <summary>
        /// Id del paquete. Este id se usa internamente para lograr asociar un paquete enviado con el paquete que se espera.
        /// Si el A envia un paquete P al extremo remoto B y A espera una respuesta, este id sirve para localizar el paquete
        /// en la lista de paquetes en cola cuando sea que B responda. Aunque hay un maximo de tiempo de espera para la respuesta
        /// de los extremos remotos de la canalizacion
        /// </summary>
        public long PacketId;


        /// <summary>
        /// Indica que el paquete que se envia debe ser puesto en cola cuando llegue a su destino
        /// </summary>
        public bool PacketQueue;


        /// <summary>
        /// Objeto serializable que se va a enviar
        /// </summary>
        public object Packet;
    }



    /// <summary>
    /// Configuracion del paquete a enviar por la canalizacion IPC
    /// </summary>
    [Serializable]
    public sealed class PacketConfiguration
    {
        /// <summary>
        /// El extremo local tiene que esperar la respuesta del extremo remoto
        /// </summary>
        public bool WaitResponse;


        /// <summary>
        /// El paquete debe ser agregado a la lista de paquetes en cola en el extremo remoto
        /// Para que esta propiedad tenga efecto <see cref="WaitResponse"/> debe ser true de lo contrario
        /// el extremo remoto puede responder a un paquete que el extremo local ya no esta esperando
        /// </summary>
        public bool EndPointQueue;



        public PacketConfiguration(bool wr, bool epq)
        {
            WaitResponse = wr;
            EndPointQueue = epq;
        }


        public PacketConfiguration()
        {
        }
    }
}
