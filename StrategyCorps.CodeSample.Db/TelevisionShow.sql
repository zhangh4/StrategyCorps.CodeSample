CREATE TABLE [dbo].[TelevisionShow]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [OriginalName] VARCHAR(50) NULL, 
    [Overview] VARCHAR(255) NULL, 
    [Popularity] DECIMAL NULL, 
    [VoteCount] INT NULL, 
    [VoteAverage] DECIMAL NULL, 
    [OriginalLanguage] VARCHAR(50) NULL, 
    [FirstAirDate] DATETIME NULL, 
    [InsertDate] DATETIME NULL
)
