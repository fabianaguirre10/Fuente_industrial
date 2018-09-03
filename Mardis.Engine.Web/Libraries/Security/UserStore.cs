using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Web.Libraries.Security
{
    public class UserStore : IUserPasswordStore<ApplicationUser>
    {
        private readonly UserBusiness _userBusiness;

        public UserStore(string conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MardisContext>();

            optionsBuilder.UseSqlServer(conn);

            var mardisContext = new MardisContext(optionsBuilder.Options);

            _userBusiness = new UserBusiness(mardisContext);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //user.UserId = Guid.NewGuid().ToString();

            //_users.Add(user);

            //return Task.FromResult(IdentityResult.Success);
            return null;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            /*var match = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (match != null)
            {
                match.UserName = user.UserName;
                match.Email = user.Email;
                match.PasswordHash = user.PasswordHash;

                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }*/
            return null;
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            /*var match = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (match != null)
            {
                _users.Remove(match);

                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed());
            }*/

            return null;
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (userId != null)
            {
                var userBdd = _userBusiness.GetUserById(new Guid(userId));
                var user = new ApplicationUser(userBdd);

                return Task.FromResult(user);
            }
            return null;
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var userDataBase = _userBusiness.GetUserByUser(normalizedUserName);
            var applicationUser = new ApplicationUser(userDataBase);

            return Task.FromResult(applicationUser);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(true);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(true);
        }


        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            // Just returning an empty list because I don't feel like implementing this. You should get the idea though...
            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            return Task.FromResult(logins);
        }

        

        public void Dispose() { }
    }
}
