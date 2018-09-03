CREATE TABLE [MardisSecurity].[TypeUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(150) NULL, 
    [StatusRegister] VARCHAR(20) NULL
)
