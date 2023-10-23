using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyCopy.Types;
using Newtonsoft.Json;
using System.IO;

namespace CopyCopy.Base
{
    public class Settings
    {
        public static Settings AppSettings = new Settings();
        
        public Options Options { get; set; }


        private string CopyCopyDirectoryOptions;
        private string CopyCopyFileOptions;


        public Settings()
        {
            CopyCopyDirectoryOptions = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\CopyCopyUI";
            CopyCopyFileOptions      = $"{CopyCopyDirectoryOptions}\\ccui_settings.json";

            if (!Directory.Exists(CopyCopyDirectoryOptions))
                Directory.CreateDirectory(CopyCopyDirectoryOptions);
        }



        public void LoadSettings()
        {
            if (File.Exists(CopyCopyFileOptions))
            {
                string json = File.ReadAllText(CopyCopyFileOptions);
                AppSettings = JsonConvert.DeserializeObject<Settings>(json);
            }
            else
                DefaultOptions();
        }



        public void SaveSettings()
        {
            string jsonOpt = JsonConvert.SerializeObject(AppSettings, Formatting.Indented);
            File.WriteAllText(CopyCopyFileOptions, jsonOpt);
        }



        private void DefaultOptions()
        {
            Options = new Options
            {
                AskWhenWindowIsForceClose = true,
                CloseWindowWhenCopyFinish = false,
                ShowIconPerCopyInTaskBar  = true,
                ShowProgressOnTitleBar    = true,
                ShowUnitsRootInTitleBar   = true,
                SoundPath                 = null,
                SoundWhenCopyFinish       = true
            };
        }
    }
}
