using System;
using System.IO;
using System.Windows.Forms;
using static CopyCopy.Base.Settings;

namespace CopyCopy.Views
{
    public partial class Options : Form
    {


        public Options()
        {
            AppSettings.LoadSettings();
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog filed = new OpenFileDialog
            {
                Filter = "Archivos WAV|*.wav",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
            };

            filed.ShowDialog(this);

            if(File.Exists(filed.FileName))
                cfg_soundPath.Text = filed.FileName;
        }



        private void Options_Load(object sender, EventArgs e)
        {
            checkBoxAskWhenFoceClose.Checked = AppSettings.Options.AskWhenWindowIsForceClose;
            checkBoxCloseWhenFinish.Checked = AppSettings.Options.CloseWindowWhenCopyFinish;
            checkBoxDoSound.Checked = AppSettings.Options.SoundWhenCopyFinish;
            checkBoxShowProgressInTitlebar.Checked = AppSettings.Options.ShowProgressOnTitleBar;
            checkBoxUnitsInTitlebar.Checked = AppSettings.Options.ShowUnitsRootInTitleBar;


            if (string.IsNullOrEmpty(AppSettings.Options.SoundPath))
                cfg_soundPath.Text = $"{Directory.GetCurrentDirectory()}\\Sounds\\0001.wav";
            else
                cfg_soundPath.Text = AppSettings.Options.SoundPath;


            if (AppSettings.Options.ShowIconPerCopyInTaskBar)
                radioButtonShowIconPerCopy.Checked = true;
            else
                radioButtonAllCopysOneIcon.Checked = true;

            if (AppSettings.Options.SoundWhenCopyFinish)
                cfg_soundPath.Enabled = true;
            else
                cfg_soundPath.Enabled = false;
        }



        private void checkBoxDoSound_CheckedChanged(object sender, EventArgs e)
        {
            cfg_soundPath.Enabled = checkBoxDoSound.Checked;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            AppSettings.Options.AskWhenWindowIsForceClose = checkBoxAskWhenFoceClose.Checked;
            AppSettings.Options.CloseWindowWhenCopyFinish = checkBoxCloseWhenFinish.Checked;
            AppSettings.Options.SoundWhenCopyFinish       = checkBoxDoSound.Checked;
            AppSettings.Options.ShowProgressOnTitleBar    = checkBoxShowProgressInTitlebar.Checked;
            AppSettings.Options.ShowUnitsRootInTitleBar   = checkBoxUnitsInTitlebar.Checked;
            AppSettings.Options.SoundPath                 = cfg_soundPath.Text;

            if (radioButtonShowIconPerCopy.Checked)
                AppSettings.Options.ShowIconPerCopyInTaskBar = true;
            else
                AppSettings.Options.ShowIconPerCopyInTaskBar = false;

            try
            {
                AppSettings.SaveSettings();
                MessageBox.Show(this, "Las opciones se han guardado correctamente!\r\n" +
                    "Tendran efecto en las proximas copias que se realicen", 
                    "Opciones", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
