using System.Security.Claims;
using System.Threading.Tasks;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Identity;

namespace Mardis.Engine.Web.Security
{
 
    public class UserPrincipalFactory : IUserClaimsPrincipalFactory<ApplicationUser>
    {
        public Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Password");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            return Task.FromResult(principal);
        }
    }
}
