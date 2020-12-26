﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class CurrentData : Form
    {
        public CurrentData()
        {
            InitializeComponent();
            GetCurrentData();
        }

        private void GetCurrentData()
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand GetRunIDCommand = new SqlCommand("[dbo].[GetCurrentData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader dr = GetRunIDCommand.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        txtCurrentData.Text = dt.Rows[0]["CurrentData"].ToString();
                    }
                }

                catch (Exception ex)
                {
                    _ = ex.Message.ToString();
                }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtCurrentData.Text = string.Empty;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            
            new WeatherCollector().Show();
            this.Hide();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCurrentData.Text);
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

        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new History().Show();
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
