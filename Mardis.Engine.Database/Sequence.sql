CREATE TABLE [MardisCore].[Sequence]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Code] VARCHAR(20) NOT NULL, 
    [Description] VARCHAR(250) NULL, 
    [Initial] VARCHAR(20) NOT NULL, 
    [SequenceCurrent] INT NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [ControlSequence] INT NOT NULL, 
    [IdAccount] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Sequence_Account] FOREIGN KEY (IdAccount) REFERENCES [MardisCommon].[Account](Id)
)
