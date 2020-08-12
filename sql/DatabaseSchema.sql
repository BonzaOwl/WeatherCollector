USE master
GO
CREATE DATABASE Weather;
GO
USE Weather;
GO
CREATE TABLE rawWeatherData
(
	ID INT IDENTITY(1,1),
	runGuid UNIQUEIDENTIFIER,
	rawData NVARCHAR(MAX)
);

GO

CREATE OR ALTER PROC SaveRawWeatherData

@runGuid UNIQUEIDENTIFIER,
@rawData NVARCHAR(MAX)

AS

BEGIN
	
	INSERT INTO rawWeatherData 
	(
		runGuid,
		rawData
	)
	VALUES
	(
		@runGuid,
		@rawData
	)

END;

GO

CREATE TABLE receivedData
(
	runGuid UNIQUEIDENTIFIER,
	runTime DATETIME,
	icon NVARCHAR(50),
	summary NVARCHAR(500),
	RcvWindSpeed varchar(6),
	RcvWindGust varchar(6),
	RcvWindBearing varchar(6),
	RcvTemperature varchar(6),
	RcvApparentTemperature varchar(6),
	RcvprecipIntensity varchar(6),
	RcvprecipIntensityError varchar(60),
	RcvprecipProbability varchar(6),
	precipType varchar(6),
	RcvNearestStormDistance varchar(6),
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

@runGuid UNIQUEIDENTIFIER,
@runTime DATETIME,
@icon NVARCHAR(50),
@summary NVARCHAR(500),
@RcvWindSpeed varchar(6),
@RcvWindGust varchar(6),
@RcvWindBearing varchar(6),
@RcvTemperature varchar(6),
@RcvApparentTemperature varchar(6),
@RcvprecipIntensity varchar(6),
@RcvprecipIntensityError varchar(60),
@RcvprecipProbability varchar(6),
@precipType varchar(6),
@RcvNearestStormDistance varchar(6),
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
	runGuid,
	runTime,
	icon,
	summary,
	RcvWindSpeed,
	RcvWindGust,
	RcvWindBearing,
	RcvTemperature,
	RcvApparentTemperature,
	RcvprecipIntensity,
	RcvprecipIntensityError,
	RcvprecipProbability,
	precipType,
	RcvNearestStormDistance,
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
	@runGuid,
	@runTime,
	@icon,
	@summary,
	@RcvWindSpeed,
	@RcvWindGust,
	@RcvWindBearing,
	@RcvTemperature,
	@RcvApparentTemperature,
	@RcvprecipIntensity,
	@RcvprecipIntensityError,
	@RcvprecipProbability,
	@precipType,
	@RcvNearestStormDistance,
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

CREATE TABLE AlertData
(
	runGuid UNIQUEIDENTIFIER,
	title varchar(1000),
	description NVARCHAR(MAX),
	expiry DATETIME,
	fromtime DATETIME,
	severity varchar(1000)
)

GO

CREATE OR ALTER PROC SaveAlertData

@runGuid UNIQUEIDENTIFIER,
@title varchar(1000),
@description NVARCHAR(MAX),
@expiry DATETIME,
@fromtime DATETIME,
@severity varchar(1000)

AS 

BEGIN

INSERT INTO AlertData
(
	runGuid,
	title,
	description,
	expiry,
	fromtime,
	severity
)
VALUES
(
	@runGuid,
	@title,
	@description,
	@expiry,
	@fromtime,
	@severity
)
END; 

GO

CREATE TABLE WeatherData
(
	runGuid UNIQUEIDENTIFIER,
	precipIntensity DECIMAL(4,2),
	precipIntensityError DECIMAL(4,2),
	precipProbability DECIMAL(4,2),
	precipType NVARCHAR(10),
	nearestStormDistance DECIMAL(4,2),
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

CREATE OR ALTER PROC SaveWeatherData

@runGuid UNIQUEIDENTIFIER,
@precipIntensity DECIMAL(4,2),
@precipIntensityError DECIMAL(4,2),
@precipProbability DECIMAL(4,2),
@precipType NVARCHAR(10),
@nearestStormDistance DECIMAL(4,2),
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

INSERT INTO WeatherData 
(
	runGuid,
	precipIntensity,
	precipIntensityError,
	precipProbability,
	precipType,
	nearestStormDistance,
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
	@runGuid,
	@precipIntensity,
	@precipIntensityError,
	@precipProbability,
	@precipType,
	@nearestStormDistance,
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
TRUNCATE TABLE [dbo].[WeatherData];

END

GO

CREATE OR ALTER VIEW allWeatherData

AS

SELECT  
    rd.runGuid,
    rd.runTime,
    rwd.rawData,
    wd.precipIntensity, 
    wd.precipIntensityError, 
    wd.precipProbability, 
    wd.precipType, 
    wd.nearestStormDistance, 
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