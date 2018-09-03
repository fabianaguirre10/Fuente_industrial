CREATE TABLE [MardisCore].[ServiceDetail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdService] UNIQUEIDENTIFIER NULL, 
    [Order] INT NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [SectionTitle] VARCHAR(250) NOT NULL, 
    [Weight] INT NOT NULL, 
    [HasPhoto] BIT NOT NULL DEFAULT 0, 
	[GroupName] VARCHAR(30) NULL,
	[IdSection] uniqueidentifier null,
	--Campos para secciones dinámicas
	[IsDynamic] BIT NOT NULL DEFAULT 0,
	[NumberOfCopies] int NOT NULL DEFAULT 1,
    CONSTRAINT [FK_ServiceDetail_Service] FOREIGN KEY (IdService) REFERENCES [MardisCore].[Service](Id),
	CONSTRAINT [FK_ServiceDetail_ServiceDetail] FOREIGN KEY (IdSection) REFERENCES [MardisCore].[ServiceDetail](Id)
)
