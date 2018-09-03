using System;
using System.Collections.Generic;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    public class AnswerController : AController<AnswerController>
    {
        
        private readonly AnswerBusiness _answerBusiness;

        public AnswerController(
                            UserManager<ApplicationUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            MardisContext mardisContext,
                            ILogger<AnswerController> logger)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _answerBusiness=new AnswerBusiness(mardisContext);
        }

        [HttpGet]
        public Answer GetAnswerValueByQuestion(Guid idQuestion, Guid idTask)
        {
            return _answerBusiness.GetAnswerValueByQuestion(idQuestion, idTask, ApplicationUserCurrent.AccountId);
        }

        [HttpGet]
        public List<OneAnswerViewModel> GetAnswerListByTypeOne(Guid idTask)
        {
            return _answerBusiness.GetAnswerListByTypeOne(idTask, ApplicationUserCurrent.AccountId);
        }

        [HttpGet]
        public List<ManyAnswerViewModel> GetAnswerListByTypeMany(Guid idTask)
        {
            return _answerBusiness.GetAnswerListByTypeMany(idTask, ApplicationUserCurrent.AccountId);
        }

        [HttpGet]
        public List<OpenAnswerViewModel> GetAnswerListByTypeOpen(Guid idTask)
        {
            return _answerBusiness.GetAnswerListByTypeOpen(idTask, ApplicationUserCurrent.AccountId);
        }
    }
}
