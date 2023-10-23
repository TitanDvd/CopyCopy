using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcCore.Base.Types
{

    /// <summary>
    /// Copy Copy Active Copy
    /// </summary>
    public interface ICCAC
    {
        /// <summary>
        /// Driver de copia de destino
        /// </summary>
        ICCD Driver { get; set; }

        /// <summary>
        /// Actualiza la lista de copias en la interfaz grafica
        /// que herede esta interfaz
        /// </summary>
        /// <param name="files"></param>
        /// <param name="dest"></param>
        void UpdateCopyList(List<string> files, string dest, ICCD drive);


        /// <summary>
        /// Inicializa la copia en una interfaz grafica
        /// </summary>
        void PerformCopy();

        /// <summary>
        /// Interrumpe la copia en una interfaz grafica
        /// </summary>
        void PauseCopy();

        /// <summary>
        /// Reanuda la copia
        /// </summary>
        void ResumeCopy();

        /// <summary>
        /// Cancela la copia
        /// </summary>
        void CancelCopy();
    }
}
