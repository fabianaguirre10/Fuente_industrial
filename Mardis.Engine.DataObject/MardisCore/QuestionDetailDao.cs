using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class QuestionDetailDao : ADao
    {
        public QuestionDetailDao(MardisContext mardisContext)
            : base(mardisContext)
        {

        }

        public int GetMax(Guid idQuestion)
        {
            var itemReturn = Context.QuestionDetails
                                     .Where(tb => tb.IdQuestion == idQuestion &&
                                                tb.StatusRegister == CStatusRegister.Active)
                                     .Max(tb => tb.Order);

            return itemReturn;
        }

        public List<QuestionDetail> GetQuestionDetails(Guid idQuestion)
        {
            var itemsReturn = Context.QuestionDetails
                                     .Where(tb => tb.IdQuestion == idQuestion &&
                                                tb.StatusRegister == CStatusRegister.Active)
                                     .OrderBy(tb => tb.Order)
                                     .ToList();
            return itemsReturn;
        }

        public QuestionDetail GetOne(Guid idQuestionDetail)
        {
            var itemReturn = Context.QuestionDetails
                                    .FirstOrDefault(tb => tb.Id == idQuestionDetail &&
                                               tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        public QuestionDetail GetOneWithQuestion(Guid idQuestionDetail)
        {
            var itemResult = Context.QuestionDetails
                                    .Include(qd => qd.Question)
                                    .FirstOrDefault(qd => qd.StatusRegister == CStatusRegister.Active &&
                                                    qd.Id == idQuestionDetail);
            return itemResult;
        }

        public List<QuestionDetail> GetQuestionDetailAfterOrder(Guid idQuestion, int order)
        {
            var items = Context.QuestionDetails
                              .Where(tb => tb.IdQuestion == idQuestion &&
                                         tb.Order > order &&
                                         tb.StatusRegister == CStatusRegister.Active)
                              .ToList();

            return items;
        }

        public List<QuestionDetail> GetQuestionDetailBeforeOrder(Guid idQuestion, int order)
        {
            var items = Context.QuestionDetails
                              .Where(tb => tb.IdQuestion == idQuestion &&
                                         tb.Order >= order &&
                                         tb.StatusRegister == CStatusRegister.Active)
                              .ToList();

            return items;
        }

        public List<QuestionDetail> GetQuestionDetailOnlyKey(Guid idQuestion)
        {
            var itemsReturn = Context.QuestionDetails.Where(tb => tb.IdQuestion == idQuestion &&
                                                                                                  tb.StatusRegister == CStatusRegister.Active)
                                                                .OrderBy(tb => tb.Order)
                                                                .Select(tb => new QuestionDetail { Id = tb.Id })
                                                                .ToList();

            return itemsReturn;
        }

        public List<QuestionDetail> GetQuestionDetailByService(Guid idService)
        {
            var itemsReturn = Context.QuestionDetails.Where(tb => tb.StatusRegister == CStatusRegister.Active)
                                                     .Join(Context.Questions,
                                                            tb => tb.IdQuestion,
                                                            que => que.Id,
                                                            (tb, que) => new { tb, que })
                                                      .Join(Context.ServiceDetails,
                                                            tb => tb.que.IdServiceDetail,
                                                            sd => sd.Id,
                                                            (tb, sd) => new { tb, sd })
                                                      .Where(tb => tb.tb.tb.StatusRegister == CStatusRegister.Active &&
                                                             tb.tb.que.StatusRegister == CStatusRegister.Active &&
                                                             tb.sd.StatusRegister == CStatusRegister.Active &&
                                                             tb.sd.IdService == idService)
                                                      .OrderBy(tb => tb.sd.Order)
                                                      .ThenBy(tb => tb.tb.que.Order)
                                                      .Select(tb => tb.tb.tb)
                                                      .ToList();



            return itemsReturn;
        }

        public List<MyTaskQuestionDetailsViewModel> GetAnswerOptionsFromQuestions(List<string> questionIdList)
        {
            var ids=questionIdList.AsParallel().Select(q => $"'{q}'");

            var query =
                $@"select IdQuestion, Id , Answer, [Order] from MardisCore.QuestionDetail with (nolock) where idQuestion in ({
                        string.Join(",", ids)
                    })";
            return Context.Query<MyTaskQuestionDetailsViewModel>(query).ToList();
            /*return Context.QuestionDetails
                .Where(q => questionIdList.Contains(q.IdQuestion.ToString()))
                .Select(q => new {IdQuestionDetail = q.Id, Answer = q.Answer, Order = q.Order});*/
        }
    }
}
