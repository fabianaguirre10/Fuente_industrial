CREATE TABLE [MardisCore].[UserCanpaign]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [idCanpaign] UNIQUEIDENTIFIER NOT NULL, 
	[idUser] UNIQUEIDENTIFIER NOT NULL, 
    [status] VARCHAR(200) NOT NULL,  
    CONSTRAINT [FK_UserCanpaign_Campaign] FOREIGN KEY ([idCanpaign]) REFERENCES MardisCore.Campaign(Id),
	CONSTRAINT [FK_UserCanpaign_User] FOREIGN KEY (idUser) REFERENCES MardisSecurity.[User](Id)
)
