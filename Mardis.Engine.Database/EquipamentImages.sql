CREATE TABLE [MardisCore].[EquipamentImages](
	[id] INT NOT NULL,
	[IdEquipament] INT NULL,
	[NameContainer] VARCHAR(100) NULL,
	[NameFile] VARCHAR(100) NULL,
	[UrlImage] VARCHAR(100) NULL,
	[IdAccount] UNIQUEIDENTIFIER NULL,
	[ContentType] [datetime] NULL,
	[ORDER] INT NULL, 
    PRIMARY KEY (id), 
	CONSTRAINT [FK_EquipamentImages_Account] FOREIGN KEY ([IdAccount]) REFERENCES MardisCommon.Account([Id]), 

)


