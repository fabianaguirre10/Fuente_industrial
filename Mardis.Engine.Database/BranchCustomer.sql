CREATE TABLE [MardisCore].[BranchCustomer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdBranch] UNIQUEIDENTIFIER NOT NULL, 
    [IdCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [IdTypeBusiness] UNIQUEIDENTIFIER NOT NULL, 
    [IdChannel] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_BranchCustomer_Branch] FOREIGN KEY (IdBranch) REFERENCES MardisCore.Branch(Id), 
    CONSTRAINT [FK_BranchCustomer_TypeBusiness] FOREIGN KEY (IdTypeBusiness) REFERENCES MardisCore.TypeBusiness(Id),
	CONSTRAINT [FK_BranchCustomer_Customer] FOREIGN KEY (IdCustomer) REFERENCES MardisCore.Customer(Id),
	CONSTRAINT [FK_BranchCustomer_Channel] FOREIGN KEY (IdChannel) REFERENCES MardisCore.Channel(Id)
)
