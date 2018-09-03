using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Web.ViewModel.UserViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertUser
    {
        public static UserIndexViewModel ToUserIndexViewModel(List<User> users)
        {
            return new UserIndexViewModel()
            {
                Users =
                    users.Select(
                        u =>
                            new UserViewModel()
                            {
                                Account = u.Account.Name,
                                Code = u.Profile.Code,
                                Email = u.Email,
                                Name = u.Profile.Name,
                                ProfileId = u.Profile.Id.ToString(),
                                UserId = u.Id.ToString(),
                                UserType = u.Profile.TypeUser.Name
                            }).ToList()
            };
        }

        public static UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                Account = user.Account.Name,
                Code = user.Profile.Code,
                Document = user.Person.Document,
                Name = user.Profile.Name,
                Email = user.Email,
                IdTypeUser = user.Profile.IdTypeUser.ToString(),
                Mobile = user.Person.Mobile,
                PersonName = user.Person.Name,
                PersonSurname = user.Person.SurName,
                Phone = user.Person.Phone,
                ProfileId = user.Profile.Id.ToString(),
                TypeDocument = user.Person.TypeDocument,
                UserId = user.Id.ToString(),
                UserType = user.Profile.TypeUser.Name,
                PersonId = user.Person.Id.ToString(),
                Avatar = user.Profile.Avatar
            };
        }
    }
}
