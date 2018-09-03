using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.TaskViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertQuestion
    {
        public static List<MyTaskQuestionsViewModel> ToMyTaskQuestionsViewModelList(List<Question> questions)
        {
            return questions.OrderBy(q => q.Order).Select(q => new MyTaskQuestionsViewModel()
            {
                Id = q.Id,
                Order = q.Order,
                HasPhoto = q.HasPhoto.IndexOf("S") >= 0,
                Weight = q.Weight,
                AnswerRequired = q.AnswerRequired,
                IdTypePoll = q.IdTypePoll,
                CodeTypePoll = q.TypePoll.Code,
                Title = q.Title,
                Answer = string.Empty,
                NamePoll = q.TypePoll.Name,
                QuestionDetailCollection =
                    q.QuestionDetails.Where(qd=>qd.StatusRegister==CStatusRegister.Active).OrderBy(qd => qd.Order).Select(qd => new MyTaskQuestionDetailsViewModel()
                    {
                        Answer = qd.Answer,
                        Checked = false,
                        Id = qd.Id,
                        IdQuestion = qd.IdQuestion,
                        IsNext = qd.IsNext,
                        Order = qd.Order,
                        Weight = qd.Weight,
                        IdServiceDetail = q.IdServiceDetail
                    }).ToList()
            }).ToList();
        }
    }
}
