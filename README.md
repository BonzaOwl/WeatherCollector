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
Weather Collector is a Windows Forms application written in C# that makes use of the OpenWeather api to get the current weather conditions for a specific location and stores it into a SQL Server Database.
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
- **Units** - Which unit would you like the data to be returned in metric or imperial

### Application Settings 

- **Refresh Interval** - This is the frequency in milliseconds that the application will request data from the API, *free accounts are limited to 60 calls per minute*

---

## I found a bug

You can open an issue or if you would really like you can change the code yourself and submit a pull request. 

## I want the application to do something else

Feel free to clone the repo and make changes to suit your individual needs or you can submit an issue requesting the change. 


