using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;


namespace IpcCore
{
    public class IpcClient:Ipc
    {
        /// <summary>
        /// Nombre del canal por el que se va a transmitir
        /// </summary>
        public string _chanel;
        


        /// <summary>
        /// Indica si la canalizacion esta actualmente activa
        /// </summary>
        public bool IsConected { get; set; }



        public IpcClient(string chanel)
        {
            _chanel = chanel ?? throw new ArgumentNullException();
        }


        /// <summary>
        /// Conecta con el canal especificado en el constructor
        /// </summary>
        /// <param name="error">Si se produce un error es asignado a este parametro/param>
        public void Connect(out string error)
        {
            try
            {
                error = null;
                _ipc = new NamedPipeClientStream(".", _chanel, PipeDirection.InOut, PipeOptions.Asynchronous);
                ((NamedPipeClientStream)_ipc).Connect();
                IsConected = true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
        }
    }
}