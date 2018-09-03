CREATE TABLE [MardisCore].[Question]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdServiceDetail] UNIQUEIDENTIFIER NOT NULL, 
    [Title] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [Order] INT NOT NULL, 
    [Weight] INT NOT NULL, 
    [IdTypePoll] UNIQUEIDENTIFIER NOT NULL, 
    [HasPhoto] VARCHAR(20) NOT NULL, 
    [CountPhoto] INT NOT NULL, 
    [IdProductCategory] UNIQUEIDENTIFIER NULL, 
    [IdProduct] UNIQUEIDENTIFIER NULL, 
    [AnswerRequired] BIT NOT NULL DEFAULT 0, 
    [sequence] INT NULL, 
    CONSTRAINT [FK_Question_TypePoll] FOREIGN KEY (IdTypePoll) REFERENCES [MardisCore].[TypePoll](Id),
	CONSTRAINT [FK_Question_ServiceDetail] FOREIGN KEY (IdServiceDetail) REFERENCES [MardisCore].[ServiceDetail](Id)
)
