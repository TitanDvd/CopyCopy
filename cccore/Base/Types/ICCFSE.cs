using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcCore.Base.Types
{
    /// <summary>
    /// Copy copy  file system entry
    /// </summary>
    public interface ICCFSE
    {
        /// <summary>
        /// Tamano del elemento en bytes
        /// </summary>
        ulong Size { get; set; }


        /// <summary>
        /// Ruta de fuente del elemento
        /// </summary>
        string SourcePath { get; set; }


        /// <summary>
        /// Driver de destino
        /// </summary>
        ICCD DestDriver { get; set; }


        /// <summary>
        /// Ruta de destino
        /// </summary>
        string DestPath { get; set; }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Directorio de destino
        /// </summary>
        string DestDir { get; set; }
    }

}
