CREATE TABLE [MardisCore].[Service]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [IdTypeService] UNIQUEIDENTIFIER NOT NULL, 
    [PollTitle] VARCHAR(250) NOT NULL, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [IdCustomer] UNIQUEIDENTIFIER NOT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IdChannel] UNIQUEIDENTIFIER NOT NULL, 
    [Icon] VARCHAR(50) NULL, 
    [IconColor] VARCHAR(50) NULL, 
    [Template] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Service_TypeService] FOREIGN KEY (IdTypeService) REFERENCES [MardisCore].[TypeService](Id), 
    CONSTRAINT [FK_Service_Account] FOREIGN KEY (IdAccount) REFERENCES [MardisCommon].[Account](Id), 
    CONSTRAINT [FK_Service_Channel] FOREIGN KEY (IdChannel) REFERENCES [MardisCore].[Channel](Id)
)
