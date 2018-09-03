CREATE TABLE [MardisCommon].[Province]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdCountry] UNIQUEIDENTIFIER NOT NULL, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Province_Country] FOREIGN KEY (IdCountry) REFERENCES MardisCommon.Country(Id)
)
