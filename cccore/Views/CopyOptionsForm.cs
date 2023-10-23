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
    public partial class CopyOptionsForm : Form
    {

        MessageBoxIcon _icon;
        public ActionResult Result;
        private bool _actionSet;
        private CopyListControls _controls;
        public delegate ActionResult FormActionResult();
        public delegate Tuple<ActionResult,bool> FormActionResultA();


        public CopyOptionsForm(MessageBoxIcon icon, string text, CopyListControls CopyListControls)
        {
            InitializeComponent();
            _icon = icon;
            lbl_text.Text = text;
            _controls = CopyListControls;
            Text = "Opciones CopyCopy";
        }

        public static ActionResult Show(MessageBoxIcon icon, string text, CopyListControls controls, out bool sameActionForAllErrors, CopyCopyMF pWnd)
        {
            FormActionResultA func = () =>
            {
                CopyOptionsForm cof = new CopyOptionsForm(icon, text, controls);
                cof.ShowDialog();
                return new Tuple<ActionResult, bool>(cof.Result, cof.chk_sameForAll.Checked);
            };

            pWnd.Activate();
            Tuple<ActionResult, bool> resul = (Tuple<ActionResult, bool>)pWnd.EndInvoke(pWnd.BeginInvoke(func));
            sameActionForAllErrors = resul.Item2;
            return resul.Item1;
        }


        public static ActionResult Show(MessageBoxIcon icon, string text, CopyListControls controls, CopyCopyMF pWnd)
        {
            FormActionResult far = () => 
            {
                CopyOptionsForm cof = new CopyOptionsForm(icon, text, controls);
                cof.ShowDialog();
                return cof.Result;
            };

            pWnd.Activate();
            return (ActionResult)pWnd.EndInvoke(pWnd.BeginInvoke(far));
        }


        private void CopyOptionsForm_Load(object sender, EventArgs e)
        {
            Icon sys;
            switch (_icon)
            {
                case MessageBoxIcon.Asterisk:
                case MessageBoxIcon.Error:
                    sys = SystemIcons.Error;
                    break;

                case MessageBoxIcon.Exclamation:
                    sys = SystemIcons.Warning;
                    break;

                default:
                    sys = SystemIcons.WinLogo;
                    break;
            }
            ShowButtonsControls();
            pictureBox1.Image = sys.ToBitmap();
            System.Media.SystemSounds.Asterisk.Play();
        }

        private void btn_overWrite_Click(object sender, EventArgs e)
        {
            Result = ActionResult.OverwriteNewer;
            _actionSet = true;
            Close();
        }

        private void btn_skip_Click(object sender, EventArgs e)
        {
            Result = ActionResult.Skip;
            _actionSet = true;
            Close();
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            Result = ActionResult.RenameNewer;
            _actionSet = true;
            Close();
        }

        private void CopyOptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_actionSet && _controls == CopyListControls.ShowRenameSkipOverwrite)
                Result = ActionResult.CancelCopy;
            else if(!_actionSet)
                Result = ActionResult.Ignore;
        }

        private void ShowButtonsControls()
        {
            if(_controls == CopyListControls.ShowAddOrNewList)
            {
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                chk_sameForAll.Visible = false;
            }

            if(_controls == CopyListControls.ShowRenameSkipOverwrite)
            {
                panel7.Visible = false;
                panel8.Visible = false;
            }
        }


        

        public enum ActionResult  
        {
            OverwriteNewer,
            OverwriteOlder,
            Skip,
            RenameNewer,
            RenameOlder,
            AddToCurrentList,
            CreateNewCopyForm,
            CancelCopy,
            Ignore
        }


        public enum CopyListControls
        {
            ShowRenameSkipOverwrite,
            ShowAddOrNewList
        }

        private void btn_addExistent_Click(object sender, EventArgs e)
        {
            Result = ActionResult.AddToCurrentList;
            _actionSet = true;
            Close();
        }

        private void btn_createNewCopy_Click(object sender, EventArgs e)
        {
            Result = ActionResult.CreateNewCopyForm;
            _actionSet = true;
            Close();
        }
    }

}
