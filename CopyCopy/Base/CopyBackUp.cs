using CopyCopy.Types;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyCopy.Base
{
    public class CopyBackUp
    {
        private string DriveSerialNumber = "";
        private string BackUpFilePath    = "";
        private string _backupDir        = "";
        private DateTime _now            = DateTime.Now;


        public CopyBackUp()
        {
            string BackUpDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\CopyCopyBackUp";

            if (!Directory.Exists(BackUpDir))
                Directory.CreateDirectory(BackUpDir);

           
            var backUpDate     = DateTime.Now.ToLongDateString();
            _backupDir         = $"{BackUpDir}\\{backUpDate}";
           
        }





        public void SaveCopyList(IEnumerable<FileListItem> files)
        {
            var fElement = files.FirstOrDefault();
            if (fElement != null)
            {
                string destDriveLetter = fElement.GetDestinationDriveLetter();
                string backUpFile      = $"ccc_{_now.ToString("yyyy_MM_dd_HH_mm_ss")}.json";
                BackUpFilePath         = _backupDir + "\\" + backUpFile;

                if (!destDriveLetter.StartsWith("\\"))
                    DriveSerialNumber = DiskDriveUtil.GetDriveSerialNumber(destDriveLetter);
                else
                    DriveSerialNumber = "NETWORK_DRIVE";

                if (!Directory.Exists(_backupDir))
                    Directory.CreateDirectory(_backupDir);
                
                var bUp = new BackUpJsonContainer { CopyListItems = files.ToList(), SerialNumber = DriveSerialNumber };
                string jsonContent = JsonConvert.SerializeObject(bUp, Formatting.Indented);
                File.WriteAllText(BackUpFilePath, jsonContent);
            }
        }

        
        public void DeleteBackUpFile()
        {
            try
            {
                File.Delete(BackUpFilePath);
            }
            catch { }
        }


        public static BackUpJsonContainer LoadBackupFile(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException();

            if(File.Exists(filepath))
            {
                var jsonStr = File.ReadAllText(filepath);
                var bUp     = JsonConvert.DeserializeObject<BackUpJsonContainer>(jsonStr);
                return bUp;
            }

            return null;
        }
    }



    public class BackUpJsonContainer
    {
        public string SerialNumber { get; set; }
        public List<FileListItem> CopyListItems { get; set; }
    }
}
