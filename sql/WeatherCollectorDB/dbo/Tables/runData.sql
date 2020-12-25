CREATE TABLE [dbo].[runData] (
    [runID]          INT              NOT NULL,
    [runGuid]        UNIQUEIDENTIFIER NOT NULL,
    [runTime]        DATETIME         NOT NULL,
    [longitude]      FLOAT (53)       NULL,
    [latitude]       FLOAT (53)       NULL,
    [timeZone]       NVARCHAR (200)   NULL,
    [timeZoneOffset] NVARCHAR (200)   NULL,
    [invalid]        BIT              NULL DEFAULT 0,
    [deleted]        BIT              NULL DEFAULT 0, 
    CONSTRAINT [PK_runData_runID] PRIMARY KEY ([runID])
);

