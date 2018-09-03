CREATE TABLE [MardisCore].[FilterField]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdFilterController] UNIQUEIDENTIFIER NOT NULL, 
    [IdFilterTable] UNIQUEIDENTIFIER NOT NULL, 
    [Field] VARCHAR(250) NOT NULL, 
    [TypeField] NCHAR(10) NOT NULL, 
    [FieldDescription] VARCHAR(250) NOT NULL, 
    [Visible] VARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_FilterTableDetail_FilterController] FOREIGN KEY (IdFilterController) REFERENCES [MardisCore].[FilterController](Id), 
    CONSTRAINT [FK_FilterField_FilterTable] FOREIGN KEY (IdFilterTable) REFERENCES [MardisCore].[FilterTable](Id)
)
