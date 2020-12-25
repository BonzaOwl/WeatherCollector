
CREATE PROCEDURE [dbo].[getHistory]

AS

SELECT
    runID
    ,invalid
    ,deleted
    ,runTime
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
