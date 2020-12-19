CREATE TABLE [dbo].[runData] (
    [runID]          INT              NOT NULL,
    [runGuid]        UNIQUEIDENTIFIER NOT NULL,
    [runTime]        DATETIME         NOT NULL,
    [longitude]      FLOAT (53)       NULL,
    [latitude]       FLOAT (53)       NULL,
    [timeZone]       NVARCHAR (200)   NULL,
    [timeZoneOffset] NVARCHAR (200)   NULL,
    [invalid]        BIT              NULL,
    [deleted]        BIT              NULL, 
    CONSTRAINT [PK_runData_runID] PRIMARY KEY ([runID])
);

