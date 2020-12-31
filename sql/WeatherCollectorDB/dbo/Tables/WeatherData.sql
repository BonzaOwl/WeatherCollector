CREATE TABLE [dbo].[WeatherData] (
    [runID]               INT              NOT NULL,
    [runGuid]             UNIQUEIDENTIFIER NULL,
    [sunrise]             DATETIME NULL,
    [sunset]              DATETIME NULL,
    [rain]                DECIMAL (4, 2)   NULL,
    [precipProbability]   DECIMAL (4, 2)   NULL,
    [temperature]         DECIMAL (4, 2)   NULL,
    [apparentTemperature] DECIMAL (4, 2)   NULL,
    [windSpeed]           DECIMAL (4, 2)   NULL,
    [windGust]            DECIMAL (4, 2)   NULL,
    [windBearing]         DECIMAL (5, 2)   NULL,
    [dewPoint]            DECIMAL (4, 2)   NULL,
    [humidity]            DECIMAL (4, 2)   NULL,
    [pressure]            DECIMAL (6, 2)   NULL,
    [cloudCover]          DECIMAL (5, 2)   NULL,
    [uvIndex]             DECIMAL (4, 2)   NULL,
    [visibility]          DECIMAL (7, 2)   NULL,
    [ozone]               DECIMAL (5, 2)   NULL, 
    CONSTRAINT [PK_WeatherData_runID] PRIMARY KEY ([runID])
);

