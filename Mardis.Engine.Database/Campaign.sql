CREATE TABLE [MardisCore].[Campaign]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [Name] VARCHAR(200) NOT NULL, 
	[Code] VARCHAR(100) NOT NULL, 
	[IdCustomer] uniqueidentifier not null,
	[IdChannel] uniqueidentifier not null,
	[IdSupervisor] uniqueidentifier not null,
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [RangeDate] DATETIME NOT NULL, 
	[CreationDate] DATETIME NOT NULL, 
    [Comment] VARCHAR(MAX) NOT NULL, 
	[IdStatusCampaign] UNIQUEIDENTIFIER NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_Campaign_Account] FOREIGN KEY ([IdAccount]) REFERENCES MardisCommon.Account(Id),
	CONSTRAINT [FK_Campaign_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES MardisCore.Customer(Id),
	CONSTRAINT [FK_Customer_StatusCampaign] FOREIGN KEY (IdStatusCampaign) REFERENCES MardisCore.StatusCampaign(Id),
	CONSTRAINT [FK_Campaign_Channel] FOREIGN KEY ([IdChannel]) REFERENCES MardisCore.Channel(Id),
	CONSTRAINT [FK_Customer_Supervisor] FOREIGN KEY (IdSupervisor) REFERENCES MardisSecurity.[User](Id)
)
