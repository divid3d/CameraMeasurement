namespace Kamerka
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_pauseOrResume = new System.Windows.Forms.Button();
            this.textbox_log = new System.Windows.Forms.TextBox();
            this.pb_orginal = new System.Windows.Forms.PictureBox();
            this.textbox_measurements = new System.Windows.Forms.TextBox();
            this.pb_processed = new System.Windows.Forms.PictureBox();
            this.btn_pauseOrResumeAnalyse = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCameraInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internalCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.externalCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_orginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_processed)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_pauseOrResume
            // 
            this.btn_pauseOrResume.Location = new System.Drawing.Point(12, 404);
            this.btn_pauseOrResume.Name = "btn_pauseOrResume";
            this.btn_pauseOrResume.Size = new System.Drawing.Size(164, 61);
            this.btn_pauseOrResume.TabIndex = 3;
            this.btn_pauseOrResume.Text = "Start camera";
            this.btn_pauseOrResume.UseVisualStyleBackColor = true;
            this.btn_pauseOrResume.Click += new System.EventHandler(this.Btn_pauseOrResume_Click);
            // 
            // textbox_log
            // 
            this.textbox_log.Location = new System.Drawing.Point(7, 604);
            this.textbox_log.Multiline = true;
            this.textbox_log.Name = "textbox_log";
            this.textbox_log.ReadOnly = true;
            this.textbox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_log.Size = new System.Drawing.Size(1202, 147);
            this.textbox_log.TabIndex = 4;
            this.textbox_log.TextChanged += new System.EventHandler(this.TxtXYRadius_TextChanged);
            // 
            // pb_orginal
            // 
            this.pb_orginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_orginal.Location = new System.Drawing.Point(12, 45);
            this.pb_orginal.Name = "pb_orginal";
            this.pb_orginal.Size = new System.Drawing.Size(432, 353);
            this.pb_orginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_orginal.TabIndex = 5;
            this.pb_orginal.TabStop = false;
            this.pb_orginal.Click += new System.EventHandler(this.pb_orginal_Click);
            // 
            // textbox_measurements
            // 
            this.textbox_measurements.Location = new System.Drawing.Point(918, 45);
            this.textbox_measurements.Multiline = true;
            this.textbox_measurements.Name = "textbox_measurements";
            this.textbox_measurements.ReadOnly = true;
            this.textbox_measurements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_measurements.Size = new System.Drawing.Size(296, 420);
            this.textbox_measurements.TabIndex = 6;
            // 
            // pb_processed
            // 
            this.pb_processed.Location = new System.Drawing.Point(490, 45);
            this.pb_processed.Name = "pb_processed";
            this.pb_processed.Size = new System.Drawing.Size(410, 353);
            this.pb_processed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_processed.TabIndex = 7;
            this.pb_processed.TabStop = false;
            // 
            // btn_pauseOrResumeAnalyse
            // 
            this.btn_pauseOrResumeAnalyse.Location = new System.Drawing.Point(280, 404);
            this.btn_pauseOrResumeAnalyse.Name = "btn_pauseOrResumeAnalyse";
            this.btn_pauseOrResumeAnalyse.Size = new System.Drawing.Size(164, 61);
            this.btn_pauseOrResumeAnalyse.TabIndex = 8;
            this.btn_pauseOrResumeAnalyse.Text = "Start analyse";
            this.btn_pauseOrResumeAnalyse.UseVisualStyleBackColor = true;
            this.btn_pauseOrResumeAnalyse.Click += new System.EventHandler(this.Btn_pauseOrResumeAnalyse_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1221, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectCameraInputToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.chooseCameraToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // selectCameraInputToolStripMenuItem
            // 
            this.selectCameraInputToolStripMenuItem.Name = "selectCameraInputToolStripMenuItem";
            this.selectCameraInputToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.selectCameraInputToolStripMenuItem.Text = "select camera input";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.clearToolStripMenuItem.Text = "clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // chooseCameraToolStripMenuItem
            // 
            this.chooseCameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internalCameraToolStripMenuItem,
            this.externalCameraToolStripMenuItem});
            this.chooseCameraToolStripMenuItem.Name = "chooseCameraToolStripMenuItem";
            this.chooseCameraToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.chooseCameraToolStripMenuItem.Text = "choose camera";
            // 
            // internalCameraToolStripMenuItem
            // 
            this.internalCameraToolStripMenuItem.Name = "internalCameraToolStripMenuItem";
            this.internalCameraToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.internalCameraToolStripMenuItem.Text = "internal camera";
            this.internalCameraToolStripMenuItem.Click += new System.EventHandler(this.InternalCameraToolStripMenuItem_Click);
            // 
            // externalCameraToolStripMenuItem
            // 
            this.externalCameraToolStripMenuItem.Name = "externalCameraToolStripMenuItem";
            this.externalCameraToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.externalCameraToolStripMenuItem.Text = "external camera";
            this.externalCameraToolStripMenuItem.Click += new System.EventHandler(this.ExternalCameraToolStripMenuItem_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(490, 495);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(410, 56);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Value = 21;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(490, 433);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(410, 56);
            this.trackBar2.TabIndex = 11;
            this.trackBar2.Value = 50;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(487, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Threshold binary 1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(490, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Blur size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 504);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Threshold 2";
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(77, 524);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(407, 56);
            this.trackBar3.TabIndex = 15;
            this.trackBar3.Value = 70;
            this.trackBar3.ValueChanged += new System.EventHandler(this.trackBar3_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(853, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 503);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "label5";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1221, 763);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btn_pauseOrResumeAnalyse);
            this.Controls.Add(this.pb_processed);
            this.Controls.Add(this.textbox_measurements);
            this.Controls.Add(this.pb_orginal);
            this.Controls.Add(this.textbox_log);
            this.Controls.Add(this.btn_pauseOrResume);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pb_orginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_processed)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_pauseOrResume;
        private System.Windows.Forms.TextBox textbox_log;
        private System.Windows.Forms.PictureBox pb_orginal;
        private System.Windows.Forms.TextBox textbox_measurements;
        private System.Windows.Forms.PictureBox pb_processed;
        private System.Windows.Forms.Button btn_pauseOrResumeAnalyse;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectCameraInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internalCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem externalCameraToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

