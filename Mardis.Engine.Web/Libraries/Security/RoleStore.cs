using System;
using System.Threading;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Web.Libraries.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Web.Libraries.Security
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly ProfileBusiness _profileBusiness;

        public RoleStore(string conn)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MardisContext>();

            optionsBuilder.UseSqlServer(conn);

            var mardisContext = new MardisContext(optionsBuilder.Options);

            _profileBusiness = new ProfileBusiness(mardisContext);
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            //_roles.Add(role);

            //return Task.FromResult(IdentityResult.Success);
            return null;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            //var match = _roles.FirstOrDefault(r => r.RoleId == role.RoleId);
            //if (match != null)
            //{
            //    match.RoleName = role.RoleName;

            //    return Task.FromResult(IdentityResult.Success);
            //}
            //else
            //{
            //    return Task.FromResult(IdentityResult.Failed());
            //}
            return null;
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            //var match = _roles.FirstOrDefault(r => r.RoleId == role.RoleId);
            //if (match != null)
            //{
            //    _roles.Remove(match);

            //    return Task.FromResult(IdentityResult.Success);
            //}
            //else
            //{
            //    return Task.FromResult(IdentityResult.Failed());
            //}
            return null;
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {

            var profile = _profileBusiness.GetById(new Guid(roleId));
            var role = new ApplicationRole(profile);

            return Task.FromResult(role);
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var profile = _profileBusiness.GetByCode(normalizedRoleName);
            var role = new ApplicationRole(profile);

            return Task.FromResult(role);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleId);
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.RoleName = roleName;

            return Task.FromResult(true);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.RoleName = normalizedName;

            return Task.FromResult(true);
        }

        public void Dispose() { }
    }
}
