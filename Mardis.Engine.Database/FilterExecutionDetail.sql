CREATE TABLE [MardisCore].[FilterExecutionDetail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdFilterExecution] UNIQUEIDENTIFIER NOT NULL, 
    [IdFilterCriteria] UNIQUEIDENTIFIER NOT NULL, 
    [Value] VARCHAR(2000) NOT NULL, 
    [CreationFilter] DATETIME NOT NULL, 
    CONSTRAINT [FK_FilterExecutionDetail_Master] FOREIGN KEY (IdFilterExecution) REFERENCES [MardisCore].[FilterExecution](Id), 
    CONSTRAINT [FK_FilterExecutionDetail_Criteria] FOREIGN KEY (IdFilterCriteria) REFERENCES [MardisCore].[FilterCriteria](Id)
)
