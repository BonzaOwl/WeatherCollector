USE master
GO

CREATE DATABASE weatherCollector;
GO

USE weatherCollector;
GO

CREATE SCHEMA ref AUTHORIZATION dbo

GO

CREATE TABLE runData
(
    runID INT NOT NULL,
    runGuid UNIQUEIDENTIFIER NOT NULL,
    runTime DATETIME NOT NULL,
	units varchar(10),
    longitude FLOAT NULL,
    latitude FLOAT NULL,
    timeZone nvarchar(200),
    timeZoneOffset nvarchar(200),
	invalid BIT DEFAULT 0,
	deleted BIT DEFAULT 0
);

ALTER TABLE runData ADD CONSTRAINT PK_runData_runID PRIMARY KEY (runID);

GO

CREATE TABLE rawWeatherData
(
	ID INT NOT NULL IDENTITY(1,1),
	runID INT NOT NULL,
	runGuid UNIQUEIDENTIFIER,
	rawData NVARCHAR(MAX)
);

ALTER TABLE rawWeatherData ADD CONSTRAINT PK_rawWeatherData_ID PRIMARY KEY(ID);
ALTER TABLE rawWeatherData ADD CONSTRAINT FK_rawWeatherData_runID FOREIGN KEY(runID) REFERENCES runData(runID);

GO

CREATE TABLE weatherData
(
	ID INT NOT NULL IDENTITY(1,1),
    runID INT NOT NULL,
    runGuid UNIQUEIDENTIFIER NOT NULL,
    collectionID INT,
    summary varchar(100),
    description varchar(100),
    icon varchar(20)    
);

GO

ALTER TABLE weatherData ADD CONSTRAINT PK_weatherData_ID PRIMARY KEY (ID);
ALTER TABLE weatherData ADD CONSTRAINT FK_weatherData_runID FOREIGN KEY(runID) REFERENCES runData(runID);

GO

CREATE TABLE currentlyData
(
	ID INT NOT NULL IDENTITY(1,1),
	runID INT NOT NULL,
	runGuid UNIQUEIDENTIFIER,
	temperature DECIMAL(4,2),
	apparentTemperature DECIMAL(4,2),
	windSpeed DECIMAL(4,2),
	windGust DECIMAL(4,2),
	windBearing DECIMAL(5,2),
	dewPoint DECIMAL(4,2),
	humidity DECIMAL(4,2),
	pressure DECIMAL(6,2),
	cloudCover DECIMAL(4,2),
	uvIndex DECIMAL(4,2),
	visibility DECIMAL(4,2),
	ozone DECIMAL(5,2)
);

GO

ALTER TABLE currentlyData ADD CONSTRAINT PK_currentlyData_ID PRIMARY KEY(ID);
ALTER TABLE weatherData ADD CONSTRAINT FK_currentlyData_runID FOREIGN KEY(runID) REFERENCES runData(runID);

GO

CREATE TABLE Ref.Codes
(
    ID INT NOT NULL,
    Main varchar(100),
    Description varchar(200),
    Icon CHAR(3),
	IconURL nvarchar(100)
);

ALTER TABLE ref.Codes ADD CONSTRAINT PK_RefCodes_ID PRIMARY KEY(ID);