CREATE TABLE [MardisSecurity].[AuthorizationComponent]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
	[IdTypeUser] UNIQUEIDENTIFIER NOT NULL , 
    [IdComponent] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY (Id), 
    CONSTRAINT [FK_AuthorizationComponent_TypeUser] FOREIGN KEY (IdTypeUser) REFERENCES MardisSecurity.TypeUser, 
    CONSTRAINT [FK_AuthorizationComponent_Component] FOREIGN KEY (IdComponent) REFERENCES MardisSecurity.Component
)
