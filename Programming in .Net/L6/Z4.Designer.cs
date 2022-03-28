namespace L6
{
    partial class Z4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.smoothProgressBar2 = new L6.SmoothProgressBar();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add progress";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(461, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Sub progress";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // smoothProgressBar2
            // 
            this.smoothProgressBar2.Location = new System.Drawing.Point(0, 0);
            this.smoothProgressBar2.Max = 100;
            this.smoothProgressBar2.Min = 0;
            this.smoothProgressBar2.Name = "smoothProgressBar2";
            this.smoothProgressBar2.Size = new System.Drawing.Size(555, 226);
            this.smoothProgressBar2.TabIndex = 0;
            this.smoothProgressBar2.Value = 0;
            this.smoothProgressBar2.Load += new System.EventHandler(this.smoothProgressBar2_Load);
            // 
            // Z4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 290);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.smoothProgressBar2);
            this.Name = "Z4";
            this.Text = "Z4";
            this.ResumeLayout(false);

        }

        #endregion

        private SmoothProgressBar smoothProgressBar2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}