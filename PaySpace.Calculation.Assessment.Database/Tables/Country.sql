CREATE TABLE [dbo].[Country]
(
	[CountryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Description] VARCHAR(50) NOT NULL, 
    [Code] VARCHAR(3) NOT NULL, 
    [TaxRegimeId] INT NOT NULL,
    CONSTRAINT [FK_Country_TaxRegime] FOREIGN KEY ([TaxRegimeId]) REFERENCES [TaxRegime]([Id])
)
