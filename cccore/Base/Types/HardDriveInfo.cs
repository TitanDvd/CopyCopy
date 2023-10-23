using System;
using System.Collections.Generic;
using System.Management;

namespace CcCore.Base.Types
{
    [Serializable]
    public class HardDriveInfo
    {
        public string DiskName;
        public string DiskModel;
        public string Interface;
        public string DriveName;
        public string FileSystem;
        public ulong FreeSpace;
        public ulong TotalSpace;
        public string VolumeName;
        public string VolumeSerial;
        public string HardDriveSerialNumber;
        public List<string> Letters;


        public static List<HardDriveInfo> GetHardDiskInfo()
        {
            List<HardDriveInfo> hddinfo = new List<HardDriveInfo>();

            var driveQuery = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            foreach (ManagementObject d in driveQuery.Get())
            {
                HardDriveInfo hd = new HardDriveInfo();
                string sn = (string)d.GetPropertyValue("SerialNumber");
                hd.HardDriveSerialNumber = (sn == null) ? "" : sn.Trim();
                hd.Letters = new List<string>();

                var deviceId = d.Properties["DeviceId"].Value;

                var partitionQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_DiskDriveToDiskPartition", d.Path.RelativePath);
                var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
                ManagementObjectCollection ddcollection = partitionQuery.Get();
                foreach (ManagementObject p in ddcollection)
                {
                    //Console.WriteLine("Partition");
                    //Console.WriteLine(p);
                    var logicalDriveQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_LogicalDiskToPartition", p.Path.RelativePath);
                    var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
                    ManagementObjectCollection logicalCollection = logicalDriveQuery.Get();
                    foreach (ManagementObject ld in logicalCollection)
                    {
                        //Console.WriteLine("Logical drive");
                        //Console.WriteLine(ld);

                        //var physicalName = Convert.ToString(d.Properties["Name"].Value); // \\.\PHYSICALDRIVE2
                        hd.DiskName = Convert.ToString(d.Properties["Caption"].Value); // WDC WD5001AALS-xxxxxx
                        hd.DiskModel = Convert.ToString(d.Properties["Model"].Value); // WDC WD5001AALS-xxxxxx
                        hd.Interface = Convert.ToString(d.Properties["InterfaceType"].Value); // IDE
                        //var capabilities = (UInt16[])d.Properties["Capabilities"].Value; // 3,4 - random access, supports writing
                        //var mediaLoaded = Convert.ToBoolean(d.Properties["MediaLoaded"].Value); // bool
                        //hd.MediaType = Convert.ToString(d.Properties["MediaType"].Value); // Fixed hard disk media
                        //var mediaSignature = Convert.ToUInt32(d.Properties["Signature"].Value); // int32
                        //var mediaStatus = Convert.ToString(d.Properties["Status"].Value); // OK

                        hd.DriveName = Convert.ToString(ld.Properties["Name"].Value); // C:
                        hd.Letters.Add(Convert.ToString(ld.Properties["DeviceId"].Value)); // C:
                        //var driveCompressed = Convert.ToBoolean(ld.Properties["Compressed"].Value);
                        //hd.dType = Convert.ToUInt32(ld.Properties["DriveType"].Value); // C: - 3
                        hd.FileSystem = Convert.ToString(ld.Properties["FileSystem"].Value); // NTFS
                        hd.FreeSpace = Convert.ToUInt64(ld.Properties["FreeSpace"].Value); // in bytes
                        hd.TotalSpace = Convert.ToUInt64(ld.Properties["Size"].Value); // in bytes
                        //var driveMediaType = Convert.ToUInt32(ld.Properties["MediaType"].Value); // c: 12
                        hd.VolumeName = Convert.ToString(ld.Properties["VolumeName"].Value); // System
                        hd.VolumeSerial = Convert.ToString(ld.Properties["VolumeSerialNumber"].Value); // 12345678

                    }
                }

                hddinfo.Add(hd);
            }

            return hddinfo;
        }
    }
}