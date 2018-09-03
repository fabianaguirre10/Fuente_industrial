CREATE TABLE [MardisSecurity].[Component]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(150) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL
)
