

CREATE   VIEW [dbo].[allWeatherData]

AS

SELECT  
    rd.runGuid,
    rd.runTime,
    rwd.rawData,
    wd.rain,
    wd.temperature, 
    wd.apparentTemperature, 
    wd.windSpeed, 
    wd.windGust, 
    wd.windBearing, 
    wd.dewPoint, 
    wd.humidity, 
    wd.pressure, 
    wd.cloudCover, 
    wd.uvIndex, 
    wd.visibility, 
    wd.ozone
FROM 
    Weather.dbo.runData rd

    INNER JOIN dbo.WeatherData wd ON
        wd.runGuid = rd.runGuid

    INNER JOIN [dbo].[rawWeatherData] rwd ON
        rwd.runGuid = rd.runGuid
