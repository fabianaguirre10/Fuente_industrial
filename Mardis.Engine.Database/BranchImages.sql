CREATE TABLE [MardisCore].[BranchImages]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdBranch] UNIQUEIDENTIFIER NOT NULL, 
	[IdCampaign] UNIQUEIDENTIFIER NULL, 
    [NameContainer] Varchar(100) NOT NULL, 
    [NameFile] Varchar(100) NOT NULL, 
    [UrlImage] VARCHAR(200) NOT NULL,
    [ContentType] VARCHAR(50) NULL, 
    [Order] INT NULL, 
    CONSTRAINT [FK_BranchImages_Branch] FOREIGN KEY (IdBranch) REFERENCES MardisCore.Branch(Id),
	CONSTRAINT [FK_BranchImages_Campaign] FOREIGN KEY (IdCampaign) REFERENCES MardisCore.Campaign(Id)
)
