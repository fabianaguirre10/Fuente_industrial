CREATE TABLE [MardisCore].[ProductCategory]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL,
	[IdCustomer] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_ProductCategory_Customer] FOREIGN KEY (IdCustomer) REFERENCES MardisCore.Customer(Id)
)
