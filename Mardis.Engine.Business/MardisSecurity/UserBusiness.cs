using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.DataObject.MardisSecurity;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.UserViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace Mardis.Engine.Business.MardisSecurity
{
    /// <summary>
    /// Negocio de Usuario
    /// </summary>
    public class UserBusiness : ABusiness
    {
        private readonly UserDao _userDao;
        private readonly TaskCampaignDao _taskCampaignDao;
        private readonly ProfileDao _profileDao;

        public UserBusiness(MardisContext context) : base(context)
        {
            _userDao = new UserDao(context);
            _taskCampaignDao = new TaskCampaignDao(context);
            _profileDao = new ProfileDao(context);
        }

        /// <summary>
        /// Dame Usuario
        /// </summary>
        /// <param name="userName">Email (User)</param>
        /// <returns>Retorna un Usuario</returns>
        public User GetUserByUser(string userName)
        {
            return _userDao.GetUserByUser(userName);
        }

        /// <summary>
        /// Dame Usuario
        /// </summary>
        /// <param name="userId">Identificador de Usuario</param>
        /// <returns>Retorna un Usuario</returns>
        public User GetUserById(Guid userId)
        {
            return _userDao.GetUserById(userId);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _userDao.GetUserByIdasync(userId);
        }

        /// <summary>
        /// Obtiene el listado de los usuarios por tipo de usuario
        /// </summary>
        /// <param name="typeUser">tipo de usuario</param>
        /// <param name="idAccount">id de la cuenta</param>
        /// <returns>Listado de Usuarios de la cuenta actual y tipo de usuario</returns>
        public List<User> GetUserListByType(string typeUser, Guid idAccount)
        {
            var userList = _userDao.GetUserListByType(typeUser, idAccount);

            foreach (var user in userList)
            {
                user.Profile = _profileDao.GetById(user.IdProfile);
            }

            return userList;
        }

        internal List<User> GetMerchantsByCampaign(Guid idCampaign, Guid idAccount, List<FilterValue> filters, int pageIndex, int pageSize)
        {
            //var taskList = _taskCampaignDao.GetAlltasksByCampaignId(idCampaign, idAccount, filters);
            //var merchantIdlList = taskList.Select(t => t.IdMerchant).ToArray();
            return _userDao.GetMerchantsListByArrayIds(null, idAccount, idCampaign);
        }

        public List<User> GetMerchantsByCampaign(Guid idCampaign, Guid idAccount)
        {
            //var taskList = _taskCampaignDao.GetAlltasksByCampaignId(idCampaign, idAccount);

            //var merchantIdlList = taskList.Select(t => t.IdMerchant).ToArray();

            return _userDao.GetMerchantsListByArrayIds(null, idAccount, idCampaign);
        }

        public UserIndexViewModel GetIndex(List<FilterValue> filters, int pageIndex, int pageSize, Guid idAccount, IDataProtector protector)
        {
            var users = _userDao.GetUsers(filters, pageIndex, pageSize, idAccount);
            var itemResult = ConvertUser.ToUserIndexViewModel(users);

            itemResult.Users.ForEach(u =>
            {
                u.UserId = protector.Protect(u.UserId);
                u.ProfileId = protector.Protect(u.ProfileId);
            });

            var userCount = _userDao.GetCount(filters, idAccount);

            return ConfigurePagination(itemResult, pageIndex, pageSize, filters, userCount);
        }

        public UserViewModel GetUser(string userCript, IDataProtector protector, Guid idAccount)
        {
            var itemResult =new UserViewModel();

            if (!string.IsNullOrEmpty(userCript))
            {
                var idUser = Guid.Parse(protector.Unprotect(userCript));
                var user = _userDao.GetUserById(idUser);
                itemResult = ConvertUser.ToUserViewModel(user);
            }

            return itemResult;
        }

        public void Save(UserViewModel model, Guid idAccount)
        {
            var profile = new Profile()
            {
                Code = model.Code,
                Id = Guid.Parse(model.ProfileId),
                Name = model.Name,
                StatusRegister = CStatusRegister.Active,
                IdTypeUser = Guid.Parse(model.IdTypeUser),
                Avatar = model.Avatar
            };

            _profileDao.InsertOrUpdate(profile);

            var person = new Person()
            {
                IdAccount = idAccount,
                Name = model.PersonName,
                Id = Guid.Parse(model.PersonId),
                Code = model.Code,
                StatusRegister = CStatusRegister.Active,
                Document = model.Document,
                TypeDocument = model.TypeDocument,
                Mobile = model.Mobile,
                Phone = model.Phone,
                SurName = model.PersonSurname
            };

            _profileDao.InsertOrUpdate(person);

            var bdUser = _userDao.GetUserById(Guid.Parse(model.UserId));

            var user = new User()
            {
                Email = model.Email,
                Id = Guid.Parse(model.UserId),
                StatusRegister = CStatusRegister.Active,
                IdAccount = idAccount,
                IdPerson = person.Id,
                IdProfile = profile.Id,
                Password = bdUser.Password
            };

            _userDao.InsertOrUpdate(user);
        }
    }
}
