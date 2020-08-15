<h1 align="center" style="font-size:80px">⛈</h1>


<p align="center">
    <a href="https://github.com/BonzaOwl/WeatherCollector/stargazers" target="_blank">
        <img alt="GitHub stars" src="https://img.shields.io/github/stars/BonzaOwl/WeatherCollector.svg" />
    </a>
    <a href="https://github.com/BonzaOwl/WeatherCollector/releases" target="_blank">
        <img alt="All releases" src="https://img.shields.io/github/downloads/BonzaOwl/WeatherCollector/total.svg" />
    </a>
</p>

<h1 align="center">Weather Collector</h1>

<p align="center">
Weather Collector is a Windows Forms application written in C# that makes use of the OpenWeather API to get the current weather conditions for a specific location and store it into a SQL Server Database.
</p>

# Main Application Window

<p align="center">
<img src="assets\main-screen-nodb.png">
<img src="assets\main-screen.png">
</p>


# Settings Screen
<p align="center">
<img src="assets\settings-screen.png">
</p>

### Database Settings 

- **Server Name** - The server where the database is stored, e.g. localhost
- **Database Name** - The name of the database, e.g. weatherHarvest
- **Database User** - The username of a login which has access to insert and update the above database 
- **Database Password** - The password for the above user. 

### Log Location

- **Root Location** - For example C:\
- **Folder Name** - The folder you would like to use for logs e.g. Logs
- **File Name** - The name of the log file e.g weatherHarvest.log

### API Settings 

- **API Key** - Obtain an API key from [here](https://home.openweathermap.org/users/sign_up) once you have it enter it in this box.
- **Logitude** - This can be obtained from [here](https://www.latlong.net/)
- **Latitude** - As above this can be obtained from [her](https://www.latlong.net/)
- **Units** - Which unit would you like the data to be returned in metric or imperial, if neither are selected the default will be used. 

### Application Settings 

- **Refresh Interval** - This is the frequency in milliseconds that the application will request data from the API, *free accounts are limited to 60 calls per minute*

## Database Schema

## runData

| Column Name | DataType          | Constraint | Default | Description  |
|-------------|-------------------|------------|---------|------------- |
|runID        |INT                |Primary KEY        | None    | Uniquie key to identify the run             |
|runGuid      |UNIQUEIDENTIFIER   | None       | None    |              |
|runTime      |DATETIME           |None        | None    | Time of the forecasted data              |
|units | varchar(10) | None | None | Requested unit type (metric or imperial) |
|longitude    |FLOAT              |None        | None    | Longitude requested             |
|latitude     |FLOAT              |None        | None    | Latitude requested            |
|timeZone     |nvarchar(200)      | None       | None    | Timezone requested             |
|timeZoneOffset |nvarchar(200)    | None       | None    | Shift in seconds from UTC              |
|invalid      |BIT                |None        | 0    | To allow for the run to be marked as invalid             |
|Deleted      |BIT                |None        | 0    | To allow for the run to be deleted             |

## rawWeatherData

| Column Name | DataType          | Constraint | Default | Description  |
|-------------|-------------------|------------|---------|------------- |
|ID           |INT                |Primary KEY | None    |              |
|runID        |INT                |Foreign KEY | None    | Uniquie key to identify the run              |
|runGuid      |UNIQUEIDENTIFIER   |None        | None    |              |
|rawData      |NVARCHAR(MAX)      |None        | None    | Returned JSON data             |

## weatherData

| Column Name | DataType          | Constraint | Default | Description  |
|-------------|-------------------|------------|---------|------------- |
|ID           |INT                |Primary KEY | None    |              |
|runID        |INT                |Foreign KEY | None    |              |
|runGuid      |UNIQUEIDENTIFIER   |None        | None    |              |
|collectionID |INT                |None        | None    |              |
|summary      |varchar(100)       |None        | None    |              |
|description  |varchar(100)       |None        | None    |              |
|icon         |varchar(20)        |None        | None    |              |



## currentlyData

| Column Name | DataType          | Constraint | Default | Description  |
|-------------|-------------------|------------|---------|------------- |
|ID           |INT                |Primary KEY | None    |               |
|runID        |INT                |Foreign KEY | None    | Unique identifier for the row of data             |
|runGuid      |UNIQUEIDENTIFIER   |None        | None    |              |
|temperature      |DECIMAL(4,2)      |None        | None    | Temprature stored in the requested unit format             |
|apparentTemperature      |DECIMAL(4,2)      |None        | None    |              |
|windSpeed      |DECIMAL(4,2)      |None        | None    |              |
|windGust      |DECIMAL(4,2)      |None        | None    |              |
|windBearing      |DECIMAL(5,2)      |None        | None    |              |
|dewPoint      |DECIMAL(4,2)      |None        | None    |              |
|humidity      |DECIMAL(4,2)      |None        | None    |              |
|pressure      |DECIMAL(6,2)      |None        | None    |              |
|cloudCover      |DECIMAL(4,2)      |None        | None    |              |
|uvIndex      |DECIMAL(4,2)      |None        | None    |              |
|visibility      |DECIMAL(4,2)      |None        | None    |              |
|ozone      |DECIMAL(5,2)      |None        | None    |              |

## Ref.Codes

| Column Name | DataType          | Constraint | Default | Description  |
|-------------|-------------------|------------|---------|------------- |
|ID           |INT                |Primary KEY | None    | Unique identifier for the row of data             |
|Main        |varchar(100)                |None | None   | Main summary for the icon type e.g Drizzle              |
|Description      |varchar(200)    |None        | None    | Description of the weather type the icon represents              |
|Icon      |CHAR(3)      |None        | None    | Icon code            |
|IconURL      |nvarchar(100)      |None        | None    | Collection URL for the Icon             |

## Collected Data

The API in use by Weather Collector is the [One Call API](https://openweathermap.org/api/one-call-api) from [OpenWeather](https://openweathermap.org) this returns a collection of data points however at this time not all of them are in use by the Weather Collector, the below list shows which data points are used in the API

**Please note:** The entire JSON response is stored in the rawWeatherData table in SQL it is just not extracted for use by the application. 

It is also worth noting that not all supported data points are available in all regions, some regions may return null values. 

- lat *Geographical coordinates of the location (latitude)*
- lon *Geographical coordinates of the location (longitude)*
- timezone *Timezone name for the requested location*
- timezone_offset *Shift in seconds from UTC*

- current Data point dt refers to the requested time, rather than the current time
    - dt Requested time, Unix, UTC (*This is converted to datetime in app*)
    - sunrise Sunrise time, Unix, UTC (*This is converted to datetime in app*)
    - sunset Sunset time, Unix, UTC (*This is converted to datetime in app*)
    - temp *Temperature*.
    - feels_like *Temperature*. 
    - pressure *Atmospheric pressure on the sea level, hPa*
    - humidity *Humidity, %*
    - dew_point *Atmospheric temperature (varying according to pressure and humidity) below which water - droplets begin to condense and dew can form. Units – default: kelvin, metric: Celsius, imperial: Fahrenheit.*
    - clouds *Cloudiness, %*
    - uvi *Midday UV index*
    - visibility *Average visibility, metres*
    - wind_speed *Wind speed. Units – default: metre/sec, metric: metre/sec, imperial: miles/hour.*
    - wind_gust *(where available) Wind gust.*
    - wind_deg *Wind direction, degrees (meteorological)*
    - rain *(where available) Precipitation volume, mm*
    - snow *(where available) Snow volume, mm*
    - current.weather
        - id *Weather condition id*
        - main *Group of weather parameters (Rain, Snow, Extreme etc.)*
        - description *Weather condition within the group (full list of weather conditions).* **Currently returned in English only. Future plans include support for other languages**
        - icon *[Weather icon](https://openweathermap.org/weather-conditions#How-to-get-icon-URL) id*

### More Information

If you are looking for more information on the OpenWeather API you can view the full documentation [here](https://openweathermap.org/api/one-call-api)

---

## I found a bug

You can open an issue or if you would really like you can change the code yourself and submit a pull request. 

## I want the application to do something else

Feel free to clone the repo and make changes to suit your individual needs or you can submit an issue requesting the change. 


