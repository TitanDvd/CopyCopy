using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CccInstaller.Views
{
    public partial class UninstallView : Form
    {
        public UninstallView()
        {
            InitializeComponent();
        }



        private void btn_uninstall_Click(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName("cccore").Length > 0)
            {

                var res = MessageBox.Show(this, 
                    "Installation assistant found a cccore instance running. Do you want force the process shut down?", 
                    "3C Installer", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    foreach (var ccc in System.Diagnostics.Process.GetProcessesByName("cccore"))
                        try
                        {
                            ccc.Kill();
                        }
                        catch { }
                else
                    return;
            }



            if (System.Diagnostics.Process.GetProcessesByName("CopyCopy").Length > 0)
            {

                var res = MessageBox.Show(this,
                    "Installation assistant found a CopyCopy instance running. Do you want force the process shut down?",
                    "3C Installer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    foreach (var ccc in System.Diagnostics.Process.GetProcessesByName("CopyCopy"))
                        try
                        {
                            ccc.Kill();
                        }
                        catch { }
                else
                    return;
            }

            progressBar1.Visible = true;
            btn_uninstall.Visible = false;

            Base.RegistryUtils.Uninstall("cccore.exe", new Action<int>((percent)=> 
            {
                BeginInvoke(new Action(()=> 
                {
                    progressBar1.Value = percent;
                }));
            }));

            MessageBox.Show(this, 
                "Unistallation complete. Some files and directories will be deleted after system reboot", 
                "3C Uninstaller",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            Close();
        }
    }
}
