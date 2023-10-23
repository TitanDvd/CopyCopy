using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CcCore.Views
{
    public partial class Configurations : Form
    {
        public Configurations()
        {
            InitializeComponent();
            cfg_delegatedExe.Text = CCContext.Settings.DelegatedExe;
        }

        private void btn_searchExe_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Executable|*.exe";
            fileDialog.ShowDialog(this);
            string file = fileDialog.FileName;
            if(File.Exists(file))
            {
                CCContext.Settings.DelegatedExe = file;
                cfg_delegatedExe.Text = file;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            CCContext.Settings.SaveSettings();
            MessageBox.Show(this, "Settings saved succesfully!", "CCC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
