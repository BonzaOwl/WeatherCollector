CREATE PROC dbo.ClearDownDatabase

AS

BEGIN

    BEGIN TRANSACTION 

        BEGIN TRY
            
            TRUNCATE TABLE dbo.rawWeatherData;
            TRUNCATE TABLE dbo.WeatherData;
            TRUNCATE TABLE dbo.runData;

        COMMIT TRANSACTION

        END TRY

        BEGIN CATCH

        ROLLBACK TRANSACTION

        END CATCH
END