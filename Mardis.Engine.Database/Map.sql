CREATE TABLE [MardisCommon].[Map](
	[id] [uniqueidentifier] NOT NULL,
	[idAccount] [uniqueidentifier] NULL,
	[scr] VARCHAR(500) NULL,
	[status] CHAR(1) NULL
	PRIMARY KEY (id), 
	CONSTRAINT [FK_Map_Account] FOREIGN KEY ([idAccount]) REFERENCES MardisCommon.Account([Id])
	
)


