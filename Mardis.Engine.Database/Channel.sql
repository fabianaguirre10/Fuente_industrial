CREATE TABLE [MardisCore].[Channel]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
	[IdCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL,
	CONSTRAINT [FK_Channel_Account] FOREIGN KEY ([IdAccount]) REFERENCES MardisCommon.Account([Id]),
	CONSTRAINT [FK_Channel_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES MardisCore.Customer([Id])
)
