CREATE PROC dbo.GetRunID 

AS

BEGIN

    DECLARE @RunID INT

    SELECT @RunID = MAX(RunID) FROM dbo.runData

    SET @RunID = @RunID + 1

    SELECT @RunID as RunID 

END
