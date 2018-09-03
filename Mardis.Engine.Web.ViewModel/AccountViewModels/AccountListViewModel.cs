using System.Collections.Generic;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.AccountViewModels
{
    public class AccountListViewModel : PaginatedList
    {
        public AccountListViewModel() : base("AccountList", "EngineAccount", "Index")
        {
        }

        public List<AccountRegisterViewModel> Accounts { get; set; }
    }
}
