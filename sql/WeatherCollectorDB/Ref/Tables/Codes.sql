CREATE TABLE [Ref].[Codes] (
    [ID]          INT            NOT NULL, 
    [Main]        VARCHAR (100)  NULL,
    [Description] VARCHAR (200)  NULL,
    [Icon]        CHAR (3)       NULL,
    [IconURL]     NVARCHAR (100) NULL, 
    CONSTRAINT [PK_Codes_ID] PRIMARY KEY ([ID])
);
