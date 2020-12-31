using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class WeatherCollector : Form
    {
        private readonly int secondsToWait = Properties.Settings.Default.RefreshInterval;
        private DateTime startTime;
        private int runTimes;
        private bool databaseExists = false;

        private int appRunning;

        public WeatherCollector()
        {
            InitializeComponent();            
        }

        protected override async void OnLoad(EventArgs e)
        {
            await DatabaseCheck();            
            lblCurTime.Text = DateTime.Now.ToShortTimeString(); //Set the current time label to the initial value 

            if (appRunning == 0)
            {
                lblCountDown.Text = string.Empty;
            } else
            {
                lblCountDown.Text = secondsToWait.ToString(); //Set the count down label to the initial value 
            }

            lblRunTimes.Text = runTimes.ToString(); //Set the status label to the initial value             
            timer2.Start(); //Start the timer2

            if (databaseExists == true)
            {
                lblDatabaseExist.Visible = false;
                lblStatus.Text = "Stopped"; //Set the status label to the initial value 
            }

            lblTotalRunTimes.Visible = true;
            lblRunTimes.Visible = true;
            

            if (appRunning == 0)
            {
                btn_Stop.Visible = false;
            }            
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            string apiKey = Properties.Settings.Default.weatherAPIKey;
            if (apiKey.Length == 0 || apiKey == null)
            {
                MessageBox.Show("You need to specify a API key in the settings before starting collection", "No API Key Defined", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Application Started " + Environment.NewLine);
                timer1.Start(); // start timer (you can do it on form load, if you need)            
                startTime = DateTime.Now; // and remember start time
                WeatherRun(); //Start the collection from the API
                runTimes += 1; //Increment the amount of times this has run by 1
                lblRunTimes.Text = runTimes.ToString(); //Set the total number or run times to the label
                lblStatus.Text = "Running"; //If the start button has been clicked, we are running.

                trayIcon.BalloonTipText = "Weather Collection In Progress";

                appRunning = 1;

                if (appRunning == 1)
                {
                    btn_Stop.Visible = true;
                    btn_Start.Visible = false;
                }
            }
        }

        private void BtnRunNow_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            startTime = DateTime.Now;
            WeatherRun();
            runTimes += 1; //Increment the amount of times this has run by 1
            lblRunTimes.Text = runTimes.ToString(); //Set the total number or run times to the label
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            int remainingSeconds = secondsToWait - elapsedSeconds;

            if (remainingSeconds <= 0)
            {
                WeatherRun();
                startTime = DateTime.Now;
                remainingSeconds = secondsToWait;

                runTimes += 1; //Increment the amount of times this has run by 1
                lblRunTimes.Text = runTimes.ToString(); //Set the total number or run times to the label

            }

            lblCountDown.Text = String.Format("{0}", remainingSeconds);
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            lblCurTime.Text = DateTime.Now.ToLongTimeString();
        }

        private async Task DatabaseCheck()
        {
            //Connect to the database and check if the stored procedure exists, if we run into an error database doesn't exist or is not accessible.
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    await con.OpenAsync();

                    SqlCommand SaveRawWeatherData = new SqlCommand("[dbo].[SystemAlive]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveRawWeatherData.Parameters.Add("@databaseName", SqlDbType.VarChar, 100);
                    SaveRawWeatherData.Parameters["@databaseName"].Value = Properties.Settings.Default.DataBaseName;

                    SaveRawWeatherData.Parameters.Add("@rowCnt", SqlDbType.Int);
                    SaveRawWeatherData.Parameters["@rowCnt"].Direction = ParameterDirection.Output;                    

                    await SaveRawWeatherData.ExecuteNonQueryAsync();

                    lblDatabaseExist.Text = "Attempting to connect to the database";

                    int value = (int)SaveRawWeatherData.Parameters["@rowCnt"].Value;
                    int retunvalue = value;

                    if(retunvalue == 0)                    
                    {
                        lblDatabaseExist.Visible = true;
                    }

                    databaseExists = true;
                }

                catch (Exception ex)
                {
                    string error = ex.Message.ToString();
                    lblDatabaseExist.Visible = true;
                    lblDatabaseExist.Text = "Database connection failed";
                    lblTotalRunTimes.Visible = false;
                    lblRunTimes.Visible = false;                     
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                    btn_Start.Visible = false;
                    btnRunNow.Visible = false;
                    lblStatus.Text = "Cnfg Err";
                }
        }

        public void WeatherRun()
        {
            var runGuid = Guid.NewGuid(); //The primary identifier for the run

            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Starting Application with guid " + runGuid + Environment.NewLine);

            string apiKey = Properties.Settings.Default.weatherAPIKey;

            if(apiKey.Length == 0)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " It looks like the API Key has not been specified in the settings" + Environment.NewLine);
                return;
            }

            string cordsLong = Properties.Settings.Default.weatherLong;

            if(cordsLong == null)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " The logitude has not been defined. No Location Defined " + Environment.NewLine);                
                return;
            }

            string cordsLat = Properties.Settings.Default.weatherLat;

            if(cordsLat == null)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " The latitude has not been defined. No Location Defined " + Environment.NewLine);                
                return;
            }

            string units = Properties.Settings.Default.weatherUnits;

            if(units.Length == 0)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " No units have been specified, defaulting to metric " + Environment.NewLine);
                units = "metric";
            }

            string language = Properties.Settings.Default.weatherLanguage;

            if(language == null)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " The language that you want the results returned in has not been specified, defaulting to en " + Environment.NewLine);
                language = "en";
            }            
            
            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Fetching URL based on application settings" + Environment.NewLine);
            string forecastUrl = ConstructRequestUrl.ForecastUrl(apiKey, cordsLat, cordsLong, units, language);

            int runID = GetRunID.LatestRunID();

            txtRunIDCnt.Text = runID.ToString();

            string content = GetWeatherData(runID, runGuid, forecastUrl);

            ProcessWeatherJson(runID, runGuid, content);
            
        }

        private string GetWeatherData(int runID, Guid runGuid, string forecastUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(forecastUrl);
            request.Timeout = 600000;
            request.ContentType = "application/json; charset=utf-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;            
            var content = string.Empty;

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);                
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Raw Weather Json now being requested from the API" + Environment.NewLine);
                content = reader.ReadToEnd();
                                
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Raw Weather data being saved to the database this will allow us to review it later" + Environment.NewLine);
                SaveRawWeatherData(runID, runGuid, content);
            }

            return content;
        }

        private void ProcessWeatherJson(int runID, Guid runGuid, string content)
        {
            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving received values to the database" + Environment.NewLine);

            //Deserialize the json from the content string passed
            dynamic forecastJson = GetDeserializedData(content);

            //Convert the content string into JsonObjects
            var jsonObj = JsonConvert.DeserializeObject<RootObject>(content);

            var weatherObj = jsonObj.Weather;

            if (weatherObj != null)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Weather data present" + Environment.NewLine);

                foreach (var obj in weatherObj)
                {
                    try
                    {
                        int id = obj.Id;
                        string main = obj.Main;
                        string description = obj.Description;
                        string icon = obj.Icon;
                        int iconID = GetIconID.Iconid(icon, description);

                        SaveWeatherData(runID, runGuid, id, main, description, iconID);

                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Saving alert data to the database" + Environment.NewLine);

                    }
                    catch (Exception ex)
                    {
                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                        txtLogging.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            float? longitude = forecastJson.lon;
            float? latitude = forecastJson.lat;

            //Get the forecasted time from the JSON
            long time = forecastJson.current.dt;
            DateTime runTime = FromUnixTime.Convert(time);

            string timeZone = forecastJson.timezone;
            string timeZoneOffset = forecastJson.timezone_offset;

            string units = Properties.Settings.Default.weatherUnits;

            SaveRunData(runID, runGuid, runTime, longitude, latitude, timeZone, timeZoneOffset, units);

            long sunrise = forecastJson.current.sunrise;
            DateTime dtSunrise = FromUnixTime.Convert(sunrise);

            long sunset = forecastJson.current.sunset;
            DateTime dtSunset = FromUnixTime.Convert(sunset);

            //Rain might not always be populated, if it isn't we need to skip it
            decimal? rain = null;

            if (forecastJson["current"]["rain"] != null)
            {
                rain = forecastJson["rain"]["1h"];
            }

            decimal? snow = null;

            if (forecastJson.current.snow != null)
            {
                snow = forecastJson.current.snow;
            }            


            decimal? temperature = null;

            if (forecastJson.current.temp != null)
            {
                temperature = forecastJson.current.temp;
            }

            decimal? apparentTemperature = null;

            if(forecastJson.current.feels_like != null)
            {
                apparentTemperature = forecastJson.current.feels_like;
            }

            decimal? windSpeed = forecastJson.current.wind_speed;

            if(forecastJson.current.wind_speed != null)
            {
                windSpeed = forecastJson.current.wind_speed;
            }

            decimal? windGust = forecastJson.current.wind_gust;

            if(forecastJson.current.wind_gust != null)
            {
                windGust = forecastJson.current.wind_gust;
            }

            decimal? windBearing = null;

            if(forecastJson.current.wind_deg != null)
            {
                windBearing = forecastJson.current.wind_deg;
            }

            decimal? dewPoint = null;
            
            if(forecastJson.current.dew_point != null)
            {
                dewPoint = forecastJson.current.dew_point;
            }

            decimal? humidity = null;

            if(forecastJson.current.humidity != null)
            {
                humidity = forecastJson.current.humidity;
            }

            decimal? pressure = null;

            if(forecastJson.current.pressure != null)
            {
                pressure = forecastJson.current.pressure;
            }

            decimal? cloudCover = null;

            if(forecastJson.current.clouds != null)
            {
                cloudCover = forecastJson.current.clouds;
            }

            decimal? uvIndex = null;

            if(forecastJson.current.uvi != null)
            {
                uvIndex = forecastJson.current.uvi;
            }

            decimal? visibility = null;

            if(forecastJson.current.visibility != null)
            {
                visibility = forecastJson.current.visibility;
            }

            decimal? ozone = null;

            if(forecastJson.current.ozone != null)
            {
                ozone = forecastJson.current.ozone;
            }

            SaveCurrentlyData(runID, dtSunrise, dtSunset, runGuid, snow ,rain, temperature, apparentTemperature, windSpeed, windGust, windBearing, dewPoint, humidity, pressure, cloudCover, uvIndex, visibility, ozone);
        }        

        private void SaveRunData(int runID, Guid runGuid, DateTime runTime, float? longitude, float? latitude, string timeZone, string timeZoneOffset, string units)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveRunData = new SqlCommand("[dbo].[SaveRunData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //SaveRunData.Parameters.Add("@units", SqlDbType.VarChar, 10);
                    //if (units != null)
                    //{
                    //    SaveRunData.Parameters["@units"].Value = units;
                    //}
                    //else
                    //{
                    //    SaveRunData.Parameters["@units"].Value = DBNull.Value;
                    //}

                    SaveRunData.Parameters.Add("@runID", SqlDbType.Int);
                    SaveRunData.Parameters["@runID"].Value = runID;

                    SaveRunData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveRunData.Parameters["@runGuid"].Value = runGuid;

                    SaveRunData.Parameters.Add("@runTime", SqlDbType.DateTime);
                    SaveRunData.Parameters["@runTime"].Value = runTime;

                    SaveRunData.Parameters.Add("@longitude", SqlDbType.Float);
                    SaveRunData.Parameters["@longitude"].Value = longitude;

                    SaveRunData.Parameters.Add("@latitude", SqlDbType.Float);
                    SaveRunData.Parameters["@latitude"].Value = latitude;

                    SaveRunData.Parameters.Add("@timeZone", SqlDbType.NVarChar,200);
                    SaveRunData.Parameters["@timeZone"].Value = timeZone;

                    SaveRunData.Parameters.Add("@timeZoneOffset", SqlDbType.NVarChar,200);
                    SaveRunData.Parameters["@timeZoneOffset"].Value = timeZoneOffset;

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save weather run data to the database" + Environment.NewLine);
                    SaveRunData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                }

        }

        private void SaveWeatherData(int runID, Guid runGuid, int CollectionID, string summary, string description, int icon)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveWeatherData = new SqlCommand("[dbo].[SaveWeatherData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveWeatherData.Parameters.Add("@runID", SqlDbType.Int);
                    SaveWeatherData.Parameters["@runID"].Value = runID;

                    SaveWeatherData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveWeatherData.Parameters["@runGuid"].Value = runGuid;                    

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save weather run data to the database" + Environment.NewLine);
                    SaveWeatherData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                }

        }

        private dynamic GetDeserializedData(string content)
        {
            dynamic forecastJson;

            Console.ForegroundColor = ConsoleColor.Green;
            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Json Weather Data is now being Deserialized" + Environment.NewLine);
            forecastJson = JsonConvert.DeserializeObject(content);

            return forecastJson;

        }

        private void SaveRawWeatherData(int runID,Guid runGuid, string content)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveRawWeatherData = new SqlCommand("[dbo].[SaveRawWeatherData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveRawWeatherData.Parameters.Add("@runID", SqlDbType.Int);
                    SaveRawWeatherData.Parameters["@runID"].Value = runID;

                    SaveRawWeatherData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveRawWeatherData.Parameters["@runGuid"].Value = runGuid;

                    SaveRawWeatherData.Parameters.Add("@rawData", SqlDbType.NVarChar, -1);
                    SaveRawWeatherData.Parameters["@rawData"].Value = content;

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save raw weather data to the database" + Environment.NewLine);
                    SaveRawWeatherData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                }
        }        

        private void SaveCurrentlyData(int runID, DateTime sunrise, DateTime sunset, Guid runGuid, Decimal? snow, Decimal? rain, Decimal? temperature, Decimal? apparentTemperature, Decimal? windSpeed, Decimal? windGust, Decimal? windBearing, Decimal? dewPoint, Decimal? humidity, Decimal? pressure, Decimal? cloudCover, Decimal? uvIndex, Decimal? visibility, Decimal? ozone)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveWeatherData = new SqlCommand("[dbo].[SaveWeatherData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveWeatherData.Parameters.Add("@runID", SqlDbType.Int);
                    SaveWeatherData.Parameters["@runID"].Value = runID;

                    SaveWeatherData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveWeatherData.Parameters["@runGuid"].Value = runGuid;

                    SaveWeatherData.Parameters.Add("@sunrise", SqlDbType.DateTime);

                    if (sunrise != null)
                    {
                        SaveWeatherData.Parameters["@sunrise"].Value = sunrise;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@sunrise"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@sunset", SqlDbType.DateTime);

                    if (sunset != null)
                    {
                        SaveWeatherData.Parameters["@sunset"].Value = sunset;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@sunset"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@rain", SqlDbType.Decimal);

                    if (rain != null)
                    {                      
                        SaveWeatherData.Parameters["@rain"].Value = rain;
                    } else
                    {                        
                        SaveWeatherData.Parameters["@rain"].Value = DBNull.Value;
                    }                                                       

                    if (temperature != null)
                    {
                        SaveWeatherData.Parameters.Add("@temperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@temperature"].Value = temperature;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@temperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@temperature"].Value = DBNull.Value;
                    }

                    if (apparentTemperature != null)
                    {
                        SaveWeatherData.Parameters.Add("@apparentTemperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@apparentTemperature"].Value = apparentTemperature;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@apparentTemperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@apparentTemperature"].Value = DBNull.Value;
                    }

                    if (windSpeed != null)

                    {
                        SaveWeatherData.Parameters.Add("@windSpeed", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windSpeed"].Value = windSpeed;
                    }
                    else

                    {
                        SaveWeatherData.Parameters.Add("@windSpeed", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windSpeed"].Value = DBNull.Value;
                    }

                    if (windGust != null)
                    {
                        SaveWeatherData.Parameters.Add("@windGust", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windGust"].Value = windGust;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@windGust", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windGust"].Value = DBNull.Value;
                    }

                    if (windBearing != null)
                    {
                        SaveWeatherData.Parameters.Add("@windBearing", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windBearing"].Value = windBearing;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@windBearing", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windBearing"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@dewPoint", SqlDbType.Decimal);
                    if (dewPoint != null)
                    {
                        SaveWeatherData.Parameters["@dewPoint"].Value = dewPoint;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@dewPoint"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@humidity", SqlDbType.Decimal);
                    if (humidity != null)
                    {
                        SaveWeatherData.Parameters["@humidity"].Value = humidity;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@humidity"].Value = DBNull.Value;

                    }

                    SaveWeatherData.Parameters.Add("@pressure", SqlDbType.Decimal);
                    if (pressure != null)
                    {
                        SaveWeatherData.Parameters["@pressure"].Value = pressure;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@pressure"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@cloudCover", SqlDbType.Decimal);
                    if (cloudCover != null)
                    {
                        SaveWeatherData.Parameters["@cloudCover"].Value = cloudCover + 'M';
                        //SaveWeatherData.Parameters["@cloudCover"].Value = DBNull.Value;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@cloudCover"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@uvIndex", SqlDbType.Decimal);
                    if (uvIndex != null)
                    {
                        SaveWeatherData.Parameters["@uvIndex"].Value = uvIndex;
                    } else
                    {
                        SaveWeatherData.Parameters["@uvIndex"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@visibility", SqlDbType.Decimal);
                    if (visibility != null)
                    {
                        SaveWeatherData.Parameters["@visibility"].Value = visibility;
                        //SaveWeatherData.Parameters["@visibility"].Value = DBNull.Value;
                    } else
                    {
                        SaveWeatherData.Parameters["@visibility"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@ozone", SqlDbType.Decimal);
                    if (ozone != null)
                    {
                        SaveWeatherData.Parameters["@ozone"].Value = ozone;
                    } else
                    {
                        SaveWeatherData.Parameters["@ozone"].Value = DBNull.Value;
                    }

                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Attempting to save data to the database" + Environment.NewLine);
                    SaveWeatherData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Saving to the database was sucessful" + Environment.NewLine);
                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Application Stopped" + Environment.NewLine);
            lblStatus.Text = "Stopped";

            trayIcon.BalloonTipText = string.Empty;

            appRunning = 0;

            lblCountDown.Text = string.Empty;

            btn_Start.Visible = true;
            btn_Stop.Visible = false;
        }

        private void BtnExportLogs_Click(object sender, EventArgs e)
        {
            int logLength = txtLogging.Text.Length;

            if (logLength != 0)
            {

                string fileRoot = Properties.Settings.Default.LogPath;

                if (fileRoot.Length == 0)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " File Root Isn't Set, this must be configured in the settings before exporting logs." + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string fileDir = Properties.Settings.Default.LogDirectory;

                if (fileDir.Length == 0)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Folder Name Isn't Set, this must be configured in the settings before exporting logs." + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string fileName = "weathercollector" + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".log";

                if (fileName.Length == 0)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Filename Isn't Set, this must be configured in the settings before exporting logs." + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string filePath = fileRoot + "\\" + fileDir + "\\" + fileName;

                try
                {
                    if (Directory.Exists(fileRoot + fileDir))
                    {
                        File.WriteAllText(filePath, txtLogging.Text);
                        MessageBox.Show("Logs successfully exported", "Logs Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Logs successfully exported." + Environment.NewLine);
                        txtLogging.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(fileRoot + fileDir);

                            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Directory doesn't exist." + Environment.NewLine);
                            txtLogging.ForeColor = System.Drawing.Color.Red;

                            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Directory sucesssfully created." + Environment.NewLine);
                            txtLogging.ForeColor = System.Drawing.Color.Green;

                            File.WriteAllText(filePath, txtLogging.Text);
                            MessageBox.Show("Logs successfully exported", "Logs Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch
                        {
                            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Error exporting logs to file." + Environment.NewLine);
                            txtLogging.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " " + ex.Message.ToString() + Environment.NewLine);
                    txtLogging.ForeColor = System.Drawing.Color.Red;
                }
            } else
            {
                txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Currently no log data to export." + Environment.NewLine);
                txtLogging.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void WeatherCollector_Resize(object sender, EventArgs e)
        {
            if(FormWindowState.Minimized == this.WindowState)
            {                
                trayIcon.ShowBalloonTip(500);
                this.Hide();
            }             
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            trayIcon.Visible = false;
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

        private void WeatherCollector_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseCancel.ConfirmCloseCancel() == true)
            {
                e.Cancel = false;
                
                //Remove the tray icon from view and dispose of it 
                trayIcon.Icon.Dispose();
                //Dispose of the tray icon object 
                trayIcon.Dispose();

            } else
            {
                e.Cancel = true;
            }
        }

        private void BtnLatestJSON_Click(object sender, EventArgs e)
        {
            new CurrentData().Show();
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
