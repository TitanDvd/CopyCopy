using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CcCore.Base
{
    public class Settings
    {
        public string DelegatedExe { get; set; }
        private string settingsFile = "ccc_settings.json";
        private string UserDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string _settingsFullPath;

        public Settings()
        {
            string appConfDir = $"{UserDataDirectory}\\CCC";
            if (!Directory.Exists(appConfDir))
                Directory.CreateDirectory(appConfDir);
            _settingsFullPath = $"{appConfDir}\\{settingsFile}";
        }


        public void SaveSettings()
        {
            string jsonStr = JsonConvert.SerializeObject(this);
            File.WriteAllText(_settingsFullPath, jsonStr);
        }



        public bool LoadSettings()
        {
            if (File.Exists(_settingsFullPath))
            {
                string jsonStr = File.ReadAllText(_settingsFullPath);
                Settings settings = JsonConvert.DeserializeObject<Settings>(jsonStr);
                DelegatedExe = settings.DelegatedExe;
                return true;
            }
            else
            {
                // Some default values
                DelegatedExe = "Delegate\\CopyCopy.exe";
            }

            return false;
        }
    }
}
