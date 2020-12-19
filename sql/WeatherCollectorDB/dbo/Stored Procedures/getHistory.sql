
CREATE   PROCEDURE [dbo].[getHistory]

AS

SELECT 

    [runGuid]
    ,[runTime]
    ,[rawData]
    ,[temperature]
    ,[apparentTemperature]
    ,[windSpeed]
    ,[windGust]
    ,[windBearing]
    ,[dewPoint]
    ,[humidity]
    ,[pressure]
    ,[cloudCover]
    ,[uvIndex]
    ,[visibility]
    ,[ozone]
      
FROM [dbo].[allWeatherData]
