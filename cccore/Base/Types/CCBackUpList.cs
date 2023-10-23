using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcCore.Base.Types
{
    public class CCBackUpList
    {
        public string VolumeSerial;
        public List<KeyValuePair<string,List<string>>> FilesAndDestFolder;
        public CCBackUpList()
        {
            FilesAndDestFolder = new List<KeyValuePair<string, List<string>>>();
        }
    }
}
