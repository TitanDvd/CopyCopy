using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CccInstaller
{
    public partial class MUI : Form
    {


        private List<string> _installationFiles;



        public MUI()
        {
            InitializeComponent();
            _installationFiles = new List<string>();
            cfg_installPath.Text = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\CCC";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            lbl_status.Text = "Initializing...";
            string cccInstallPath = $"{cfg_installPath.Text}";
            string delegateExeDir = $"{cccInstallPath}\\Delegate";
            string delegateSounds = $"{delegateExeDir}\\Sounds";
            string cccoreExe      = $"{cfg_installPath.Text}\\cccore.exe";


            if(System.Diagnostics.Process.GetProcessesByName("cccore").Length > 0)
            {

                MessageBox.Show(this, "Installation assistant found a cccore instance running. Please close cccore before continue", "3C Installer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            if(System.Diagnostics.Process.GetProcessesByName("CopyCopy").Length > 0)
            {

                MessageBox.Show(this, "Installation assistant found a CopyCopy instance running. Please close CopyCopy before continue", "3C Installer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            cfg_installPath.Visible =
            btn_install.Visible =
            btn_searchPath.Visible = false;
            progressBarInstallation.Visible = true;

            var wExpl = Process.GetProcessesByName("explorer");
            foreach (var we in wExpl)
                try
                {
                    we.Kill();
                }
                catch { }

            try
            {
                if (!Directory.Exists(cccInstallPath))
                    Directory.CreateDirectory(cccInstallPath);
                if (!Directory.Exists(delegateExeDir))
                    Directory.CreateDirectory(delegateExeDir);
                if (!Directory.Exists(delegateSounds))
                    Directory.CreateDirectory(delegateSounds);

                byte[] bin = System.IO.File.ReadAllBytes("ccc_installer.bin");
                Dictionary<string, byte[]> files = (Dictionary<string, byte[]>)CcLib.ObjSerializer.Serializer.Deserialize(bin);
                int p = 1;

                foreach (var file in files)
                {
                    lbl_status.Text = $"Copying: {file.Key}";
                    string iFile = $"{cfg_installPath.Text}\\{file.Key}";
                    _installationFiles.Add(iFile);
                    System.IO.File.WriteAllBytes(iFile, file.Value);

                    progressBarInstallation.Value = p * 100 / files.Count;
                    p++;
                }

                _installationFiles.Add($@"{cfg_installPath.Text}\uninstall.json");
                _installationFiles.Add($@"{cfg_installPath.Text}\Delegate\Sounds");
                _installationFiles.Add($@"{cfg_installPath.Text}\Delegate");
                _installationFiles.Add(cfg_installPath.Text);


                var rgData = new Base.RegistryUtils.RegistryData
                {
                    ExeName = "cccore.exe",
                    InstallationFiles = _installationFiles,
                    InstallationPath = cfg_installPath.Text,
                    Version = "10052022 Alpha Release",
                    ApplicationName = "Copy Copy",
                    Contact = "titandvd92@gmail.com",
                    Publisher = "ByDvd",
                    UninstallExe = cfg_installPath.Text + "\\CccInstaller.exe uninstall"
                };


                Base.RegistryUtils.CreateUninstaller(rgData);
                Base.RegistryUtils.CreateUninstallJson(rgData);


                lbl_status.Text = "Installation Complete!";
                MessageBox.Show(this, "Ccc Installation successfully complete!", "CCC Installer Assistant", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (checkBoxRunOnInstallComplete.Checked)
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = cccoreExe,
                        WorkingDirectory = cfg_installPath.Text
                    });

                if(checkBoxRunAtBoot.Checked)
                    Base.RegistryUtils.StartAtBootRegedit(rgData);

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "CCC Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Process.Start("explorer");
        }



        private void btn_searchPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.ProgramFilesX86
            };

            if (!string.IsNullOrEmpty(fbd.SelectedPath))
                cfg_installPath.Text = fbd.SelectedPath;
        }



        private bool StartAtOsBootFn(string targetExeName, string workingDir, bool autostart = true)
        {
            var startUpFolderW10 = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var shortCutDest     = $"{startUpFolderW10}\\{targetExeName}.lnk";

            if (autostart)
                CreateShortCut(shortCutDest, workingDir, targetExeName);

            else
            {
                try
                {
                    System.IO.File.Delete(shortCutDest);
                }
                catch { }
            }

            return autostart;
        }



        



        private void CreateShortCut(string placeToLnk, string workingdir, string exename)
        {
            WshShell shell = new WshShell();
            // Where to place the shortcut
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(placeToLnk);
            // Execution path example: D:\myprogram\run.exe
            shortcut.TargetPath = $"{workingdir}\\{exename}";
            shortcut.WorkingDirectory = workingdir;
            shortcut.Save();
        }




       
    }
}
