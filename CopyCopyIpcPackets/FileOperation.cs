using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyCopyIpcPackets
{

    [Serializable]
    public sealed class FileOperationIpcPacket
    {
        /// <summary>
        /// File or folder source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Folder destination
        /// </summary>
        public string Dest { get; set; }

        /// <summary>
        /// Operation type
        /// 0- Copy
        /// 1- Move
        /// </summary>
        public byte Operation { get; set; }
    }
}
