using System;
using System.Collections.Generic;
using AutoMapper;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel;

namespace Mardis.Engine.Business.MardisCore
{
    public class AnswerBusiness : ABusiness
    {

        private readonly AnswerDao _answerDao;

        public AnswerBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _answerDao = new AnswerDao(mardisContext);
            Mapper.Initialize(cfg => cfg.CreateMap<AnswerDetail, OneAnswerViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<AnswerDetail, ManyAnswerViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<AnswerDetail, OpenAnswerViewModel>());
        }

        public List<Answer> GetAnswers(Guid idServiceDetail, Guid idTask, Guid idMerchant, Guid idAccount)
        {
            return _answerDao.GetAnswers(idServiceDetail, idTask, idMerchant, idAccount);
        }

        public Answer GetAnswerValueByQuestion(Guid idQuestion, Guid idTask, Guid idAccount)
        {
            return _answerDao.GetAnswerValueByQuestion(idQuestion, idTask, idAccount);
        }

        public List<OneAnswerViewModel> GetAnswerListByTypeOne(Guid idTask, Guid idAccount)
        {
            return
                Mapper.Map<List<AnswerDetail>, List<OneAnswerViewModel>>(_answerDao.GetAnswerListByType(idTask,
                    CTypePoll.One, idAccount));
        }

        public List<ManyAnswerViewModel> GetAnswerListByTypeMany(Guid idTask, Guid idAccount)
        {
            return
                Mapper.Map<List<AnswerDetail>, List<ManyAnswerViewModel>>(_answerDao.GetAnswerListByType(idTask,
                    CTypePoll.Many, idAccount));
        }

        public List<OpenAnswerViewModel> GetAnswerListByTypeOpen(Guid idTask, Guid idAccount)
        {
            return
                Mapper.Map<List<AnswerDetail>, List<OpenAnswerViewModel>>(_answerDao.GetAnswerListByType(idTask,
                    CTypePoll.Open, idAccount));
        }
    }
}
