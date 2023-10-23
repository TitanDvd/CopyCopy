using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class CustomLabel : Label
{
    [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }

    public CustomLabel()
    {
        AutoSize = false;
    }

    void Fit()
    {
        Size size = GetPreferredSize(new Size(Width, 0));
        Height = size.Height;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Fit();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        Fit();
    }
}