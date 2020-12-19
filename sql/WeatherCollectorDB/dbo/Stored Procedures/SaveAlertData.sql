
CREATE   PROC [dbo].[SaveAlertData]

@runID INT,
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
    runID,
	runGuid,
	title,
	description,
	expiry,
	fromtime,
	severity
)
VALUES
(
    @runID,
	@runGuid,
	@title,
	@description,
	@expiry,
	@fromtime,
	@severity
)
END; 

