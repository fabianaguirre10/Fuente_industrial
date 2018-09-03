CREATE TABLE [MardisCore].[Customer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [Code] NCHAR(10) NOT NULL, 
    [DateCreation] DATE NOT NULL, 
    [Name] VARCHAR(500) NOT NULL, 
    [Abbreviation] VARCHAR(50) NOT NULL, 
    [IdTypeCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [IdStatusCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [Contact] VARCHAR(500) NOT NULL, 
    [Phone] VARCHAR(100) NOT NULL, 
    [Email] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL,
	CONSTRAINT [FK_Customer_Account] FOREIGN KEY (IdAccount) REFERENCES MardisCommon.Account(Id),
	CONSTRAINT [FK_Customer_TypeCustomer] FOREIGN KEY (IdTypeCustomer) REFERENCES MardisCore.TypeCustomer(Id),
	CONSTRAINT [FK_Customer_StatusRegister] FOREIGN KEY (IdStatusCustomer) REFERENCES MardisCore.StatusCustomer(Id)
)
