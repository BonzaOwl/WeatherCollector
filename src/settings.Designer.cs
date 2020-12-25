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
            this.LblLanguageHead = new System.Windows.Forms.Label();
            this.LblUnitsHead = new System.Windows.Forms.Label();
            this.LnkMoreInfo = new System.Windows.Forms.LinkLabel();
            this.chkListLang = new System.Windows.Forms.CheckedListBox();
            this.lnkLbl_Location = new System.Windows.Forms.LinkLabel();
            this.chkListUnits = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLogDirectory = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLogRoot = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblRefreshInfo = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClearDatabase = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "API Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Refresh Interval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(192, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Database Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Database User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(195, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Database Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Server Name";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(509, 475);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(94, 23);
            this.btnSaveSettings.TabIndex = 12;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(609, 475);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 13;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LblLanguageHead);
            this.groupBox1.Controls.Add(this.LblUnitsHead);
            this.groupBox1.Controls.Add(this.LnkMoreInfo);
            this.groupBox1.Controls.Add(this.chkListLang);
            this.groupBox1.Controls.Add(this.lnkLbl_Location);
            this.groupBox1.Controls.Add(this.chkListUnits);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtLat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtLong);
            this.groupBox1.Controls.Add(this.txtWeatherAPI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 308);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "API Settings";
            // 
            // LblLanguageHead
            // 
            this.LblLanguageHead.AutoSize = true;
            this.LblLanguageHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLanguageHead.Location = new System.Drawing.Point(162, 162);
            this.LblLanguageHead.Name = "LblLanguageHead";
            this.LblLanguageHead.Size = new System.Drawing.Size(63, 13);
            this.LblLanguageHead.TabIndex = 23;
            this.LblLanguageHead.Text = "Language";
            // 
            // LblUnitsHead
            // 
            this.LblUnitsHead.AutoSize = true;
            this.LblUnitsHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblUnitsHead.Location = new System.Drawing.Point(19, 162);
            this.LblUnitsHead.Name = "LblUnitsHead";
            this.LblUnitsHead.Size = new System.Drawing.Size(36, 13);
            this.LblUnitsHead.TabIndex = 22;
            this.LblUnitsHead.Text = "Units";
            // 
            // LnkMoreInfo
            // 
            this.LnkMoreInfo.AutoSize = true;
            this.LnkMoreInfo.Location = new System.Drawing.Point(159, 283);
            this.LnkMoreInfo.Name = "LnkMoreInfo";
            this.LnkMoreInfo.Size = new System.Drawing.Size(71, 13);
            this.LnkMoreInfo.TabIndex = 21;
            this.LnkMoreInfo.TabStop = true;
            this.LnkMoreInfo.Text = "Find out more";
            this.LnkMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkMoreInfo_LinkClicked);
            // 
            // chkListLang
            // 
            this.chkListLang.FormattingEnabled = true;
            this.chkListLang.Items.AddRange(new object[] {
            "af \t",
            "al \t",
            "ar ",
            "az",
            "bg",
            "ca",
            "cz",
            "da",
            "de",
            "el",
            "en",
            "eu",
            "fa",
            "fi",
            "fr",
            "gl",
            "he",
            "hi",
            "hr",
            "hu",
            "id",
            "it",
            "ja",
            "kr",
            "la",
            "lt",
            "mk",
            "no",
            "nl",
            "pl",
            "pt",
            "pt_br",
            "ro",
            "ru",
            "sv ",
            "se \t",
            "sk",
            "sl",
            "sp ",
            "es \t",
            "sr",
            "th",
            "tr",
            "ua",
            "uk \t\t",
            "vi",
            "zh_cn",
            "zh_tw",
            "zu"});
            this.chkListLang.Location = new System.Drawing.Point(162, 181);
            this.chkListLang.Name = "chkListLang";
            this.chkListLang.Size = new System.Drawing.Size(120, 94);
            this.chkListLang.TabIndex = 20;
            // 
            // lnkLbl_Location
            // 
            this.lnkLbl_Location.AutoSize = true;
            this.lnkLbl_Location.Location = new System.Drawing.Point(16, 133);
            this.lnkLbl_Location.Name = "lnkLbl_Location";
            this.lnkLbl_Location.Size = new System.Drawing.Size(148, 13);
            this.lnkLbl_Location.TabIndex = 19;
            this.lnkLbl_Location.TabStop = true;
            this.lnkLbl_Location.Text = "Latitude and Longitude Finder";
            this.lnkLbl_Location.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLbl_Location_LinkClicked);
            // 
            // chkListUnits
            // 
            this.chkListUnits.FormattingEnabled = true;
            this.chkListUnits.Items.AddRange(new object[] {
            "standard",
            "metric",
            "imperial"});
            this.chkListUnits.Location = new System.Drawing.Point(20, 181);
            this.chkListUnits.Name = "chkListUnits";
            this.chkListUnits.Size = new System.Drawing.Size(120, 94);
            this.chkListUnits.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(194, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Latitude ";
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(194, 103);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Longitude ";
            // 
            // txtLong
            // 
            this.txtLong.Location = new System.Drawing.Point(19, 103);
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
            this.groupBox3.Controls.Add(this.txtLogDirectory);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtLogRoot);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(374, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 137);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log Location";
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
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(169, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
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
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Root Location";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 485);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(325, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "This project uses the OpenWeather API to obtain the weather data.\r\n";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblRefreshInfo);
            this.groupBox4.Controls.Add(this.txtRefresh);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(374, 322);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 99);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Application Settings";
            // 
            // lblRefreshInfo
            // 
            this.lblRefreshInfo.AutoSize = true;
            this.lblRefreshInfo.Location = new System.Drawing.Point(20, 71);
            this.lblRefreshInfo.Name = "lblRefreshInfo";
            this.lblRefreshInfo.Size = new System.Drawing.Size(63, 13);
            this.lblRefreshInfo.TabIndex = 8;
            this.lblRefreshInfo.Text = "milliseconds";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClearDatabase);
            this.groupBox5.Location = new System.Drawing.Point(374, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(310, 137);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Database Maintenence";
            // 
            // btnClearDatabase
            // 
            this.btnClearDatabase.BackColor = System.Drawing.Color.Red;
            this.btnClearDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDatabase.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClearDatabase.Location = new System.Drawing.Point(54, 43);
            this.btnClearDatabase.Name = "btnClearDatabase";
            this.btnClearDatabase.Size = new System.Drawing.Size(212, 50);
            this.btnClearDatabase.TabIndex = 0;
            this.btnClearDatabase.Text = "Clear Down Database";
            this.btnClearDatabase.UseVisualStyleBackColor = false;
            this.btnClearDatabase.Click += new System.EventHandler(this.btnClearDatabase_Click);
            // 
            // settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 506);
            this.Controls.Add(this.groupBox5);
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
            this.Load += new System.EventHandler(this.settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLogDirectory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLogRoot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel lnkLbl_Location;
        private System.Windows.Forms.Label lblRefreshInfo;
        private System.Windows.Forms.LinkLabel LnkMoreInfo;
        private System.Windows.Forms.CheckedListBox chkListLang;
        private System.Windows.Forms.Label LblLanguageHead;
        private System.Windows.Forms.Label LblUnitsHead;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClearDatabase;
    }
}