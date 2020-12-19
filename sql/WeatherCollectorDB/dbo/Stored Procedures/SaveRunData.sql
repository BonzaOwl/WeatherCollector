CREATE   PROC SaveRunData

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
    (@runID,
    @runGuid,
    @runTime,
    @longitude,
    @latitude,
    @timeZone,
    @timeZoneOffset)

END