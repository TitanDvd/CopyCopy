using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CccInstaller.Base
{
    
    public class RegistryUtils
    {
        public class RegistryData
        {
            public List<string> InstallationFiles { get; set; }
            public string InstallationPath { get; set; }
            public string ExeName { get; set; }
            public string Version { get; set; }
            public string ApplicationName { get; set; }
            public string Contact { get; set; }
            public string UninstallExe { get; set; }
            public string Publisher { get; set; }
        }



        public static void CreateUninstaller(RegistryData regData)
        {
            RegistryKey uninstallKey;
            using (uninstallKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (uninstallKey == null)
                    uninstallKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");


                try
                {
                    RegistryKey key = null;

                    try
                    {
                        var sb = uninstallKey.OpenSubKey(regData.ExeName, true);
                        if (sb == null)
                            key = uninstallKey.CreateSubKey(regData.ExeName);
                        

                        key.SetValue("DisplayName", regData.ApplicationName);
                        key.SetValue("ApplicationVersion", regData.Version);
                        key.SetValue("Publisher", regData.Publisher);
                        key.SetValue("DisplayIcon", regData.InstallationPath + "\\" + regData.ExeName);
                        key.SetValue("DisplayVersion", regData.Version);
                        key.SetValue("Contact", regData.Contact);
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", regData.UninstallExe);


                       
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }



        public static void CreateUninstallJson(RegistryData rData)
        {
            string json = JsonConvert.SerializeObject(rData.InstallationFiles);
            System.IO.File.WriteAllText($"{rData.InstallationPath}\\uninstall.json", json);
        }



        public static void RemoveUninstallKey(string exename)
        {
            Registry.LocalMachine.DeleteSubKeyTree($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{exename}");
        }



        public static void StartAtBootRegedit(RegistryData regData)
        {
            var kAppPaths = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths", true);
            if (kAppPaths == null)
                kAppPaths = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths");

            var sk = kAppPaths.CreateSubKey(regData.ExeName);
            sk.SetValue("Path", regData.InstallationPath);
            sk.SetValue("", regData.InstallationPath + "\\" + regData.ExeName);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", 
                regData.ExeName, 
                regData.InstallationPath + "\\" + regData.ExeName);
        }



        public static void RemoveStartAtBootKeys(string exename)
        {
            var kAppPaths = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\", true);
            var runK = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run", true);
            if (kAppPaths == null)
                return;
            if (runK == null)
                return;

            runK.DeleteValue(exename);
            kAppPaths.DeleteSubKeyTree(exename);
        }



        public static void Uninstall(string exename, Action<int> callback)
        {
            string jsonUninstallPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\" + exename, "Path", null);
            if (jsonUninstallPath != null)
            {
                jsonUninstallPath += "\\uninstall.json";
                int i = 0;
                List<string> installationFiles = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(jsonUninstallPath));
                foreach (var file in installationFiles)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        MoveFileEx(file, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }

                    callback.Invoke(i * 100 / installationFiles.Count);
                    i++;
                }

                RemoveStartAtBootKeys(exename);
                RemoveUninstallKey(exename);
            }
        }



        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern int MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);



        [Flags]
        enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 0x00000001,
            MOVEFILE_COPY_ALLOWED = 0x00000002,
            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
            MOVEFILE_WRITE_THROUGH = 0x00000008,
            MOVEFILE_CREATE_HARDLINK = 0x00000010,
            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }
    }
}
