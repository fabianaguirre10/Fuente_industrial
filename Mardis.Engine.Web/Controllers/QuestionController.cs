using System;
using System.Collections.Generic;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Web.Controllers
{
    public class QuestionController : AController<QuestionController>
    {
        #region Variables y Constructores

        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly TaskNotImplementedReasonBusiness _taskNotImplementedReasonBusiness;
        private readonly BranchImageBusiness _branchImageBusiness;
        private readonly ILogger<QuestionController> _logger;
        private readonly Guid _idAccount;
        private readonly IDataProtector _protector;
        private readonly UserBusiness _userBusiness;
        private readonly Guid _userId;
        private readonly QuestionBusiness _questionBusiness;
        private readonly ServiceDetailBusiness _serviceDetailBusiness;
        private RedisCache _cache;

        public QuestionController(UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<QuestionController> logger,
                                ILogger<ServicesFilterController> loggeFilter,
                                    IDataProtectionProvider protectorProvider,
                                    IMemoryCache memoryCache,
                                    RedisCache distributedCache)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _cache = distributedCache;
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _logger = logger;
            ControllerName = CTask.Controller;
            TableName = CTask.TableName;
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _taskNotImplementedReasonBusiness = new TaskNotImplementedReasonBusiness(mardisContext);
            _branchImageBusiness = new BranchImageBusiness(mardisContext);
            _userBusiness = new UserBusiness(mardisContext);
            _questionBusiness = new QuestionBusiness(mardisContext);

            if (ApplicationUserCurrent.UserId != null)
            {
                _userId = new Guid(ApplicationUserCurrent.UserId);
            }

            _idAccount = ApplicationUserCurrent.AccountId;
        }

        #endregion

        //[HttpGet]
        //public JsonResult GetPollQuestion(Guid idServiceDetail, Guid idTask, int copyNumber)
        //{
        //    List<MyTaskQuestionsViewModel> model = _questionBusiness.GetPollQuestion(idServiceDetail, idTask, copyNumber,
        //        _idAccount);
        //    return Json(model);
        //}
    }
}
