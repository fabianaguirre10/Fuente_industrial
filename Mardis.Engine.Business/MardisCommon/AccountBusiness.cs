using System;
using System.Collections.Generic;
using AutoMapper;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Web.ViewModel.AccountViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.DataProtection;

namespace Mardis.Engine.Business.MardisCommon
{
    public class AccountBusiness : ABusiness
    {

        private readonly AccountDao _accountDao;

        public AccountBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _accountDao = new AccountDao(mardisContext);

        }

        public AccountListViewModel GetAccounts(IDataProtector protector, List<FilterValue> filterValues, int pageSize, int pageIndex)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Account, AccountRegisterViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => protector.Protect(src.Id.ToString())))
            );

            var resultList = Mapper.Map<List<Account>, List<AccountRegisterViewModel>>(_accountDao.GetAll(filterValues, pageSize, pageIndex));

            var itemResult = new AccountListViewModel()
            {
                Accounts = resultList
            };

            var count = _accountDao.GetCount(filterValues);

            return ConfigurePagination(itemResult, pageIndex, pageSize, filterValues, count);
        }

        public AccountRegisterViewModel GetAccount(IDataProtector protector, string account)
        {
            if (!string.IsNullOrEmpty(account))
            {
                var idAccount = Guid.Parse(protector.Unprotect(account));
                var currentAccount = _accountDao.Get(idAccount);

                return new AccountRegisterViewModel()
                {
                    Code = currentAccount.Code,
                    Id = protector.Protect(currentAccount.Id.ToString()),
                    Name = currentAccount.Name,
                    StatusRegister = currentAccount.StatusRegister
                };
            }

            return new AccountRegisterViewModel();
        }

        public void Save(AccountRegisterViewModel model, IDataProtector protector)
        {

            var id = string.IsNullOrEmpty(model.Id) ? Guid.Empty : Guid.Parse(protector.Unprotect(model.Id));

            var account = new Account()
            {
                Id = id,
                Code = model.Code,
                Name = model.Name,
                StatusRegister = model.StatusRegister
            };
            _accountDao.InsertOrUpdate(account);
        }
    }
}
