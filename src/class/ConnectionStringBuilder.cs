using System.Data.SqlClient;

namespace WeatherCollectorDesktop
{
    class ConnectionStringBuilder
    {
            public static string ConnectionString()
            {
                SqlConnectionStringBuilder ConnString = new SqlConnectionStringBuilder();

                var server = Properties.Settings.Default.ServerName;
                var databaseName = Properties.Settings.Default.DataBaseName;
                var userName = Properties.Settings.Default.DatabaseUser;
                var password = Properties.Settings.Default.DatabasePassword;

                ConnString.DataSource = server;
                ConnString.InitialCatalog = databaseName;
                ConnString.UserID = userName;
                ConnString.Password = password;
                ConnString.IntegratedSecurity = true;

                string ConnectionString = ConnString.ToString();

                return ConnectionString;

            }
    }
}
