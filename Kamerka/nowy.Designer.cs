namespace Kamerka
{
    partial class nowy
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textbox_log = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectVideoFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearButton = new System.Windows.Forms.Button();
            this.internalCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.externalCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(30, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1371, 542);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 627);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(326, 115);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start cam";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(418, 627);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(326, 115);
            this.button2.TabIndex = 2;
            this.button2.Text = "Analyse start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(1416, 620);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(464, 345);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // textbox_log
            // 
            this.textbox_log.Location = new System.Drawing.Point(30, 802);
            this.textbox_log.Multiline = true;
            this.textbox_log.Name = "textbox_log";
            this.textbox_log.ReadOnly = true;
            this.textbox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_log.Size = new System.Drawing.Size(1363, 163);
            this.textbox_log.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1416, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(464, 474);
            this.dataGridView1.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1902, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seleToolStripMenuItem,
            this.selectVideoFileToolStripMenuItem});
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.cameraToolStripMenuItem.Text = "Image";
            // 
            // seleToolStripMenuItem
            // 
            this.seleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internalCameraToolStripMenuItem,
            this.externalCameraToolStripMenuItem});
            this.seleToolStripMenuItem.Name = "seleToolStripMenuItem";
            this.seleToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.seleToolStripMenuItem.Text = "Select camera";
            // 
            // selectVideoFileToolStripMenuItem
            // 
            this.selectVideoFileToolStripMenuItem.Name = "selectVideoFileToolStripMenuItem";
            this.selectVideoFileToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.selectVideoFileToolStripMenuItem.Text = "Select video file";
            this.selectVideoFileToolStripMenuItem.Click += new System.EventHandler(this.selectVideoFileToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // clearButton
            // 
            this.clearButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.clearButton.Location = new System.Drawing.Point(1416, 546);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(464, 56);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "Clear measures";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // internalCameraToolStripMenuItem
            // 
            this.internalCameraToolStripMenuItem.Name = "internalCameraToolStripMenuItem";
            this.internalCameraToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.internalCameraToolStripMenuItem.Text = "Internal camera";
            this.internalCameraToolStripMenuItem.Click += new System.EventHandler(this.internalCameraToolStripMenuItem_Click);
            // 
            // externalCameraToolStripMenuItem
            // 
            this.externalCameraToolStripMenuItem.Name = "externalCameraToolStripMenuItem";
            this.externalCameraToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.externalCameraToolStripMenuItem.Text = "External camera";
            this.externalCameraToolStripMenuItem.Click += new System.EventHandler(this.externalCameraToolStripMenuItem_Click);
            // 
            // nowy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textbox_log);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "nowy";
            this.Text = "Camera measurement";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textbox_log;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectVideoFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ToolStripMenuItem internalCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem externalCameraToolStripMenuItem;
    }
}