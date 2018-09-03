CREATE TABLE [MardisCore].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL,
	[IdProductCategory] UNIQUEIDENTIFIER NOT NULL,
	[IdAccount] UNIQUEIDENTIFIER NOT NULL, 
	[IdCustomer] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_Product_Account] FOREIGN KEY (IdAccount) REFERENCES MardisCommon.Account(Id),
	CONSTRAINT [FK_Product_Customer] FOREIGN KEY (IdCustomer) REFERENCES MardisCore.Customer(Id),
	CONSTRAINT FK_Product_ProductCategory FOREIGN KEY (IdProductCategory) REFERENCES MardisCore.ProductCategory(Id)
)
