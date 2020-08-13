using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class WeatherCollector : Form
    {
        private readonly int secondsToWait = Properties.Settings.Default.RefreshInterval;
        private DateTime startTime;
        private int runTimes;

        private int appRunning;

        public WeatherCollector()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            DatabaseCheck();
            lblCurTime.Text = DateTime.Now.ToShortTimeString(); //Set the current time label to the initial value 
            lblCountDown.Text = secondsToWait.ToString(); //Set the count down label to the initial value 
            lblRunTimes.Text = runTimes.ToString(); //Set the status label to the initial value 
            lblStatus.Text = "Stopped"; //Set the status label to the initial value 
            timer2.Start(); //Start the timer2

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

                appRunning = 1;

                if (appRunning == 1)
                {
                    btn_Stop.Visible = true;
                    btn_Start.Visible = false;
                }
            }
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

        private void DatabaseCheck()
        {
            //Connect to the database and check if the stored procedure exists, if we run into an error database doesn't exist or is not accessible.
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveRawWeatherData = new SqlCommand("[dbo].[SystemAlive]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveRawWeatherData.Parameters.Add("@databaseName", SqlDbType.VarChar, 100);
                    SaveRawWeatherData.Parameters["@databaseName"].Value = Properties.Settings.Default.DataBaseName;

                    SaveRawWeatherData.Parameters.Add("@rowCnt", SqlDbType.Int);
                    SaveRawWeatherData.Parameters["@rowCnt"].Direction = ParameterDirection.Output;                    

                    SaveRawWeatherData.ExecuteNonQuery();

                    int retunvalue = (int)SaveRawWeatherData.Parameters["@rowCnt"].Value;

                    if(retunvalue == 0)                    
                    {
                        lblDatabaseExist.Visible = true;
                    }                    
                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + ex.Message.ToString() + Environment.NewLine);
                    lblDatabaseExist.Visible = true;
                    lblDatabaseExist.Text = "DATABASE DOESN'T EXIST";
                }
        }

        public void WeatherRun()
        {
            var runGuid = Guid.NewGuid(); //The primary identifier for the run

            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Starting Application with guid " + runGuid + Environment.NewLine);

            string apiKey = Properties.Settings.Default.weatherAPIKey;
            string cordsLong = Properties.Settings.Default.weatherLong;
            string cordsLat = Properties.Settings.Default.weatherLat;
            string units = Properties.Settings.Default.weatherUnits;

            if (cordsLong == null || cordsLat == null)
            {
                MessageBox.Show("Either the logitude or latitude has not been defined", "No location Defined", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Fetching URL based on application settings" + Environment.NewLine);
                string forecastUrl = ConstructRequestUrl.ForecastUrl(apiKey, cordsLat, cordsLong, units);

                //Get the runID
                int runID = Properties.Settings.Default.runID;

                //Increment the runID and save it back to the config file
                runID = runID + 1;
                Properties.Settings.Default.runID = runID;

                string content = GetWeatherData(runID, runGuid, forecastUrl);

                ProcessWeatherJson(runID, runGuid, content);
            }
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

        public class Weather
        {
            public int Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }

        }

        public class Rootobject
        {
            public Weather[] Weather { get; set; }
        }

        private void ProcessWeatherJson(int runID, Guid runGuid, string content)
        {
            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving received values to the database" + Environment.NewLine);

            //Deserialize the json from the content string passed
            dynamic forecastJson = GetDeserializedData(content);

            //Convert the content string into JsonObjects
            var jsonObj = JsonConvert.DeserializeObject<Rootobject>(content);

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

                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Saving alert data to the database" + Environment.NewLine);
                        
                    }
                    catch (Exception ex)
                    {
                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + ex.Message.ToString() + Environment.NewLine);
                    }
                }
            }            

            float? longitude = forecastJson.lon;
            float? latitude = forecastJson.lat;

            var time = forecastJson.dt;
            DateTime runTime = FromUnixTime.Convert(time);

            string timeZone = forecastJson.timezone;
            string timeZoneOffset = forecastJson.timezone_offset;

            SaveRunData(runID, runGuid, runTime, longitude, latitude, timeZone, timeZoneOffset);

            long sunrise = forecastJson.current.sunrise;
            DateTime dtSunrise = FromUnixTime.Convert(sunrise);

            long sunset = forecastJson.current.sunset;
            DateTime dtSunset = FromUnixTime.Convert(sunset);

            decimal? rain = forecastJson.current.rain; //Changed to OpenWeather Schema

            if (rain == null)
            {
                rain = 00.00m;
            }

            decimal? snow = forecastJson.current.snow; //Changed to OpenWeather Schema

            if (snow == null)
            {
                snow = 00.00m;
            }           

            decimal? temperature = forecastJson.current.temp; //Changed to OpenWeather Schema

            if (temperature == null)
            {
                temperature = 00.00m;
            }

            decimal? apparentTemperature = forecastJson.current.feels_like; //Changed to OpenWeather Schema

            if (apparentTemperature == null)
            {
                apparentTemperature = 00.00m;
            }

            decimal? windSpeed = forecastJson.current.wind_speed; //Changed to OpenWeather Schema

            if (windSpeed == null)
            {
                windSpeed = 00.00m;
            }

            decimal? windGust = forecastJson.current.wind_gust; //Changed to OpenWeather Schema

            if (windGust == null)
            {
                windGust = 00.00m;
            }

            decimal? windBearing = forecastJson.current.wind_deg; //Changed to OpenWeather Schema

            if (windBearing == null)
            {
                windBearing = 00.00m;
            }

            decimal? dewPoint = forecastJson.current.dew_point; //Changed to OpenWeather Schema

            if (dewPoint == null)
            {
                dewPoint = 00.00m;
            }

            decimal? humidity = forecastJson.current.humidity; //This doesn't need changing

            if (humidity == null)
            {
                humidity = 00.00m;
            }

            decimal? pressure = forecastJson.current.pressure; //This doesn't need changing

            if (pressure == null)
            {
                pressure = 00.00m;
            }

            decimal? cloudCover = forecastJson.current.clouds; //Changed to OpenWeather Schema

            if (cloudCover == null)
            {
                cloudCover = 00.00m;
            }

            decimal? uvIndex = forecastJson.current.uvi; //Changed to OpenWeather Schema

            if (uvIndex == null)
            {
                uvIndex = 00.00m;
            }

            decimal? visibility = forecastJson.current.visibility; //This doesn't need changing

            if (visibility == null)
            {
                visibility = 00.00m;
            }

            decimal? ozone = forecastJson.current.ozone;

            if (ozone == null)
            {
                ozone = 00.00m;
            }

            SaveWeatherData(runID, runGuid, snow ,rain, temperature, apparentTemperature, windSpeed, windGust, windBearing, dewPoint, humidity, pressure, cloudCover, uvIndex, visibility, ozone);
        }

        private void SaveRunData(int runID, Guid runGuid, DateTime runTime, float? longitude, float? latitude, string timeZone, string timeZoneOffset)
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
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
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
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
                }
        }        

        private void SaveWeatherData(int runID, Guid runGuid, Decimal? snow, Decimal? rain, Decimal? temperature, Decimal? apparentTemperature, Decimal? windSpeed, Decimal? windGust, Decimal? windBearing, Decimal? dewPoint, Decimal? humidity, Decimal? pressure, Decimal? cloudCover, Decimal? uvIndex, Decimal? visibility, Decimal? ozone)
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

                    if (rain != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@rain", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@rain"].Value = rain;

                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@rain", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@rain"].Value = DBNull.Value;
                    }

                    if(snow != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@snow", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@snow"].Value = snow;

                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@snow", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@snow"].Value = DBNull.Value;
                    }                    

                    if (temperature != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@temperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@temperature"].Value = temperature;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@temperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@temperature"].Value = DBNull.Value;
                    }

                    if (apparentTemperature != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@apparentTemperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@apparentTemperature"].Value = apparentTemperature;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@apparentTemperature", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@apparentTemperature"].Value = DBNull.Value;
                    }

                    if (windSpeed != 00.00m)

                    {
                        SaveWeatherData.Parameters.Add("@windSpeed", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windSpeed"].Value = windSpeed;
                    }
                    else

                    {
                        SaveWeatherData.Parameters.Add("@windSpeed", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windSpeed"].Value = DBNull.Value;
                    }

                    if (windGust != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@windGust", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windGust"].Value = windGust;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@windGust", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@windGust"].Value = DBNull.Value;
                    }

                    if (windBearing != 00.00m)
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
                    if (dewPoint != 00.00m)
                    {
                        SaveWeatherData.Parameters["@dewPoint"].Value = dewPoint;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@dewPoint"].Value = DBNull.Value;
                    }


                    SaveWeatherData.Parameters.Add("@humidity", SqlDbType.Decimal);
                    if (humidity != 00.00m)
                    {
                        SaveWeatherData.Parameters["@humidity"].Value = humidity;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@humidity"].Value = DBNull.Value;

                    }

                    SaveWeatherData.Parameters.Add("@pressure", SqlDbType.Decimal);
                    if (pressure != 00.00m)
                    {
                        SaveWeatherData.Parameters["@pressure"].Value = pressure;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@pressure"].Value = DBNull.Value;
                    }


                    SaveWeatherData.Parameters.Add("@cloudCover", SqlDbType.Decimal);
                    if (cloudCover != 00.00m)
                    {
                        SaveWeatherData.Parameters["@cloudCover"].Value = cloudCover;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@cloudCover"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@uvIndex", SqlDbType.Decimal);
                    if (uvIndex != 00.00m)
                    {
                        SaveWeatherData.Parameters["@uvIndex"].Value = uvIndex;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@uvIndex"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@visibility", SqlDbType.Decimal);
                    if (visibility != 00.00m)
                    {
                        SaveWeatherData.Parameters["@visibility"].Value = visibility;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@visibility"].Value = DBNull.Value;
                    }

                    SaveWeatherData.Parameters.Add("@ozone", SqlDbType.Decimal);
                    if (ozone != 00.00m)
                    {
                        SaveWeatherData.Parameters["@ozone"].Value = ozone;
                    }
                    else
                    {
                        SaveWeatherData.Parameters["@ozone"].Value = DBNull.Value;
                    }

                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Attempting to save data to the database" + Environment.NewLine);
                    SaveWeatherData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Saving to the database was sucessful" + Environment.NewLine);
                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
                }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Application Stopped" + Environment.NewLine);
            lblStatus.Text = "Stopped";

            appRunning = 0;

            btn_Start.Visible = true;
            btn_Stop.Visible = false;

        }

        private void BtnExportLogs_Click(object sender, EventArgs e)
        {
            string fileRoot = Properties.Settings.Default.LogPath;
            string fileDir = Properties.Settings.Default.LogDirectory;
            string fileName = Properties.Settings.Default.LogFile;
            string filePath = fileRoot + "\\" + fileDir + "\\" + fileName;

            try
            {
                if (Directory.Exists(fileRoot + fileDir))
                {
                    File.WriteAllText(filePath, txtLogging.Text);
                    MessageBox.Show("Logs successfully exported", "Logs Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Directory.CreateDirectory(fileRoot + fileDir);
                    File.WriteAllText(filePath, txtLogging.Text);
                    MessageBox.Show("Logs successfully exported", "Logs Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
            }

        }

        private void LnkLblSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new settings().Show();
            Hide();
        }

        private void LnkLblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
