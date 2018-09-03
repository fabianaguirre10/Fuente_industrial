using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.Services;
using Mardis.Engine.Web.ViewModel.DashBoardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Mardis.Engine.Web.Controllers
{

    [Authorize]
    [EnableCors("MPT")]

    public class HomeController : AController<HomeController>
    {
        private readonly MenuBusiness _menuBusiness;
        private readonly CampaignBusiness _campaignBusiness;
        private readonly HomeBusiness _homeBusiness;
        private readonly IDataProtector _protector;
        private readonly IDataProtector _protectorCampaign;
        private string setting;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IHttpContextAccessor httpContextAccessor,
                              MardisContext mardisContext,
                              ILogger<HomeController> logger,
                              IMenuService menuService,
                                    IDataProtectionProvider protectorProvider
                              )
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _protectorCampaign = protectorProvider.CreateProtector("Mardis.Engine.Web.Controllers.CampaignController");
            _menuBusiness = new MenuBusiness(mardisContext);
            _campaignBusiness = new CampaignBusiness(mardisContext);
            _homeBusiness = new HomeBusiness(mardisContext);
        }
        
        //[HttpPost(Name = "GetMenuByProfile")]
        //public IEnumerable GetMenuByProfile()
        //{
        //    var idProfile = ApplicationUserCurrent.ProfileId;
        //    var itemMenu =
        //    _menuBusiness.GetOnlyParentsByProfile(idProfile);

        //    foreach (var itemMenuTemp in itemMenu)
        //    {
        //        itemMenuTemp.MenuChildrens = _menuBusiness.GetChildrens(idProfile, itemMenuTemp.Id);
        //    }

        //    return itemMenu;login
        //}
        
        public IActionResult Index()
        {
            HttpContext.Session.SetString("das", "The Doctor");
            HttpContext.Session.SetInt32("dsa", 773);
            return View();
        }
        
        [HttpGet]


        public IActionResult DashBoard(DashBoardViewModel model, string filterValues, bool deleteFilter, int pageIndex = 1, int pageSize = 50)
        {

            using (var clientw = new HttpClient())
            {
                clientw.BaseAddress = new Uri("http://geomardis6728.cloudapp.net:8000/api/3.0/");



                var json = JsonConvert.SerializeObject("<tsRequest >\r\n < credentials name =\"administrador\" password=\"M@rdisserver2018\" >\r\n  <site contentUrl=\"\" />\r\n  </credentials>\r\n</tsRequest>", Formatting.Indented);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = clientw.PostAsync("auth/signin", byteContent);
                responseTask.Wait();
            }
            //    var result = responseTask.Result;
            //    var requestUrl = "http://geomardis6728.cloudapp.net:8000/api/3.0/auth/signin";
            //    string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            //    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            //    requestMessage.Content = new StringContent(jsonBody);
            //    var requestBody = requestMessage.Content.ReadAsStringAsync().Result;

            //    Add body content
            //    requestMessage.Content = new StringContent(
            //        jsonBody.ToString(),
            //        Encoding.UTF8,
            //        "application/json"
            //    );

            //    var result = client.SendAsync(requestMessage);
            //    result.Wait();
            //    var re = result.Result;
            //    //if (result.IsSuccessStatusCode)
            //    //{
            //    //    var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
            //    //    readTask.Wait();

            //    //    students = readTask.Result;
            //    //}
            //    else //web api sent error response 
            //    {
            //        //log response status here..

            //        students = Enumerable.Empty<StudentViewModel>();

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}
          
            if (!string.IsNullOrEmpty(model.IdCampaign))
            {
                SetSessionVariable("idCampaign", model.IdCampaign);
            }
            else if (!string.IsNullOrEmpty(filterValues) || pageIndex != 1)
            {
                model.IdCampaign = GetSessionVariable("idCampaign");
            }
         
            ViewBag.CampaignList =
                _campaignBusiness.GetActiveCampaignsListDasboard(ApplicationUserCurrent.AccountId, Guid.Parse(ApplicationUserCurrent.UserId)).OrderBy(x => x.Name)
                    .Select(c => new SelectListItem() { Value = _protectorCampaign.Protect(c.idcampaign.ToString()), Text = c.Name });
                  
            var filters = GetFilters(filterValues, deleteFilter);

            model = _homeBusiness.GetDashBoard(model, filters, pageIndex, pageSize, ApplicationUserCurrent.AccountId, _protectorCampaign);

            if (!string.IsNullOrEmpty(model.IdCampaign))
            {
                model.IdCampaign = _protectorCampaign.Protect(model.IdCampaign);
            }
            ViewBag.Account = ApplicationUserCurrent.AccountId;
            ViewBag.user = ApplicationUserCurrent.UserId;
            return View(model);
        }

        [HttpPost]
        public JsonResult GetDashBord(string idCampaign) {

          var id= _protectorCampaign.Unprotect(idCampaign);
            var url=_campaignBusiness.GetDashOne(Guid.Parse(id)).url;

            //var client = new RestClient("http://geomardis6728.cloudapp.net:8000/api/3.0/auth/signin");
            //var request = new RestRequest(Method.POST);

            //request.AddParameter("", "<tsRequest>\n  <credentials name=\"administrador\" password=\"M@rdisserver2018\" >\n    <site contentUrl=\"topsy\" />\n  </credentials>\n</tsRequest>", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            ////  var responseBody = Json(response.RawBytes.ToString());
            //string result = System.Text.Encoding.UTF8.GetString(response.RawBytes);

            return Json(url);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
