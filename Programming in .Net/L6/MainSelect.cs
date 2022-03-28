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
    public partial class MainSelect : Form
    {
        public MainSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form z1 = new Z1();
            z1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form z2 = new Z2();
            z2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form z3 = new Z3();
            z3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form z4 = new Z4();
            z4.Show();
        }
    }
}
