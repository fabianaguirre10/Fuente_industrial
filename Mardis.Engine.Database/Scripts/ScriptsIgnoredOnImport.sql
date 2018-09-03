
declare @_uri varchar(200), 
		@_creator_uri_user  varchar(200), 
		@_submission_date varchar(200),
		@tipo_negocio_abierto varchar(200),
		@user_info_cod varchar(200),
		@imei_id varchar(200),
		@pop_group_material_pop varchar(200),
		@validacion_1_local_nombre varchar(200), 
		@user_info_cod_arca varchar(200),
		@equipos_frio_repeat_count varchar(200),
		@pop_group_material_pop_otro varchar(200),
		@observaciones_group_obsevaciones varchar(200),
		@localizacion_group_barrio varchar(200),
		@localizacion_group_numeracion varchar(200),
		@repetido_cod_rep varchar(200),
		@tipo_negocio_group_tipo_de_negocio varchar(200), 
		@localizacion_group_zona varchar(200),
		@localizacion_group_calle_secundaria varchar(200), 
		@validacion_1_telefono varchar(200),
		@tipo_negocio_group_canal_de_negocio varchar(200), 
		@localizacion_group_ciudad varchar(200), 
		@tipo_negocio_group_tipo_negocio_otro varchar(200),
		@validacion_1_numero_frio varchar(200), 
		@user_info_ruta varchar(200),
		@sim_serial varchar(200),
		@tipo_negocio_colabora varchar(200),
		@localizacion_group_calle_principal varchar(200),
		@validacion_1_celular varchar(200),
		@localizacion_group_referencia varchar(200),
		@user_info_tipo_ejecucion varchar(200),
		@user_info_existe varchar(200),
		@validacion_1_ruc varchar(200), 
		@validacion_1_nombre_propietario varchar(200),
		@localizacion_group_provincia varchar(200),
		@ubicacion_coord_lat varchar(200),
		@ubicacion_coord_lng varchar(200)

declare @cuenta uniqueidentifier,
		@campaign uniqueidentifier,
		@status uniqueidentifier,
		@mercaderista uniqueidentifier,
		@pregunta_tipo_ejecucion uniqueidentifier,
		@respuesta_entrega uniqueidentifier,
		@respuesta_retiro uniqueidentifier,
		@pregunta_nombre_local uniqueidentifier,
		@pregunta_telefono uniqueidentifier,
		@pregunta_celular uniqueidentifier,
		@pregunta_nombre_propietario uniqueidentifier,
		@pregunta_ruc uniqueidentifier,
		@pregunta_foto_local uniqueidentifier,
		@pregunta_canal uniqueidentifier,
			@respuesta_ec_on_soc_estabsoc_s uniqueidentifier,
			@respuesta_pm_solo_distribuidor uniqueidentifier,
			@respuesta_ec_of_lle_autoscdnan uniqueidentifier,
			@respuesta_ec_on_ent_recreacmsn uniqueidentifier,
			@respuesta_ec_on_cof_restaurmsn uniqueidentifier,
			@respuesta_ec_on_cof_restaurwmn uniqueidentifier,
			@respuesta_ec_on_soc_barms_n uniqueidentifier,
			@respuesta_ec_of_lle_tiendatras uniqueidentifier,
			@respuesta_ec_nc_ter_mayoristan uniqueidentifier,
			@respuesta_ec_of_lle_autoservis uniqueidentifier,
			@respuesta_ec_of_lle_tdacomesps uniqueidentifier,
			@respuesta_ec_on_soc_barwm_n uniqueidentifier,
			@respuesta_ec_on_ent_diversnwmn uniqueidentifier,
			@respuesta_ec_on_ent_diversnmsn uniqueidentifier,
			@respuesta_ec_on_cof_comirapmsn uniqueidentifier,
			@respuesta_ec_on_ent_eventoms_n uniqueidentifier,
			@respuesta_ec_on_edt_insteducan uniqueidentifier,
			@respuesta_ec_on_edt_instlaborn uniqueidentifier,
			@respuesta_ec_on_ent_eventowmn uniqueidentifier,
		@pregunta_tipo_negocio uniqueidentifier,
			@respuesta_tienda_basica_social uniqueidentifier,
			@respuesta_solo_distribuidor uniqueidentifier,
			@respuesta_hipermercado uniqueidentifier,
			@respuesta_casino uniqueidentifier,
			@respuesta_restaurante uniqueidentifier,
			@respuesta_night_club uniqueidentifier,
			@respuesta_sala_de_billar uniqueidentifier,
			@respuesta_hotel uniqueidentifier,
			@respuesta_bar uniqueidentifier,
			@respuesta_tienda uniqueidentifier,
			@respuesta_tienda_basica uniqueidentifier,
			@respuesta_cafeteria uniqueidentifier,
			@respuesta_club_de_recreo uniqueidentifier,
			@respuesta_estadio_del_barrio uniqueidentifier,
			@respuesta_tienda_benefica uniqueidentifier,
			@respuesta_caseta_quiosco uniqueidentifier,
			@respuesta_tienda_bebs_alcohol uniqueidentifier,
			@respuesta_tienda_calle_ppal uniqueidentifier,
			@respuesta_minimarket uniqueidentifier,
			@respuesta_bazar uniqueidentifier,
			@respuesta_gallera uniqueidentifier,
			@respuesta_cangrejal uniqueidentifier,
			@respuesta_panaderia uniqueidentifier,
			@respuesta_gasolinera uniqueidentifier,
			@respuesta_ferreteria uniqueidentifier,
			@respuesta_cigarrlicores_tabac uniqueidentifier,
			@respuesta_discoteca_superior uniqueidentifier,
			@respuesta_discoteca uniqueidentifier,
			@respuesta_mini_dist_urbano uniqueidentifier,
			@respuesta_mini_dist_rural uniqueidentifier,
			@respuesta_vulcanizadora uniqueidentifier,
			@respuesta_tunel_de_lavado uniqueidentifier,
			@respuesta_comida_rapida uniqueidentifier,
			@respuesta_gimnasio uniqueidentifier,
			@respuesta_edu_escuela_deport uniqueidentifier,
			@respuesta_deposito uniqueidentifier,
			@respuesta_supermercado uniqueidentifier,
			@respuesta_pizza uniqueidentifier,
			@respuesta_picanteria uniqueidentifier,
			@respuesta_bodega uniqueidentifier,
			@respuesta_futbol_de_salon uniqueidentifier,
			@respuesta_bar_cantina uniqueidentifier,
			@respuesta_cibercafe uniqueidentifier,
			@respuesta_mini_socios uniqueidentifier,
			@respuesta_parque_atracciones uniqueidentifier,
			@respuesta_beer_express uniqueidentifier,
			@respuesta_evento_vecindario uniqueidentifier,
			@respuesta_comedor_popular uniqueidentifier,
			@respuesta_salon_de_belleza uniqueidentifier,
			@respuesta_evento_deportivo uniqueidentifier,
			@respuesta_educ_escuela uniqueidentifier,
			@respuesta_drogeria_farmacia uniqueidentifier,
			@respuesta_rest_comida_oriental uniqueidentifier,
			@respuesta_fruteria uniqueidentifier,
			@respuesta_cevicheria uniqueidentifier,
			@respuesta_teatro_cine uniqueidentifier,
			@respuesta_evento_de_empresa uniqueidentifier,
			@respuesta_empresa_fabrica uniqueidentifier,
			@respuesta_educ_escuela_priv uniqueidentifier,
			@respuesta_productos_gourmet uniqueidentifier,
			@respuesta_motel_residencia uniqueidentifier,
			@respuesta_evento_marketing uniqueidentifier,
			@respuesta_evento_musical uniqueidentifier,
			@respuesta_entidad_educativa uniqueidentifier,
			@respuesta_bar_en_la_playa uniqueidentifier,
			@respuesta_educ_universidad uniqueidentifier,
			@respuesta_recreacion uniqueidentifier,
			@respuesta_cafe uniqueidentifier,
			@respuesta_mayoristas uniqueidentifier,
		@pregunta_provincia uniqueidentifier,
			@respuesta_azuay uniqueidentifier,
			@respuesta_bolivar uniqueidentifier,
			@respuesta_cañar uniqueidentifier,
			@respuesta_carchi uniqueidentifier,
			@respuesta_chimborazo uniqueidentifier,
			@respuesta_cotopaxi uniqueidentifier,
			@respuesta_el_oro uniqueidentifier,
			@respuesta_esmeraldas uniqueidentifier,
			@respuesta_galapagos uniqueidentifier,
			@respuesta_guayas uniqueidentifier,
			@respuesta_imbabura uniqueidentifier,
			@respuesta_loja uniqueidentifier,
			@respuesta_los_rios uniqueidentifier,
			@respuesta_manabi uniqueidentifier,
			@respuesta_morona_santiago uniqueidentifier,
			@respuesta_napo uniqueidentifier,
			@respuesta_orellana uniqueidentifier,
			@respuesta_pastaza uniqueidentifier,
			@respuesta_pichincha uniqueidentifier,
			@respuesta_santa_elena uniqueidentifier,
			@respuesta_santo_domingo_de_los_tsachilas uniqueidentifier,
			@respuesta_sucumbios uniqueidentifier,
			@respuesta_tungurahua uniqueidentifier,
			@respuesta_zamora_chinchipe uniqueidentifier,
		@pregunta_ciudad uniqueidentifier,
		@pregunta_zona uniqueidentifier,
		@pregunta_barrio uniqueidentifier,
		@pregunta_calle_principal uniqueidentifier,
		@pregunta_numeracion uniqueidentifier,
		@pregunta_calle_secundaria uniqueidentifier,
		@pregunta_referencia uniqueidentifier,
		@pregunta_tipo uniqueidentifier,
		@pregunta_marca uniqueidentifier,
		@pregunta_modelo uniqueidentifier,
		@pregunta_brandeo uniqueidentifier,
			@respuesta_pilsener uniqueidentifier,
			@respuesta_club uniqueidentifier,
			@respuesta_conquer uniqueidentifier,
			@respuesta_pilsener_light uniqueidentifier,
			@respuesta_pilsenerador uniqueidentifier,
			@respuesta_pony_malta uniqueidentifier,
			@respuesta_dorada uniqueidentifier,
			@respuesta_miller_lite uniqueidentifier,
			@respuesta_miller uniqueidentifier,
			@respuesta_multimarcas uniqueidentifier,
			@respuesta_clausen uniqueidentifier,
			@respuesta_manantial uniqueidentifier,
			@respuesta_pilsener_cero uniqueidentifier,
			@respuesta_club_cacao uniqueidentifier,
			@respuesta_pony_malta_plus uniqueidentifier,
		@pregunta_placa uniqueidentifier,
		@pregunta_serie uniqueidentifier,
		@pregunta_foto_frio uniqueidentifier,
		@pregunta_pop uniqueidentifier,
			@respuesta_letrero_personalizado uniqueidentifier,
			@respuesta_banderola_metálica uniqueidentifier,
			@respuesta_totem uniqueidentifier,
			@respuesta_banderines uniqueidentifier,
			@respuesta_cabeceras uniqueidentifier,
			@respuesta_biombo uniqueidentifier,
			@respuesta_adhesivos uniqueidentifier,
			@respuesta_muñeco uniqueidentifier,
			@respuesta_tacho uniqueidentifier,
			@respuesta_otros uniqueidentifier,
		@pregunta_foto_pop uniqueidentifier,
		@pregunta_observaciones uniqueidentifier

set @cuenta='C5934CC8-EEF2-48D4-84F1-20DF18B847B1'
set @mercaderista = 'DAEB6D35-7D69-47F7-BBD3-3496F40B3C22'
set @campaign='0708084C-7DFB-4959-6F3A-08D47AE22B53'
set @status = '769087E2-4AE6-44F3-871F-291DB934431D'
set @pregunta_tipo_ejecucion = 'C7476563-94AD-4BBD-7D8E-08D47AAE6594'
	set @respuesta_entrega = '4962912F-176F-4DD1-35E8-08D47AAE7823'
	set @respuesta_retiro = 'B905BCFB-58E2-436B-35E9-08D47AAE7823'
set @pregunta_nombre_local = '6009033C-18CF-4D11-7D8F-08D47AAE6594'
set @pregunta_telefono = '8DC02718-D01E-4E08-7D90-08D47AAE6594'
set @pregunta_celular = 'EB4FB624-022F-4DD7-7D91-08D47AAE6594'
set @pregunta_nombre_propietario = 'F76C4620-296A-4FCF-7D92-08D47AAE6594'
set @pregunta_ruc = 'ABA30364-60FE-484B-7D93-08D47AAE6594'
set @pregunta_foto_local = '30B36B11-9833-4E5A-8DC6-08D47AB84999'
set @pregunta_canal = '2D1193A4-0E0A-46FB-7D94-08D47AAE6594'
	set @respuesta_ec_on_soc_estabsoc_s = '2F8A8C58-72F8-4F28-926E-2F6310F9B5D9'
	set @respuesta_pm_solo_distribuidor = '4847409A-31AF-4695-9208-591AFE70C159'
	set @respuesta_ec_of_lle_autoscdnan = '75C5C38C-F43E-42D1-8E86-4805C9E3BF3F'
	set @respuesta_ec_on_ent_recreacmsn = '974D6A49-F847-4A3B-A58F-701B5CBCA55E'
	set @respuesta_ec_on_cof_restaurmsn = 'A53F8D63-E33D-4F0D-8396-61B989BE063C'
	set @respuesta_ec_on_cof_restaurwmn = '69093D19-BDC9-4789-9551-9BFA14594325'
	set @respuesta_ec_on_soc_barms_n = '4BC09E82-BCBD-44B5-B40F-7F0CDE775D49'
	set @respuesta_ec_of_lle_tiendatras = '124A08D5-5EB7-4944-A63C-11A18C9A3CF6'
	set @respuesta_ec_nc_ter_mayoristan = '1BAA1833-0F03-4486-A49C-40A856E59F4E'
	set @respuesta_ec_of_lle_autoservis = 'CA9BBE6C-7280-4A7F-800C-D066F683028F'
	set @respuesta_ec_of_lle_tdacomesps = '62C55C32-2B85-4A55-BEEB-A945A3129EAD'
	set @respuesta_ec_on_soc_barwm_n = 'AF8BBF32-1FC8-46BE-B9C9-3353A873C266'
	set @respuesta_ec_on_ent_diversnwmn = 'E0D9F48E-76BD-4E24-961D-D2C203ADB85E'
	set @respuesta_ec_on_ent_diversnmsn = 'E98AE9D7-BB2B-4457-831E-912464E8E056'
	set @respuesta_ec_on_cof_comirapmsn = '5C6F42E5-AB71-4AC6-91F5-C744B6B6A352'
	set @respuesta_ec_on_ent_eventoms_n = '6808856C-8D4B-4A3D-9536-6BD9F03B3D36'
	set @respuesta_ec_on_edt_insteducan = 'D2665CDE-4A2E-4F7B-9D04-DF7E86E754ED'
	set @respuesta_ec_on_edt_instlaborn = '59D27470-AAA6-440F-93A0-1B8823A6DA77'
	set @respuesta_ec_on_ent_eventowmn = 'ED40EDBE-7A42-4E3E-8731-01C450D736D2'
set @pregunta_tipo_negocio = '1D28F723-C037-45E7-7D95-08D47AAE6594'
	set @respuesta_tienda_basica_social = '1C5B9A81-D899-4F94-85DC-7440A873B5F9'
	set @respuesta_solo_distribuidor = 'A4685240-A158-4740-B589-4E4FBC7D7DC3'
	set @respuesta_hipermercado = 'C596E828-2358-4722-8CAD-8C7A5B9B5389'
	set @respuesta_casino = '0D314D78-CF05-4241-A9C5-348CA1B6B999'
	set @respuesta_restaurante = 'CC47167F-1A52-43F6-8A7A-63CBB992B9C4'
	set @respuesta_night_club = 'D6D04091-266A-4ADF-ABE8-E9E5D7CD6C74'
	set @respuesta_sala_de_billar = 'F1D4E2DF-500C-4345-BE3D-6939E4FB8784'
	set @respuesta_hotel = 'E0F3F5B4-98BF-4DA1-8C1F-54E115EF2561'
	set @respuesta_bar = '647CF5E1-3409-46F4-B7F9-9B41830FCE74'
	set @respuesta_tienda = '65C9AB60-0FCA-4C9F-998F-6619D016593B'
	set @respuesta_tienda_basica = 'CA65849A-8997-4445-9709-3B0615127BB8'
	set @respuesta_cafeteria = 'AC1EA6D0-402E-4D95-B3B2-995162DF7AC5'
	set @respuesta_club_de_recreo = 'ADBE74F9-9B54-40F5-A41E-9542514ABF65'
	set @respuesta_estadio_del_barrio = '9BE4546F-83E0-4812-92A3-F9E126F7958D'
	set @respuesta_tienda_benefica = 'DF56947E-1D89-4A99-9AD7-F894D1D48FB6'
	set @respuesta_caseta_quiosco = 'BE6DFA6E-4EE4-476D-A83A-F0D0077333F4'
	set @respuesta_tienda_bebs_alcohol = '624BB6D5-C974-4836-9FE2-0E2748212526'
	set @respuesta_mayoristas = '21F50F81-F9C4-4206-A565-8AC1BD42A632'
	set @respuesta_tienda_calle_ppal = 'D441786F-C0F6-485C-90D9-ED70A1623486'
	set @respuesta_minimarket = 'BF95BB39-F5F2-432C-B8B9-34B97ACA815D'
	set @respuesta_bazar = 'C6C992FC-AFB9-4400-AD0D-0ADD4EB89675'
	set @respuesta_gallera = '09896F19-3521-47F4-BE50-0F630A457610'
	set @respuesta_cangrejal = '81D67FDC-57BB-460B-9DA3-2776EF12B9B2'
	set @respuesta_panaderia = '3DDD14C9-5D78-41DD-9C16-7BB8EDBE5ED7'
	set @respuesta_gasolinera = 'F4C55DED-1B8B-4D73-BC86-349E85B73DCE'
	set @respuesta_ferreteria = '230BDD2B-B7C3-4206-92F3-F8CC66C7801D'
	set @respuesta_cigarrlicores_tabac = 'DE6C7ADA-8400-4BE6-9AE1-7BD297ED1075'
	set @respuesta_discoteca_superior = '42EFDFFE-473B-4BB9-A3E8-45649E8FFDC7'
	set @respuesta_discoteca = 'D348A199-A100-4DA2-A8C2-61429BA3EBE8'
	set @respuesta_mini_dist_urbano = 'EADC60EA-C280-49E9-AF37-BEB9E99F2A69'
	set @respuesta_mini_dist_rural = 'E11DEF7A-3F51-432D-849D-25C7D95E0AA2'
	set @respuesta_vulcanizadora = '38D58185-77F6-4D15-B38C-135143EB1E7A'
	set @respuesta_tunel_de_lavado = '05C9832D-AB25-46BB-9DAE-A08965E679F8'
	set @respuesta_comida_rapida = 'EE2AC803-A8E3-420C-9F76-536B544D5D09'
	set @respuesta_gimnasio = '953ED0DB-FBFF-4CCB-832A-D86062B05CB1'
	set @respuesta_edu_escuela_deport = '3A52D80E-5A19-4764-A3BF-2911A7A9699B'
	set @respuesta_deposito = '50BD2E99-0569-4026-92E3-D21919512AAF'
	set @respuesta_supermercado = '2EF0CB14-017F-40FC-A2BD-A3DF6C9AB3B0'
	set @respuesta_pizza = 'C544A209-E941-424D-A06C-8ED5234C23CC'
	set @respuesta_picanteria = '168BD0E9-F620-413B-BDF9-EEAA6BB57F73'
	set @respuesta_bodega = '644FE4B6-DBA4-4FFD-BB42-6A74002BDB3B'
	set @respuesta_futbol_de_salon = '584447EB-6FB6-4D69-9E4A-63AEC2572B7F'
	set @respuesta_bar_cantina = 'AB8D8409-9838-45CE-86C3-0AD135D03F5C'
	set @respuesta_cibercafe = '7EA587A2-2F96-4A28-AB07-9AC38CF6B984'
	set @respuesta_mini_socios = '62ABE0D2-04CB-4023-A421-EFD5C1724F32'
	set @respuesta_parque_atracciones = '4ACF232E-EF91-4211-81AA-5384E84B03B5'
	set @respuesta_beer_express = '11A44032-667E-4EF5-92BE-BBFEA917CC37'
	set @respuesta_evento_vecindario = '923BC947-1C6C-493C-9786-4FEA42B775F3'
	set @respuesta_comedor_popular = '9FF7C14C-63AF-4B5D-9747-BE1219A48245'
	set @respuesta_salon_de_belleza = 'FC141D38-EB89-4E97-812B-9025C0BE25B1'
	set @respuesta_evento_deportivo = '11E30E1B-9A0A-4AB7-9CB2-8CCF99310CCF'
	set @respuesta_educ_escuela = '9F4B8A66-714A-4129-A407-840C1DF1342B'
	set @respuesta_drogeria_farmacia = 'A425BF7E-CAA8-4A16-BC81-F1883FE647FF'
	set @respuesta_rest_comida_oriental = '80D8E2FC-F43B-4A78-AF4B-ACBEAEF17CF0'
	set @respuesta_fruteria = 'EDB65B98-5CC0-4A82-B421-888DAD9D0B78'
	set @respuesta_cevicheria = 'F1090B89-458B-4B04-8F21-AE2B5B0E8566'
	set @respuesta_teatro_cine = 'FF237932-11A5-4C6C-BA54-96D16C81FB6B'
	set @respuesta_evento_de_empresa = '2CC2FDFB-197A-4050-A34A-A9DA8B06BDBF'
	set @respuesta_empresa_fabrica = 'DC3259C7-8F66-4732-AC8D-FF4CD83EF5AB'
	set @respuesta_educ_escuela_priv = '4311257E-0C6D-44EC-B904-3D2F98571647'
	set @respuesta_productos_gourmet = '717E2E1B-9E97-4991-9497-526CC28C3002'
	set @respuesta_motel_residencia = '8C6845DD-C304-4593-A365-9762A137C9C5'
	set @respuesta_evento_marketing = '9961FF03-C531-46AE-9A3E-EC7C99D55CB1'
	set @respuesta_evento_musical = '306715ED-7001-4AD6-AE38-5EA5A6859828'
	set @respuesta_entidad_educativa = '057766C4-108A-446B-BA8B-3B8DCF87915D'
	set @respuesta_bar_en_la_playa = '62A7C12F-6250-4DEE-A7A8-21B5D3373E2C'
	set @respuesta_educ_universidad = '03D7AE30-836B-44C9-AEC6-12A6A95301E0'
	set @respuesta_recreacion = 'FC4127F0-B825-41E8-8F29-EBADDD8F0AC0'
	set @respuesta_cafe = 'F0F9E01F-5487-4159-9523-8C5B5D4DDA37'
	set @respuesta_mayoristas = 'D428891D-2939-4041-B969-DD0D713F0511'
set @pregunta_provincia = '23A1474E-2E52-40CD-7D96-08D47AAE6594'
	set @respuesta_azuay = 'C6C88308-222C-475B-9667-161C82A9A95E'
	set @respuesta_bolivar = '51BE52CA-D645-46CB-8FC1-9DBB98C1A2AB'
	set @respuesta_cañar = '1CF3D5FF-EB25-49B8-97E4-6DF01BBB1F12'
	set @respuesta_carchi = '16255FCB-0043-4572-AFFF-F1C94E9253AC'
	set @respuesta_chimborazo = 'CACAF57B-A6D3-4877-BAA2-CB7D875EB675'
	set @respuesta_cotopaxi = '7515FBD1-66CB-4A64-974E-E9DDA72BA143'
	set @respuesta_el_oro = 'F3ABE0B9-D216-4E9F-AF61-217E78D888D0'
	set @respuesta_esmeraldas = '0C27D842-A6D5-4026-9A1B-7CE7FCC56C2F'
	set @respuesta_galapagos = '41EEA063-1B7D-4DE3-84DD-E6A62A161F05'
	set @respuesta_guayas = 'C596C7BE-7BD4-4F6D-90B9-18DF7B991B7E'
	set @respuesta_imbabura = '006A1901-5AE1-4150-987D-943FB0977338'
	set @respuesta_loja = '4261CDCB-2387-4892-8DE0-8FCC389C01A7'
	set @respuesta_los_rios = 'B620A9CD-1C3E-4527-B233-6B48E87FEF4C'
	set @respuesta_manabi = '66BA79C3-9D6D-4058-B691-E56453B042B8'
	set @respuesta_morona_santiago = 'FDAE8F3C-3948-4B78-81FA-6497BE5D5851'
	set @respuesta_napo = 'E4D66FA6-56E1-4553-843D-63CEE36DA2B9'
	set @respuesta_orellana = '078BC1FE-7A2C-4CF0-B01E-35D576AB18C8'
	set @respuesta_pastaza = '674EBCED-5368-4A74-9B38-574AA3C506D3'
	set @respuesta_pichincha = '3CC6002F-709F-4821-8176-26FC6E62AAAA'
	set @respuesta_santa_elena = '00B1B1AF-9FF6-4DF6-94B5-8A935D73C06D'
	set @respuesta_santo_domingo_de_los_tsachilas = '4A6E4A5A-4D67-4959-86A7-DDCC53829663'
	set @respuesta_sucumbios = '943AB406-2A02-4624-A8D6-01C6AB390E07'
	set @respuesta_tungurahua = '0D658899-ECA2-4B47-9CF2-06F52B0320FD'
	set @respuesta_zamora_chinchipe = '07AEAB74-7667-4BBF-B0C2-1C3D11CDB6D2'
set @pregunta_ciudad = '6D115C54-6E58-4A42-7D97-08D47AAE6594'
set @pregunta_zona = '456406FB-950B-4FA6-7D98-08D47AAE6594'
set @pregunta_barrio = '92D860E9-4272-4C76-7D99-08D47AAE6594'
set @pregunta_calle_principal = 'B5EDF4AE-AE5A-44AA-7D9A-08D47AAE6594'
set @pregunta_numeracion = 'DC74C69C-BCC7-476B-7D9B-08D47AAE6594'
set @pregunta_calle_secundaria = 'F991E632-3A17-4DEF-7D9C-08D47AAE6594'
set @pregunta_referencia = 'FAACCEA5-58FD-478D-7D9D-08D47AAE6594'
set @pregunta_tipo = '500EDBA8-20EB-4E37-7D9E-08D47AAE6594'
set @pregunta_marca = 'C8D89EF8-34A5-4254-7D9F-08D47AAE6594'
set @pregunta_modelo = '25FE9F12-3849-41CB-7DA0-08D47AAE6594'
set @pregunta_brandeo = '2900E524-EC3F-4DCD-7DA1-08D47AAE6594'
	set @respuesta_pilsener = '1EB350C1-4BA3-433B-BF3E-8D3A0A4EF5CC'
	set @respuesta_club = '3135AE8D-B010-4C6D-9471-6A946D2DD70B'
	set @respuesta_conquer = 'DAB41075-99F0-4FD0-B455-FD019FEB4FEB'
	set @respuesta_pilsener_light = 'EE651CBF-62E1-452C-8E8D-D177F2C97586'
	set @respuesta_pilsenerador = '16F9559A-3F2B-4916-8642-009439C1C019'
	set @respuesta_pony_malta = '07BFA053-2E4D-4703-957C-5EF3D5BEA768'
	set @respuesta_dorada = '9819ACDC-7F8B-4A50-AD23-12A6B677182D'
	set @respuesta_miller_lite = '22E79EA3-A650-4482-824A-1B5DCD9AD212'
	set @respuesta_miller = '84F2567D-ECA1-46FB-99D1-92B3C604389D'
	set @respuesta_multimarcas = '47339158-4D57-4956-BE60-9F524EA9D60D'
	set @respuesta_clausen = '42F15FF4-59C9-4782-B3FB-9CE3D42E4DD0'
	set @respuesta_manantial = '1ADC0035-B2C3-4610-A683-F93C5D3585D2'
	set @respuesta_pilsener_cero = '780BEEB9-5A2A-4913-A098-313CC804C557'
	set @respuesta_club_cacao = '60D2F357-3232-494F-A0DB-35E28FA30C5B'
	set @respuesta_pony_malta_plus = '5663B674-61D6-4268-9F07-033E0568F7EA'
set @pregunta_placa = 'AAF0A84A-F2FA-40A2-7DA2-08D47AAE6594'
set @pregunta_serie = 'D68BCD73-30BD-4522-7DA3-08D47AAE6594'
set @pregunta_foto_frio = 'A4BDBA33-F2A9-4D59-8DC7-08D47AB84999'
set @pregunta_pop = '79868865-730B-47CE-7DA4-08D47AAE6594'
	set @respuesta_letrero_personalizado = '98339B75-9DB6-4464-35EB-08D47AAE7823'
	set @respuesta_banderola_metálica = '4EAB96CF-D10B-45D7-35EC-08D47AAE7823'
	set @respuesta_totem = '833DF610-0DB2-49AB-35ED-08D47AAE7823'
	set @respuesta_banderines = 'B6C2FD43-78EB-4DD4-35EE-08D47AAE7823'
	set @respuesta_cabeceras = '0F20B617-F066-45CD-35EF-08D47AAE7823'
	set @respuesta_biombo = '5466934E-D4C3-418D-35F0-08D47AAE7823'
	set @respuesta_adhesivos = '81BA5805-F323-4B2F-35F1-08D47AAE7823'
	set @respuesta_muñeco = '6AC2DA18-4F05-48A6-35F2-08D47AAE7823'
	set @respuesta_tacho = '20980E11-909C-4D50-35F3-08D47AAE7823'
	set @respuesta_otros = 'A14669E7-FA2A-4098-35F4-08D47AAE7823'
set @pregunta_foto_pop = '7A4A3F98-31BE-4446-8DC8-08D47AB84999'
set @pregunta_observaciones = 'CF9C1DCD-2BFD-44B9-7DA5-08D47AAE6594'

declare cur_encuestas cursor for
	select 
		_uri, _creator_uri_user, _submission_date,tipo_negocio_abierto,user_info_cod,imei_id,pop_group_material_pop,
		validacion_1_local_nombre, user_info_cod_arca,equipos_frio_repeat_count,pop_group_material_pop_otro,
		observaciones_group_obsevaciones,localizacion_group_barrio,localizacion_group_numeracion,repetido_cod_rep,
		tipo_negocio_group_tipo_de_negocio, localizacion_group_zona,localizacion_group_calle_secundaria, 
		validacion_1_telefono,tipo_negocio_group_canal_de_negocio, localizacion_group_ciudad, tipo_negocio_group_tipo_negocio_otro,
		validacion_1_numero_frio, user_info_ruta,sim_serial,tipo_negocio_colabora,localizacion_group_calle_principal,validacion_1_celular,
		localizacion_group_referencia,user_info_tipo_ejecucion,user_info_existe,validacion_1_ruc, validacion_1_nombre_propietario,
		localizacion_group_provincia,ubicacion_coord_lat,ubicacion_coord_lng
	from mardisaggregate.CENSO_EJECUCION_FRIOS_CN_CORE
	where _uri not in (select isnull (AggregateUri,'uri') from mardiscore.task) and _is_complete=1

open cur_encuestas

fetch next from cur_encuestas into 
		@_uri, @_creator_uri_user, @_submission_date,@tipo_negocio_abierto,@user_info_cod,@imei_id,@pop_group_material_pop,
		@validacion_1_local_nombre, @user_info_cod_arca,@equipos_frio_repeat_count,@pop_group_material_pop_otro,
		@observaciones_group_obsevaciones,@localizacion_group_barrio,@localizacion_group_numeracion,@repetido_cod_rep,
		@tipo_negocio_group_tipo_de_negocio, @localizacion_group_zona,@localizacion_group_calle_secundaria, 
		@validacion_1_telefono,@tipo_negocio_group_canal_de_negocio, @localizacion_group_ciudad, @tipo_negocio_group_tipo_negocio_otro,
		@validacion_1_numero_frio, @user_info_ruta,@sim_serial,@tipo_negocio_colabora,@localizacion_group_calle_principal,@validacion_1_celular,
		@localizacion_group_referencia,@user_info_tipo_ejecucion,@user_info_existe,@validacion_1_ruc, @validacion_1_nombre_propietario,
		@localizacion_group_provincia,@ubicacion_coord_lat,@ubicacion_coord_lng

while(@@fetch_status=0)
begin
	
	--Locales nuevos
	if(@user_info_existe='nuevo')
	begin
		if not exists(select 1 from MardisCore.Branch where IdAccount=@cuenta and ExternalCode=@user_info_cod_arca)
		begin 
			print 'si hay'
		end
	end
	else
	begin
		declare @local_actual uniqueidentifier

		if exists(select 1 from MardisCore.Branch where IdAccount=@cuenta and ExternalCode=@user_info_cod_arca)
		begin 
			select top 1 @local_actual=id from MardisCore.Branch where IdAccount=@cuenta and ExternalCode = @user_info_cod_arca

			declare @tarea_actual uniqueidentifier,
					@hora_actual datetime,
					@hora_ejecucion datetime,
					@uri_multimedia varchar(max),
					@multimedia varbinary(max)


			set @tarea_actual = newid()
			set @hora_actual = getdate()
			set @hora_ejecucion = convert(datetime,substring(@_submission_date,0,charindex('.',@_submission_date,1)))
			--creo la tarea 
			exec sp_insert_task	   @Id = @tarea_actual OUTPUT
								  ,@IdAccount = @cuenta
								  ,@Code = 'TAR-'
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
								  ,@Route = @user_info_ruta
								  ,@DateModification = @hora_actual
								  ,@ExternalCode = @_uri
								  ,@AggregateUri = @_uri

			--creo la respuesta
			declare @respuesta_actual uniqueidentifier,
					@pregunta_actual uniqueidentifier,
					@seccion_actual uniqueidentifier,
					@opcion_respuesta_actual uniqueidentifier
			
			--sección datos de local
			set @seccion_actual = '3F567C00-49B1-4185-30D4-08D47AADD4A8'
			
			--tipo de ejecución
			set @respuesta_actual = newid()		

			if (@user_info_tipo_ejecucion is not null)
			begin
				set @pregunta_actual = @pregunta_tipo_ejecucion

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)

				if(@user_info_tipo_ejecucion = 'entrega')
				begin
					set @opcion_respuesta_actual = @respuesta_entrega
				end
				else
				begin
					set @opcion_respuesta_actual = @respuesta_retiro
				end

				insert into mardiscore.AnswerDetail values
					(
						newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
					)
			end

			--NOMBRE DE LOCAL
			set @respuesta_actual = newid()		

			if (@validacion_1_local_nombre is not null)
			begin
				set @pregunta_actual = @pregunta_nombre_local

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @validacion_1_local_nombre,null
					)
			end

			--TELÉFONO
			set @respuesta_actual = newid()		

			if (@validacion_1_telefono is not null)
			begin
				set @pregunta_actual = @pregunta_telefono

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @validacion_1_telefono,null
					)
			end

			--CELULAR
			set @respuesta_actual = newid()		

			if (@validacion_1_celular is not null)
			begin
				set @pregunta_actual = @pregunta_celular

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @validacion_1_celular,null
					)
			end

			--NOMBRE DEL PROPIETARIO
			set @respuesta_actual = newid()		

			if (@validacion_1_nombre_propietario is not null)
			begin
				set @pregunta_actual = @pregunta_nombre_propietario

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @validacion_1_nombre_propietario,null
					)
			end

			--RUC
			set @respuesta_actual = newid()		

			if (@validacion_1_ruc is not null)
			begin
				set @pregunta_actual = @pregunta_ruc

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @validacion_1_ruc,null
					)
			end

			-- foto local
			set @respuesta_actual = newid()
			select @uri_multimedia = _URI from MardisAggregate.CENSO_EJECUCION_FRIOS_CN_VALIDACION_1_IMG_LOCAL_BLB where _TOP_LEVEL_AURI = @_uri

			if(@uri_multimedia is not null)
			begin
				set @pregunta_actual = @pregunta_foto_local

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @uri_multimedia,null
					)
			end


			--sección canal y tipo de negocio
			set @seccion_actual = '852FABFE-5597-4183-217A-08D47AAEC004'
			
			--canal
			set @respuesta_actual = newid()		

			if (@tipo_negocio_group_canal_de_negocio is not null)
			begin
				set @pregunta_actual = @pregunta_canal

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)

				if(@tipo_negocio_group_canal_de_negocio = 'EC ON SOC EstabSoc S')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_soc_estabsoc_s
				end
				if(@tipo_negocio_group_canal_de_negocio = 'PM SÃ“LO DISTRIBUIDOR')
				begin
					set @opcion_respuesta_actual = @respuesta_pm_solo_distribuidor
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC OF LLE AutoSCdnaN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_of_lle_autoscdnan
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON ENT RecreacMSN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_ent_recreacmsn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON COF RestaurMSN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_cof_restaurmsn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON COF RestaurWMN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_cof_restaurwmn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON SOC BarMS N')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_soc_barms_n
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC OF LLE TiendaTraS')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_of_lle_tiendatras
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC NC TER MayoristaN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_nc_ter_mayoristan
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC OF LLE AutoServiS')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_of_lle_autoservis
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC OF LLE TdaComEspS')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_of_lle_tdacomesps
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON SOC BarWM    N')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_soc_barwm_n
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON ENT DiversnWMN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_ent_diversnwmn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON ENT DiversnMSN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_ent_diversnmsn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON COF ComiRapMSN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_cof_comirapmsn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON ENT EventoMS N')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_ent_eventoms_n
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON EDT InstEducaN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_edt_insteducan
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON EDT InstLaborN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_edt_instlaborn
				end
				if(@tipo_negocio_group_canal_de_negocio = 'EC ON ENT EventoWMN')
				begin
					set @opcion_respuesta_actual = @respuesta_ec_on_ent_eventowmn
				end
				
				insert into mardiscore.AnswerDetail values
					(
						newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
					)
			end

			--tipo de negocio
			set @respuesta_actual = newid()		

			if (@tipo_negocio_group_tipo_de_negocio is not null)
			begin
				set @pregunta_actual = @pregunta_tipo_negocio

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)					

				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda Basica Social')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda_basica_social
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Solo distribuidor')
				begin
					set @opcion_respuesta_actual = @respuesta_solo_distribuidor
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Hipermercado')
				begin
					set @opcion_respuesta_actual = @respuesta_hipermercado
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Casino')
				begin
					set @opcion_respuesta_actual = @respuesta_casino
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Restaurante')
				begin
					set @opcion_respuesta_actual = @respuesta_restaurante
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Night Club')
				begin
					set @opcion_respuesta_actual = @respuesta_night_club
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Sala de billar')
				begin
					set @opcion_respuesta_actual = @respuesta_sala_de_billar
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Hotel')
				begin
					set @opcion_respuesta_actual = @respuesta_hotel
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Bar')
				begin
					set @opcion_respuesta_actual = @respuesta_bar
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda Basica')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda_basica
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Cafetería')
				begin
					set @opcion_respuesta_actual = @respuesta_cafeteria
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Club de recreo')
				begin
					set @opcion_respuesta_actual = @respuesta_club_de_recreo
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Estadio del Barrio')
				begin
					set @opcion_respuesta_actual = @respuesta_estadio_del_barrio
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda benéfica')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda_benefica
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Caseta, Quiosco')
				begin
					set @opcion_respuesta_actual = @respuesta_caseta_quiosco
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda beb.s/alcohol')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda_bebs_alcohol
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Mayoristas')
				begin
					set @opcion_respuesta_actual = @respuesta_mayoristas
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Tienda calle ppal.')
				begin
					set @opcion_respuesta_actual = @respuesta_tienda_calle_ppal
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Minimarket')
				begin
					set @opcion_respuesta_actual = @respuesta_minimarket
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Bazar')
				begin
					set @opcion_respuesta_actual = @respuesta_bazar
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Gallera')
				begin
					set @opcion_respuesta_actual = @respuesta_gallera
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Cangrejal')
				begin
					set @opcion_respuesta_actual = @respuesta_cangrejal
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Panadería')
				begin
					set @opcion_respuesta_actual = @respuesta_panaderia
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Gasolinera')
				begin
					set @opcion_respuesta_actual = @respuesta_gasolinera
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Ferretería')
				begin
					set @opcion_respuesta_actual = @respuesta_ferreteria
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Cigarr.Licores Tabac')
				begin
					set @opcion_respuesta_actual = @respuesta_cigarrlicores_tabac
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Discoteca superior')
				begin
					set @opcion_respuesta_actual = @respuesta_discoteca_superior
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Discoteca')
				begin
					set @opcion_respuesta_actual = @respuesta_discoteca
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Mini Dist Urbano')
				begin
					set @opcion_respuesta_actual = @respuesta_mini_dist_urbano
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Mini Dist Rural')
				begin
					set @opcion_respuesta_actual = @respuesta_mini_dist_rural
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Vulcanizadora')
				begin
					set @opcion_respuesta_actual = @respuesta_vulcanizadora
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Túnel de lavado')
				begin
					set @opcion_respuesta_actual = @respuesta_tunel_de_lavado
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Comida Rápida')
				begin
					set @opcion_respuesta_actual = @respuesta_comida_rapida
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Gimnasio')
				begin
					set @opcion_respuesta_actual = @respuesta_gimnasio
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Edu Escuela deport.')
				begin
					set @opcion_respuesta_actual = @respuesta_edu_escuela_deport
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Depósito')
				begin
					set @opcion_respuesta_actual = @respuesta_deposito
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Supermercado')
				begin
					set @opcion_respuesta_actual = @respuesta_supermercado
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Pizza')
				begin
					set @opcion_respuesta_actual = @respuesta_pizza
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Picantería')
				begin
					set @opcion_respuesta_actual = @respuesta_picanteria
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Bodega')
				begin
					set @opcion_respuesta_actual = @respuesta_bodega
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Fútbol de salón')
				begin
					set @opcion_respuesta_actual = @respuesta_futbol_de_salon
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Bar - Cantina')
				begin
					set @opcion_respuesta_actual = @respuesta_bar_cantina
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Cibercafé')
				begin
					set @opcion_respuesta_actual = @respuesta_cibercafe
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Mini Socios')
				begin
					set @opcion_respuesta_actual = @respuesta_mini_socios
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Parque atracciones')
				begin
					set @opcion_respuesta_actual = @respuesta_parque_atracciones
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Beer Express')
				begin
					set @opcion_respuesta_actual = @respuesta_beer_express
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Evento vecindario')
				begin
					set @opcion_respuesta_actual = @respuesta_evento_vecindario
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Comedor Popular')
				begin
					set @opcion_respuesta_actual = @respuesta_comedor_popular
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Salón de belleza')
				begin
					set @opcion_respuesta_actual = @respuesta_salon_de_belleza
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Evento deportivo')
				begin
					set @opcion_respuesta_actual = @respuesta_evento_deportivo
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Educ.: Escuela')
				begin
					set @opcion_respuesta_actual = @respuesta_educ_escuela
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Drogería, Farmacia')
				begin
					set @opcion_respuesta_actual = @respuesta_drogeria_farmacia
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Rest Comida Oriental')
				begin
					set @opcion_respuesta_actual = @respuesta_rest_comida_oriental
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Frutería')
				begin
					set @opcion_respuesta_actual = @respuesta_fruteria
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Cevichería')
				begin
					set @opcion_respuesta_actual = @respuesta_cevicheria
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Teatro - Cine')
				begin
					set @opcion_respuesta_actual = @respuesta_teatro_cine
				end		
				if(@tipo_negocio_group_tipo_de_negocio = 'Evento de empresa')
				begin
					set @opcion_respuesta_actual = @respuesta_evento_de_empresa
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Empresa, Fábrica')
				begin
					set @opcion_respuesta_actual = @respuesta_empresa_fabrica
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Educ.: Escuela priv.')
				begin
					set @opcion_respuesta_actual = @respuesta_educ_escuela_priv
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Productos gourmet')
				begin
					set @opcion_respuesta_actual = @respuesta_productos_gourmet
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Motel, Residencia')
				begin
					set @opcion_respuesta_actual = @respuesta_motel_residencia
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Evento marketing')
				begin
					set @opcion_respuesta_actual = @respuesta_evento_marketing
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Evento musical')
				begin
					set @opcion_respuesta_actual = @respuesta_evento_musical
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Entidad Educativa')
				begin
					set @opcion_respuesta_actual = @respuesta_entidad_educativa
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Bar en la playa')
				begin
					set @opcion_respuesta_actual = @respuesta_bar_en_la_playa
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Educ.: Universidad')
				begin
					set @opcion_respuesta_actual = @respuesta_educ_universidad
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Recreación')
				begin
					set @opcion_respuesta_actual = @respuesta_recreacion
				end
				if(@tipo_negocio_group_tipo_de_negocio = 'Café')
				begin
					set @opcion_respuesta_actual = @respuesta_cafe
				end
				
				insert into mardiscore.AnswerDetail values
					(
						newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
					)
			end

			--ubicación
			set @seccion_actual = '60F2733A-2C90-40FB-217B-08D47AAEC004'
			
			--provincia
			set @respuesta_actual = newid()		

			if (@localizacion_group_provincia is not null)
			begin
				set @pregunta_actual = @pregunta_provincia

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)

				if(@localizacion_group_provincia = 'Azuay')
				begin
					set @opcion_respuesta_actual = @respuesta_azuay
				end
				if(@localizacion_group_provincia = 'Bolívar')
				begin
					set @opcion_respuesta_actual = @respuesta_bolivar
				end
				if(@localizacion_group_provincia = 'Cañar')
				begin
					set @opcion_respuesta_actual = @respuesta_cañar
				end
				if(@localizacion_group_provincia = 'Carchi')
				begin
					set @opcion_respuesta_actual = @respuesta_carchi
				end
				if(@localizacion_group_provincia = 'Chimborazo')
				begin
					set @opcion_respuesta_actual = @respuesta_chimborazo
				end
				if(@localizacion_group_provincia = 'Cotopaxi')
				begin
					set @opcion_respuesta_actual = @respuesta_cotopaxi
				end
				if(@localizacion_group_provincia = 'El_Oro')
				begin
					set @opcion_respuesta_actual = @respuesta_el_oro
				end
				if(@localizacion_group_provincia = 'Esmeraldas')
				begin
					set @opcion_respuesta_actual = @respuesta_esmeraldas
				end
				if(@localizacion_group_provincia = 'Galápagos')
				begin
					set @opcion_respuesta_actual = @respuesta_galapagos
				end
				if(@localizacion_group_provincia = 'Guayas')
				begin
					set @opcion_respuesta_actual = @respuesta_guayas
				end
				if(@localizacion_group_provincia = 'Imbabura')
				begin
					set @opcion_respuesta_actual = @respuesta_imbabura
				end
				if(@localizacion_group_provincia = 'Loja')
				begin
					set @opcion_respuesta_actual = @respuesta_loja
				end
				if(@localizacion_group_provincia = 'Los Ríos')
				begin
					set @opcion_respuesta_actual = @respuesta_los_rios
				end
				if(@localizacion_group_provincia = 'Manabí')
				begin
					set @opcion_respuesta_actual = @respuesta_manabi
				end
				if(@localizacion_group_provincia = 'Morona_Santiago')
				begin
					set @opcion_respuesta_actual = @respuesta_morona_santiago
				end
				if(@localizacion_group_provincia = 'Napo')
				begin
					set @opcion_respuesta_actual = @respuesta_napo
				end
				if(@localizacion_group_provincia = 'Orellana')
				begin
					set @opcion_respuesta_actual = @respuesta_orellana
				end
				if(@localizacion_group_provincia = 'Pastaza')
				begin
					set @opcion_respuesta_actual = @respuesta_pastaza
				end
				if(@localizacion_group_provincia = 'Pichincha')
				begin
					set @opcion_respuesta_actual = @respuesta_pichincha
				end
				if(@localizacion_group_provincia = 'Santa_Elena')
				begin
					set @opcion_respuesta_actual = @respuesta_santa_elena
				end
				if(@localizacion_group_provincia = 'Santo_Domingo')
				begin
					set @opcion_respuesta_actual = @respuesta_santo_domingo_de_los_tsachilas
				end
				if(@localizacion_group_provincia = 'Sucumbíos')
				begin
					set @opcion_respuesta_actual = @respuesta_sucumbios
				end
				if(@localizacion_group_provincia = 'Tungurahua')
				begin
					set @opcion_respuesta_actual = @respuesta_tungurahua
				end
				if(@localizacion_group_provincia = 'Zamora')
				begin
					set @opcion_respuesta_actual = @respuesta_zamora_chinchipe
				end
				
				insert into mardiscore.AnswerDetail values
					(
						newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
					)
			end

			--CIUDAD
			set @respuesta_actual = newid()		

			if (@localizacion_group_ciudad is not null)
			begin
				set @pregunta_actual = @pregunta_ciudad

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_ciudad,null
					)
			end

			--ZONA
			set @respuesta_actual = newid()		

			if (@localizacion_group_zona is not null)
			begin
				set @pregunta_actual = @pregunta_zona

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_zona,null
					)
			end

			--BARRIO
			set @respuesta_actual = newid()		

			if (@localizacion_group_barrio is not null)
			begin
				set @pregunta_actual = @pregunta_barrio

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_barrio,null
					)
			end

			--CALLE PRINCIPAL
			set @respuesta_actual = newid()		

			if (@localizacion_group_calle_principal is not null)
			begin
				set @pregunta_actual = @pregunta_calle_principal

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_calle_principal,null
					)
			end

			--NUMERACIÓN
			set @respuesta_actual = newid()		

			if (@localizacion_group_numeracion is not null)
			begin
				set @pregunta_actual = @pregunta_numeracion

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_numeracion,null
					)
			end

			--CALLE SECUNDARIA
			set @respuesta_actual = newid()		

			if (@localizacion_group_calle_secundaria is not null)
			begin
				set @pregunta_actual = @pregunta_calle_secundaria

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_calle_secundaria,null
					)
			end

			--REFERENCIA
			set @respuesta_actual = newid()		

			if (@localizacion_group_referencia is not null)
			begin
				set @pregunta_actual = @pregunta_referencia

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @localizacion_group_referencia,null
					)
			end

			--------------------------------------------------------------------------------------------------------
			----------------------------------------------EQUIPOS DE FRIO-------------------------------------------
			--------------------------------------------------------------------------------------------------------
			
			declare @f_copynumber int,
					@f_brandeo varchar(100),
					@f_tipo varchar(100),
					@f_marca varchar(100),
					@f_modelo varchar(100),
					@f_placa varchar(100),
					@f_serie varchar(100),
					@f_uri varchar(100)

			declare cur_frios cursor for
				select [_URI],[_ORDINAL_NUMBER],[BRANDEO],[TIPO],[MARCA],[MODELO],[PLACA],[SERIE]
				from [MardisAggregate].[CENSO_EJECUCION_FRIOS_CN_EQUIPOS_FRIO_REPEAT]
				where [_TOP_LEVEL_AURI] = @_uri
			
			open cur_frios

			fetch next from cur_frios into @f_uri, @f_copynumber, @f_brandeo, @f_tipo, @f_marca, @f_modelo, @f_placa, @f_serie

			while(@@FETCH_STATUS=0)
			begin
				
				set @f_copynumber = @f_copynumber-1

				print @f_copynumber

				--EQUIPO DE FRÍO.	
				set @seccion_actual = '2EAB98E1-5722-4F8E-217C-08D47AAEC004'

				--TIPO
				set @respuesta_actual = newid()		

				if (@f_tipo is not null)
				begin
					set @pregunta_actual = @pregunta_tipo

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount = @cuenta 
										and IdTask = @tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion = @pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @f_tipo,null
						)
				end

				--MARCA
				set @respuesta_actual = newid()		

				if (@f_marca is not null)
				begin
					set @pregunta_actual = @pregunta_marca

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount=@cuenta 
										and IdTask=@tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion = @pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @f_marca,null
						)
				end

				--MODELO
				set @respuesta_actual = newid()		

				if (@f_modelo is not null)
				begin
					set @pregunta_actual = @pregunta_modelo

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount=@cuenta 
										and IdTask=@tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion=@pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @f_modelo,null
						)
				end
			
				--BRANDEO
				set @respuesta_actual = newid()		

				if (@f_brandeo is not null)
				begin
					set @pregunta_actual = @pregunta_brandeo

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount=@cuenta 
										and IdTask=@tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion=@pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end

					if(@f_brandeo = 'pilsener')
					begin
						set @opcion_respuesta_actual = @respuesta_pilsener
					end
					if(@f_brandeo = 'club')
					begin
						set @opcion_respuesta_actual = @respuesta_club
					end
					if(@f_brandeo = 'conquer')
					begin
						set @opcion_respuesta_actual = @respuesta_conquer
					end
					if(@f_brandeo = 'pilsener_light')
					begin
						set @opcion_respuesta_actual = @respuesta_pilsener_light
					end
					if(@f_brandeo = 'pilsenerador')
					begin
						set @opcion_respuesta_actual = @respuesta_pilsenerador
					end
					if(@f_brandeo = 'pony_malta')
					begin
						set @opcion_respuesta_actual = @respuesta_pony_malta
					end
					if(@f_brandeo = 'dorada')
					begin
						set @opcion_respuesta_actual = @respuesta_dorada
					end
					if(@f_brandeo = 'miller_lite')
					begin
						set @opcion_respuesta_actual = @respuesta_miller_lite
					end
					if(@f_brandeo = 'miller')
					begin
						set @opcion_respuesta_actual = @respuesta_miller
					end
					if(@f_brandeo = 'multimarcas')
					begin
						set @opcion_respuesta_actual = @respuesta_multimarcas
					end
					if(@f_brandeo = 'clausen')
					begin
						set @opcion_respuesta_actual = @respuesta_clausen
					end
					if(@f_brandeo = 'manantial')
					begin
						set @opcion_respuesta_actual = @respuesta_manantial
					end
					if(@f_brandeo = 'pilsener_cero')
					begin
						set @opcion_respuesta_actual = @respuesta_pilsener_cero
					end
					if(@f_brandeo = 'club_cacao')
					begin
						set @opcion_respuesta_actual = @respuesta_club_cacao
					end
					if(@f_brandeo = 'pony_malta_plus')
					begin
						set @opcion_respuesta_actual = @respuesta_pony_malta_plus
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', @f_copynumber, null,null
						)
				end

				--PLACA
				set @respuesta_actual = newid()		

				if (@f_placa is not null)
				begin
					set @pregunta_actual = @pregunta_placa

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount=@cuenta 
										and IdTask=@tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion=@pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @f_placa,null
						)
				end

				--SERIE
				set @respuesta_actual = newid()		

				if (@f_serie is not null)
				begin
					set @pregunta_actual = @pregunta_serie

					if not exists(
									select id from mardiscore.Answer 
									where IdAccount=@cuenta 
										and IdTask=@tarea_actual 
										and IdServiceDetail = @seccion_actual 
										and IdMerchant = @mercaderista 
										and IdQuestion=@pregunta_actual
								)
					begin
						--inserto respuesta
						insert into MardisCore.Answer values
							(
								@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
								@pregunta_actual, 'A'
							)
					end
					else
					begin
						--recupero respuesta
						select @respuesta_actual = id 
						from mardiscore.Answer 
						where IdAccount=@cuenta and IdTask=@tarea_actual 
							and IdServiceDetail = @seccion_actual 
							and IdMerchant = @mercaderista 
							and IdQuestion = @pregunta_actual
					end
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @f_serie,null
						)
				end

				-- foto FRIO
				set @respuesta_actual = newid()
				select @uri_multimedia = D._URI 
				from MardisAggregate.CENSO_EJECUCION_FRIOS_CN_EQUIPOS_FRIO_REPEAT A
					JOIN MardisAggregate.CENSO_EJECUCION_FRIOS_CN_IMG_FRIO_BN B ON A._URI = B._PARENT_AURI
					JOIN MardisAggregate.CENSO_EJECUCION_FRIOS_CN_IMG_FRIO_REF C ON B._URI=C._DOM_AURI
					JOIN MardisAggregate.CENSO_EJECUCION_FRIOS_CN_IMG_FRIO_BLB D ON D._URI=C._SUB_AURI 
				where A._URI = @f_uri

				if(@uri_multimedia is not null)
				begin
					set @pregunta_actual = @pregunta_foto_frio

					insert into MardisCore.Answer values
						(
							@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
							@pregunta_actual, 'A'
						)
					
					insert into mardiscore.AnswerDetail values
						(
							newid(), null, getdate(), @respuesta_actual, 'A', @f_copynumber, @uri_multimedia,null
						)
				end

				--inserto ocurrencias de seccion dinamica
				insert into MardisCore.ServiceDetailTask values(newid(), @seccion_actual, @tarea_actual , 'A')

				fetch next from cur_frios into @f_uri, @f_copynumber, @f_brandeo, @f_tipo, @f_marca, @f_modelo, @f_placa, @f_serie
			end

			close cur_frios
			deallocate cur_frios
			--------------------------------------------------------------------------------------------------------

			--MATERIAL POP
			set @seccion_actual = '8C9F7C90-74CC-4D0A-217D-08D47AAEC004'
			
			--SELECCIONAR POP
			set @respuesta_actual = newid()		

			if (@pop_group_material_pop is not null)
			begin
				set @pregunta_actual = @pregunta_pop

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)

				if(@pop_group_material_pop = 'letrero_personalizado')
				begin
					set @opcion_respuesta_actual = @respuesta_letrero_personalizado
				end
				if(@pop_group_material_pop = 'banderola_metalica')
				begin
					set @opcion_respuesta_actual = @respuesta_banderola_metálica
				end
				if(@pop_group_material_pop = 'totem')
				begin
					set @opcion_respuesta_actual = @respuesta_totem
				end
				if(@pop_group_material_pop = 'banderines')
				begin
					set @opcion_respuesta_actual = @respuesta_banderines
				end
				if(@pop_group_material_pop = 'cabeceras')
				begin
					set @opcion_respuesta_actual = @respuesta_cabeceras
				end
				if(@pop_group_material_pop = 'biombo')
				begin
					set @opcion_respuesta_actual = @respuesta_biombo
				end
				if(@pop_group_material_pop = 'adhesivos')
				begin
					set @opcion_respuesta_actual = @respuesta_adhesivos
				end
				if(@pop_group_material_pop = 'muneco')
				begin
					set @opcion_respuesta_actual = @respuesta_muñeco
				end
				if(@pop_group_material_pop = 'tacho')
				begin
					set @opcion_respuesta_actual = @respuesta_tacho
				end
				if(@pop_group_material_pop = 'otro')
				begin
					set @opcion_respuesta_actual = @respuesta_otros
				end
				
				insert into mardiscore.AnswerDetail values
					(
						newid(), @opcion_respuesta_actual, getdate(), @respuesta_actual, 'A', 0, null,null
					)
			end

			-- foto pop
			set @respuesta_actual = newid()
			select @uri_multimedia = _URI from MardisAggregate.CENSO_EJECUCION_FRIOS_CN_POP_GROUP_IMG_POP_BLB where _TOP_LEVEL_AURI = @_uri

			if(@uri_multimedia is not null)
			begin
				set @pregunta_actual = @pregunta_foto_pop

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @uri_multimedia,null
					)
			end

			--OBSERVACIONES
			set @seccion_actual = 'A5AB6763-53F8-44F8-217E-08D47AAEC004'

			--OBSERVACIONES
			set @respuesta_actual = newid()		

			if (@observaciones_group_obsevaciones is not null)
			begin
				set @pregunta_actual = @pregunta_observaciones

				insert into MardisCore.Answer values
					(
						@respuesta_actual, @cuenta, @tarea_actual, getdate(), @seccion_actual, @mercaderista,
						@pregunta_actual, 'A'
					)
					
				insert into mardiscore.AnswerDetail values
					(
						newid(), null, getdate(), @respuesta_actual, 'A', 0, @observaciones_group_obsevaciones,null
					)
			end

		end

	end

	fetch next from cur_encuestas into 
		@_uri, @_creator_uri_user, @_submission_date,@tipo_negocio_abierto,@user_info_cod,@imei_id,@pop_group_material_pop,
		@validacion_1_local_nombre, @user_info_cod_arca,@equipos_frio_repeat_count,@pop_group_material_pop_otro,
		@observaciones_group_obsevaciones,@localizacion_group_barrio,@localizacion_group_numeracion,@repetido_cod_rep,
		@tipo_negocio_group_tipo_de_negocio, @localizacion_group_zona,@localizacion_group_calle_secundaria, 
		@validacion_1_telefono,@tipo_negocio_group_canal_de_negocio, @localizacion_group_ciudad, @tipo_negocio_group_tipo_negocio_otro,
		@validacion_1_numero_frio, @user_info_ruta,@sim_serial,@tipo_negocio_colabora,@localizacion_group_calle_principal,@validacion_1_celular,
		@localizacion_group_referencia,@user_info_tipo_ejecucion,@user_info_existe,@validacion_1_ruc, @validacion_1_nombre_propietario,
		@localizacion_group_provincia,@ubicacion_coord_lat,@ubicacion_coord_lng
end

close cur_encuestas
deallocate cur_encuestas

GO
