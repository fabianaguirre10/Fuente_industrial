CREATE TABLE [MardisCore].[BulkLoadCatalog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [ColumnNumber] INT NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [Separator] VARCHAR NOT NULL, 
    [Code] VARCHAR(20) NOT NULL
)
