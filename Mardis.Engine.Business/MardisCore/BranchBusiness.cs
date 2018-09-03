using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class BranchBusiness : ABusiness
    {
        private readonly BranchDao _branchDao;
        private readonly SequenceBusiness _sequenceBusiness;
        private readonly PersonDao _personDao;
        private readonly BranchCustomerDao _branchCustomerDao;
        private readonly TaskCampaignDao _taskCampaignDao;

        public BranchBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _branchDao = new BranchDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
            _personDao = new PersonDao(mardisContext);
            _branchCustomerDao = new BranchCustomerDao(mardisContext);
            _taskCampaignDao = new TaskCampaignDao(mardisContext);
        }

        public IQueryable<Branch> GetAll()
        {
            return _branchDao.GetAllBranches();
        }

        public Branch GetOne(Guid id, Guid idAccount)
        {
            return _branchDao.GetOne(id, idAccount);
        }

        public Branch GetBranchCompleteProfile(Guid id, Guid idAccount)
        {
            var itemResult = _branchDao.GetOne(id, idAccount);

            var taskCampaignList = _taskCampaignDao.GetTaskListByBranch(itemResult.Id, idAccount);

            itemResult.TaskCampaigns = taskCampaignList;

            return itemResult;
        }

        public void DeleteAccounts(string[] results)
        {
            _branchCustomerDao.DeleteAccounts(results);
        }

        public void AddAccount(BranchRegisterViewModel model, Guid idAccount)
        {
            var branchCustomer = new BranchCustomer()
            {
                IdBranch = model.Id,
                IdChannel = model.IdChannel,
                IdCustomer = model.IdCustomer,
                IdTypeBusiness = model.IdTypeBusiness
            };

            _branchCustomerDao.InsertOrUpdate(branchCustomer);
        }
        public int GetCountBranch(Guid idaccount)
        {
            return _branchDao.GetAllCount(idaccount);
        }
        public List<Branch> GetBranchesList(string[] idBranches, Guid idAccount)
        {
            return _branchDao.GetBranchesList(idBranches, idAccount);
        }

        public List<Branch> GetBranchesList(Guid idAccount)
        {
            return _branchDao.GetBranchesList(idAccount);
        }
        //listar branch para android
        public object GetBranchesListAndroid(Guid idAccount , String Imeid)
        {
            return _branchDao.GetBranchList(idAccount, Imeid);
        }

        public List<Branch> SearchBranches(Guid idCountry, Guid idProvince, Guid idDistrict,
            string documentType, string document, string nameBranch, string ownerName, string codeBranch, Guid idAccount)
        {
            return _branchDao.SearchBranches(idCountry, idProvince, idDistrict, documentType, document, nameBranch, ownerName, codeBranch, idAccount);
        }

        public void DeleteBranchCustomers(Guid id, Guid idAccount)
        {
            _branchDao.DeleteBranchCustomers(id, idAccount);
        }

        public Branch SaveBranch(Branch branch, Guid idAccount)
        {
            branch.PersonOwner.IdAccount = idAccount;
            branch.PersonAdministration.IdAccount = idAccount;
            branch.StatusRegister = CStatusRegister.Active;
            branch.IdAccount = idAccount;
            branch.Country = null;
            branch.Province = null;
            branch.District = null;
            branch.Parish = null;
            branch.Sector = null;

            if (branch.PersonAdministration.Id != Guid.Empty)
            {
                branch.PersonAdministration = null;
            }
            if (branch.PersonOwner.Id != Guid.Empty)
            {
                branch.PersonOwner = null;
            }

            branch.IdAccount = idAccount;
            branch.StatusRegister = CStatusRegister.Active;

            var stateRegister = Guid.Empty == branch.Id ? EntityState.Added : EntityState.Modified;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {

                    //Cuando el dueño es administrador y es un nuevo dueño
                    if (branch.IsAdministratorOwner == CBranch.Yes &&
                       (branch.IdPersonAdministrator == Guid.Empty || null == branch.IdPersonAdministrator))
                    {
                        var personOwner = branch.PersonOwner;
                        var secuencia = _sequenceBusiness.NextSequence(CPerson.SequenceCode, idAccount);

                        if (personOwner != null)
                        {
                            personOwner.Code = secuencia.ToString();
                            Context.Persons.Add(personOwner);
                            Context.SaveChanges();
                            branch.PersonOwner = null;
                            branch.PersonAdministration = null;

                            branch.IdPersonAdministrator = personOwner.Id;
                            branch.IdPersonOwner = personOwner.Id;
                        }
                        else
                        {
                            throw new ExceptionMardis("Error al insertar Persona");
                        }
                    }
                   Sequence nextSequence;
                    if (string.IsNullOrEmpty(branch.Code))
                    {
                        nextSequence = _sequenceBusiness.NextSequence(CBranch.SequenceCode, idAccount);
                        var returnCode = nextSequence.Initial + "-" + nextSequence.SequenceCurrent;

                        branch.Code = returnCode;
                    }
                    else
                    {
                        //Elimino los Branchustomers
                        _branchDao.DeleteBranchCustomers(branch.Id, idAccount);
                    }

                    if (branch.IdPersonAdministrator == Guid.Empty)
                    {
                        nextSequence = _sequenceBusiness.NextSequence(CPerson.SequenceCode, idAccount);
                        if (branch.PersonAdministration != null)
                            branch.PersonAdministration.Code = nextSequence.ToString();
                    }

                    if (branch.IdPersonOwner == Guid.Empty)
                    {
                        nextSequence = _sequenceBusiness.NextSequence(CPerson.SequenceCode, idAccount);
                        if (branch.PersonOwner != null) branch.PersonOwner.Code = nextSequence.ToString();
                    }

                    Context.Branches.Add(branch);

                    Context.Entry(branch).State = stateRegister;

                    Context.SaveChanges();

                    transaction.Commit();

                    branch = GetOne(branch.Id, idAccount);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    branch = null;
                }

            }

            return branch;
        }

        [Obsolete]
        public Branch GetBranchByExternalCode(string externalCode, Guid idAccount)
        {
            return _branchDao.GetBranchByExternalCode(externalCode, idAccount);
        }

        public BranchListViewModel GetPaginatedBranches(List<FilterValue> filterValues, int pageSize, int pageIndex, Guid idAccount)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Branch, BranchItemViewModel>();
            });
            var itemResult = new BranchListViewModel();
            var branches = _branchDao.GetPaginatedBranchesList(filterValues, pageSize, pageIndex, idAccount);
            var countBranches = _branchDao.GetPaginatedBranchesCount(filterValues, pageSize, pageIndex, idAccount);

            itemResult.BranchList = Mapper.Map<List<BranchItemViewModel>>(branches);

            return ConfigurePagination(itemResult, pageIndex, pageSize, filterValues, countBranches);
        }

        public BranchRegisterViewModel GetBranchForRegister(Guid idBranch, Guid idAccount)
        {
            return ConvertBranch.ToBranchRegisterViewModel(_branchDao.GetOne(idBranch, idAccount));
        }

        public bool Save(BranchRegisterViewModel model, Guid idAccount)
        {
            var branch = ConvertBranch.FromBranchRegisterViewModel(model);
            //Recupero personas por Documento
            var person = _personDao.GetPersonByDocument(branch.PersonOwner.Document);
            branch.IdPersonOwner = person?.Id ?? Guid.Empty;

            person = _personDao.GetPersonByDocument(branch.PersonAdministration.Document);
            branch.IdPersonAdministrator = person?.Id ?? Guid.Empty;

            SaveBranch(branch, idAccount);
            return true;
        }
    }
}
