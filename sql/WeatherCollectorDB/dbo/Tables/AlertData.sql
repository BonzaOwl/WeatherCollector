CREATE TABLE [dbo].[AlertData] (
    [runGuid]     UNIQUEIDENTIFIER NOT NULL,
    [title]       VARCHAR (1000)   NULL,
    [description] NVARCHAR (MAX)   NULL,
    [expiry]      DATETIME         NULL,
    [fromtime]    DATETIME         NULL,
    [severity]    VARCHAR (1000)   NULL,
    [runID]       INT              NOT NULL, 
    CONSTRAINT [PK_AlertData_runGuid] PRIMARY KEY ([runGuid])    
);

