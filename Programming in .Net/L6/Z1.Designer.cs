namespace L6
{
    partial class Z1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tB1 = new System.Windows.Forms.TextBox();
            this.tB0 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tB4 = new System.Windows.Forms.CheckBox();
            this.tB3 = new System.Windows.Forms.CheckBox();
            this.tB2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tB1);
            this.groupBox1.Controls.Add(this.tB0);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uczelnia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adres:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nazwa:";
            // 
            // tB1
            // 
            this.tB1.Location = new System.Drawing.Point(89, 63);
            this.tB1.Name = "tB1";
            this.tB1.Size = new System.Drawing.Size(391, 22);
            this.tB1.TabIndex = 0;
            this.tB1.Text = "pl. Uniwersytecki 1, 50,137 Wrocław";
            // 
            // tB0
            // 
            this.tB0.Location = new System.Drawing.Point(89, 26);
            this.tB0.Name = "tB0";
            this.tB0.Size = new System.Drawing.Size(391, 22);
            this.tB0.TabIndex = 0;
            this.tB0.Text = "Uniwersytet Wrocławski";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tB4);
            this.groupBox2.Controls.Add(this.tB3);
            this.groupBox2.Controls.Add(this.tB2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 101);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rodzaj studiów";
            // 
            // tB4
            // 
            this.tB4.AutoSize = true;
            this.tB4.Location = new System.Drawing.Point(175, 67);
            this.tB4.Name = "tB4";
            this.tB4.Size = new System.Drawing.Size(117, 21);
            this.tB4.TabIndex = 3;
            this.tB4.Text = "uzupełniające";
            this.tB4.UseVisualStyleBackColor = true;
            this.tB4.Click += new System.EventHandler(this.tB4_Click);
            // 
            // tB3
            // 
            this.tB3.AutoSize = true;
            this.tB3.Location = new System.Drawing.Point(89, 68);
            this.tB3.Name = "tB3";
            this.tB3.Size = new System.Drawing.Size(80, 21);
            this.tB3.TabIndex = 3;
            this.tB3.Text = "dzienne";
            this.tB3.UseVisualStyleBackColor = true;
            this.tB3.Click += new System.EventHandler(this.tB3_Click);
            // 
            // tB2
            // 
            this.tB2.FormattingEnabled = true;
            this.tB2.Items.AddRange(new object[] {
            "3-letnie",
            "3.5-letnie",
            "1.5-letnie",
            "2-letnie"});
            this.tB2.Location = new System.Drawing.Point(89, 37);
            this.tB2.Name = "tB2";
            this.tB2.Size = new System.Drawing.Size(391, 24);
            this.tB2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cykl nauki:";
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(22, 231);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 2;
            this.accept.Text = "Akceptuj";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(439, 230);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Anuluj";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Z1
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(527, 263);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Z1";
            this.Text = "Z1";
            this.Load += new System.EventHandler(this.Z1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB1;
        private System.Windows.Forms.TextBox tB0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox tB4;
        private System.Windows.Forms.CheckBox tB3;
        private System.Windows.Forms.ComboBox tB2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
    }
}