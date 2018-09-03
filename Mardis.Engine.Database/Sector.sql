CREATE TABLE [MardisCommon].[Sector]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdDistrict] UNIQUEIDENTIFIER NOT NULL, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_Sector_District] FOREIGN KEY (IdDistrict) REFERENCES MardisCommon.District(Id)
)
