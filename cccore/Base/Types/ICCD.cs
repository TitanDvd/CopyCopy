using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcCore.Base.Types
{
    /// <summary>
    /// Copy copy drive interface
    /// </summary>
    public interface ICCD
    {
        /// <summary>
        /// Letra de la unidad
        /// </summary>
        string Letter { get; set; }

        /// <summary>
        /// Nombre de la unidad (Ej: Maria,CASA, MiMemo)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Etiqueta extendida personalizada de la unidad
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// Numero serial "inmutable" del dispositivo
        /// </summary>
        string VolumeSerial { get; set; }

        /// <summary>
        /// Espacio disponible en el dispositivo
        /// </summary>
        ulong FreeSpace { get; set; }

        /// <summary>
        /// Capacidad total del dispositivo
        /// </summary>
        ulong Capacity { get; set; }
    }
}
