CREATE TABLE [MardisCommon].[District]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IdProvince] UNIQUEIDENTIFIER NOT NULL, 
    [Code] VARCHAR(20) NOT NULL, 
    [Name] VARCHAR(250) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [IdManagement] BIGINT NULL, 
    CONSTRAINT [FK_District_Province] FOREIGN KEY (IdProvince) REFERENCES MardisCommon.Province(Id), 
    CONSTRAINT [FK_District_Management] FOREIGN KEY (IdManagement) REFERENCES MardisCommon.Management(Id)

)
