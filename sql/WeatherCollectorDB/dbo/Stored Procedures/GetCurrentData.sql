CREATE PROC GetCurrentData

AS

BEGIN

SELECT TOP 1 
    rawData as CurrentData 
FROM 
    dbo.rawWeatherData 
ORDER BY 
    ID DESC

END