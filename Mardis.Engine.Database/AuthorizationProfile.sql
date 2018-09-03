CREATE TABLE [MardisSecurity].[AuthorizationProfile]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
	[IdProfile] UNIQUEIDENTIFIER NOT NULL , 
    [IdMenu] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY (Id), 
    CONSTRAINT [FK_AuthorizationProfile_Profile] FOREIGN KEY (IdProfile) REFERENCES MardisSecurity.Profile (Id), 
    CONSTRAINT [FK_AuthorizationProfile_Menu] FOREIGN KEY (IdMenu) REFERENCES MardisSecurity.Menu(Id)
)
