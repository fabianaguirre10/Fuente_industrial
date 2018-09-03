CREATE TABLE [MardisSecurity].[User]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Email] VARCHAR(1500) NOT NULL, 
    [Password] VARCHAR(1500) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IdProfile] UNIQUEIDENTIFIER NOT NULL, 
    [IdPerson] UNIQUEIDENTIFIER NOT NULL, 
    [Key] UNIQUEIDENTIFIER NULL, 
    [DateKey] DATETIME NULL, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [InitialPage] VARCHAR(50) NULL, 
    CONSTRAINT [FK_User_Profile] FOREIGN KEY (IdProfile) REFERENCES MardisSecurity.Profile(Id), 
    CONSTRAINT [FK_User_Person] FOREIGN KEY (IdPerson) REFERENCES MardisCommon.Person(Id), 
    CONSTRAINT [FK_User_Account] FOREIGN KEY (IdAccount) REFERENCES MardisCommon.Account(Id)
)
