CREATE TABLE [MardisCore].[QuestionDetail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdQuestion] UNIQUEIDENTIFIER NOT NULL, 
    [Order] INT NOT NULL, 
    [Weight] INT NOT NULL, 
    [Answer] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IsNext] VARCHAR(20) NOT NULL, 
    [IdQuestionLink] UNIQUEIDENTIFIER NULL, 
    [IdQuestionRequired] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_QuestionDetail_Question] FOREIGN KEY (IdQuestion) REFERENCES [MardisCore].[Question](Id), 
    CONSTRAINT [FK_QuestionDetail_QuestionLink] FOREIGN KEY (IdQuestionLink) REFERENCES [MardisCore].[Question](Id), 
)
