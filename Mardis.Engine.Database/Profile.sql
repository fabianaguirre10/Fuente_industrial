CREATE TABLE [MardisSecurity].[Profile]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(150) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IdTypeUser] UNIQUEIDENTIFIER NOT NULL, 
    [Avatar] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Profile_TypeUser] FOREIGN KEY (IdTypeUser) REFERENCES MardisSecurity.TypeUser
)
