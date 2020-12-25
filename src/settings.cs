using System;
using System.Data;
using System.Data.SqlClient;
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
            txtLat.Text = Properties.Settings.Default.weatherLat;
            txtLong.Text = Properties.Settings.Default.weatherLong;
            txtDatabaseUsername.Text = Properties.Settings.Default.DatabaseUser;
            txtDatabasePassword.Text = Properties.Settings.Default.DatabasePassword;
            txtLogDirectory.Text = Properties.Settings.Default.LogDirectory;            
            txtLogRoot.Text = Properties.Settings.Default.LogPath;

            GetSetLanguage();
            GetSetUnit();
        }

        //Get the language that the user has set to sent to the api and ensure that the check box is selected
        private void GetSetLanguage()
        {
            //Current language
            string curLang = Properties.Settings.Default.weatherLanguage;

            //Get all the items in the CheckBoxList
            for (int i = 0; i < chkListLang.Items.Count; i++)
            {
                //Get the currect item from the loop
                var chkItem = chkListLang.Items[i].ToString();

                //If the currentlanguage matches the items from the loop
                if (curLang == chkItem)
                {
                    //Get the language checkbox to checked
                    chkListLang.SetItemChecked(i,true);            
                }
            }
        }

        //Get the unit that the user has set to send to the api and ensure that the check box is selected
        private void GetSetUnit()
        {
            string curUnit = Properties.Settings.Default.weatherUnits;

            for (int i = 0; i < chkListUnits.Items.Count; i++)
            {
                var chkItem = chkListUnits.Items[i].ToString();

                if (curUnit == chkItem)
                {
                    chkListUnits.SetItemChecked(i, true);
                }
            }
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

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseCancel.ConfirmCloseCancel() == true)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnClearDatabase_Click(object sender, EventArgs e)
        {
            if(ConfirmDatabaseClear() == true)
            {
                ClearDatabase();
            }
        }

        public static bool ConfirmDatabaseClear()
        {
            const string message = "Are you sure you want to clear down the database? This isn't reversable unless you have backups";
            const string caption = "Confirm Database Cleardown";

            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearDatabase()
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand GetRunIDCommand = new SqlCommand("[dbo].[ClearDownDatabase]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    GetRunIDCommand.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    _ = ex.Message.ToString();
                }            
        }
    }    
}
