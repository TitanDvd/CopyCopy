using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace CcCore.Controls
{
    public partial class FAButton : UserControl
    {
        public PictureBox Button => pictureBox1;
        private Color _backcolor { get; set; }
        private Color _backHoverColor { get; set; }
        private Color activeColorBeforeActivesw { get; set; }
        private bool alternateActive = false;
        private bool _swActive = false;
        public bool ActiveOnClick
        {
            get => alternateActive;
            set => alternateActive = value;
        }

        public Color BackgroundColor
        {
            get => _backcolor;
            set { pictureBox1.BackColor = BackColor = _backcolor = value; Invalidate(); }
        }

        private IconChar _icon
        {
            get; set;
        }

        public IconChar IconGraph
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                pictureBox1.Image = _icon.ToBitmap(Color.DarkGray, 64);
                Invalidate();
            }
        }


        private Color _iconColor
        {
            get;
            set;
        }

        public Color IconColor
        {
            get => _iconColor;
            set
            {
                _iconColor = value;
                pictureBox1.Image = IconGraph.ToBitmap(_iconColor, 64);
                Invalidate();
            }
        }


        public Color BackgroundHoverColor
        {
            get => _backHoverColor;
            set => _backHoverColor = value;
        }


        private int _iconSize { get; set; }
        public int IconSize
        {
            get => _iconSize;
            set
            {
                _iconSize = value;
                pictureBox1.Image = IconGraph.ToBitmap(IconColor, _iconSize);
                Invalidate();
            }
        }


        public FAButton()
        {
            InitializeComponent();
            IconColor = Color.DarkGray;
            IconSize = 64;
            Button.Click += delegate(object o, EventArgs e)
            {
                if(!_swActive && ActiveOnClick)
                {
                    activeColorBeforeActivesw = BackgroundColor;
                    BackgroundColor = BackgroundHoverColor;
                    _swActive = true;
                }
                else
                {
                    BackgroundColor = activeColorBeforeActivesw;
                    _swActive = false;
                }
                OnClick(e);
            };
        }

        private void FAButton_MouseEnter(object sender, EventArgs e)
        {
            BackColor = BackgroundHoverColor;
            pictureBox1.BackColor = BackgroundHoverColor;
        }

        private void FAButton_MouseLeave(object sender, EventArgs e)
        {
            BackColor = BackgroundColor;
            pictureBox1.BackColor = BackgroundColor;
        }
    }
}
