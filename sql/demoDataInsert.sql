DECLARE @guid UNIQUEIDENTIFIER,@runID INT,@date DATETIME,@maxID INT 
SET @guid = NEWID()
SET @runID = 1
SET @maxID = 50

WHILE @runID <= @maxID

BEGIN

    SET @date = DATEADD(DAY,1,GETUTCDATE()) 
    SET @date = DATEADD(hh,@runID,@date)

    INSERT INTO 
        [dbo].[runData] 
                        (
                            [runID], 
                            [runGuid], 
                            [runTime], 
                            [longitude], 
                            [latitude], 
                            [timeZone], 
                            [timeZoneOffset]
                        )
    VALUES
                        (
                            @runID,
                            @guid,
                            @date,
                            40.7127,
                            -74.0059,
                            'America/Chicago',
                            -18000
                        )
    INSERT INTO 
        [dbo].[currentlyData] (
                                runID,
                                runGuid,
                                [temperature], 
                                [apparentTemperature], 
                                [windSpeed], 
                                [windGust], 
                                [windBearing], 
                                [dewPoint], 
                                [humidity], 
                                [pressure], 
                                [cloudCover], 
                                [uvIndex], 
                                [visibility], 
                                [ozone]
                              )
    SELECT 
                                @runID, 
                                @guid,
                                [temperature], 
                                [apparentTemperature], 
                                [windSpeed], 
                                [windGust], 
                                [windBearing], 
                                [dewPoint], 
                                [humidity], 
                                [pressure], 
                                [cloudCover], 
                                [uvIndex], 
                                [visibility], 
                                [ozone] 
    FROM 
        [Weather].[dbo].[WeatherData] 

    INSERT INTO 
        rawWeatherData 
                        (
                            [runID], 
                            [runGuid],
                            rawData
                        )
    SELECT 
                            @runID, 
                            @guid,
                            [rawData] 
    FROM 
        [Weather].[dbo].[rawWeatherData]

    SET @runID = @runID + 1

END