CREATE TABLE [MardisCore].[TypeBusiness]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [IdCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL,
	CONSTRAINT [FK_TypeBusiness_Account] FOREIGN KEY ([IdAccount]) REFERENCES MardisCommon.Account([Id]),
	CONSTRAINT [FK_TypeBusiness_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES MardisCore.Customer([Id])
)
