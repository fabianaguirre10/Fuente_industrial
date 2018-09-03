CREATE TABLE [dbo].[CodigoReservados]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [idAccount] UNIQUEIDENTIFIER NOT NULL, 
    [code] INT NOT NULL, 
    [estado] CHAR NULL, 
    [uri] VARCHAR(50) NULL, 
    [imei_id] VARCHAR(50) NULL, 
    [codeunico] NCHAR(10) NULL
)
