using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class AnswerDao : ADao
    {
        public AnswerDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<Answer> GetAnswers(Guid idServiceDetail, Guid idTask, Guid idMerchant, Guid idAccount)
        {
            var listServiceDetails = Context.ServiceDetails
                .Where(sd => sd.Id == idServiceDetail && sd.Service.IdAccount == idAccount)
                .ToList();
            
            var listResult = new List<Answer>();

            foreach (var serviceDetail in listServiceDetails)
            {
                listResult.AddRange(serviceDetail.Questions.Select(question => new Answer()
                {
                    IdAccount = serviceDetail.Service.IdAccount,
                    DateCreation = DateTime.Now,
                    IdServiceDetail = serviceDetail.Id,
                    IdTask = idTask,
                    IdMerchant = idMerchant,
                    IdQuestion = question.Id
                }));
            }

            return listResult;
        }

        public Answer GetAnswerValueByQuestion(Guid idQuestion, Guid idTask, Guid idAccount)
        {
            var itemReturn = Context.Answers
                .Include(a => a.AnswerDetails)
                .FirstOrDefault(a => a.StatusRegister == CStatusRegister.Active &&
                            a.IdQuestion == idQuestion &&
                            a.IdTask == idTask &&
                            a.IdAccount == idAccount);

            return itemReturn;
        }

        public List<Answer> GetAnswerListByQuestion(Guid idQuestion, Guid idTask, Guid idAccount)
        {
            var listResult = Context.Answers
                .Include(a => a.AnswerDetails)
                .Where(a => a.StatusRegister == CStatusRegister.Active &&
                            a.IdQuestion == idQuestion &&
                            a.IdTask == idTask &&
                            a.IdAccount == idAccount)
                            .ToList();
            return listResult;
        }
        public List<Answer> GetAnswerListByQuestionAccount(Guid idQuestion, Guid idAccount, Guid idTask)
        {
            var listResult = Context.Answers
                .Where(a => a.StatusRegister == CStatusRegister.Active &&
                            a.IdQuestion == idQuestion &&
                              a.IdTask == idTask &&
                            a.IdAccount == idAccount)
                            .ToList();
            return listResult;
        }

        public List<AnswerDetail> GetAnswerListByType(Guid idTask, string typePoll, Guid idAccount)
        {
            return Context.AnswerDetails
                .Include(a => a.Answer)
                .Where(a => a.Answer.IdTask == idTask &&
                            a.StatusRegister == CStatusRegister.Active &&
                            a.Answer.Question.TypePoll.Code == typePoll &&
                            a.Answer.IdAccount == idAccount)
                .ToList();
        }

        public List<Answer> GetAllAnswers(Guid idTask)
        {
            return Context.Answers
                .Include(a => a.AnswerDetails)
                .Include(a=>a.Question.TypePoll)
                .Where(a => a.IdTask == idTask && a.StatusRegister==CStatusRegister.Active)
                .ToList();
        }
    }
}
