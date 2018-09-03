using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
using Mardis.Engine.Framework;
using AutoMapper;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class EquipmentDao : ADao
    {

        public EquipmentDao(MardisContext mardisContext)
            : base(mardisContext)
        {
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
            CoreFilterDao = new CoreFilterDao(mardisContext);
        }

        public List<Equipament> GetPaginatedEquipmentList(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            var resultList = Context.Equipaments
                .Include(x => x.Equipament_statuss)
                .Include(z=>z.Branches)
                .Where(strPredicate)
                .OrderBy(b => b.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                 ;
            var result = resultList.ToList<Equipament>();

            return result;
        }
        public int GetPaginatedEquipmentsCount(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.Equipaments
                .Where(strPredicate)
                .Count();
        }

        public List<Equipament_type> GetUserListByType()
        {
            var resultList = Context.Equipaments_type
                .Include(u => u.Equipaments)
                //.Where(usr => usr.Equipaments. == typeUser &&
                //              usr.StatusRegister == CStatusRegister.Active &&
                //              usr.IdAccount == idAccount)
                .ToList();
            return resultList;
        }

        public List<Equipament_status> GetUserListBystatus()
        {
            var resultList = Context.Equipaments_status
                .Include(u => u.Equipaments)
                //.Where(usr => usr.Equipaments. == typeUser &&
                //              usr.StatusRegister == CStatusRegister.Active &&
                //              usr.IdAccount == idAccount)
                .ToList();
            return resultList;
        }

        public Equipament GetEquipament_Edit(int Id)
        {
            var resultList = Context.Equipaments
                 .Include(u =>u.Equipament_statuss)
                .Where(x => x.Id.Equals(Id)).First();
            //.Where(usr => usr.Equipaments. == typeUser &&
            //              usr.StatusRegister == CStatusRegister.Active &&
            //              usr.IdAccount == idAccount)

            return resultList;
        }
        public List<Equipament> GetEquipamentFotos(int codigo)
        {
            return Context.Equipaments.Where(x => x.Id > codigo).ToList();
        }
        public Equipament GetEquipamentProfile(int Id)
        {
            var resultList = Context.Equipaments
                .Include(u => u.Branches)
                 .Include(u => u.Branches.TaskCampaigns)
               .Include(u => u.Equipament_statuss)
               .Include(u => u.Branches.BranchImages)
               .Include(u=>u.EquipamentImg)
               .Include(u=>u.Activities)
               
                .Where(x => x.Id.Equals(Id)).First();
            //.Where(usr => usr.Equipaments. == typeUser &&
            //              usr.StatusRegister == CStatusRegister.Active &&
            //              usr.IdAccount == idAccount)

            return resultList;
        }

        public Equipament SaveEquipment(Equipament entity)
        {

            if (entity.Idbranch == Guid.Parse("00000000-0000-0000-0000-000000000000")) entity.Idbranch = null;
         ///  
            Context.Equipaments.Add(entity);

            if (entity.Id < 1) {
                entity.CreationDate = DateTime.Now;
                Context.Entry(entity).State = EntityState.Added; 
            } 
            else Context.Entry(entity).State = EntityState.Modified;


            Context.SaveChanges();

            return null;
        }

        public int DeleteEquipment(int Id)
        {
            Context.Equipaments.RemoveRange(Context.Equipaments.Where(x => x.Id == Id));
            var status = Context.SaveChanges();
            return 0;
        }
    }
}
