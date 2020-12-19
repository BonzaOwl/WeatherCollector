
CREATE   PROC ClearAllWeatherData

AS

BEGIN

TRUNCATE TABLE [dbo].[AlertData];
TRUNCATE TABLE [dbo].[rawWeatherData];
TRUNCATE TABLE [dbo].[receivedData];
TRUNCATE TABLE [dbo].[WeatherData];

END