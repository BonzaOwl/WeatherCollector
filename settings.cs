using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DarkSkyCollectorDesktop
{
    public partial class settings : Form
    {
        private const int V = 0;
        public settings()
        {
            InitializeComponent();
            LoadSettings();

            //I would like the label to be empty, thank you!
            lblDeleteResponse.Text = string.Empty;
        }

        private void LoadSettings()
        {
            txtServerName.Text = Properties.Settings.Default.ServerName;
            txtDatabaseName.Text = Properties.Settings.Default.DataBaseName;
            txtRefresh.Text = Properties.Settings.Default.RefreshInterval.ToString();
            txtDarkSkyAPI.Text = Properties.Settings.Default.DarkSkyAPIKey;
            lblExclude.Text = Properties.Settings.Default.DarkSkyExclusions;
            lblLang.Text = Properties.Settings.Default.DarkSkyUnits;
            txtLat.Text = Properties.Settings.Default.DarkSkyLat;
            txtLong.Text = Properties.Settings.Default.DarkSkyLong;
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
            Properties.Settings.Default.DarkSkyAPIKey = txtDarkSkyAPI.Text;
            Properties.Settings.Default.DarkSkyLat = txtLat.Text;
            Properties.Settings.Default.DarkSkyLong = txtLong.Text;
            Properties.Settings.Default.DatabaseUser = txtDatabaseUsername.Text;
            Properties.Settings.Default.DatabasePassword = txtDatabasePassword.Text;
            Properties.Settings.Default.LogDirectory = txtLogDirectory.Text;
            Properties.Settings.Default.LogFile = txtLogFile.Text;
            Properties.Settings.Default.LogPath = txtLogRoot.Text;

            int unitsCount = V;

            for (int i = 0; i < chkListUnits.Items.Count; i++)
            {
                if (chkListUnits.GetItemChecked(i))
                {
                    unitsCount += 1;
                }
            }

            if(unitsCount > 1)
            {
                MessageBox.Show("You can't select more than one language", "Validation Error",MessageBoxButtons.OK, MessageBoxIcon.Error);                
            } else
            {
                string units = "units=";
                for (int i = 0; i < chkListUnits.Items.Count; i++)
                {
                    if (chkListUnits.GetItemChecked(i))
                    {
                        units += (string)chkListUnits.Items[i];
                    }
                }

                Properties.Settings.Default.DarkSkyUnits = units;
            }

            int exclusionCount = 0;

            for (int i = 0; i < chkListExclusions.Items.Count; i++)
            {
                if (chkListExclusions.GetItemChecked(i))
                {
                    exclusionCount += 1;
                }
            }

            var exclusions = "exclude=";
            if (exclusionCount == 0)
            {
                exclusions = string.Empty;
            }
            else
            {

                for (int i = 0; i < chkListExclusions.Items.Count; i++)
                {
                    string exclusionName;
                    if (chkListExclusions.GetItemChecked(i))
                    {
                        exclusionName = (string)chkListExclusions.Items[i];

                        exclusions += (string)chkListExclusions.Items[i] + ",";
                    }
                    else
                    {
                        exclusionName = (string)chkListExclusions.Items[i];
                    }
                }

                //Remove the last comma to avoid the request being rejected
                exclusions = exclusions.Remove(exclusions.Length - 1, 1);
            }

            Properties.Settings.Default.DarkSkyExclusions = exclusions;

        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Successfully Saved", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            new DarkSkyCollector().Show();
            this.Hide();
        }

        private void BtnClearDb_Click(object sender, EventArgs e)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveRawWeatherData = new SqlCommand("[dbo].[ClearAllWeatherData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveRawWeatherData.ExecuteNonQuery();

                    lblDeleteResponse.Text = "Data Deleted Successfully";

                }

                catch (Exception ex)
                {
                    var err = ex.Message.ToString();

                    lblDeleteResponse.Text = "Data not deleted, something went wrong";

                }
        }
    }
}
