namespace Client2ndCourse
{
    partial class ConnectingForm
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
            this.ip_label = new System.Windows.Forms.Label();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Button();
            this.admin_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Location = new System.Drawing.Point(12, 15);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(258, 17);
            this.ip_label.TabIndex = 0;
            this.ip_label.Text = "Put IPv4-adress of server to connect to:";
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(285, 12);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(257, 22);
            this.ip_textBox.TabIndex = 1;
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(234, 87);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 2;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // admin_checkBox
            // 
            this.admin_checkBox.AutoSize = true;
            this.admin_checkBox.Location = new System.Drawing.Point(285, 40);
            this.admin_checkBox.Name = "admin_checkBox";
            this.admin_checkBox.Size = new System.Drawing.Size(133, 21);
            this.admin_checkBox.TabIndex = 3;
            this.admin_checkBox.Text = "I\'m administrator";
            this.admin_checkBox.UseVisualStyleBackColor = true;
            // 
            // ConnectingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 119);
            this.Controls.Add(this.admin_checkBox);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.ip_label);
            this.Name = "ConnectingForm";
            this.Text = "ConnectingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.CheckBox admin_checkBox;
    }
}