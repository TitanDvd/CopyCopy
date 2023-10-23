using System;
using System.Drawing;
using System.Windows.Forms;

namespace CopyCopy.Controls
{
    class CustomProgressBar : ProgressBar
    {

        public String CustomText { get; set; }



        public Color TextColor { get; set; }



        public CustomProgressBar()
        {
            TextColor = Color.Black;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }




        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            // rect.Inflate(-3, -3);
            if (Value > 0)
            {
                // As we doing this ourselves we need to draw the chunks on the progress bar
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawVerticalChunks(g, clip);
            }
            
            int percent = (int)(((double)this.Value / (double)this.Maximum) * 100);

            using (Font f = new Font("Arial", 8.25F))
            {

                SizeF len = g.MeasureString(CustomText, f);
                // Calculate the location of the text (the middle of progress bar)
                // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
                Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
                // Draw the custom text
                g.DrawString(CustomText, f, new SolidBrush(TextColor), location);
            }
        }
    }
}
