using CopyCopyIpcPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CcCore.Base
{
    public class FileNameComparer : IComparer<FileOperationIpcPacket>
    {
        public int Compare(FileOperationIpcPacket x, FileOperationIpcPacket y)
        {
            String resultX = Regex.Match(x.Source.Split('\\').Last(), @"\d+").ToString();
            String resultY = Regex.Match(y.Source.Split('\\').Last(), @"\d+").ToString();

            if (resultX == "" || resultY == "")
                return x.Source.CompareTo(y.Source);

            return resultX.CompareTo(resultY);
        }
        
    }
}
