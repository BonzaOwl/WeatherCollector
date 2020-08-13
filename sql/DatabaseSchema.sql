USE master
GO
CREATE DATABASE Weather;
GO
USE Weather;
GO
CREATE SCHEMA Ref AUTHORIZATION dbo
GO
CREATE TABLE Ref.Codes
(
    ID INT,
    Main varchar(100),
    Description varchar(200),
    Icon CHAR(3),
	IconURL nvarchar(100)
);
GO
CREATE TABLE rawWeatherData
(
	runID INT NOT NULL,
	runGuid UNIQUEIDENTIFIER,
	rawData NVARCHAR(MAX)
);

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

CREATE TABLE runData
(
    runID INT NOT NULL,
    runGuid UNIQUEIDENTIFIER NOT NULL,
    runTime DATETIME NOT NULL,
    longitude FLOAT NULL,
    latitude FLOAT NULL,
    timeZone nvarchar(200),
    timeZoneOffset nvarchar(200),
	invalid BIT,
	deleted BIT
);

GO

CREATE OR ALTER PROC SaveRunData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@runTime DATETIME,
@longitude FLOAT,
@latitude FLOAT,
@timeZone nvarchar(200),
@timeZoneOffset nvarchar(200)

AS

BEGIN

    INSERT INTO runData (runID, runGuid, runTime, longitude, latitude, timeZone, timeZoneOffset)
    VALUES
    (@runID,@runGuid,@runTime,@longitude,@latitude,@timeZone,@timeZoneOffset)

END

GO

CREATE TABLE weatherData
(
    runID INT NOT NULL,
    runGuid UNIQUEIDENTIFIER NOT NULL,
    collectionID INT,
    summary varchar(100),
    description varchar(100),
    icon varchar(20)    
);

GO

CREATE TABLE receivedData
(
	runID INT NOT NULL,
	runGuid UNIQUEIDENTIFIER,
	runTime DATETIME,
	icon NVARCHAR(50),
	summary NVARCHAR(500),
	RcvWindSpeed varchar(6),
	RcvWindGust varchar(6),
	RcvWindBearing varchar(6),
	RcvTemperature varchar(6),
	RcvApparentTemperature varchar(6),
	--RcvprecipIntensity varchar(6),
	--RcvprecipIntensityError varchar(60),
	--RcvprecipProbability varchar(6),
	--precipType varchar(6),
	--RcvNearestStormDistance varchar(6),
	RcvDewPoint varchar(6),
	RcvHumidity varchar(6),
	RcvPressure varchar(6),
	RcvCloudCover varchar(6),
	RcvuvIndex varchar(6),
	RcvVisibility varchar(6),
	RcvOzone varchar(6)
);

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
-- @RcvprecipIntensity varchar(6),
-- @RcvprecipIntensityError varchar(60),
-- @RcvprecipProbability varchar(6),
-- @precipType varchar(6),
-- @RcvNearestStormDistance varchar(6),
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
	-- RcvprecipIntensity,
	-- RcvprecipIntensityError,
	-- RcvprecipProbability,
	-- precipType,
	-- RcvNearestStormDistance,
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
	-- @RcvprecipIntensity,
	-- @RcvprecipIntensityError,
	-- @RcvprecipProbability,
	-- @precipType,
	-- @RcvNearestStormDistance,
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

-- CREATE TABLE AlertData
-- (
-- 	runID INT NOT NULL,
-- 	runGuid UNIQUEIDENTIFIER,
-- 	title varchar(1000),
-- 	description NVARCHAR(MAX),
-- 	expiry DATETIME,
-- 	fromtime DATETIME,
-- 	severity varchar(1000)
-- )

--GO

-- CREATE OR ALTER PROC SaveAlertData

-- @runID INT,
-- @runGuid UNIQUEIDENTIFIER,
-- @title varchar(1000),
-- @description NVARCHAR(MAX),
-- @expiry DATETIME,
-- @fromtime DATETIME,
-- @severity varchar(1000)

-- AS 

-- BEGIN

-- INSERT INTO AlertData
-- (
-- 	runID,
-- 	runGuid,
-- 	title,
-- 	description,
-- 	expiry,
-- 	fromtime,
-- 	severity
-- )
-- VALUES
-- (
-- 	@runID,
-- 	@runGuid,
-- 	@title,
-- 	@description,
-- 	@expiry,
-- 	@fromtime,
-- 	@severity
-- )
-- END; 

-- GO

CREATE TABLE currentlyData
(
	runID INT NOT NULL,
	runGuid UNIQUEIDENTIFIER,
	temperature DECIMAL(4,2),
	apparentTemperature DECIMAL(4,2),
	windSpeed DECIMAL(4,2),
	windGust DECIMAL(4,2),
	windBearing DECIMAL(5,2),
	dewPoint DECIMAL(4,2),
	humidity DECIMAL(4,2),
	pressure DECIMAL(6,2),
	cloudCover DECIMAL(4,2),
	uvIndex DECIMAL(4,2),
	visibility DECIMAL(4,2),
	ozone DECIMAL(5,2)
);

GO

CREATE OR ALTER PROC SaveCurrentlyData

@runID INT,
@runGuid UNIQUEIDENTIFIER,
-- @precipIntensity DECIMAL(4,2),
-- @precipIntensityError DECIMAL(4,2),
-- @precipProbability DECIMAL(4,2),
-- @precipType NVARCHAR(10),
-- @nearestStormDistance DECIMAL(4,2),
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
	-- precipIntensity,
	-- precipIntensityError,
	-- precipProbability,
	-- precipType,
	-- nearestStormDistance,
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
	-- @precipIntensity,
	-- @precipIntensityError,
	-- @precipProbability,
	-- @precipType,
	-- @nearestStormDistance,
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
    -- wd.precipIntensity, 
    -- wd.precipIntensityError, 
    -- wd.precipProbability, 
    -- wd.precipType, 
    -- wd.nearestStormDistance, 
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
--https://openweathermap.org/weather-conditions#Weather-Condition-Codes-2
INSERT INTO Ref.Codes(ID,Main,Description,Icon)
VALUES
('200','Thunderstorm','thunderstorm with light rain','11d'),
('201','Thunderstorm','thunderstorm with rain','11d'),
('202','Thunderstorm','thunderstorm with heavy rain','11d'),
('210','Thunderstorm','light thunderstorm','11d'),
('211','Thunderstorm','thunderstorm','11d'),
('212','Thunderstorm','heavy thunderstorm','11d'),
('221','Thunderstorm','ragged thunderstorm','11d'),
('230','Thunderstorm','thunderstorm with light drizzle','11d'),
('231','Thunderstorm','thunderstorm with drizzle','11d'),
('232','Thunderstorm','thunderstorm with heavy drizzle','11d'),
('300','Drizzle','light intensity drizzle','09d'),
('301','Drizzle','drizzle','09d'),
('302','Drizzle','heavy intensity drizzle','09d'),
('310','Drizzle','light intensity drizzle rain','09d'),
('311','Drizzle','drizzle rain','09d'),
('312','Drizzle','heavy intensity drizzle rain','09d'),
('313','Drizzle','shower rain and drizzle','09d'),
('314','Drizzle','heavy shower rain and drizzle','09d'),
('321','Drizzle','shower drizzle','09d'),
('500','Rain','light rain','10d'),
('501','Rain','moderate rain','10d'),
('502','Rain','heavy intensity rain','10d'),
('503','Rain','very heavy rain','10d'),
('504','Rain','extreme rain','10d'),
('511','Rain','freezing rain','13d'),
('520','Rain','light intensity shower rain','09d'),
('521','Rain','shower rain','09d'),
('522','Rain','heavy intensity shower rain','09d'),
('531','Rain','ragged shower rain','09d'),
('600','Snow','light snow','13d'),
('601','Snow','Snow','13d'),
('602','Snow','Heavy snow','13d'),
('611','Snow','Sleet','13d'),
('612','Snow','Light shower sleet','13d'),
('613','Snow','Shower sleet','13d'),
('615','Snow','Light rain and snow','13d'),
('616','Snow','Rain and snow','13d'),
('620','Snow','Light shower snow','13d'),
('621','Snow','Shower snow','13d'),
('622','Snow','Heavy shower snow','13d'),
('701','Mist','mist','50d'),
('711','Smoke','Smoke','50d'),
('721','Haze','Haze','50d'),
('731','Dust','sand/ dust whirls','50d'),
('741','Fog','fog','50d'),
('751','Sand','sand','50d'),
('761','Dust','dust','50d'),
('762','Ash','volcanic ash','50d'),
('771','Squall','squalls','50d'),
('781','Tornado','tornado','50d'),
('800','Clear','clear sky','01d'),
('801','Clouds','few clouds: 11-25%','02d'),
('802','Clouds','scattered clouds: 25-50%','03d'),
('803','Clouds','broken clouds: 51-84%','04d'),
('804',	'Clouds','overcast clouds: 85-100%','04d');

  UPDATE [Ref].[Codes]
  SET IconURL = 'http://openweathermap.org/img/wn/'+ icon + '@2x.png'