using System;
using System.Collections.Generic;

namespace CcCore.Base.Types
{
    [Serializable]
    public class CCFile : ICCFSE
    {
        public ulong Size { get; set; }
        public string SourcePath { get; set; }
        public ICCD DestDriver { get; set; }
        public string Name { get; set; }
        public string DestDir { get; set; }
        public string DestPath { get; set; }
    }
}
