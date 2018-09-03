CREATE TABLE [MardisCommon].[Management]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [IdRegion] BIGINT NOT NULL, 
    CONSTRAINT [FK_Management_Region] FOREIGN KEY (IdRegion) REFERENCES MardisCommon.Region(Id)
)
