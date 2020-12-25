

CREATE   VIEW [dbo].[allWeatherData]

AS

SELECT  
    rd.runID,
    rd.invalid,
    rd.deleted,
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
    dbo.runData rd

    INNER JOIN dbo.WeatherData wd ON
        wd.runGuid = rd.runGuid

    INNER JOIN [dbo].[rawWeatherData] rwd ON
        rwd.runGuid = rd.runGuid
GO
