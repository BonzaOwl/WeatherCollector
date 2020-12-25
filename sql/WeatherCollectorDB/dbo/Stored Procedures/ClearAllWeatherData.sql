
CREATE   PROC ClearAllWeatherData

AS

BEGIN

TRUNCATE TABLE [dbo].[rawWeatherData];
TRUNCATE TABLE [dbo].[WeatherData];

END