CREATE TABLE [MardisCommon].[Person]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(500) NOT NULL, 
    [SurName] VARCHAR(500) NOT NULL, 
    [TypeDocument] VARCHAR(20) NOT NULL, 
    [Document] VARCHAR(50) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Mobile] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Person_Account] FOREIGN KEY (IdAccount) REFERENCES MardisCommon.Account(Id)
)
