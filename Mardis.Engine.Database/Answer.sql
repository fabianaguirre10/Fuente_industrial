CREATE TABLE [MardisCore].[Answer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL,
	[IdTask] UNIQUEIDENTIFIER NOT NULL,
	[DateCreation] DATE NOT NULL, 
	[IdServiceDetail] uniqueidentifier,
	[IdMerchant] UNIQUEIDENTIFIER NOT NULL, 
	[IdQuestion] UNIQUEIDENTIFIER NOT NULL, 
	[StatusRegister] VARCHAR(20) NOT NULL,
	[sequenceSection] INT NULL, 
    CONSTRAINT [FK_Answer_Account] FOREIGN KEY (IdAccount) REFERENCES MardisCommon.Account(Id),
	CONSTRAINT [FK_Answer_Merchant] FOREIGN KEY (IdMerchant) REFERENCES MardisSecurity.[User](Id),
	CONSTRAINT [FK_Answer_Task] FOREIGN KEY (IdTask) REFERENCES MardisCore.Task(Id),
	CONSTRAINT [FK_Answer_Question] FOREIGN KEY (IdQuestion) REFERENCES MardisCore.Question(Id),
	CONSTRAINT [FK_Answer_ServiceDetail] FOREIGN KEY (IdServiceDetail) REFERENCES MardisCore.ServiceDetail(Id)
)
