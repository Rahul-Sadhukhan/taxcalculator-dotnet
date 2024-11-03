CREATE TABLE [dbo].[TaxBracketLine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CountryId] INT NOT NULL,
    [OrderNumber] INT NOT NULL, 
    [LowerLimit] DECIMAL(31, 12) NOT NULL, 
    [UpperLimit] DECIMAL(31, 12) NOT NULL, 
    [Rate] DECIMAL(31, 12) NOT NULL, 
    CONSTRAINT [FK_TaxBracketLine_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([CountryId])
)
