BEGIN 
DECLARE @TypeUserId uniqueidentifier
DECLARE @ProfileId uniqueidentifier
DECLARE @ProfileIdMercaderista uniqueidentifier
DECLARE @ProfileIdSupervisor uniqueidentifier
DECLARE @AccountId uniqueidentifier
DECLARE @PersonId uniqueidentifier
DECLARE @PersonIdMercaderista uniqueidentifier
DECLARE @PersonIdSupervisor uniqueidentifier
DECLARE @MenuId uniqueidentifier
DECLARE @MenuParentId uniqueidentifier
DECLARE @FilterControllerId uniqueidentifier
DECLARE @FilterTableId uniqueidentifier
DECLARE @FilterFieldId uniqueidentifier
DECLARE @TypeFilterEqual uniqueidentifier
DECLARE @TypeFilterContain uniqueidentifier
DECLARE @FilterExecutionId uniqueidentifier
DECLARE @FilterCriteriaId uniqueidentifier
DECLARE @TypeServiceId uniqueidentifier

SET @TypeUserId = NEWID()
SET @ProfileId = NEWID()
SET @AccountId = NEWID()
SET @PersonId = NEWID()
SET @PersonIdMercaderista = NEWID()
SET @PersonIdSupervisor = NEWID()

--ACCOUNT
INSERT INTO  MardisCommon.Account (Id,Code,Name,StatusRegister)
VALUES(@AccountId,'MARDIS','Mardis CIA Ltda','A')

--TIPO DE ENCUESTA
INSERT INTO MardisCore.TypePoll (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'MANY','Opción Multiple','A');
INSERT INTO MardisCore.TypePoll (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'ONE','Una Selección','A');
INSERT INTO MardisCore.TypePoll (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'OPEN','Abierta','A');
--
INSERT INTO MardisSecurity.TypeUser ( Id,Code,Name,StatusRegister)
VALUES(@TypeUserId,'S','Sistema','A');
--PROFILE
INSERT INTO MardisSecurity.Profile (Id,Code,Name,StatusRegister,IdTypeUser)
VALUES(@ProfileId,'A','Admin','A',@TypeUserId);

SET @TypeUserId = NEWID()
SET @ProfileIdMercaderista = NEWID()

INSERT INTO MardisSecurity.TypeUser ( Id,Code,Name,StatusRegister)
VALUES(@TypeUserId,'M','Mercaderista','A');
--PROFILE
INSERT INTO MardisSecurity.Profile (Id,Code,Name,StatusRegister,IdTypeUser)
VALUES(@ProfileIdMercaderista,'M','Mercaderista Estandar','A',@TypeUserId);
--PEOPLE
INSERT INTO MardisCommon.Person (Id,IdAccount,Code,Name,SurName,TypeDocument,Document,StatusRegister)
VALUES(@PersonIdMercaderista,@AccountId,'MERC','Mercaderista','Estandar','CI','','A')
--USER
INSERT INTO [MardisSecurity].[User] (Id,Email,Password,StatusRegister,IdProfile,IdPerson,IdAccount)
VALUES(NEWID(),'mercaderista@mardis.com.ec','AQAAAAEAACcQAAAAENoVpY6g5jNxIixUGwWABxUJ5KHE8IY5WucV9esNYwIw/DTDVAumKUvz27RCB1cRvg==','A',@ProfileIdMercaderista,@PersonIdMercaderista,@AccountId)

SET @TypeUserId = NEWID()
SET @ProfileIdSupervisor = NEWID()

INSERT INTO MardisSecurity.TypeUser ( Id,Code,Name,StatusRegister)
VALUES(@TypeUserId,'S','Supervisor','A');
--PROFILE
INSERT INTO MardisSecurity.Profile (Id,Code,Name,StatusRegister,IdTypeUser)
VALUES(@ProfileIdSupervisor,'S','Supervisor Estandar','A',@TypeUserId);
--PEOPLE
INSERT INTO MardisCommon.Person (Id,IdAccount,Code,Name,SurName,TypeDocument,Document,StatusRegister)
VALUES(@PersonIdSupervisor,@AccountId,'SUP','Supervisor','Estandar','CI','','A')
--USER
INSERT INTO [MardisSecurity].[User] (Id,Email,Password,StatusRegister,IdProfile,IdPerson,IdAccount)
VALUES(NEWID(),'supervisor@mardis.com.ec','AQAAAAEAACcQAAAAENoVpY6g5jNxIixUGwWABxUJ5KHE8IY5WucV9esNYwIw/DTDVAumKUvz27RCB1cRvg==','A',@ProfileIdSupervisor,@PersonIdSupervisor,@AccountId)

--PEOPLE
INSERT INTO MardisCommon.Person (Id,IdAccount,Code,Name,SurName,TypeDocument,Document,StatusRegister)
VALUES(@PersonId,@AccountId,'ADMIN','Administrador','Mardis','CI','','A')
--USER
INSERT INTO [MardisSecurity].[User] (Id,Email,Password,StatusRegister,IdProfile,IdPerson,IdAccount)
VALUES(NEWID(),'admin@mardis.com.ec','AQAAAAEAACcQAAAAENoVpY6g5jNxIixUGwWABxUJ5KHE8IY5WucV9esNYwIw/DTDVAumKUvz27RCB1cRvg==','A',@ProfileId,@PersonId,@AccountId)
--MENU Y AUTORIZACION
--Home
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Home','clip-home','/Home/DashBoard','A',1);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Locales
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Locales','clip-user-3','/Branch','A',2);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Clientes
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Clientes','fa fa-building','/Customer','A',3);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Tareas
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Tareas','fa fa-tasks','/Task','A',4);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdMercaderista,@MenuId);
--Campañas
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Campañas','fa fa-bullhorn','/Campaign','A',5);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Servicios
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Servicios','clip-folder-open','/Service','A',6);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Analitics
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,StatusRegister,OrderMenu)
VALUES(@MenuId,'Analitics','clip-bars','A',7);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Seguridad
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,StatusRegister,OrderMenu)
VALUES(@MenuId,'Seguridad','fa fa-lock','A',8);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Mantenimiento
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,StatusRegister,OrderMenu)
VALUES(@MenuId,'Mantenimiento','clip-wrench','A',9);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Catálogo de Mantenimiento
SET @MenuParentId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu,IdParent)
VALUES(@MenuParentId,'Catálogo',null,'/Catalog','A',1,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuParentId);
--Reportes
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,StatusRegister,OrderMenu)
VALUES(@MenuId,'Reportes','clip-stats','A',10);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Carga Masiva
SET @MenuId = NEWID();
INSERT INTO MardisSecurity.Menu (Id,Name,Icon,UrlMenu,StatusRegister,OrderMenu)
VALUES(@MenuId,'Carga Masiva','fa fa-upload','/BulkLoad','A',11);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileId,@MenuId);
INSERT INTO MardisSecurity.AuthorizationProfile (Id,IdProfile,IdMenu)
VALUES(NEWID(),@ProfileIdSupervisor,@MenuId);
--Tipos de Filtros
SET @TypeFilterEqual = NEWID();
INSERT INTO MardisCore.TypeFilter (Id,SignFilter,Name)
VALUES(@TypeFilterEqual,'=','Igual');
SET @TypeFilterContain = NEWID();
INSERT INTO MardisCore.TypeFilter (Id,SignFilter,Name)
VALUES(@TypeFilterContain,'IN','Contiene');
--Filtro de Locales
SET @FilterControllerId = NEWID();
--Controlador
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'BranchController','MardisCore.Branch','S','S');
--Tabla de Cantones
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Cantones','S','S','MardisCommon.District','IdDistrict','Id','di','S','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Code','String','Código de Cantón','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterContain);
--Fin Tabla
--Tabla de Provincias
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Provincias','S','S','MardisCommon.Province','IdProvince','Id','pr','N','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Code','String','Código de Provincia','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterContain);
--Fin Tabla

--Filtro de Clientes
SET @FilterControllerId = NEWID();
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'CustomerController','MardisCore.Customer','S','S');
--Búsqueda Simple
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Búsqueda Simple','S','N','N','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Code','String','Código de Cliente','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
--Fin Tabla
--Poner Relacion
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Tipo de Cliente','N','S','MardisCore.TypeCustomer','IdTypeCustomer','Id','tc','S','N');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre de Tipo de Cliente','N');
--Fin
--Filtro Campañas
SET @FilterControllerId = NEWID();
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'CampaignController','MardisCore.Campaign','S','S');
--Búsqueda Simple
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Búsqueda Simple','S','N','N','N');
--Fin Filtros
--Filtro de Productos
SET @FilterControllerId = NEWID();
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'ProductController','MardisCore.Product','S','S');
--Búsqueda Simple
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Búsqueda Simple','S','N','N','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Code','String','Código de Producto','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
--Fin Tabla
--Poner Relacion Categorias
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Categoría de Producto','N','S','MardisCore.ProductCategory','IdProductCategory','Id','a','S','S');
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre de Categoría de Producto','N');
--Fin
--Fin Poner Relacion
--Fin filtro productos
--Filtro Tareas
SET @FilterControllerId = NEWID();
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'TaskController','MardisCore.Task','S','S');
--Búsqueda Simple
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Búsqueda Simple','S','N','N','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Code','String','Código de Tarea','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
--Fin Tabla
--Poner Relacion Locales
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Local de Tarea','N','S','MardisCore.Branch','IdBranch','Id','a','S','S');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre de Campaña','N');
--Fin
--Poner Relacion Status
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Status de Tarea','N','S','MardisCore.StatusTask','IdStatusTask','Id','b','S','N');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre del Status','N');
--Fin
--Poner Relacion Mercaderista Usuario
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Mercaderista asociado','N','S','MardisSecurity.[User]','IdMerchant','Id','c','S','S');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Email','String','Nombre del Mercaderista','N');
--Fin
--Poner Relacion Mercaderista Profile
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Perfil de Usuario','N','S','MardisSecurity.Profile','c.IdProfile','Id','d','S','S');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre del Mercaderista','N');
--Fin
--Filtro Carga Masiva
SET @FilterControllerId = NEWID();
INSERT INTO MardisCore.FilterController (Id,NameController,NameTable,HasStatus,HasAccount)
VALUES(@FilterControllerId,'BulkLoadController','MardisCore.BulkLoad','S','N');
--Búsqueda Simple
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Búsqueda Simple','S','N','N','N');
--
SET @FilterFieldId = NEWID();
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(@FilterFieldId,@FilterControllerId,@FilterTableId,'Name','String','Nombre del Archivo','S');
--
INSERT INTO MardisCore.FilterCriteria(Id,IdFilterField,IdTypeFilter)
VALUES(NEWID(),@FilterFieldId,@TypeFilterEqual);
--Fin Tabla
--Poner Relacion Tipo de Carga Masiva
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Tipo de Carga Masiva','N','S','MardisCore.BulkLoadCatalog','IdBulkLoadCatalog','Id','a','S','S');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre de Tipo','N');
--Fin
--Poner Relacion Status
SET @FilterTableId = NEWID();
INSERT INTO MardisCore.FilterTable(Id,IdFilterController,Description,Visible,HasRelation,TableRelation,FieldMainTable,FieldRelationTable,TableInitial,HasStatus,HasAccount)
VALUES(@FilterTableId,@FilterControllerId,'Status de Carga Masiva','N','S','MardisCore.BulkLoadStatus','IdBulkLoadStatus','Id','b','S','N');
--Fin Poner Relacion
--Poner columnas en la tabla de relacion
INSERT INTO MardisCore.FilterField (Id,IdFilterController,IdFilterTable,Field,TypeField,FieldDescription,Visible)
VALUES(NEWID(),@FilterControllerId,@FilterTableId,'Name','String','Nombre del Status','N');
--Fin
--Fin filtro caega Masiva
--Fin Filtros
--StatusCustomer
INSERT INTO MardisCore.StatusCustomer (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'A','Activo','A');
INSERT INTO MardisCore.StatusCustomer (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'I','InActivo','A');
--Type Customer
INSERT INTO MardisCore.TypeCustomer (Id,Code,Name,StatusRegister)
VALUES(NEWID(),'T','Tipo de Cliente','A');

--SECUENCIALES POR CUENTA
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_CUS','CL',1,1,'A');
--SECUENCIALES POR BRANCH
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_BR','LOC',1,1,'A');
--SECUENCIALES POR PERSON
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_PER','PER',1,1,'A');
--SECUENCIALES POR CAMPAIGN
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_CMP','CMP',1,1,'A');
--SECUENCIALES POR SERVICIOS
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_SER','SER',1,1,'A');
--SECUENCIALES POR TAREAS
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_TSK','TAR',1,1,'A');
--SECUENCIALES POR PRODUCTOS
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_PROD','PROD',1,1,'A');
--SECUENCIALES POR CATEGORIA DE PRODUCTOS
INSERT INTO MardisCore.Sequence (Id,IdAccount,Code,Initial,SequenceCurrent,ControlSequence,StatusRegister)
VALUES(NEWID(),@AccountId,'SEQ_PRCT','PRCT',1,1,'A');
--Tipo de Servicios
set @TypeServiceId = NEWID()
INSERT INTO MardisCore.TypeService (Id,Code,Name,StatusRegister)
VALUES(@TypeServiceId,'ECU','Encuesta','A');
--
--Estados de Tareas
INSERT INTO MardisCore.StatusTask(Id,Code,Name,StatusRegister)
VALUES(NEWID(),'P','Pendiente','A');
INSERT INTO MardisCore.StatusTask(Id,Code,Name,StatusRegister)
VALUES(NEWID(),'S','Iniciado','A');
INSERT INTO MardisCore.StatusTask(Id,Code,Name,StatusRegister)
VALUES(NEWID(),'I','Implementado','A');
INSERT INTO MardisCore.StatusTask(Id,Code,Name,StatusRegister)
VALUES(NEWID(),'N','No Implementado','A');
--Estado de Campaña
INSERT INTO MardisCore.StatusCampaign(Id,Code,Name,StatusRegister) VALUES
(NEWID(),'A','Activo','A'),
(NEWID(),'I','Inactivo','A'),
(NEWID(),'P','En Pausa','A')
--
--Motivos de Tareas no implementadas
INSERT INTO MardisCore.TaskNoImplementedReason(Id,Code,Name,StatusRegister) VALUES
(NEWID(), 'A', 'Local cerrado','A'),
(NEWID(), 'B', 'No colabora','A'),
(NEWID(), 'C', 'Local no existe','A')
--Catalogo de Carga Masiva
INSERT INTO [MardisCore].[BulkLoadCatalog] (Id,Code, Name, ColumnNumber, StatusRegister, Separator) VALUES
(NEWID(),'BBRANCH', 'Locales', 31, 'A', '|')
--Status de Carga Masiva
INSERT INTO [MardisCore].[BulkLoadStatus] (Id,Code, Name, StatusRegister)VALUES
(NEWID(),'P', 'Pendiente', 'A'),
(NEWID(),'E', 'En Proceso', 'A'),
(NEWID(),'A', 'Aceptado', 'A'),
(NEWID(),'R', 'Rechazado', 'A')

--ÍNDICES
 create nonclustered index IX_ANSWER_TASKSERVICEDETAIL
 on MARDISCORE.ANSWER(IDTASK, IDSERVICEDETAIL);
  create nonclustered index IX_ANSWERDETAIL_ANSWERCOPYNUMBER
 on MARDISCORE.ANSWERDETAIL(COPYNUMBER, IDANSWER);
  create nonclustered index IX_BRANCH_EXTERNALCODE
 on MARDISCORE.BRANCH(EXTERNALCODE);
  create nonclustered index IX_TASK_MERCHANTSTATUSTASK
 on MARDISCORE.TASK(IDMERCHANT, IDSTATUSTASK);

CREATE NONCLUSTERED INDEX [IX_Task_Select_All]
ON [MardisCore].[Task] ([DateModification])
INCLUDE ([IdAccount],[Code],[DateCreation],[IdCampaign],[StartDate],[Description],[IdBranch],[IdMerchant],[StatusRegister],[IdStatusTask],[CommentTaskNoImplemented],[IdTaskNoImplementedReason],[Route])

CREATE NONCLUSTERED INDEX [IX_TASK_IDMERCHANT]
ON [MardisCore].[Task] ([IdMerchant])
INCLUDE ([IdCampaign],[IdBranch],[IdStatusTask],[Route],[DateModification])

CREATE NONCLUSTERED INDEX [IX_TASK_IDACCOUNT]
ON [MardisCore].[Task] ([IdAccount],[IdBranch],[IdMerchant])

CREATE NONCLUSTERED INDEX IX_ANSWERDETAIL_ANSWER
ON [MardisCore].[AnswerDetail] ([IdAnswer])
INCLUDE ([Id],[IdQuestionDetail],[DateCreation],[StatusRegister],[CopyNumber],[AnswerValue])

CREATE NONCLUSTERED INDEX IX_Task_Account
ON [MardisCore].[Task] ([IdAccount])
INCLUDE ([StatusRegister],[IdStatusTask])

CREATE NONCLUSTERED INDEX IX_TASK_MERCHANT
ON [MardisCore].[Task] ([IdMerchant])
INCLUDE ([Id],[IdAccount],[Code],[DateCreation],[IdCampaign],[StartDate],[Description],[IdBranch],[StatusRegister],[IdStatusTask],[CommentTaskNoImplemented],[IdTaskNoImplementedReason],[Route],[DateModification],[ExternalCode])

CREATE NONCLUSTERED INDEX IX_TASK_MERCHANT_STATUS
ON [MardisCore].[Task] ([IdMerchant],[IdStatusTask])
INCLUDE ([Id],[IdAccount],[Code],[DateCreation],[IdCampaign],[StartDate],[Description],[IdBranch],[StatusRegister],[CommentTaskNoImplemented],[IdTaskNoImplementedReason],[Route],[DateModification],[ExternalCode])

CREATE NONCLUSTERED INDEX IX_Task_Per_Status_Task
ON [MardisCore].[Task] ([StatusRegister])
INCLUDE ([IdAccount],[IdCampaign],[IdStatusTask])

CREATE NONCLUSTERED INDEX IX_Task_Count_Per_Status
ON [MardisCore].[Task] ([IdAccount],[IdCampaign],[StatusRegister])
INCLUDE ([IdStatusTask])

CREATE NONCLUSTERED INDEX IX_Task_Per_Status
ON [MardisCore].[Task] ([IdAccount],[IdCampaign])
INCLUDE ([Code],[DateCreation],[StartDate],[Description],[IdBranch],[IdMerchant],[StatusRegister],[IdStatusTask],[CommentTaskNoImplemented],[IdTaskNoImplementedReason],[Route],[DateModification],[ExternalCode])

CREATE NONCLUSTERED INDEX IX_Image_Campaign_Branch
ON [MardisCore].[BranchImages] ([IdBranch],[IdCampaign])

CREATE NONCLUSTERED INDEX IX_Image_Campaign
ON [MardisCore].[BranchImages] ([IdCampaign])
INCLUDE ([IdBranch])

CREATE UNIQUE CLUSTERED INDEX [IX_UQ_vw_Campaign_Information] ON [dbo].[vw_Campaign_Information]
(
	[IdTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

CREATE NONCLUSTERED INDEX IX_Task_Account_All
ON [MardisCore].[Task] ([IdAccount])
INCLUDE ([Code],[DateCreation],[IdCampaign],[StartDate],[Description],[IdBranch],[IdMerchant],[StatusRegister],[IdStatusTask],[CommentTaskNoImplemented],[IdTaskNoImplementedReason],[Route],[DateModification],[ExternalCode],[AggregateUri],[UserValidator],[DateValidation])

CREATE NONCLUSTERED INDEX IX_BranchImages_Campaign
ON [MardisCore].[BranchImages] ([IdCampaign])
INCLUDE ([NameContainer])

--Claves únicas
alter table mardiscore.branchcustomer add constraint uq_BranchCustomer unique (idbranch, idcustomer, idtypebusiness,idchannel)

 DECLARE @IDFILTER UNIQUEIDENTIFIER

 SET @IDFILTER = NEWID()
 --FILTROS TAREAS
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'Task');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table]) VALUES
		--(NEWID(), @IDFILTER, 'Código de Tarea','Code',''),
		--(NEWID(), @IDFILTER, 'Nombre de Tarea','Name',''),
		--(NEWID(), @IDFILTER, 'Ruta de Tarea','Route',''),
		--(NEWID(), @IDFILTER, 'Estado de Tarea','Name','StatusTask'),
		--(NEWID(), @IDFILTER, 'Nombre de Mercaderista','Name','User.Profile'),
		--(NEWID(), @IDFILTER, 'Código de Mercaderista','Name','User.Profile'),
		--(NEWID(), @IDFILTER, 'Nombre de Local','Name','Branch'),
		(NEWID(), @IDFILTER, 'Código de Local','ExternalCode','Branch');
INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table],Visible) VALUES
		(NEWID(), @IDFILTER, 'Id de Campaña','IdCampaign','',0),
		(NEWID(), @IDFILTER, 'Id de Mercaderista','IdMerchant','',0);

 SET @IDFILTER = NEWID()
 --FILTROS LOCALES
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'BranchList');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table]) VALUES
		(NEWID(), @IDFILTER, 'Código de Local','ExternalCode',''),
		(NEWID(), @IDFILTER, 'Nombre de Local','Name',''),
		(NEWID(), @IDFILTER, 'Nombre de Provincia','Name','Province'),
		(NEWID(), @IDFILTER, 'Nombre de Ciudad','Name','District'),
		(NEWID(), @IDFILTER, 'Nombre de Parroquia','Name','Parish'),
		(NEWID(), @IDFILTER, 'Tipo de Negocio','TypeBusiness','');

 SET @IDFILTER = NEWID()
 --FILTROS CAMPAÑAS
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'CampaignList');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table]) VALUES
		(NEWID(), @IDFILTER, 'Código de Campaña','Code',''),
		(NEWID(), @IDFILTER, 'Nombre de Campaña','Name',''),
		(NEWID(), @IDFILTER, 'Estado de Campaña','Name','StatusCampaign'),
		(NEWID(), @IDFILTER, 'Estado de Campaña','Name','StatusCampaign')

SET @IDFILTER = NEWID()
 --FILTROS CAMPAÑAS
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'CampaignMap');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table],Visible) VALUES
		(NEWID(), @IDFILTER, 'Código de Local','ExternalCode','Branch',1),
		(NEWID(), @IDFILTER, 'Nombre de Local','Name','Branch',1),
		(NEWID(), @IDFILTER, 'Estado de Tarea','Name','StatusCampaign',1),
		(NEWID(), @IDFILTER, 'Ruta','Route','',1),
		(NEWID(), @IDFILTER, 'Nombre de Mercaderista','Name','User.Profile',1),
		(NEWID(), @IDFILTER, 'Código de Mercaderista','Email','User.Profile',1),
		(NEWID(), @IDFILTER, 'Id de Campaña','IdCampaign','',0)

SET @IDFILTER = NEWID()
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'SelectCampaign');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table],Visible,ManyToMany) VALUES
		(NEWID(), @IDFILTER, 'Ciudad','Name','District',1,0),
		(NEWID(), @IDFILTER, 'Provincia','Name','Province',1,0),
		(NEWID(), @IDFILTER, 'Tipo de Negocio','Name','BranchCustomers.TypeBusiness',1,1),
		(NEWID(), @IDFILTER, 'Canal','Name','BranchCustomers.Channel',1,1)

SET @IDFILTER = NEWID()
 INSERT INTO MardisCommon.CoreFilter VALUES(@IDFILTER, 'DashBoard');
 INSERT INTO MardisCommon.CoreFilterDetail(Id,IdCoreFilter,Name,Property,[Table],Visible,ManyToMany) VALUES
		(NEWID(), @IDFILTER, 'Id de Mercaderista','IdMerchant','',0,0),
		(NEWID(), @IDFILTER, 'Provincia','Name','Branch.Province',1,0),
		(NEWID(), @IDFILTER, 'Ciudad','Name','Branch.District',1,0),
		(NEWID(), @IDFILTER, 'Id de Campaña','IdCampaign','',0,0),
		(NEWID(), @IDFILTER, 'Código de Local','ExternalCode','Branch',1,0),
		(NEWID(), @IDFILTER, 'Ruta de Tarea','Route','',1,0),
		(NEWID(), @IDFILTER, 'Estado de Tarea','Name','StatusTask',1,0),
		(NEWID(), @IDFILTER, 'Nombre de Mercaderista','Name','User.Profile',1,0),
		(NEWID(), @IDFILTER, 'Código de Mercaderista','Code','User.Profile',1,0)
END