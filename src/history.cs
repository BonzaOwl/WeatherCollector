﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class history : Form
    {
        int runID = 0;

        public history()
        {
            InitializeComponent();
            
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
                    gvHistory.Columns[1].Visible = false; //GUID
                    gvHistory.Columns[2].Visible = false; //Invalid
                    gvHistory.Columns[3].Visible = false; //Delete

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            WeatherCollector wc = new WeatherCollector();
            Hide();
            wc.Show();
        }

        private void gvHistory_DoubleClick(object sender, EventArgs e)
        {
            runID = Convert.ToInt32(gvHistory.CurrentRow.Cells[0].Value);

            if(gvHistory.CurrentRow.Index != -1)
            {
                if (Convert.ToBoolean(gvHistory.CurrentRow.Cells[2].Value) == true)
                {
                    chkInvalid.Checked = true;
                }

                if (Convert.ToBoolean(gvHistory.CurrentRow.Cells[3].Value) == true)
                {
                    chkDelete.Checked = true;
                }

                btnUpdate.Visible = true;
            }            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetWeatherHistory();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            runID = 0;
            chkDelete.Checked = false;
            chkInvalid.Checked = false;
            btnUpdate.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
    }
}
