﻿CREATE TABLE [MardisCore].[BulkLoadStatus]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [StatusRegister] VARCHAR(20) NOT NULL, 
    [Code] VARCHAR(20) NOT NULL
)
