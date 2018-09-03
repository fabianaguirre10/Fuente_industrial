CREATE TABLE [MardisCore].[BulkLoad]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FileName] VARCHAR(50) NOT NULL, 
    [ContainerName] VARCHAR(50) NOT NULL, 
    [IdBulkLoadStatus] UNIQUEIDENTIFIER NOT NULL, 
    [IdBulkLoadCatalog] UNIQUEIDENTIFIER NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL , 
    [TotalAdded] INT NULL, 
    [TotalUpdated] INT NULL, 
    [TotalFailed] INT NULL, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [CurrentFile] INT NULL, 
    [TotalRegister] INT NOT NULL, 
    CONSTRAINT [FK_BulkLoad_BulkLoadStatus] FOREIGN KEY ([IdBulkLoadStatus]) REFERENCES MardisCore.BulkLoadStatus([Id]), 
    CONSTRAINT [FK_BulkLoad_BulkLoadCatalog] FOREIGN KEY ([IdBulkLoadCatalog]) REFERENCES MardisCore.BulkLoadCatalog([Id]) 
)
