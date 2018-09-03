using System.Collections.Generic;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.UserViewModels
{
    public class UserIndexViewModel : PaginatedList
    {
        public UserIndexViewModel() : base("User", "User", "Index")
        {
        }

        public List<UserViewModel> Users { get; set; }
    }
}
