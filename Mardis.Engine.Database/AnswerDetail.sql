CREATE TABLE [MardisCore].[AnswerDetail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[IdQuestionDetail] UNIQUEIDENTIFIER NULL,
	[DateCreation] DATE NOT NULL, 
	[IdAnswer] uniqueidentifier NOT NULL,
	[StatusRegister] VARCHAR(20) NOT NULL,
	[CopyNumber] INT NOT NULL DEFAULT 0, 
    [AnswerValue] NVARCHAR(MAX) NULL, 
    [AnswerMultimedia] VARBINARY(MAX) NULL, 
    CONSTRAINT [FK_AnswerDetail_QuestionDetail] FOREIGN KEY (IdQuestionDetail) REFERENCES MardisCore.QuestionDetail(Id),
	CONSTRAINT [FK_AnswerDetail_Answer] FOREIGN KEY (IdAnswer) REFERENCES MardisCore.Answer(Id)
)
