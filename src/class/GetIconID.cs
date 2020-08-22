using System;
using System.Data;
using System.Data.SqlClient;


namespace WeatherCollectorDesktop
{
    class GetIconID
    {       

        public static int Iconid(string icon, string description)
        {

            int ID = 0;

            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand GetIconIDCommand = new SqlCommand("[dbo].[getIconID]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    GetIconIDCommand.Parameters.Add("@icon", SqlDbType.Char,3);
                    GetIconIDCommand.Parameters["@icon"].Value = icon;

                    GetIconIDCommand.Parameters.Add("@description", SqlDbType.VarChar,100);
                    GetIconIDCommand.Parameters["@description"].Value = description;

                    SqlDataReader dr = GetIconIDCommand.ExecuteReader();

                    DataTable dt = new DataTable();

                    dt.Load(dr);

                    if(dt.Rows.Count > 0)
                    {
                        ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    }
                }

                catch (Exception ex)
                {
                    _ = ex.Message.ToString();
                }

            return ID;
        }
    }
}

