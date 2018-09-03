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
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BranchDao : ADao
    {
        public BranchDao(MardisContext mardisContext)
            : base(mardisContext)
        {
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
            CoreFilterDao = new CoreFilterDao(mardisContext);
        }

        public Branch GetOne(Guid id, Guid idAccount)
        {
            return Context.Branches
                .Include(b => b.PersonAdministration)
                .Include(b => b.PersonOwner)
                .Include(b => b.BranchCustomers)
                    .ThenInclude(bc => bc.Channel)
                .Include(b => b.BranchCustomers)
                    .ThenInclude(bc => bc.Customer)
                .Include(b => b.BranchCustomers)
                    .ThenInclude(bc => bc.TypeBusiness)
                .Include(b => b.Parish)
                .Include(b => b.Sector)
                .Include(b => b.District)
                .Include(b => b.BranchImages)
                .FirstOrDefault(tb => tb.Id == id &&
                                      tb.StatusRegister == CStatusRegister.Active &&
                                      tb.IdAccount == idAccount);
        }

        public Person GetOnePerson(Guid? id)
        {
            return Context.Persons.Where(x => x.Id==id).First();
        }
        public object GetBranchList(Guid idAccount,String Imeid)
        {
            List<Branch> consulta = new List<Branch>();
            if (Imeid == "")
            {
                 consulta = Context.Branches.Include(b => b.PersonAdministration).Where(tb => tb.StatusRegister == CStatusRegister.Active && tb.IdAccount == idAccount && tb.ESTADOAGGREGATE == "S").ToList();
            }
            else
            {
                consulta = Context.Branches.Include(b => b.PersonAdministration).Where(tb => tb.StatusRegister == CStatusRegister.Active && tb.IdAccount == idAccount && tb.ESTADOAGGREGATE == "S" && tb.IMEI_ID.Contains(Imeid)).ToList();

            }
                return consulta.Select(x => new
                {
                    x.Id,
                    x.IdAccount,
                    x.ExternalCode,
                    x.Code,
                    x.Name,
                    x.MainStreet,
                    x.Neighborhood,
                    x.Reference,
                    Propietario = x.PersonAdministration.Name,
                    x.IdProvince,
                    x.IdDistrict,
                    x.IdParish,
                    x.RUTAAGGREGATE,
                    x.IMEI_ID,
                    x.LatitudeBranch,
                    x.LenghtBranch,
                    Celular=x.PersonAdministration.Phone
                });
            
        }

        public IQueryable<Branch> GetAllBranches()
        {
            return Context.Branches;
        }

        public Branch SaveBranch(Branch branch)
        {
            if (branch.Id != Guid.Empty)
            {
                Context.Branches.Update(branch);
            }
            InsertOrUpdate(branch);
            return branch;
        }

        public List<Branch> GetBranchesList(string[] idBranches, Guid idAccount)
        {
            try
            {
                var items = Context.Branches
                .Where(b => idBranches.Contains(b.Id.ToString()) &&
                b.IdAccount == idAccount)
                .ToList();
                return items;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Branch> GetBranchesList(Guid idAccount)
        {
            var items = Context.Branches
            .Where(b => b.StatusRegister == CStatusRegister.Active &&
            b.IdAccount == idAccount)
            .ToList();

            return items;
        }

        public List<Branch> SearchBranches(Guid idCountry, Guid idProvince, Guid idDistrict,
            string documentType, string document, string nameBranch, string ownerName, string codeBranch, Guid idAccount)
        {

            var strPredicate = $" StatusRegister ==\"{CStatusRegister.Active}\" && IdAccount== \"{idAccount.ToString()}\" ";

            if (idCountry != Guid.Empty)
            {
                strPredicate += $"&& IdCountry == \"{idCountry}\" ";
            }

            if (idProvince != Guid.Empty)
            {
                strPredicate += $"&& IdProvince == \"{idProvince}\" ";
            }

            if (idDistrict != Guid.Empty)
            {
                strPredicate += $"&& IdDistrict == \"{idDistrict}\" ";
            }

            if (!string.IsNullOrEmpty(documentType))
            {
                strPredicate += $" && PersonOwner.TypeDocument.Contains(\"{documentType}\") ";
            }

            if (!string.IsNullOrEmpty(document))
            {
                strPredicate += $" && PersonOwner.Document.Contains(\"{document}\") ";
            }

            if (!string.IsNullOrEmpty(nameBranch))
            {
                strPredicate += $" && Name.Contains(\"{nameBranch}\") ";
            }

            if (!string.IsNullOrEmpty(ownerName))
            {
                strPredicate += $" && PersonOwner.Name.Contains(\"{ownerName}\") ";
            }

            if (!string.IsNullOrEmpty(codeBranch))
            {
                strPredicate += $" && ExternalCode.Contains(\"{codeBranch}\") ";
            }

            var searchResult = Context.Branches
                .Include(b => b.PersonOwner)
                .Where(strPredicate)
                .ToList();
            return searchResult;
        }

        public void DeleteBranchCustomers(Guid id, Guid idAccount)
        {
            var itemsDelete = Context.BranchCustomers
                .Where(bc => bc.IdBranch == id && bc.Branch.IdAccount == idAccount)
                .ToList();

            Context.BranchCustomers.RemoveRange(itemsDelete);
            Context.SaveChanges();
        }

        public Branch GetBranchByExternalCode(string externalCode, Guid idAccount)
        {
            var itemReturn = Context.Branches
                                   .FirstOrDefault(tb => tb.ExternalCode == externalCode &&
                                                 tb.IdAccount == idAccount &&
                                                 tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        public List<Branch> GetPaginatedBranchesList(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            var resultList = Context.Branches
                .Where(strPredicate)
                .OrderBy(b => b.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return resultList;
        }

        public int GetPaginatedBranchesCount(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.Branches
                .Where(strPredicate)
                .Count();
        }

        public List<Branch> GetAll(List<FilterValue> filters, int pageIndex, int pageSize, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            if (filters.Any())
            {
                strPredicate += "&&(";
            }

            var predicate = GetFilterPredicate(filters, "OR");

            if (!string.IsNullOrEmpty(predicate))
            {
                strPredicate += predicate.Substring(3) + ")";
            }

            return Context.Branches
                .Where(strPredicate)
                .OrderBy(b => b.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetAllCount(Guid idaccount){
            var CountBranch = Context.Branches.Where(x => x.IdAccount == idaccount).Count();
            if (CountBranch == null)
            {
                return 0;
            }
            else
            {
                return (int)CountBranch;
            }


        }

        public BranchImages GetDataImage(Guid id)
        {
             
            return Context.BranchImageses.Where (x=>x.Id.Equals(id)).First();
        }


        public int UpdateDataImage(Guid id,string url)
        {
            try
            {
                var stateRegister = EntityState.Modified;

                var _model = Context.BranchImageses.Where(x => x.Id.Equals(id)).First();
                _model.UrlImage = url;
               Context.BranchImageses.Add(_model);
                Context.Entry(_model).State = stateRegister;
                Context.SaveChanges();
                return 1;

            }
            catch (Exception)
            {

                return 0;
            }
         
   
        }

    }
}
