CREATE TABLE [MardisCore].[Sms](
	[id] [uniqueidentifier] NOT NULL,
	[idAccount] [uniqueidentifier] NULL,
	[idCampaign] [uniqueidentifier] NULL,
	[Mensaje] [text] NULL,
	[enviados] [int] NULL,
	[fecha] [datetime] NULL,
	[motivo] [varchar](500) NULL,
	[estado] [varchar](50) NULL,
	[hora_envio] [datetime] NULL,
	PRIMARY KEY (id), 
	CONSTRAINT [FK_Sms_Account] FOREIGN KEY ([idAccount]) REFERENCES MardisCommon.Account([Id]), 
	CONSTRAINT [FK_Sms_Campaign] FOREIGN KEY ([idCampaign]) REFERENCES   MardisCore.Campaign([Id])
)


