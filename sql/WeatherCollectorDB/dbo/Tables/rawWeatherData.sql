CREATE TABLE [dbo].[rawWeatherData] (
    [ID]      INT              IDENTITY (1, 1) NOT NULL,
    [runGuid] UNIQUEIDENTIFIER NULL,
    [rawData] NVARCHAR (MAX)   NULL,
    [runID]   INT              NOT NULL, 
    CONSTRAINT [PK_rawWeatherData_ID] PRIMARY KEY ([ID])
);

