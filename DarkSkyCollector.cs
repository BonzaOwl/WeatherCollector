using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;

namespace DarkSkyCollectorDesktop
{
    public partial class DarkSkyCollector : Form
    {
        private readonly int secondsToWait = Properties.Settings.Default.RefreshInterval;
        private DateTime startTime;
        private int runTimes;

        private int appRunning;

        public DarkSkyCollector()
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
            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Application Started " + Environment.NewLine);
            timer1.Start(); // start timer (you can do it on form load, if you need)            
            startTime = DateTime.Now; // and remember start time
            DarkSkyRun(); //Start the collection from Dark Sky
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            int remainingSeconds = secondsToWait - elapsedSeconds;

            if (remainingSeconds <= 0)
            {
                DarkSkyRun();
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

                    SqlCommand SaveRawWeatherData = new SqlCommand("[dbo].[ClearAllWeatherData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveRawWeatherData.ExecuteNonQuery();

                    lblDatabaseExist.Visible = false;

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToLongTimeString() + ex.Message.ToString() + Environment.NewLine);
                    lblDatabaseExist.Visible = true;
                    lblDatabaseExist.Text = "DATABASE DOESN'T EXIST";
                }
        }

        public void DarkSkyRun()
        {
            var runGuid = Guid.NewGuid(); //The primary identifier for the run

            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Starting Application with guid " + runGuid + Environment.NewLine);

            string apiKey = Properties.Settings.Default.DarkSkyAPIKey;
            string cordsLong = Properties.Settings.Default.DarkSkyLong;
            string cordsLat = Properties.Settings.Default.DarkSkyLat;
            string exclude = Properties.Settings.Default.DarkSkyExclusions;
            string units = Properties.Settings.Default.DarkSkyUnits;

            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Fetching URL based on application settings" + Environment.NewLine);
            string forecastUrl = ConstructRequestUrl.ForecastUrl(apiKey, cordsLat, cordsLong, exclude, units);
            
            string content = GetDarkSkyData(runGuid, forecastUrl);            

            ProcessDarkSkyJson(runGuid, content);
        }

        private string GetDarkSkyData(Guid runGuid, string forecastUrl)
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
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Raw Dark Sky Json now being requested from the Dark Sky API" + Environment.NewLine);
                content = reader.ReadToEnd();
                                
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Raw Dark Sky data being saved to the database this will allow us to review it later" + Environment.NewLine);
                SaveRawDarkSkyData(runGuid, content);
            }

            return content;
        }

        private void ProcessDarkSkyJson(Guid runGuid, string content)
        {
            txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving received values to the database" + Environment.NewLine);

            //Deserialize the json from the content string passed
            dynamic forecastJson = GetDeserializedData(content);

            //Convert the content string into JsonObjects
            var jsonObj = JsonConvert.DeserializeObject<Rootobject>(content);

            var alertObj = jsonObj.alerts;

            if (alertObj != null)
            {
                txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Alert Data Present" + Environment.NewLine);

                foreach (var obj in alertObj)
                {
                    try
                    {
                        //https://darksky.net/forecast/40.7956,-124.1627/uk212/en
                        string title = obj.title;
                        string description = obj.description;
                        int expires = obj.expires;

                        DateTime expiry = new DateTime();
                        DateTime fromtime = new DateTime();

                        if (expires != 0)
                        {
                            expiry = FromUnixTime.Convert(expires);
                        }
                        int time = obj.time;

                        if (time != 0)
                        {
                            fromtime = FromUnixTime.Convert(time);
                        }

                        string severity = obj.severity;
                        long timeRan = forecastJson.currently.time;
                        DateTime runTime = FromUnixTime.Convert(timeRan);

                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Saving alert data to the database" + Environment.NewLine);

                        SaveDarkSkyAlerts(runGuid, runTime, title, description, expiry, fromtime, severity);
                    }
                    catch (Exception ex)
                    {
                        txtLogging.AppendText(DateTime.Now.ToLongTimeString() + ex.Message.ToString() + Environment.NewLine);
                    }
                }
            }

            SaveReceivedData(runGuid, forecastJson);

            decimal? precipIntensity = forecastJson.currently.precipIntensity;

            if (precipIntensity == null)
            {
                precipIntensity = 00.00m;
            }

            decimal? precipIntensityError = forecastJson.currently.precipIntensityError;

            if (precipIntensityError == null)
            {
                precipIntensityError = 00.00m;
            }

            decimal? precipProbability = forecastJson.currently.precipProbability;

            if (precipProbability == null)
            {
                precipProbability = 00.00m;
            }

            string precipType = forecastJson.currently.precipType;

            if (precipType == null)
            {
                precipType = "00.00";
            }

            decimal? nearestStormDistance = forecastJson.currently.nearestStormDistance;

            if (nearestStormDistance == null)
            {
                nearestStormDistance = 00.00m;
            }

            decimal? temperature = forecastJson.currently.temperature;

            if (temperature == null)
            {
                temperature = 00.00m;
            }

            decimal? apparentTemperature = forecastJson.currently.apparentTemperature;

            if (apparentTemperature == null)
            {
                apparentTemperature = 00.00m;
            }

            decimal? windSpeed = forecastJson.currently.windSpeed;

            if (windSpeed == null)
            {
                windSpeed = 00.00m;
            }

            decimal? windGust = forecastJson.currently.windGust;

            if (windGust == null)
            {
                windGust = 00.00m;
            }

            decimal? windBearing = forecastJson.currently.windBearing;

            if (windBearing == null)
            {
                windBearing = 00.00m;
            }

            decimal? dewPoint = forecastJson.currently.dewPoint;

            if (dewPoint == null)
            {
                dewPoint = 00.00m;
            }

            decimal? humidity = forecastJson.currently.humidity;

            if (humidity == null)
            {
                humidity = 00.00m;
            }

            decimal? pressure = forecastJson.currently.pressure;

            if (pressure == null)
            {
                pressure = 00.00m;
            }

            decimal? cloudCover = forecastJson.currently.cloudCover;

            if (cloudCover == null)
            {
                cloudCover = 00.00m;
            }

            decimal? uvIndex = forecastJson.currently.uvIndex;

            if (uvIndex == null)
            {
                uvIndex = 00.00m;
            }

            decimal? visibility = forecastJson.currently.visibility;

            if (visibility == null)
            {
                visibility = 00.00m;
            }

            decimal? ozone = forecastJson.currently.ozone;

            if (ozone == null)
            {
                ozone = 00.00m;
            }

            SaveDarkSkyData(runGuid, precipIntensity, precipIntensityError, precipProbability, precipType, nearestStormDistance, temperature, apparentTemperature, windSpeed, windGust, windBearing, dewPoint, humidity, pressure, cloudCover, uvIndex, visibility, ozone);

        }

        private dynamic GetDeserializedData(string content)
        {

            dynamic forecastJson;

            Console.ForegroundColor = ConsoleColor.Green;
            txtLogging.AppendText(DateTime.Now.ToLongTimeString() + " Dark Sky Json is now being Deserialized" + Environment.NewLine);
            forecastJson = JsonConvert.DeserializeObject(content);

            return forecastJson;

        }

        private void SaveRawDarkSkyData(Guid runGuid, string content)
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

                    SaveRawWeatherData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveRawWeatherData.Parameters["@runGuid"].Value = runGuid;

                    SaveRawWeatherData.Parameters.Add("@rawData", SqlDbType.NVarChar, -1);
                    SaveRawWeatherData.Parameters["@rawData"].Value = content;

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save raw data to the database" + Environment.NewLine);
                    SaveRawWeatherData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
                }
        }

        private void SaveReceivedData(Guid runGuid, dynamic forecastJson)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveReceivedData = new SqlCommand("[dbo].[SaveReceivedData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    long time = forecastJson.currently.time;
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Converting run time from Json to C# Date Time" + Environment.NewLine);
                    DateTime runTime = FromUnixTime.Convert(time);
                    string summary = forecastJson.currently.summary;
                    string icon = forecastJson.currently.icon;

                    SaveReceivedData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveReceivedData.Parameters["@runGuid"].Value = runGuid;

                    SaveReceivedData.Parameters.Add("@runTime", SqlDbType.DateTime);
                    SaveReceivedData.Parameters["@runTime"].Value = runTime;

                    SaveReceivedData.Parameters.Add("@icon", SqlDbType.NVarChar, 50);
                    SaveReceivedData.Parameters["@icon"].Value = icon;

                    if (summary != null)
                    {
                        SaveReceivedData.Parameters.Add("@summary", SqlDbType.NVarChar, 500);
                        SaveReceivedData.Parameters["@summary"].Value = summary;
                    } else
                    {
                        SaveReceivedData.Parameters.Add("@summary", SqlDbType.NVarChar, 500);
                        SaveReceivedData.Parameters["@summary"].Value = DBNull.Value;
                    }
                    

                    string RcvWindSpeed = forecastJson.currently.windSpeed;
                    if(RcvWindSpeed != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindSpeed", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindSpeed"].Value = RcvWindSpeed;
                    }else
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindSpeed", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindSpeed"].Value = DBNull.Value;
                    }

                    string RcvWindGust = forecastJson.currently.windSpeed;
                    if(RcvWindGust != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindGust", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindGust"].Value = RcvWindGust;
                    } else
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindGust", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindGust"].Value = DBNull.Value;
                    }                    

                    string RcvWindBearing = forecastJson.currently.windSpeed;
                    if (RcvWindBearing != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindBearing", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindBearing"].Value = RcvWindBearing;
                    } else
                    {
                        SaveReceivedData.Parameters.Add("@RcvWindBearing", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvWindBearing"].Value = DBNull.Value;
                    }

                    string RcvTemperature = forecastJson.currently.windSpeed;
                    if (RcvTemperature != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvTemperature", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvTemperature"].Value = RcvTemperature;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvTemperature", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvTemperature"].Value = DBNull.Value;

                    }

                    string RcvApparentTemperature = forecastJson.currently.windSpeed;
                    if (RcvApparentTemperature != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvApparentTemperature", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvApparentTemperature"].Value = forecastJson.currently.apparentTemperature;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvApparentTemperature", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvApparentTemperature"].Value = DBNull.Value;
                    }

                    string RcvprecipIntensity = forecastJson.currently.windSpeed;
                    if (RcvprecipIntensity != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipIntensity", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvprecipIntensity"].Value = RcvprecipIntensity;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipIntensity", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvprecipIntensity"].Value = DBNull.Value;
                    }

                    string RcvprecipIntensityError = forecastJson.currently.windSpeed;
                    if (RcvprecipIntensityError != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipIntensityError", SqlDbType.VarChar, 60);
                        SaveReceivedData.Parameters["@RcvprecipIntensityError"].Value = RcvprecipIntensityError;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipIntensityError", SqlDbType.VarChar, 60);
                        SaveReceivedData.Parameters["@RcvprecipIntensityError"].Value = DBNull.Value;
                    }

                    string RcvprecipProbability = forecastJson.currently.windSpeed;
                    if (RcvprecipProbability != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipProbability", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvprecipProbability"].Value = RcvprecipProbability;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvprecipProbability", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvprecipProbability"].Value = DBNull.Value;
                    }

                    string precipType = forecastJson.currently.windSpeed;
                    if (precipType != null)
                    {
                        SaveReceivedData.Parameters.Add("@precipType", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@precipType"].Value = precipType;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@precipType", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@precipType"].Value = DBNull.Value;
                    }

                    string RcvNearestStormDistance = forecastJson.currently.windSpeed;
                    if (RcvNearestStormDistance != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvNearestStormDistance", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvNearestStormDistance"].Value = RcvNearestStormDistance;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvNearestStormDistance", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvNearestStormDistance"].Value = DBNull.Value;
                    }

                    string RcvDewPoint = forecastJson.currently.windSpeed;
                    if (RcvDewPoint != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvDewPoint", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvDewPoint"].Value = RcvDewPoint;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvDewPoint", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvDewPoint"].Value = DBNull.Value;
                    }

                    string RcvHumidity = forecastJson.currently.windSpeed;
                    if (RcvHumidity != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvHumidity", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvHumidity"].Value = RcvHumidity;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvHumidity", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvHumidity"].Value = DBNull.Value;
                    }

                    string RcvPressure = forecastJson.currently.windSpeed;
                    if (RcvPressure != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvPressure", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvPressure"].Value = RcvPressure;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvPressure", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvPressure"].Value = DBNull.Value;
                    }

                    string RcvCloudCover = forecastJson.currently.windSpeed;
                    if (RcvCloudCover != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvCloudCover", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvCloudCover"].Value = RcvCloudCover;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvCloudCover", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvCloudCover"].Value = DBNull.Value;
                    }

                    string RcvuvIndex = forecastJson.currently.windSpeed;
                    if (RcvuvIndex != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvuvIndex", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvuvIndex"].Value = RcvuvIndex;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvuvIndex", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvuvIndex"].Value = DBNull.Value;
                    }

                    string RcvVisibility = forecastJson.currently.windSpeed;
                    if (RcvVisibility != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvVisibility", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvVisibility"].Value = RcvVisibility;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvVisibility", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvVisibility"].Value = DBNull.Value;
                    }

                    string RcvOzone = forecastJson.currently.windSpeed;
                    if (RcvOzone != null)
                    {
                        SaveReceivedData.Parameters.Add("@RcvOzone", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvOzone"].Value = RcvOzone;
                    }
                    else
                    {
                        SaveReceivedData.Parameters.Add("@RcvOzone", SqlDbType.VarChar, 6);
                        SaveReceivedData.Parameters["@RcvOzone"].Value = DBNull.Value;
                    }

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save received data to the database" + Environment.NewLine);
                    SaveReceivedData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
                }
        }        

        private void SaveDarkSkyAlerts(Guid runGuid, DateTime runTime,string title, string description, DateTime expiry, DateTime fromtime, string severity)
        {
            var sqldb_connection = ConnectionStringBuilder.ConnectionString();
            using (SqlConnection con = new SqlConnection(sqldb_connection))
                try
                {
                    con.Open();

                    SqlCommand SaveAlertData = new SqlCommand("[dbo].[SaveAlertData]", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SaveAlertData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveAlertData.Parameters["@runGuid"].Value = runGuid;

                    SaveAlertData.Parameters.Add("@runTime", SqlDbType.DateTime);
                    SaveAlertData.Parameters["@runTime"].Value = runTime;

                    if(title != null)
                    {
                        SaveAlertData.Parameters.Add("@title", SqlDbType.VarChar, 1000);
                        SaveAlertData.Parameters["@title"].Value = title;
                    } else
                    {
                        SaveAlertData.Parameters.Add("@title", SqlDbType.VarChar, 1000);
                        SaveAlertData.Parameters["@title"].Value = DBNull.Value;
                    }

                    if(description != null)
                    {
                        SaveAlertData.Parameters.Add("@description", SqlDbType.VarChar, -1);
                        SaveAlertData.Parameters["@description"].Value = description;
                    }
                    else
                    {
                        SaveAlertData.Parameters.Add("@description", SqlDbType.VarChar, -1);
                        SaveAlertData.Parameters["@description"].Value = DBNull.Value;
                    }

                    if (expiry != null)
                    {
                        SaveAlertData.Parameters.Add("@expiry", SqlDbType.DateTime);
                        SaveAlertData.Parameters["@expiry"].Value = expiry;
                    }
                    else
                    {
                        SaveAlertData.Parameters.Add("@expiry", SqlDbType.DateTime);
                        SaveAlertData.Parameters["@expiry"].Value = DBNull.Value;
                    }

                    if (fromtime != null)
                    {
                        SaveAlertData.Parameters.Add("@fromtime", SqlDbType.DateTime);
                        SaveAlertData.Parameters["@fromtime"].Value = fromtime;
                    }
                    else
                    {
                        SaveAlertData.Parameters.Add("@fromtime", SqlDbType.DateTime);
                        SaveAlertData.Parameters["@fromtime"].Value = DBNull.Value;

                    }

                    if (severity != null)
                    {
                        SaveAlertData.Parameters.Add("@severity", SqlDbType.VarChar, 1000);
                        SaveAlertData.Parameters["@severity"].Value = severity;
                    }
                    else
                    {
                        SaveAlertData.Parameters.Add("@severity", SqlDbType.VarChar, 1000);
                        SaveAlertData.Parameters["@severity"].Value = DBNull.Value;
                    }

                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Attempting to save raw data to the database" + Environment.NewLine);
                    SaveAlertData.ExecuteNonQuery();
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + " Saving to the database was sucessful" + Environment.NewLine);

                }

                catch (Exception ex)
                {
                    txtLogging.AppendText(DateTime.Now.ToShortTimeString() + ex.Message.ToString() + Environment.NewLine);
                }
        }

        private void SaveDarkSkyData(Guid runGuid, Decimal? precipIntensity, Decimal? precipIntensityError, Decimal? precipProbability, string precipType, Decimal? nearestStormDistance, Decimal? temperature, Decimal? apparentTemperature, Decimal? windSpeed, Decimal? windGust, Decimal? windBearing, Decimal? dewPoint, Decimal? humidity, Decimal? pressure, Decimal? cloudCover, Decimal? uvIndex, Decimal? visibility, Decimal? ozone)
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
                    SaveWeatherData.Parameters.Add("@runGuid", SqlDbType.UniqueIdentifier);
                    SaveWeatherData.Parameters["@runGuid"].Value = runGuid;


                    if (precipIntensity != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@precipIntensity", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipIntensity"].Value = precipIntensity;

                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@precipIntensity", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipIntensity"].Value = DBNull.Value;
                    }

                    if (precipIntensityError != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@precipIntensityError", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipIntensityError"].Value = precipIntensityError;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@precipIntensityError", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipIntensityError"].Value = DBNull.Value;
                    }

                    if (precipProbability != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@precipProbability", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipProbability"].Value = precipProbability;

                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@precipProbability", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@precipProbability"].Value = DBNull.Value;
                    }

                    if (precipType != "00.00")
                    {
                        SaveWeatherData.Parameters.Add("@precipType", SqlDbType.NVarChar, 10);
                        SaveWeatherData.Parameters["@precipType"].Value = precipType;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@precipType", SqlDbType.NVarChar, 10);
                        SaveWeatherData.Parameters["@precipType"].Value = DBNull.Value;
                    }

                    if (nearestStormDistance != 00.00m)
                    {
                        SaveWeatherData.Parameters.Add("@nearestStormDistance", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@nearestStormDistance"].Value = nearestStormDistance;
                    }
                    else
                    {
                        SaveWeatherData.Parameters.Add("@nearestStormDistance", SqlDbType.Decimal);
                        SaveWeatherData.Parameters["@nearestStormDistance"].Value = DBNull.Value;
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
                        SaveWeatherData.Parameters["@visibility"].Value = visibility;
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
        
        public class Rootobject
        {
            public Alert[] alerts { get; set; }
        }      

        public class Alert
        {
            public string title { get; set; }
            public string severity { get; set; }
            public int time { get; set; }
            public int expires { get; set; }
            public string description { get; set; }
        }
    }
}
