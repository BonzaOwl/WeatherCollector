
CREATE   PROC [dbo].[SaveReceivedData]

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

