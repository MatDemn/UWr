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
    public partial class Z2 : Form
    {
        int nodeCount = -1;
        public Z2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(progressBar1.Value-10 >= 0)
                progressBar1.Value -= 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value + 10 <= 100)
                progressBar1.Value += 10;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                progressBar1.Style = ProgressBarStyle.Blocks;
            else if (comboBox1.SelectedIndex == 1)
                progressBar1.Style = ProgressBarStyle.Continuous;
            else
                progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            nodeCount++;
            if(treeView1.Nodes.Count == 0 || treeView1.SelectedNode == null)
                treeView1.Nodes.Add("Node" + nodeCount);
            else {
                treeView1.SelectedNode.Nodes.Add("Node" + nodeCount);
                treeView1.SelectedNode.Expand();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(treeView1.Nodes.Count == 0) return;
            if (treeView1.SelectedNode == null) { 
                treeView1.Nodes.RemoveAt(nodeCount);
                nodeCount--;
            }
            else
            {
                treeView1.SelectedNode.Remove();
            }
            treeView1.SelectedNode = null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = null;
            nodeCount++;
            treeView1.Nodes.Add("Node" + nodeCount);
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Click to add new node to tree";
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Click to add new root to tree";
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Click to delete selected node from tree";
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Change style of progress bar";
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Shrink progress bar";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Reset progress bar";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Enlarge progress bar";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "";
        }
    }
}
