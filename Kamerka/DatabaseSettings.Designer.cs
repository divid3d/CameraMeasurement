namespace Kamerka
{
    partial class DatabaseSettings
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
            this.textBox_host = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_database = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_databaseEnable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_host
            // 
            this.textBox_host.Location = new System.Drawing.Point(59, 49);
            this.textBox_host.Name = "textBox_host";
            this.textBox_host.Size = new System.Drawing.Size(318, 22);
            this.textBox_host.TabIndex = 0;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(59, 112);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(318, 22);
            this.textBox_username.TabIndex = 1;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(59, 174);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(318, 22);
            this.textBox_password.TabIndex = 2;
            // 
            // textBox_database
            // 
            this.textBox_database.Location = new System.Drawing.Point(59, 235);
            this.textBox_database.Name = "textBox_database";
            this.textBox_database.Size = new System.Drawing.Size(318, 22);
            this.textBox_database.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Database";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(59, 280);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(119, 30);
            this.button_connect.TabIndex = 8;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(253, 280);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(124, 30);
            this.button_cancel.TabIndex = 9;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // checkBox_databaseEnable
            // 
            this.checkBox_databaseEnable.AutoSize = true;
            this.checkBox_databaseEnable.Location = new System.Drawing.Point(59, 5);
            this.checkBox_databaseEnable.Name = "checkBox_databaseEnable";
            this.checkBox_databaseEnable.Size = new System.Drawing.Size(195, 21);
            this.checkBox_databaseEnable.TabIndex = 10;
            this.checkBox_databaseEnable.Text = "Enable datanbase logging";
            this.checkBox_databaseEnable.UseVisualStyleBackColor = true;
            this.checkBox_databaseEnable.CheckedChanged += new System.EventHandler(this.checkBox_databaseEnable_CheckedChanged);
            // 
            // DatabaseSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 339);
            this.Controls.Add(this.checkBox_databaseEnable);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_database);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.textBox_host);
            this.Name = "DatabaseSettings";
            this.Text = "Database settings";
            this.Load += new System.EventHandler(this.DatabaseSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_host;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_database;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_databaseEnable;
    }
}