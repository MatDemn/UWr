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
    public partial class Z1 : Form
    {
        

        public Z1()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            string [] dane = { "", "", "", "" };
            dane[0] = tB0.Text;
            dane[1] = tB1.Text;
            dane[2] = tB2.SelectedItem.ToString();
            dane[3] = tB3.Checked ? tB3.Text : tB4.Text;

            string msg = string.Join(Environment.NewLine, dane);
            string cap = "Uczelnia";

            DialogResult res;

            res = MessageBox.Show(msg, cap);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Z1_Load(object sender, EventArgs e)
        {
            tB2.SelectedIndex = 0;
            tB3.Checked = true;
        }

        private void tB3_Click(object sender, EventArgs e)
        {
            tB4.Checked = false;
        }

        private void tB4_Click(object sender, EventArgs e)
        {
            tB3.Checked = false;
        }
    }
}
