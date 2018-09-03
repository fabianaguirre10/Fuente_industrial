using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Web.ViewModel;

namespace Mardis.Engine.Converter
{
    public class UserConverter
    {

        public static List<SelectViewModel> ConvertUserListToSelectViewModelList(List<User> users)
        {
            return users
                .Select(u => new SelectViewModel()
                {
                    Id = u.Id,
                    Name = u.Profile.Name
                })
                .ToList();

        }

    }
}
