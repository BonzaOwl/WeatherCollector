CREATE OR ALTER PROC SaveCurrentlyData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@temperature DECIMAL(4,2),
@apparentTemperature DECIMAL(4,2),
@windSpeed DECIMAL(4,2),
@windGust DECIMAL(4,2),
@windBearing DECIMAL(5,2),
@dewPoint DECIMAL(4,2),
@humidity DECIMAL(4,2),
@pressure DECIMAL(6,2),
@cloudCover DECIMAL(4,2),
@uvIndex DECIMAL(4,2),
@visibility DECIMAL(4,2),
@ozone DECIMAL(5,2)

AS

BEGIN

INSERT INTO currentlyData 
(
	runID,
	runGuid,
	temperature,
	apparentTemperature,
	windSpeed,
	windGust,
	windBearing,
	dewPoint,
	humidity,
	pressure,
	cloudCover,
	uvIndex,
	visibility,
	ozone
)
VALUES
(
	@runID,
	@runGuid,
	@temperature,
	@apparentTemperature,
	@windSpeed,
	@windGust,
	@windBearing,
	@dewPoint,
	@humidity,
	@pressure,
	@cloudCover,
	@uvIndex,
	@visibility,
	@ozone
)

END;

GO

CREATE OR ALTER PROC ClearAllWeatherData

AS

BEGIN

TRUNCATE TABLE [dbo].[AlertData];
TRUNCATE TABLE [dbo].[rawWeatherData];
TRUNCATE TABLE [dbo].[receivedData];
TRUNCATE TABLE [dbo].[currentlyData];

END

GO

CREATE OR ALTER VIEW allWeatherData

AS

SELECT  
    rd.runGuid,
    rd.runTime,
    rwd.rawData,
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
    Weather.dbo.receivedData rd

    INNER JOIN dbo.WeatherData wd ON
        wd.runGuid = rd.runGuid

    INNER JOIN [dbo].[rawWeatherData] rwd ON
        rwd.runGuid = rd.runGuid

GO

CREATE OR ALTER PROC SystemAlive 

@databaseName varchar(100),
@rowCnt INT OUTPUT

AS

SELECT 
    name 
FROM 
    master.sys.databases d
WHERE 
    d.name = @databaseName

SET @rowCnt = @@ROWCOUNT

SELECT @rowCnt

GO 

CREATE OR ALTER PROC SaveReceivedData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@runTime DATETIME,
@icon NVARCHAR(50),
@summary NVARCHAR(500),
@RcvWindSpeed varchar(6),
@RcvWindGust varchar(6),
@RcvWindBearing varchar(6),
@RcvTemperature varchar(6),
@RcvApparentTemperature varchar(6),
@RcvDewPoint varchar(6),
@RcvHumidity varchar(6),
@RcvPressure varchar(6),
@RcvCloudCover varchar(6),
@RcvuvIndex varchar(6),
@RcvVisibility varchar(6),
@RcvOzone varchar(6)

AS

BEGIN

INSERT INTO receivedData
(	
	runID,
	runGuid,
	runTime,
	icon,
	summary,
	RcvWindSpeed,
	RcvWindGust,
	RcvWindBearing,
	RcvTemperature,
	RcvApparentTemperature,
	RcvDewPoint,
	RcvHumidity,
	RcvPressure,
	RcvCloudCover,
	RcvuvIndex,
	RcvVisibility,
	RcvOzone
)

VALUES
(
	@runID,
	@runGuid,
	@runTime,
	@icon,
	@summary,
	@RcvWindSpeed,
	@RcvWindGust,
	@RcvWindBearing,
	@RcvTemperature,
	@RcvApparentTemperature,
	@RcvDewPoint,
	@RcvHumidity,
	@RcvPressure,
	@RcvCloudCover,
	@RcvuvIndex,
	@RcvVisibility,
	@RcvOzone
)

END

GO

CREATE OR ALTER PROC SaveRunData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@runTime DATETIME,
@units VARCHAR(10),
@longitude FLOAT,
@latitude FLOAT,
@timeZone nvarchar(200),
@timeZoneOffset nvarchar(200)

AS

BEGIN

    INSERT INTO runData (runID, runGuid, runTime, longitude, latitude, timeZone, timeZoneOffset, units)
    VALUES
    (@runID,@runGuid,@runTime,@longitude,@latitude,@timeZone,@timeZoneOffset,@units)

END

GO

CREATE OR ALTER PROC SaveRawWeatherData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@rawData NVARCHAR(MAX)

AS

BEGIN
	
	INSERT INTO rawWeatherData 
	(	
		runID,
		runGuid,
		rawData
	)
	VALUES
	(
		@runID,
		@runGuid,
		@rawData
	)

END;

GO

CREATE OR ALTER VIEW [dbo].[allWeatherData]

AS

SELECT  
    rd.runID,
    rd.runGuid,
    rd.runTime,
    rwd.rawData,
    cd.temperature, 
    cd.apparentTemperature, 
    cd.windSpeed, 
    cd.windGust, 
    cd.windBearing, 
    cd.dewPoint, 
    cd.humidity, 
    cd.pressure, 
    cd.cloudCover, 
    cd.uvIndex, 
    cd.visibility, 
    cd.ozone
FROM 
    dbo.runData rd

    INNER JOIN dbo.currentlyData cd ON
        cd.runID = rd.runID

    INNER JOIN [dbo].[rawWeatherData] rwd ON
        rwd.runID = rd.runID

GO

CREATE OR ALTER PROCEDURE [dbo].[getHistory]

AS

SELECT 

    [runid]
    ,[runGuid]
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
GO