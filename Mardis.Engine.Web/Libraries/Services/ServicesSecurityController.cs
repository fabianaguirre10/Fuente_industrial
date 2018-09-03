using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Mardis.Engine.Web.Security.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class ServicesSecurityController : Controller
    {
      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ServicesSecurityController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ISmsSender smsSender,
          ILogger<ServicesSecurityController> loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory;
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<string> ValidateUser(string inpuEmail, string inputPassword, bool rememberMe)
        {
            string returResult = "-1";

            if (!string.IsNullOrEmpty(inpuEmail) && !string.IsNullOrEmpty(inputPassword))
            {
                var identity = new ClaimsIdentity();

                identity.AddClaim(new Claim(inpuEmail, inputPassword));

                var result = await _signInManager.PasswordSignInAsync(inpuEmail, inputPassword, rememberMe, lockoutOnFailure: false);
                //            

                if (result.Succeeded)
                {
                    returResult = "1";
                }

                if (result.IsLockedOut)
                {
                    returResult = "2";
                }

            }
            else
            {
                returResult = "-1";
            }

            return returResult;
        }


        [HttpGet]
        [AllowAnonymous]
        public string Test() {

            return "Hola Mundo";
        }







    }
}
