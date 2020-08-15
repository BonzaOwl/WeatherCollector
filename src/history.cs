using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class history : Form
    {
        public history()
        {
            InitializeComponent();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            GetWeatherHistory();            
        }


        private void GetWeatherHistory()
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            WeatherCollector wc = new WeatherCollector();
            Hide();
            wc.Show();
        }
    }
}
