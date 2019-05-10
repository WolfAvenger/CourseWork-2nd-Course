namespace Client2ndCourse
{
    partial class ControllingForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.processes_richTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.physmem_richTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.virtmem_richTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.disks_richTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.proc_richTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.sys_richTextBox = new System.Windows.Forms.RichTextBox();
            this.comps_listBox = new System.Windows.Forms.ListBox();
            this.ip_label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.processes_richTextBox);
            this.groupBox1.Location = new System.Drawing.Point(296, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 436);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Active Processes";
            // 
            // processes_richTextBox
            // 
            this.processes_richTextBox.DetectUrls = false;
            this.processes_richTextBox.Location = new System.Drawing.Point(7, 22);
            this.processes_richTextBox.Name = "processes_richTextBox";
            this.processes_richTextBox.ReadOnly = true;
            this.processes_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.processes_richTextBox.Size = new System.Drawing.Size(377, 402);
            this.processes_richTextBox.TabIndex = 0;
            this.processes_richTextBox.TabStop = false;
            this.processes_richTextBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.physmem_richTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.virtmem_richTextBox);
            this.groupBox2.Location = new System.Drawing.Point(296, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 112);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Memory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Physical (MB):";
            // 
            // physmem_richTextBox
            // 
            this.physmem_richTextBox.Location = new System.Drawing.Point(160, 61);
            this.physmem_richTextBox.Name = "physmem_richTextBox";
            this.physmem_richTextBox.ReadOnly = true;
            this.physmem_richTextBox.Size = new System.Drawing.Size(224, 30);
            this.physmem_richTextBox.TabIndex = 2;
            this.physmem_richTextBox.TabStop = false;
            this.physmem_richTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Virtual (MB):";
            // 
            // virtmem_richTextBox
            // 
            this.virtmem_richTextBox.Location = new System.Drawing.Point(160, 24);
            this.virtmem_richTextBox.Name = "virtmem_richTextBox";
            this.virtmem_richTextBox.ReadOnly = true;
            this.virtmem_richTextBox.Size = new System.Drawing.Size(224, 30);
            this.virtmem_richTextBox.TabIndex = 0;
            this.virtmem_richTextBox.TabStop = false;
            this.virtmem_richTextBox.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.disks_richTextBox);
            this.groupBox3.Location = new System.Drawing.Point(692, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(482, 436);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Disks Info";
            // 
            // disks_richTextBox
            // 
            this.disks_richTextBox.DetectUrls = false;
            this.disks_richTextBox.Location = new System.Drawing.Point(6, 22);
            this.disks_richTextBox.Name = "disks_richTextBox";
            this.disks_richTextBox.ReadOnly = true;
            this.disks_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.disks_richTextBox.Size = new System.Drawing.Size(470, 402);
            this.disks_richTextBox.TabIndex = 0;
            this.disks_richTextBox.TabStop = false;
            this.disks_richTextBox.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.proc_richTextBox);
            this.groupBox4.Location = new System.Drawing.Point(296, 568);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 152);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Processor\'s Activity";
            // 
            // proc_richTextBox
            // 
            this.proc_richTextBox.Location = new System.Drawing.Point(7, 21);
            this.proc_richTextBox.Name = "proc_richTextBox";
            this.proc_richTextBox.ReadOnly = true;
            this.proc_richTextBox.Size = new System.Drawing.Size(377, 125);
            this.proc_richTextBox.TabIndex = 0;
            this.proc_richTextBox.TabStop = false;
            this.proc_richTextBox.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.sys_richTextBox);
            this.groupBox5.Location = new System.Drawing.Point(692, 450);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(482, 270);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "System Info";
            // 
            // sys_richTextBox
            // 
            this.sys_richTextBox.Location = new System.Drawing.Point(7, 22);
            this.sys_richTextBox.Name = "sys_richTextBox";
            this.sys_richTextBox.ReadOnly = true;
            this.sys_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.sys_richTextBox.Size = new System.Drawing.Size(469, 242);
            this.sys_richTextBox.TabIndex = 0;
            this.sys_richTextBox.TabStop = false;
            this.sys_richTextBox.Text = "";
            // 
            // comps_listBox
            // 
            this.comps_listBox.FormattingEnabled = true;
            this.comps_listBox.ItemHeight = 16;
            this.comps_listBox.Location = new System.Drawing.Point(13, 13);
            this.comps_listBox.Name = "comps_listBox";
            this.comps_listBox.Size = new System.Drawing.Size(241, 676);
            this.comps_listBox.TabIndex = 5;
            this.comps_listBox.SelectedIndexChanged += new System.EventHandler(this.comps_listBox_SelectedIndexChanged);
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Location = new System.Drawing.Point(13, 703);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(75, 17);
            this.ip_label.TabIndex = 6;
            this.ip_label.Text = "Current IP:";
            // 
            // ControllingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 732);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.comps_listBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ControllingForm";
            this.Text = "Observer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ControllingForm_FormClosed);
            this.Load += new System.EventHandler(this.ControllingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox processes_richTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox physmem_richTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox virtmem_richTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox disks_richTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox proc_richTextBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox sys_richTextBox;
        private System.Windows.Forms.ListBox comps_listBox;
        private System.Windows.Forms.Label ip_label;
    }
}

