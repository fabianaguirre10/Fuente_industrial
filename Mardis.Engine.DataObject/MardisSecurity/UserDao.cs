using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.DataObject.MardisSecurity
{
    /// <summary>
    /// Negocio de Usuario
    /// </summary>
    public class UserDao : ADao
    {

        public UserDao(MardisContext context)
            : base(context)
        {
            CoreFilterDao = new CoreFilterDao(context);
            CoreFilterDetailDao = new CoreFilterDetailDao(context);
        }

        /// <summary>
        /// Dame Usuario
        /// </summary>
        /// <param name="userName">Email (User)</param>
        /// <returns>Retorna un Usuario</returns>
        public User GetUserByUser(string userName)
        {
            User returnValue = null;

            var user = Context.Users;

            try
            {
                returnValue = user
                .Include(u => u.Account)
                .Include(u => u.Person)
                .FirstOrDefault(u => u.Email == userName &&
                                     u.StatusRegister == CStatusRegister.Active);
            }
            catch (Exception ex)
            {
                // ignored
            }

            return returnValue;
        }

        /// <summary>
        /// Dame Usuario
        /// </summary>
        /// <param name="userId">Identificador de Usuario</param>
        /// <returns>Retorna un Usuario</returns>
        public User GetUserById(Guid userId)
        {
            try
            {


                var returnValue = Context.Users
                    .Include(u => u.Account)
                    .Include(u => u.Person)
                    .Include(u => u.Profile)
                        .ThenInclude(p => p.TypeUser)
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Id == userId &&
                                         u.StatusRegister == CStatusRegister.Active);

                return returnValue;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetUserByIdasync(Guid userId)
        {
            var returnValue = Context.Users
                .Include(u => u.Account)
                .Include(u => u.Person)
                .Include(u => u.Profile)
                    .ThenInclude(p => p.TypeUser)
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == userId &&
                                     u.StatusRegister == CStatusRegister.Active);

            return await Task.FromResult(returnValue);
        }

        public List<User> GetMerchantsListByArrayIds(Guid[] merchantIdlList, Guid idAccount, Guid idCampaign)
        {

            var query =$@"select * from mardissecurity.[user] where id in (select idmerchant from mardiscore.task where idcampaign='{idCampaign}') and idaccount='{idAccount}'";

            return Context.Query<User>(query).ToList();

            //var users = Context.Campaigns
            //    .Where(c => c.Id == idCampaign)
            //    .Select(c => c.Tasks.Select(t => t.Merchant)).ToList();

            //return Context.Users
            //    .Where(u => merchantIdlList.Contains(u.Id) &&
            //                u.StatusRegister == CStatusRegister.Active &&
            //                u.IdAccount == idAccount)
            //    .ToList();

        }

        public List<User> GetUserListByType(string typeUser, Guid idAccount)
        {
            var resultList = Context.Users
                .Include(u => u.Profile)
                .Include(u => u.Person)
                .Where(usr => usr.Profile.TypeUser.Name == typeUser &&
                              usr.StatusRegister == CStatusRegister.Active &&
                              usr.IdAccount == idAccount)
                .ToList();
            return resultList;
        }

        public List<User> GetUsers(List<FilterValue> filters, int pageIndex, int pageSize, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" " +
                               $"&& IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filters);

            return Context.Users
                .Include(u => u.Profile)
                    .ThenInclude(p => p.TypeUser)
                .Include(u => u.Account)
                .Where(strPredicate)
                .OrderByDescending(u => u.Profile.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetCount(List<FilterValue> filters, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" " +
                               $"&& IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filters);

            return Context.Users
                .Where(strPredicate)
                .Count();
        }

        public List<User> GetMerchants(Guid idAccount)
        {
            return Context.Users
                .Include(u => u.Profile)
                .Where(u => u.StatusRegister == CStatusRegister.Active &&
                            u.IdAccount == idAccount)
                .ToList();
        }
    }
}