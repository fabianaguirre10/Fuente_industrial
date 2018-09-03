CREATE TABLE [MardisCore].[FilterController]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[NameController] VARCHAR(250) NOT NULL,
    [NameTable] VARCHAR(250) NOT NULL,  
    [HasStatus] VARCHAR(20) NOT NULL, 
    [HasAccount] VARCHAR(20) NOT NULL, 
    CONSTRAINT [AK_FilterController_NameController] UNIQUE (NameController) 
)  
GO
