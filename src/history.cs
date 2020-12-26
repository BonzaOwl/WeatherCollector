using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class History : Form
    {
        int runID = 0;

        public History()
        {
            InitializeComponent();

            //Hide the close buttons
            this.ControlBox = false;

        }

        protected override void OnLoad(EventArgs e)
        {
            GetWeatherHistory();            
        }

        void GetWeatherHistory()
        {
            var ConnString = ConnectionStringBuilder.ConnectionString();

            using (SqlConnection conn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[getHistory]", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    //Set the SqlDataAdapter object
                    SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);                   


                    //define dataset
                    DataSet ds = new DataSet();

                    //fill dataset with query results
                    dAdapter.Fill(ds);

                    //set the DataGridView control's data source/data table
                    gvHistory.DataSource = ds.Tables[0];

                    gvHistory.Columns[0].Visible = false;
                    gvHistory.Columns[1].Visible = false; //Invalid
                    gvHistory.Columns[2].Visible = false; //Delete

                    //close connection
                    conn.Close();

                }
                catch (Exception ex)
                {
                    //display error message
                    string err = ex.Message.ToString();
                }
            }
        }

        void UpdateWeatherHistory(int runID, bool invalid, bool delete)
        {
            var ConnString = ConnectionStringBuilder.ConnectionString();

            using (SqlConnection conn = new SqlConnection(ConnString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[updateHistory]", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@runID", SqlDbType.Int);
                    cmd.Parameters["@runID"].Value = runID;

                    cmd.Parameters.Add("@invalid", SqlDbType.Bit);
                    cmd.Parameters["@invalid"].Value = invalid;

                    cmd.Parameters.Add("@delete", SqlDbType.Bit);
                    cmd.Parameters["@delete"].Value = delete;

                    cmd.ExecuteNonQuery();
                }

                catch
                {

                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            WeatherCollector wc = new WeatherCollector();
            this.Hide();
            wc.Show();
        }

        private void GvHistory_DoubleClick(object sender, EventArgs e)
        {
            runID = Convert.ToInt32(gvHistory.CurrentRow.Cells[0].Value);

            if(gvHistory.CurrentRow.Index != -1)
            {
                if (Convert.ToBoolean(gvHistory.CurrentRow.Cells[1].Value) == true)
                {
                    chkInvalid.Checked = true;
                }

                if (Convert.ToBoolean(gvHistory.CurrentRow.Cells[2].Value) == true)
                {
                    chkDelete.Checked = true;
                }

                btnUpdate.Visible = true;
            }            
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            GetWeatherHistory();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            runID = 0;
            chkDelete.Checked = false;
            chkInvalid.Checked = false;
            btnUpdate.Visible = false;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            bool invalid = chkInvalid.Checked;
            bool deleted = chkDelete.Checked;

            try
            {
                UpdateWeatherHistory(runID, invalid, deleted);
            }
            catch
            {

            }

            MessageBox.Show("Updated Sucessfully");

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

        private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WeatherCollector().Show();
            this.Hide();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new settings().Show();
            this.Hide();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string docs = Properties.Settings.Default.DocLink.ToString();
            _ = System.Diagnostics.Process.Start(docs);
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new about().Show();
            this.Hide();
        }
    }
}
