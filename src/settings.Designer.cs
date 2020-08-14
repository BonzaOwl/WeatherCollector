namespace WeatherCollectorDesktop
{
    partial class settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settings));
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtDatabaseUsername = new System.Windows.Forms.TextBox();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.txtRefresh = new System.Windows.Forms.TextBox();
            this.txtWeatherAPI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkLbl_Location = new System.Windows.Forms.LinkLabel();
            this.lblLang = new System.Windows.Forms.Label();
            this.chkListUnits = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLogDirectory = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLogRoot = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(23, 43);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(131, 20);
            this.txtServerName.TabIndex = 0;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(192, 43);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(131, 20);
            this.txtDatabaseName.TabIndex = 1;
            // 
            // txtDatabaseUsername
            // 
            this.txtDatabaseUsername.Location = new System.Drawing.Point(23, 92);
            this.txtDatabaseUsername.Name = "txtDatabaseUsername";
            this.txtDatabaseUsername.Size = new System.Drawing.Size(131, 20);
            this.txtDatabaseUsername.TabIndex = 2;
            // 
            // txtDatabasePassword
            // 
            this.txtDatabasePassword.Location = new System.Drawing.Point(192, 92);
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            this.txtDatabasePassword.Size = new System.Drawing.Size(131, 20);
            this.txtDatabasePassword.TabIndex = 3;
            // 
            // txtRefresh
            // 
            this.txtRefresh.Location = new System.Drawing.Point(17, 46);
            this.txtRefresh.Name = "txtRefresh";
            this.txtRefresh.Size = new System.Drawing.Size(131, 20);
            this.txtRefresh.TabIndex = 4;
            // 
            // txtWeatherAPI
            // 
            this.txtWeatherAPI.Location = new System.Drawing.Point(19, 46);
            this.txtWeatherAPI.Name = "txtWeatherAPI";
            this.txtWeatherAPI.Size = new System.Drawing.Size(277, 20);
            this.txtWeatherAPI.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "API Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Refresh Interval";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Database Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Database Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Server Name";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(408, 438);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(94, 23);
            this.btnSaveSettings.TabIndex = 12;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(582, 438);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 13;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkLbl_Location);
            this.groupBox1.Controls.Add(this.lblLang);
            this.groupBox1.Controls.Add(this.chkListUnits);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtLat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtLong);
            this.groupBox1.Controls.Add(this.txtWeatherAPI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 275);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "API Settings";
            // 
            // lnkLbl_Location
            // 
            this.lnkLbl_Location.AutoSize = true;
            this.lnkLbl_Location.Location = new System.Drawing.Point(16, 119);
            this.lnkLbl_Location.Name = "lnkLbl_Location";
            this.lnkLbl_Location.Size = new System.Drawing.Size(148, 13);
            this.lnkLbl_Location.TabIndex = 19;
            this.lnkLbl_Location.TabStop = true;
            this.lnkLbl_Location.Text = "Latitude and Longitude Finder";
            this.lnkLbl_Location.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl_Location_LinkClicked);
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.Location = new System.Drawing.Point(19, 247);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(35, 13);
            this.lblLang.TabIndex = 18;
            this.lblLang.Text = "label9";
            // 
            // chkListUnits
            // 
            this.chkListUnits.FormattingEnabled = true;
            this.chkListUnits.Items.AddRange(new object[] {
            "metric",
            "imperial"});
            this.chkListUnits.Location = new System.Drawing.Point(20, 143);
            this.chkListUnits.Name = "chkListUnits";
            this.chkListUnits.Size = new System.Drawing.Size(120, 94);
            this.chkListUnits.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Latitude ";
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(194, 89);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Longitude ";
            // 
            // txtLong
            // 
            this.txtLong.Location = new System.Drawing.Point(19, 89);
            this.txtLong.Name = "txtLong";
            this.txtLong.Size = new System.Drawing.Size(100, 20);
            this.txtLong.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtServerName);
            this.groupBox2.Controls.Add(this.txtDatabaseName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtDatabasePassword);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDatabaseUsername);
            this.groupBox2.Location = new System.Drawing.Point(16, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 137);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLogFile);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtLogDirectory);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtLogRoot);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(374, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 137);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log Location";
            // 
            // txtLogFile
            // 
            this.txtLogFile.Location = new System.Drawing.Point(17, 92);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(249, 20);
            this.txtLogFile.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "File Name";
            // 
            // txtLogDirectory
            // 
            this.txtLogDirectory.Location = new System.Drawing.Point(166, 43);
            this.txtLogDirectory.Name = "txtLogDirectory";
            this.txtLogDirectory.Size = new System.Drawing.Size(100, 20);
            this.txtLogDirectory.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Folder Name";
            // 
            // txtLogRoot
            // 
            this.txtLogRoot.Location = new System.Drawing.Point(17, 43);
            this.txtLogRoot.Name = "txtLogRoot";
            this.txtLogRoot.Size = new System.Drawing.Size(100, 20);
            this.txtLogRoot.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Root Location";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 453);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(325, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "This project uses the OpenWeather API to obtain the weather data.\r\n";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtRefresh);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(374, 170);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 99);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Application Settings";
            // 
            // settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 475);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.btnSaveSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "settings";
            this.Text = "Weather Collector - Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtDatabaseUsername;
        private System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.TextBox txtRefresh;
        private System.Windows.Forms.TextBox txtWeatherAPI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chkListUnits;
        private System.Windows.Forms.Label lblLang;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLogDirectory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLogRoot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel lnkLbl_Location;
    }
}