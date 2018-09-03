CREATE TABLE [MardisCore].[FilterCriteria]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdFilterField] UNIQUEIDENTIFIER NOT NULL, 
    [IdTypeFilter] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_FilterCriteria_FilterField] FOREIGN KEY (IdFilterField) REFERENCES [MardisCore].[FilterField](Id), 
    CONSTRAINT [FK_FilterCriteria_TypeFilter] FOREIGN KEY (IdTypeFilter) REFERENCES  [MardisCore].[TypeFilter](Id)
)
