create procedure sp_Mistery_Shopper_Audio
AS
BEGIN
	declare @campaign	uniqueidentifier
	declare @cuenta		uniqueidentifier
	--Secciones
	declare @seccion_Existencia uniqueidentifier
	declare @seccion_Pilsener_600CC uniqueidentifier
	declare @seccion_Pilsener_Light_550CC uniqueidentifier
	declare @seccion_Club_550CC uniqueidentifier
	declare @seccion_Estado_y_Observaciones uniqueidentifier
	--Preguntas
	declare @pregunta_Existencia_Existe_Local uniqueidentifier
	declare @pregunta_Existencia_Longitud uniqueidentifier
	declare @pregunta_Existencia_Latitud uniqueidentifier
	declare @pregunta_Existencia_Validación_longitud uniqueidentifier
	declare @pregunta_Existencia_Validación_latitud uniqueidentifier
	declare @pregunta_Pilsener_600CC_Vende_Pilsener_600CC uniqueidentifier
	declare @pregunta_Pilsener_600CC_Dispone_Pilsener_600CC uniqueidentifier
	declare @pregunta_Pilsener_600CC_Precio_Pilsener_600CC uniqueidentifier
	declare @pregunta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC uniqueidentifier
	declare @pregunta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC uniqueidentifier
	declare @pregunta_Pilsener_Light_550CC_Precio_Pilsener_Light_550CC uniqueidentifier
	declare @pregunta_Club_550CC_Vende_Club_550CC uniqueidentifier
	declare @pregunta_Club_550CC_Dispone_Club_550CC uniqueidentifier
	declare @pregunta_Club_550CC_Precio_Club_550CC uniqueidentifier
	declare @pregunta_Estado_y_Observaciones_Estado uniqueidentifier
	declare @pregunta_Estado_y_Observaciones_Observaciones uniqueidentifier
	--Respuestas
	declare @respuesta_Existencia_Existe_Local_Si uniqueidentifier
	declare @respuesta_Existencia_Existe_Local_No uniqueidentifier
	declare @respuesta_Existencia_Existe_Local_Cerrado uniqueidentifier
	declare @respuesta_Existencia_Existe_Local_No_ubicado uniqueidentifier
	declare @respuesta_Existencia_Validación_longitud_Ok uniqueidentifier
	declare @respuesta_Existencia_Validación_longitud_No uniqueidentifier
	declare @respuesta_Existencia_Validación_latitud_Ok uniqueidentifier
	declare @respuesta_Existencia_Validación_latitud_No uniqueidentifier
	declare @respuesta_Pilsener_600CC_Vende_Pilsener_600CC_Si uniqueidentifier
	declare @respuesta_Pilsener_600CC_Vende_Pilsener_600CC_No uniqueidentifier
	declare @respuesta_Pilsener_600CC_Dispone_Pilsener_600CC_Si uniqueidentifier
	declare @respuesta_Pilsener_600CC_Dispone_Pilsener_600CC_No uniqueidentifier
	declare @respuesta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC_Si uniqueidentifier
	declare @respuesta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC_No uniqueidentifier
	declare @respuesta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC_Si uniqueidentifier
	declare @respuesta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC_No uniqueidentifier
	declare @respuesta_Club_550CC_Vende_Club_550CC_Si uniqueidentifier
	declare @respuesta_Club_550CC_Vende_Club_550CC_No uniqueidentifier
	declare @respuesta_Club_550CC_Dispone_Club_550CC_Si uniqueidentifier
	declare @respuesta_Club_550CC_Dispone_Club_550CC_No uniqueidentifier
	declare @respuesta_Estado_y_Observaciones_Estado_Validado uniqueidentifier
	declare @respuesta_Estado_y_Observaciones_Estado_Anulado uniqueidentifier

	set @campaign	=	'A8518E0D-ED41-4700-F7B2-08D48F3AE93C'
	set @cuenta		=	'C5934CC8-EEF2-48D4-84F1-20DF18B847B1'
	--Secciones
	set @seccion_Existencia = '73F1FD3D-D53E-4D4B-8F0D-CE1E1A8BC303'
	set @seccion_Pilsener_600CC = 'B9B70907-67C2-41B9-9C9C-45BBCD5E51F9'
	set @seccion_Pilsener_Light_550CC = '53914024-0AE5-4090-822B-1D14D763235E'
	set @seccion_Club_550CC = 'E5F69162-ADF1-4868-A4C1-E48AAF5893FB'
	set @seccion_Estado_y_Observaciones = '98D6E4CD-D24A-434B-A31F-E04D04C21570'
	--Preguntas
	set @pregunta_Existencia_Existe_Local = 'E1BA84FF-2B91-423A-A8AE-419CC29F27C8'
	set @pregunta_Existencia_Longitud = '5A056DE3-DC71-46E9-BBFD-9D6000F67E8F'
	set @pregunta_Existencia_Latitud = '43659783-495E-4304-9A42-9F2F44E93EC1'
	set @pregunta_Existencia_Validación_longitud = '7F45A55C-153A-44F0-8F74-69EF93B9F8DC'
	set @pregunta_Existencia_Validación_latitud = '53F7206B-CAB3-41FB-A35B-BCAED346916E'
	set @pregunta_Pilsener_600CC_Vende_Pilsener_600CC = 'E1831759-72AF-45F2-8850-B87826886B00'
	set @pregunta_Pilsener_600CC_Dispone_Pilsener_600CC = '93253545-D1BE-4D4E-B767-DF18DDED98CC'
	set @pregunta_Pilsener_600CC_Precio_Pilsener_600CC = '4422616B-60C8-4E28-B2B1-043A9A2EB530'
	set @pregunta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC = 'D6254A17-6C25-4AAF-B731-8DE8C74D803A'
	set @pregunta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC = '1984582B-5AAD-4D6F-B0B6-815CDC8AA54C'
	set @pregunta_Pilsener_Light_550CC_Precio_Pilsener_Light_550CC = 'A4521690-6834-4C39-94DF-2F9F9BF06A15'
	set @pregunta_Club_550CC_Vende_Club_550CC = '74C9616B-D18D-473F-BFE1-1BEB2B3AEF5A'
	set @pregunta_Club_550CC_Dispone_Club_550CC = '479EA809-AB73-41F4-917E-F7028425CE04'
	set @pregunta_Club_550CC_Precio_Club_550CC = '419B9A38-2AF0-4E1A-80FB-023233AFAB57'
	set @pregunta_Estado_y_Observaciones_Estado = '2AA9DC83-9D87-4F83-B13E-64BDEE4B611B'
	set @pregunta_Estado_y_Observaciones_Observaciones = '6298A4F7-4E83-4999-8733-07E7F7E6DDAD'
	--Respuestas
	set @respuesta_Existencia_Existe_Local_Si = 'E67D34A1-668A-498F-9F1A-B7B37CAA1295'
	set @respuesta_Existencia_Existe_Local_No = '76A1B3E6-389E-400C-9D4A-D0B12C8979BC'
	set @respuesta_Existencia_Existe_Local_Cerrado = 'F5A10E22-FABB-400F-86AF-ED36AA6E56DD'
	set @respuesta_Existencia_Existe_Local_No_ubicado = 'CF36E972-F1DF-426A-96ED-1D651CEAC91F'
	set @respuesta_Existencia_Validación_longitud_Ok = '2AD63892-7C44-45E1-8131-1A9884D7516E'
	set @respuesta_Existencia_Validación_longitud_No = '2D89C517-9C04-430D-82F0-5B2BA30CC47A'
	set @respuesta_Existencia_Validación_latitud_Ok = 'F52DAFC2-2011-4203-8404-521E978E6B6B'
	set @respuesta_Existencia_Validación_latitud_No = 'B7D401C6-09F0-4EF6-BB35-13320C255DD9'
	set @respuesta_Pilsener_600CC_Vende_Pilsener_600CC_Si = '86458F60-17DD-4A9F-9959-486617E2AFB8'
	set @respuesta_Pilsener_600CC_Vende_Pilsener_600CC_No = '75B5B951-8F6B-4B81-A79D-FA67BEBD577C'
	set @respuesta_Pilsener_600CC_Dispone_Pilsener_600CC_Si = '5A3A65BD-FC18-42BA-8397-2AA3543026D5'
	set @respuesta_Pilsener_600CC_Dispone_Pilsener_600CC_No = '1F6656F8-DEBA-4662-A72C-DEB8A629C86B'
	set @respuesta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC_Si = 'CE646CAC-FB8A-4F68-8BEE-E54A7999CBED'
	set @respuesta_Pilsener_Light_550CC_Vende_Pilsener_Light_550CC_No = '54329A8E-18B7-42A4-A44D-6FDE40BBC14B'
	set @respuesta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC_Si = '19E8F444-DB6B-4BF0-8758-67E626BDCF3D'
	set @respuesta_Pilsener_Light_550CC_Dispone_Pilsener_Light_550CC_No = '68CAE8E5-37CA-4B29-A1B9-56C02F7E9495'
	set @respuesta_Club_550CC_Vende_Club_550CC_Si = 'A021DE73-1D99-41D8-9E61-60595530048B'
	set @respuesta_Club_550CC_Vende_Club_550CC_No = '1EB5D307-879A-4B06-A341-65436CFA9AAF'
	set @respuesta_Club_550CC_Dispone_Club_550CC_Si = 'EEF71693-B062-44D3-B562-705E466A42BF'
	set @respuesta_Club_550CC_Dispone_Club_550CC_No = '58D5FC4A-A1E8-44BC-A54F-B19FA95961EA'
	set @respuesta_Estado_y_Observaciones_Estado_Validado = '3DE402D2-A662-4565-AC06-9FDBE2889768'
	set @respuesta_Estado_y_Observaciones_Estado_Anulado = 'D0513E78-9819-4240-B01A-F98E0F01C530'
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	
	declare	
			@_uri varchar(100),				@_creator_uri_user varchar(100),	@_creation_date varchar(100),	@_last_update_uri_user varchar(100),	@_last_update_date varchar(100)
			,@_model_version varchar(100),	@_ui_version varchar(100),			@_is_complete varchar(100),		@_submission_date varchar(100),			@_marked_as_complete_date varchar(100)
			,@ui_cod varchar(100),			@en_coord_alt varchar(100),			@en_coord_lat varchar(100),		@imei_id varchar(100),					@start varchar(100)
			,@end varchar(100),				@meta_instance_name varchar(100),	@en_coord_acc varchar(100),		@ui_existe varchar(100),				@en_coord_lng varchar(100)
			,@ui_ruta varchar(100),			@meta_instance_id varchar(100)

	declare cur_encuesta cursor for
	SELECT	[_URI],							[_CREATOR_URI_USER],				[_CREATION_DATE],				[_LAST_UPDATE_URI_USER],				[_LAST_UPDATE_DATE]
			,[_MODEL_VERSION],				[_UI_VERSION],						[_IS_COMPLETE],					[_SUBMISSION_DATE],						[_MARKED_AS_COMPLETE_DATE]
			,[UI_COD],						[EN_COORD_ALT],						[EN_COORD_LAT],					[IMEI_ID],								[START]
			,[END],							[META_INSTANCE_NAME],				[EN_COORD_ACC],					[UI_EXISTE],							[EN_COORD_LNG]
			,[UI_RUTA],						[META_INSTANCE_ID]
	FROM [MardisAggregate].[INSTANCE_NAME_CORE]
	WHERE _URI NOT IN (SELECT ISNULL(AggregateUri,'uri') FROM MardisCore.Task where IdCampaign = @campaign) and _is_complete=1

	open cur_encuesta

	fetch next from cur_encuesta into
			@_uri						,@_creator_uri_user						,@_creation_date				,@_last_update_uri_user					,@_last_update_date 
			,@_model_version			,@_ui_version							,@_is_complete					,@_submission_date						,@_marked_as_complete_date 
			,@ui_cod					,@en_coord_alt							,@en_coord_lat					,@imei_id								,@start       
			,@end						,@meta_instance_name					,@en_coord_acc					,@ui_existe								,@en_coord_lng 
			,@ui_ruta					,@meta_instance_id 

	while(@@FETCH_STATUS=0)
	BEGIN
		declare @local_actual	uniqueidentifier,
				@tarea_actual	uniqueidentifier

		if not exists(select 1 from mardiscore.Branch where ExternalCode = @ui_cod and IdAccount = @cuenta)
		begin
		end

		--CUANDO ENCUENTRO EL LOCAL
		if exists(select 1 from mardiscore.Branch where ExternalCode = @ui_cod and IdAccount = @cuenta)
		begin
			declare @hora_actual datetime,
					@hora_ejecucion datetime,
					@status uniqueidentifier,
					@mercaderista uniqueidentifier,
					@user_code varchar(20)	,
					@codigo_nuevo varchar(20),
					@secuencial_nuevo int,
					@prefijo_ruta varchar(20),
					@respuesta_actual uniqueidentifier,
					@pregunta_actual uniqueidentifier,
					@seccion_actual uniqueidentifier,
					@opcion_respuesta_actual uniqueidentifier	
			
			set @tarea_actual = newid()
			set @hora_actual = getdate()
			set @hora_ejecucion = convert(datetime,substring(@_submission_date,0,charindex('.',@_submission_date,1)))
			set @status = '3488BA56-CB18-4A7E-B56D-8FE348E9E4E4'
			set @mercaderista='DAEB6D35-7D69-47F7-BBD3-3496F40B3C22'		--Asigno a mercaderista 1

			select @codigo_nuevo = max(Code) from MardisCore.Task where IdAccount=@cuenta and ExternalCode like 'TAR-%'
			select top 1 @local_actual = id from MardisCore.Branch where IdAccount = @cuenta and ExternalCode = @ui_cod

			if(@codigo_nuevo is null)
			begin
				set @secuencial_nuevo=1
			end
			else
			begin
				set @secuencial_nuevo = convert(int,substring(@codigo_nuevo,5,len(@codigo_nuevo)-3))+1
			end

			set @codigo_nuevo='TAR-' + RIGHT('00000' + Ltrim(Rtrim(@secuencial_nuevo)),5)

			--creo la tarea 
			exec sp_insert_task	@Id = @tarea_actual OUTPUT
								,@IdAccount = @cuenta
								,@Code = @codigo_nuevo
								,@DateCreation = @hora_actual
								,@IdCampaign = @campaign
								,@StartDate = @hora_ejecucion
								,@Description = 'Tarea migrada desde bases Aggregate'
								,@IdBranch = @local_actual
								,@IdMerchant = @mercaderista
								,@StatusRegister = 'A'
								,@IdStatusTask = @status
								,@CommentTaskNoImplemented = ''
								,@IdTaskNoImplementedReason = null
								,@Route = @ui_ruta
								,@DateModification = @hora_actual
								,@ExternalCode = @_uri
								,@AggregateUri = @_uri

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------nombre y ubicación
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			set @seccion_actual = @seccion_Existencia

			if(@ui_existe is not null)
			begin
				set @respuesta_actual=newid()
				set @pregunta_actual = @pregunta_Existencia_Existe_Local

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)

				set @opcion_respuesta_actual = @respuesta_Existencia_Existe_Local_Si

				if(@ui_existe = 'no')
				begin
					set @opcion_respuesta_actual = @respuesta_Existencia_Existe_Local_No
				end
				if(@ui_existe = 'cerrado')
				begin
					set @opcion_respuesta_actual = @respuesta_Existencia_Existe_Local_Cerrado
				end
				if(@ui_existe = 'no_ubicado')
				begin
					set @opcion_respuesta_actual = @respuesta_Existencia_Existe_Local_No_ubicado
				end

				insert into mardiscore.AnswerDetail values
				(
					newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
				)
			end		

			if(@en_coord_lat is not null)
			begin
				set @respuesta_actual=newid()
				set @pregunta_actual = @pregunta_Existencia_Latitud

				insert into MardisCore.Answer values
				(
					@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
					@pregunta_actual, 'A'
				)

				insert into mardiscore.AnswerDetail values
				(
					newid(), null, getdate(), @respuesta_actual, 'A', 0, @en_coord_lat,null
				)
			end

			if(@en_coord_lng is not null)
			begin
				set @respuesta_actual=newid()
				set @pregunta_actual = @pregunta_Existencia_Longitud

				insert into MardisCore.Answer values
				(
					@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
					@pregunta_actual, 'A'
				)

				insert into mardiscore.AnswerDetail values
				(
					newid(), null, getdate(), @respuesta_actual, 'A', 0, @en_coord_lng,null
				)
			end
		end

		fetch next from cur_encuesta into
			@_uri						,@_creator_uri_user						,@_creation_date				,@_last_update_uri_user					,@_last_update_date 
			,@_model_version			,@_ui_version							,@_is_complete					,@_submission_date						,@_marked_as_complete_date 
			,@ui_cod					,@en_coord_alt							,@en_coord_lat					,@imei_id								,@start       
			,@end						,@meta_instance_name					,@en_coord_acc					,@ui_existe								,@en_coord_lng 
			,@ui_ruta					,@meta_instance_id 
	END
	
	close cur_encuesta
	deallocate cur_encuesta
END