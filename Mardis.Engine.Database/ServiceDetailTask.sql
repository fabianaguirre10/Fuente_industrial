CREATE TABLE [MardisCore].[ServiceDetailTask]
(
    [Id] UNIQUEIDENTIFIER NOT NULL, 
	[IdServiceDetail] UNIQUEIDENTIFIER NOT NULL , 
    [IdTask] UNIQUEIDENTIFIER NOT NULL, 
    [StatusRegister] VARCHAR(10) NOT NULL, 
    PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ServiceDetailTask_ServiceDetail] FOREIGN KEY ([IdServiceDetail]) REFERENCES MardisCore.ServiceDetail([Id]), 
    CONSTRAINT [FK_ServiceDetailTask_Task] FOREIGN KEY ([IdTask]) REFERENCES MardisCore.Task([Id])
)
