CREATE TABLE [MardisCore].[FilterExecution]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdFilterController] UNIQUEIDENTIFIER NOT NULL, 
    [DateInit] DATETIME NULL, 
    [DateEnd] DATETIME NULL, 
    [IdUser] UNIQUEIDENTIFIER NOT NULL, 
    [LastExecution] DATETIME NULL, 
    CONSTRAINT [FK_FilterExecution_User] FOREIGN KEY (IdUser) REFERENCES [MardisSecurity].[User](Id) 
)
