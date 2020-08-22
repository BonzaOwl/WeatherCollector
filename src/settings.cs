using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class settings : Form
    {
        private const int V = 0;
        public settings()
        {
            InitializeComponent();
            LoadSettings();            

            //Hide the close buttons
            this.ControlBox = false;
        }

        private void LoadSettings()
        {
            txtServerName.Text = Properties.Settings.Default.ServerName;
            txtDatabaseName.Text = Properties.Settings.Default.DataBaseName;
            txtRefresh.Text = Properties.Settings.Default.RefreshInterval.ToString();
            txtWeatherAPI.Text = Properties.Settings.Default.weatherAPIKey;            
            lblLang.Text = Properties.Settings.Default.weatherUnits;
            txtLat.Text = Properties.Settings.Default.weatherLat;
            txtLong.Text = Properties.Settings.Default.weatherLong;
            txtDatabaseUsername.Text = Properties.Settings.Default.DatabaseUser;
            txtDatabasePassword.Text = Properties.Settings.Default.DatabasePassword;
            txtLogDirectory.Text = Properties.Settings.Default.LogDirectory;
            txtLogFile.Text = Properties.Settings.Default.LogFile;
            txtLogRoot.Text = Properties.Settings.Default.LogPath;

        }

        private void SaveSettings()
        {
            Properties.Settings.Default.ServerName = txtServerName.Text;
            Properties.Settings.Default.DataBaseName = txtDatabaseName.Text;
            Properties.Settings.Default.RefreshInterval = Convert.ToInt32(txtRefresh.Text);
            Properties.Settings.Default.weatherAPIKey = txtWeatherAPI.Text;
            Properties.Settings.Default.weatherLat = txtLat.Text;
            Properties.Settings.Default.weatherLong = txtLong.Text;
            Properties.Settings.Default.DatabaseUser = txtDatabaseUsername.Text;
            Properties.Settings.Default.DatabasePassword = txtDatabasePassword.Text;
            Properties.Settings.Default.LogDirectory = txtLogDirectory.Text;
            Properties.Settings.Default.LogFile = txtLogFile.Text;
            Properties.Settings.Default.LogPath = txtLogRoot.Text;

            //https://openweathermap.org/api/one-call-api#data
            int unitsCount = V;

            for (int i = 0; i < chkListUnits.Items.Count; i++)
            {
                if (chkListUnits.GetItemChecked(i))
                {
                    unitsCount += 1;
                }
            }

            if (unitsCount > 1)
            {
                MessageBox.Show("You can't select more than one unit", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string units = string.Empty;
                for (int i = 0; i < chkListUnits.Items.Count; i++)
                {
                    if (chkListUnits.GetItemChecked(i))
                    {
                        units += (string)chkListUnits.Items[i];
                    }
                }

                Properties.Settings.Default.weatherUnits = units;
            }

            int langCount = V;

            for (int i = 0; i < chkListLang.Items.Count; i++)
            {
                if (chkListLang.GetItemChecked(i))
                {
                    langCount += 1;
                }
            }

            if (langCount > 1)
            {
                MessageBox.Show("You can't select more than one language", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string lang = string.Empty;
                for (int i = 0; i < chkListLang.Items.Count; i++)
                {
                    if (chkListLang.GetItemChecked(i))
                    {
                        lang += (string)chkListLang.Items[i];
                    }
                }

                Properties.Settings.Default.weatherLanguage = lang;
            }
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Successfully Saved", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            new WeatherCollector().Show();
            this.Hide();
        }

        private void LnkMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://openweathermap.org/api/one-call-api#multi");
        }

        private void LnkLbl_Location_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.latlong.net");
        }
    }
}
