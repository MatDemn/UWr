using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace L6
{
    class SmoothProgressBar : UserControl
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 100;

        private int val = 0;
        public int Value {
            get
            {
                return val; 
            }
            set
            {
                if (value < Min) val = Min;
                else if (value > Max) val = Max;
                else val = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush b = new LinearGradientBrush(new Point(0, 10), new Point(Width, 10), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 51, 204, 51));

            double newValue = (double)(Value - Min) / (double)(Max - Min);
            Rectangle rectan = new Rectangle(Location.X, Location.Y, (int)(Width*newValue), Height);

            g.FillRectangle(b, rectan);

            rectan.Width = Width - 1;
            rectan.Height -= 1;
            g.DrawRectangle(Pens.DarkBlue, rectan);
        }
    }
}
