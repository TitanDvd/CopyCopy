using System;
using System.Drawing;
using System.Windows.Forms;

namespace CopyCopy.Controls
{
    public partial class DialogForm : Form
    {
        private bool             _doSameOp;
        private DialogFormResult _result;


        public DialogForm(string messages, DialogButtons showButtons, bool showSameOpCheckBox = true)
        {
            InitializeComponent();

            if (showButtons.HasFlag(DialogButtons.AbortButton))
                btn_abort.Visible    = true;
            if (showButtons.HasFlag(DialogButtons.IgnoreButton))
                btn_ignore.Visible   = true;
            if (showButtons.HasFlag(DialogButtons.OverrideButton))
                btn_override.Visible = true;
            if (showButtons.HasFlag(DialogButtons.RenameButton))
                btn_rename.Visible   = true;
            if (showButtons.HasFlag(DialogButtons.RetryButton))
                btn_retry.Visible    = true;

            chk_itinerance.Visible = showSameOpCheckBox;
            lbl_text.Text     = messages;
            pictureBox1.Image = SystemIcons.Warning.ToBitmap();
        }



        public static DialogFormResult Show(IWin32Window parent, string message, DialogButtons showButtons, out bool doAlways, ref IWin32Window hWin)
        {
            var df = new DialogForm(message, showButtons);
            df.ShowDialog(parent);
            doAlways = df._doSameOp;
            hWin = df;
            return df._result;
        }



        public static DialogFormResult Show(IWin32Window parent, string message, DialogButtons showButtons, out bool doAlways)
        {
            var df = new DialogForm(message, showButtons);
            df.ShowDialog(parent);
            doAlways = df._doSameOp;
            return df._result;
        }



        public static DialogFormResult Show(IWin32Window parent, string message, DialogButtons showButtons)
        {
            var df = new DialogForm(message, showButtons, false);
            df.ShowDialog(parent);
            return df._result;
        }




        private void btn_rename_Click(object sender, EventArgs e)
        {
            _result = DialogFormResult.Rename;
            Close();
        }

        private void btn_override_Click(object sender, EventArgs e)
        {
            _result = DialogFormResult.Override;
            Close();
        }

        private void btn_ignore_Click(object sender, EventArgs e)
        {
            _result = DialogFormResult.Ignore;
            Close();
        }

        private void chk_itinerance_CheckedChanged(object sender, EventArgs e)
        {
            _doSameOp = chk_itinerance.Checked;
        }

        private void btn_retry_Click(object sender, EventArgs e)
        {
            _result = DialogFormResult.Retry;
            Close();
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            _result = DialogFormResult.Abort;
            Close();
        }
    }


    public enum DialogFormResult:byte
    {
        Override,
        Ignore,
        Rename,
        Abort,
        Retry
    }

    [Flags]
    public enum DialogButtons
    {
        AbortButton    = 1, // Abort
        IgnoreButton   = 2, // Ignore
        RenameButton   = 4, // Rename
        OverrideButton = 8, // Override
        RetryButton    = 16 // Retry
    }
}
