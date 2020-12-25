using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WeatherCollectorDesktop
{
    class GetRunID
    {
        public static int LatestRunID()
        {

            int RunID = 0;

            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand GetRunIDCommand = new SqlCommand("[dbo].[GetRunID]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };                    

                    SqlDataReader dr = GetRunIDCommand.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        RunID = Convert.ToInt32(dt.Rows[0]["RunID"]);
                    }
                }

                catch (Exception ex)
                {
                    _ = ex.Message.ToString();
                }

            return RunID;
        }
    }
}

