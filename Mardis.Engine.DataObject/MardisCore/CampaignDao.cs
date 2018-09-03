using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class CampaignDao : ADao
    {
        public CampaignDao(MardisContext mardisContext) :
            base(mardisContext)
        {
            CoreFilterDao = new CoreFilterDao(mardisContext);
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
        }

        public Campaign GetCampaignById(Guid idCampaign, Guid idAccount)
        {
            var itemReturn =
                Context.Campaigns
                            .Join(Context.CampaignsServices,
                                                      tb => tb.Id,
                                                      cse => cse.IdCampaign,
                                                      (tb, cse) => new { tb, cse })
                            .Join(Context.Services,
                                                      tb => tb.cse.IdService,
                                                      ser => ser.Id,
                                                      (tb, ser) => new { tb, ser })
                            .FirstOrDefault(tb => tb.tb.tb.Id == idCampaign &&
                                       tb.tb.tb.StatusRegister == CStatusRegister.Active &&
                                       tb.ser.StatusRegister == CStatusRegister.Active &&
                                       tb.tb.cse.StatusRegister == CStatusRegister.Active &&
                                       tb.tb.cse.IdAccount == idAccount);

            return itemReturn?.tb.tb;
        }

        public Campaign GetOne(Guid idCampaign, Guid idAccount)
        {
            return Context.Campaigns
                .FirstOrDefault(c => c.Id == idCampaign &&
                c.StatusRegister == CStatusRegister.Active &&
                c.IdAccount == idAccount);
        }
        public object GetCampaing(){
            var consulta=Context.Campaigns.Where(c=> c.StatusRegister == CStatusRegister.Active ).ToList();
            return consulta.Select(x => new
            {
                x.Id,
                x.Name,
                x.IdAccount
                ,AccountName=Context.Accounts.Where(y=> y.Id==x.IdAccount).FirstOrDefault().Name
            });

        }
        
        public Campaign GetCampaignByName(string nameCampaign, Guid idAccount)
        {
            return Context.Campaigns
                .FirstOrDefault(c => c.Name == nameCampaign &&
                                c.StatusRegister == CStatusRegister.Active &&
                                c.IdAccount == idAccount);
        }
        
        public List<Campaign> GetActiveCampaignsList(Guid idAccount)
        {
            return Context.Campaigns
               .Where(c => c.StatusRegister == CStatusRegister.Active &&
               c.IdAccount == idAccount)
               .ToList();



        }
        public List<Dashboard> GetActiveCampaignsListDasboard(Guid idAccount, Guid idusr)
        {
            var innerJoinQuery =
             from c in Context.Dashboards
             join u in Context.UserCanpaign on c.idcampaign equals u.idCanpaign
             join d in Context.Campaigns on u.idCanpaign equals  d.Id
             where u.idUser == idusr && d.IdAccount == idAccount
             select c; //produces flat sequence
            List<Dashboard> result = innerJoinQuery.ToList<Dashboard>();

            return result;



        }
        

        public List<Campaign> GetPaginatedCampaignList(List<FilterValue> filterValues, int pageSize, int pageNumber, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            var resultList = Context.Campaigns
                .Where(strPredicate)
                .OrderBy(c => c.CreationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return resultList;
        }
        public List<Campaign> GetPaginatedCampaignListbyUser(List<FilterValue> filterValues, int pageSize, int pageNumber, Guid idAccount, Guid iduser, Guid _typeuser)
        {

            if (_typeuser.Equals(new Guid("30DB815C-8B82-47EE-9279-B28922BEB616")))
            {
                var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

                strPredicate += GetFilterPredicate(filterValues);
                var innerJoinQuery =
                 from c in Context.UserCanpaign
                 join d in Context.Campaigns on c.idCanpaign equals d.Id
                 where c.idUser == iduser && d.IdAccount == idAccount
                 orderby d.CreationDate
                 
                 select d; //produces flat sequence

                return innerJoinQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(strPredicate).ToList();
            }
            else {
                //En caso de ser usuario administrador

                var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            var resultList = Context.Campaigns
                .Where(strPredicate)
                .OrderBy(c => c.CreationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return resultList;
            }
        }

        public int GetPaginatedCampaignCount(List<FilterValue> filterValues, int pageSize, int pageNumber, Guid idAccount ,Guid _typeuser, Guid _iduser)
        {
           

            /*Error*/
            //var resultList = Context.Campaigns
            //    .Skip((pageNumber) * pageSize)
            //    .Take(pageSize)
            //    .Count(strPredicate);
            if (_typeuser.Equals(new Guid("30DB815C-8B82-47EE-9279-B28922BEB616")))
            {


                var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount ==\"{idAccount.ToString()}\" ";

                strPredicate += GetFilterPredicate(filterValues);
                var innerJoinQuery =
                 from c in Context.UserCanpaign
                 join d in Context.Campaigns on c.idCanpaign equals d.Id
                 where c.idUser == _iduser && d.IdAccount == idAccount
                 orderby d.CreationDate

                 select d; //produces flat sequence

                return innerJoinQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(strPredicate).Count();
            }
            else {
                var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount ==\"{idAccount.ToString()}\" ";

                strPredicate += GetFilterPredicate(filterValues);
                var resultList = Context.Campaigns

                               .Where(strPredicate)
                              .Count();
                return resultList;
            }
        }

        public List<GeoPositionViewModel> GetCampaignGeopositionBranches(List<FilterValue> filterValues,
            Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount ==\"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.TaskCampaigns
                .Where(strPredicate)
                .Select(c =>
                   new GeoPositionViewModel()
                   {
                       Title = c.Code,
                       IconUrl = GetIcon(c.StatusTask.Name),
                       Latitude = c.Branch.LatitudeBranch.Replace(",", "."),
                       Longitude = c.Branch.LenghtBranch.Replace(",", "."),
                       IdTask = GetIdTask(c.Id),
                       NameBranch = c.Branch.Name,
                       CodeBranch = c.Branch.ExternalCode,
                       //ImageUrl = c.Branch.BranchImages.FirstOrDefault().UrlImage?? ""
                   })
                .ToList();
        }

        private string GetIcon(string status)
        {
            switch (status)
            {
                case CTask.StatusNotImplemented:
                    return CImages.BlueMarker;
                case CTask.StatusImplemented:
                    return CImages.GreenMarker;
                case CTask.StatusPending:
                    return CImages.RedMarker;
                default:
                    return CImages.OrangeMarker;
            }
        }

        public int NumbertaskbyCampaign(Guid idcampaing) {


            return Context.TaskCampaigns.Where(x => x.IdCampaign.Equals(idcampaing) && x.StatusRegister == CStatusRegister.Active).Count();
        }

        private string GetIdTask(Guid id)
        {
            return id.ToString();
        }
    }
}
