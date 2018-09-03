CREATE view [dbo].[vw_Campaign_Information] with schemabinding
AS
select 
	j.id as IdTask, j.code as TaskCode, j.IdCampaign, j.IdAccount, a.Name as CampaignName, a.IdCustomer, k.name as CustomerName, k.code as CustomerCode, 
	j.IdMerchant, --m.Name as NameCustomer, m.SurName as SurNameCustomer,	
	j.DateCreation, j.StartDate, j.IdBranch, o.Name as BranchName, o.LenghtBranch, 
	o.LatitudeBranch, j.IdStatusTask, u.Name as StatusTaskName, t.Name as OwnerName, s.Name as DistrictName, o.Zone, p.Name as ParishName, o.Neighborhood, o.MainStreet, o.SecundaryStreet as BranchDirection, 
	o.Reference, j.IdTaskNoImplementedReason, 
	j.CommentTaskNoImplemented, j.Description, o.TypeBusiness, o.ExternalCode, o.Code as BranchCode, j.Route, o.Label, t.Phone, t.Mobile,
	q.Name as ProvinceName, t.document, l.IdPerson as IdMerchantPerson
from mardiscore.campaign a
	--inner join mardiscore.campaignservices b on a.id=b.idCampaign
	--inner join mardiscore.service c on c.id=b.idService
	--Secciones normales
	--inner join mardiscore.servicedetail d on d.idService = c.id
	--inner join mardiscore.question f on f.idServiceDetail=d.id
	--inner join mardiscore.questiondetail g on g.idQuestion=f.id
	----Subsecciones
	--inner join mardiscore.servicedetail e on d.id=e.idsection
	--inner join mardiscore.question h on h.idServiceDetail=e.id
	--inner join mardiscore.questiondetail i on i.idQuestion=h.id
	--Info General
	inner join mardiscore.task j on j.idCampaign=a.Id 
	inner join mardiscore.Customer k on k.id=a.idCustomer
	inner join mardissecurity.[User] l on l.Id=j.IdMerchant
	--inner join mardiscommon.Person m on m.Id=l.IdPerson
	inner join mardissecurity.Profile n on n.id=l.idProfile
	inner join mardiscore.branch o on o.id=j.idBranch
	inner join mardiscommon.parish p on p.id=o.idParish
	inner join mardiscommon.province q on q.id=o.idProvince
	inner join mardiscommon.sector r on r.id=o.idSector
	inner join mardiscommon.district s on s.id=o.idDistrict
	inner join mardiscommon.person t on t.id=o.idPersonOwner
	inner join mardiscore.statustask u on u.id=j.idStatustask