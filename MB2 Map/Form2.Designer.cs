namespace MB2_Map
{
    partial class Form2
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
            listBox1 = new System.Windows.Forms.ListBox();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            UpdateBtn = new System.Windows.Forms.Button();
            DeleteBtn = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            textBox3 = new System.Windows.Forms.TextBox();
            numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            AddBtn = new System.Windows.Forms.Button();
            numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new System.Drawing.Point(12, 42);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(237, 379);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(237, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(6, 22);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(156, 23);
            textBox2.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 4;
            numericUpDown1.Location = new System.Drawing.Point(6, 51);
            numericUpDown1.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(156, 23);
            numericUpDown1.TabIndex = 3;
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 4;
            numericUpDown2.Location = new System.Drawing.Point(6, 80);
            numericUpDown2.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new System.Drawing.Size(156, 23);
            numericUpDown2.TabIndex = 4;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Location = new System.Drawing.Point(6, 109);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new System.Drawing.Size(75, 23);
            UpdateBtn.TabIndex = 5;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += Update_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new System.Drawing.Point(87, 109);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new System.Drawing.Size(75, 23);
            DeleteBtn.TabIndex = 6;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += Delete_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(DeleteBtn);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(UpdateBtn);
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Location = new System.Drawing.Point(269, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(175, 141);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Edit";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.Controls.Add(AddBtn);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Location = new System.Drawing.Point(269, 189);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(175, 141);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Add";
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(6, 22);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(156, 23);
            textBox3.TabIndex = 2;
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 4;
            numericUpDown3.Location = new System.Drawing.Point(6, 51);
            numericUpDown3.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new System.Drawing.Size(156, 23);
            numericUpDown3.TabIndex = 3;
            // 
            // AddBtn
            // 
            AddBtn.Location = new System.Drawing.Point(6, 109);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new System.Drawing.Size(156, 23);
            AddBtn.TabIndex = 5;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            AddBtn.Click += Add_Click;
            // 
            // numericUpDown4
            // 
            numericUpDown4.DecimalPlaces = 4;
            numericUpDown4.Location = new System.Drawing.Point(6, 80);
            numericUpDown4.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new System.Drawing.Size(156, 23);
            numericUpDown4.TabIndex = 4;
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(467, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Name = "Form2";
            Text = "Town Editor";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
    }
}