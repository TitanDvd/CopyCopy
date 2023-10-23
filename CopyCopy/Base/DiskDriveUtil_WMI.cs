using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace CopyCopy.Base
{
    public class DiskDriveUtil
    {
        /// <summary>
        /// https://stackoverflow.com/questions/31129989/obtaining-hdd-serial-number-via-drive-letter-using-wmi-query-in-c-sharp
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns></returns>
        public static string GetDriveSerialNumber(string driveLetter)
        {
            if (driveLetter.Contains("\\"))
                driveLetter = driveLetter.Replace("\\", "");
            
            try
            {
                using (var partitions = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + driveLetter +
                                                    "'} WHERE ResultClass=Win32_DiskPartition"))
                {
                    foreach (var partition in partitions.Get())
                    {
                        using (var drives = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" +
                                                                partition["DeviceID"] +
                                                                "'} WHERE ResultClass=Win32_DiskDrive"))
                        {
                            foreach (var drive in drives.Get())
                                return (string)drive["SerialNumber"];
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            // Not Found
            return null;
        }



        public static string GetDriveLetterFromSn(string id)
        {
            ManagementClass devs = new ManagementClass(@"Win32_Diskdrive");
            {
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    string a = (string)mo["SerialNumber"];
                    if (a == id)
                    {
                        foreach (ManagementObject b in
                        mo.GetRelated("Win32_DiskPartition"))
                        {
                            foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                                return c["DeviceID"].ToString() + "\\";
                        }
                    }
                }
            }
            return null;
        }
    }
}