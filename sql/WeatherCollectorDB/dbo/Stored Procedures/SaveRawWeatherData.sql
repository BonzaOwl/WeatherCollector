
CREATE   PROC [dbo].[SaveRawWeatherData]

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

