namespace WeatherCollectorDesktop
{
    partial class WeatherCollector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeatherCollector));
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.lblCountDown = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblRunTimes = new System.Windows.Forms.Label();
            this.lblCurTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogging = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lnkLblHelp = new System.Windows.Forms.LinkLabel();
            this.btnExportLogs = new System.Windows.Forms.Button();
            this.lnkLblSettings = new System.Windows.Forms.LinkLabel();
            this.lblDatabaseExist = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(155, 126);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(92, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start Collecting";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(155, 126);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(92, 23);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "Stop Collecting";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // lblCountDown
            // 
            this.lblCountDown.AutoSize = true;
            this.lblCountDown.BackColor = System.Drawing.Color.Transparent;
            this.lblCountDown.Location = new System.Drawing.Point(359, 9);
            this.lblCountDown.Name = "lblCountDown";
            this.lblCountDown.Size = new System.Drawing.Size(31, 13);
            this.lblCountDown.TabIndex = 3;
            this.lblCountDown.Text = "3600";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // lblRunTimes
            // 
            this.lblRunTimes.AutoSize = true;
            this.lblRunTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunTimes.Location = new System.Drawing.Point(190, 86);
            this.lblRunTimes.Name = "lblRunTimes";
            this.lblRunTimes.Size = new System.Drawing.Size(29, 31);
            this.lblRunTimes.TabIndex = 4;
            this.lblRunTimes.Text = "0";
            // 
            // lblCurTime
            // 
            this.lblCurTime.AutoSize = true;
            this.lblCurTime.Location = new System.Drawing.Point(96, 9);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(34, 13);
            this.lblCurTime.TabIndex = 5;
            this.lblCurTime.Text = "00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(234, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Time Until Next Run:";
            // 
            // txtLogging
            // 
            this.txtLogging.Location = new System.Drawing.Point(12, 165);
            this.txtLogging.Multiline = true;
            this.txtLogging.Name = "txtLogging";
            this.txtLogging.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogging.Size = new System.Drawing.Size(390, 87);
            this.txtLogging.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(135, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Total Executions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Current Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(101, 262);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(30, 13);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "state";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // lnkLblHelp
            // 
            this.lnkLblHelp.AutoSize = true;
            this.lnkLblHelp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lnkLblHelp.Location = new System.Drawing.Point(375, 292);
            this.lnkLblHelp.Name = "lnkLblHelp";
            this.lnkLblHelp.Size = new System.Drawing.Size(29, 13);
            this.lnkLblHelp.TabIndex = 12;
            this.lnkLblHelp.TabStop = true;
            this.lnkLblHelp.Text = "Help";
            this.lnkLblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLblHelp_LinkClicked);
            // 
            // btnExportLogs
            // 
            this.btnExportLogs.Location = new System.Drawing.Point(327, 258);
            this.btnExportLogs.Name = "btnExportLogs";
            this.btnExportLogs.Size = new System.Drawing.Size(75, 23);
            this.btnExportLogs.TabIndex = 13;
            this.btnExportLogs.Text = "Export Log";
            this.btnExportLogs.UseVisualStyleBackColor = true;
            this.btnExportLogs.Click += new System.EventHandler(this.BtnExportLogs_Click);
            // 
            // lnkLblSettings
            // 
            this.lnkLblSettings.AutoSize = true;
            this.lnkLblSettings.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lnkLblSettings.Location = new System.Drawing.Point(324, 292);
            this.lnkLblSettings.Name = "lnkLblSettings";
            this.lnkLblSettings.Size = new System.Drawing.Size(45, 13);
            this.lnkLblSettings.TabIndex = 14;
            this.lnkLblSettings.TabStop = true;
            this.lnkLblSettings.Text = "Settings";
            this.lnkLblSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLblSettings_LinkClicked);
            // 
            // lblDatabaseExist
            // 
            this.lblDatabaseExist.AutoSize = true;
            this.lblDatabaseExist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseExist.ForeColor = System.Drawing.Color.Red;
            this.lblDatabaseExist.Location = new System.Drawing.Point(125, 35);
            this.lblDatabaseExist.Name = "lblDatabaseExist";
            this.lblDatabaseExist.Size = new System.Drawing.Size(170, 13);
            this.lblDatabaseExist.TabIndex = 15;
            this.lblDatabaseExist.Text = "DATABASE DOESN\'T EXIST";
            this.lblDatabaseExist.Visible = false;
            // 
            // WeatherCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 313);
            this.Controls.Add(this.lblDatabaseExist);
            this.Controls.Add(this.lnkLblSettings);
            this.Controls.Add(this.btnExportLogs);
            this.Controls.Add(this.lnkLblHelp);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLogging);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurTime);
            this.Controls.Add(this.lblRunTimes);
            this.Controls.Add(this.lblCountDown);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WeatherCollector";
            this.Text = "Weather Collector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label lblCountDown;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblRunTimes;
        private System.Windows.Forms.Label lblCurTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogging;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.LinkLabel lnkLblHelp;
        private System.Windows.Forms.Button btnExportLogs;
        private System.Windows.Forms.LinkLabel lnkLblSettings;
        private System.Windows.Forms.Label lblDatabaseExist;
    }
}

