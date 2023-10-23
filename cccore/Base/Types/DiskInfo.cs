using System;
using System.Runtime.InteropServices;

namespace CcCore.Base
{
    [Serializable]
    public class DiskInfo
    {
        public string Letter { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string VolumeSerial { get; set; }
        public string FileSystem { get; set; }
        public ulong FreeSpace
        {
            get
            {
                long totalFreebytes = 0;
                long _ = 0;
                long __ = 0;
                GetDiskFreeSpaceEx(Letter, ref _, ref __, ref totalFreebytes);
                return (ulong)totalFreebytes;
            }
        }
        public ulong Capacity
        {
            get
            {
                long capacity = 0;
                long _ = 0;
                GetDiskFreeSpaceEx(Letter, ref _, ref capacity, ref _);
                return (ulong)capacity;
            }
        }
        public bool IsLocalDrive
        {
            get
            {
                return !Letter.StartsWith("\\");
            }
        }


        public DiskInfo(string letter)
        {
            Letter = letter;
            if (letter.StartsWith("\\"))
                VolumeSerial = letter;
        }


        [DllImport("kernel32")]
        private static extern int GetDiskFreeSpaceEx(string lpDirectoryName, ref long lpFreeBytesAvailable, ref long lpTotalNumberOfBytes, ref long lpTotalNumberOfFreeBytes);
    }
}
