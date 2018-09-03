CREATE TABLE [MardisCore].[CampaignServices]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IdCampaign] UNIQUEIDENTIFIER NOT NULL, 
	[IdService] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_CampaignServices_Account] FOREIGN KEY ([IdAccount]) REFERENCES MardisCommon.Account(Id), 
    CONSTRAINT [FK_CampaignServices_Campaign] FOREIGN KEY (IdCampaign) REFERENCES MardisCore.Campaign(Id),
	CONSTRAINT [FK_CampaignServices_Service] FOREIGN KEY (IdService) REFERENCES MardisCore.Service(Id)
)
