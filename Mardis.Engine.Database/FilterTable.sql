CREATE TABLE [MardisCore].[FilterTable]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdFilterController] UNIQUEIDENTIFIER NOT NULL, 
    [Description] VARCHAR(250) NOT NULL, 
    [TableRelation] VARCHAR(250) NULL, 
    [FieldMainTable] VARCHAR(250) NULL, 
    [FieldRelationTable] VARCHAR(250) NULL, 
    [Visible] VARCHAR(10) NOT NULL, 
    [HasRelation] VARCHAR(10) NOT NULL, 
    [TableInitial] VARCHAR(10) NULL, 
    [HasStatus] NCHAR(10) NOT NULL, 
    [HasAccount] NCHAR(10) NOT NULL 
)
