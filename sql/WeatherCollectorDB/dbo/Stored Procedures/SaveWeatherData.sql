
CREATE PROC [dbo].[SaveWeatherData]

@runID INT,
@runGuid UNIQUEIDENTIFIER,
@rain DECIMAL(4,2),
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
@visibility DECIMAL(7,2),
@ozone DECIMAL(5,2)

AS

BEGIN

INSERT INTO WeatherData 
(
    runID,
	runGuid,
    rain,
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
    @rain,
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

