CREATE TABLE [MardisSecurity].[Menu]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(250) NOT NULL, 
    [Icon] VARCHAR(100) NULL, 
    [UrlMenu] VARCHAR(1000) NULL, 
    [IdParent] UNIQUEIDENTIFIER NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [OrderMenu] INT NOT NULL, 
    CONSTRAINT [FK_Menu_Menu] FOREIGN KEY (IdParent) REFERENCES MardisSecurity.Menu
)
