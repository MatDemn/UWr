using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L6
{
    public partial class Z3 : Form
    {
        public Z3()
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
            InitializeComponent();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int width = e.ClipRectangle.Width;
            int height = e.ClipRectangle.Height;
            Point center = new Point(width / 2, height / 2);
            Point newPoint = new Point(center.X + 50, center.Y + 50);
            Graphics GDI = e.Graphics;
            Pen hPen = new Pen(Color.Black, 2);
            Pen mPen = new Pen(Color.Gray, 1);
            Pen sPen = new Pen(Color.Red, 1);
            SolidBrush dBrush = new SolidBrush(Color.Black);

            int hs = DateTime.Now.Hour;
            int ms = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            int calcX;
            int calcY;

            GDI.Clear(Color.White);
            GDI.DrawEllipse(mPen, 0, 0, width - 1, height - 1);

            // Digits on clock

            GDI.DrawString("12", new Font("Arial", 16), dBrush, new Point(center.X-16, 10));
            GDI.DrawString("3", new Font("Arial", 16), dBrush, new Point(width-26, center.Y-8));
            GDI.DrawString("6", new Font("Arial", 16), dBrush, new Point(center.X - 8, height-30));
            GDI.DrawString("9", new Font("Arial", 16), dBrush, new Point(16, center.Y - 10));

            // S
            calcX = (center.X) + (int)(0.8 * center.X * Math.Sin(2 * Math.PI * (double)ss / 60));
            calcY = (center.Y) - (int)(0.8 * center.Y * Math.Cos(2 * Math.PI * (double)ss / 60));
            GDI.DrawLine(sPen, center, new Point(calcX, calcY));

            // M
            calcX = (center.X) + (int)(0.7 * center.X * Math.Sin(2 * Math.PI * (double)ms / 60));
            calcY = (center.Y) - (int)(0.7 * center.Y * Math.Cos(2 * Math.PI * (double)ms / 60));
            GDI.DrawLine(mPen, center, new Point(calcX, calcY));

            // H
            calcX = (center.X) + (int)(0.5 * center.X * Math.Sin(2 * Math.PI * (double)hs / 12));
            calcY = (center.Y) - (int)(0.5 * center.Y * Math.Cos(2 * Math.PI * (double)hs / 12));
            GDI.DrawLine(hPen, center, new Point(calcX, calcY));
        }
    }
}
