using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{

    public class QuestionDao : ADao
    {
        public QuestionDao(MardisContext mardisContext)
              : base(mardisContext)
        {
        }

        public List<Question> GetQuestion(Guid idServiceDetail)
        {
            var itemsReturn = Context.Questions
                                     .Where(tb => tb.IdServiceDetail == idServiceDetail &&
                                                  tb.StatusRegister == CStatusRegister.Active)
                                     .OrderBy(tb => tb.Order)
                                     .ToList();

            return itemsReturn;
        }

        public int GetMax(Guid idServiceDetail)
        {
            var itemReturn = Context.Questions
                                    .Where(tb => tb.IdServiceDetail == idServiceDetail
                                           && tb.StatusRegister == CStatusRegister.Active)
                                    .Max(tb => tb.Order);

            return itemReturn;
        }

        public Question GetOne(Guid idQuestion)
        {
            var itemReturn = Context.Questions
                  .Include(q => q.TypePoll)
                  .Include(q => q.QuestionDetails)
                                    .FirstOrDefault(tb => tb.Id == idQuestion &&
                                                 tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        public List<Question> GetQuestionAfterOrder(Guid idServiceDetail, int order, MardisContext context = null)
        {

            var items = Context.Questions.Where(tb => tb.IdServiceDetail == idServiceDetail &&
                                                      tb.Order > order &&
                                                       tb.StatusRegister == CStatusRegister.Active)
                                         .ToList();

            return items;
        }

        public List<Question> GetQuestionBeforeOrder(Guid idServiceDetail, int order)
        {
            var items =
                                  Context.Questions.Where(tb => tb.IdServiceDetail == idServiceDetail &&
                                                                tb.Order >= order &&
                                                                tb.StatusRegister == CStatusRegister.Active)
                                                    .ToList();

            return items;
        }

        public List<Question> GetQuestionOnlyKeys(Guid idServiceDetail)
        {
            var itemsRetun = Context.Questions.Where(tb => tb.IdServiceDetail == idServiceDetail &&
                                                                                  tb.StatusRegister == CStatusRegister.Active)
                                                                 .OrderBy(tb => tb.Order)
                                                                 .Select(tb => new Question { Id = tb.Id })
                                                                 .ToList();

            return itemsRetun;
        }

        public List<Question> GetQuestionsToLink(Guid idService, Guid idAccount)
        {
            return Context.Questions
                .Where(
                    q =>
                        q.ServiceDetail.IdService == idService &&
                        q.ServiceDetail.Service.IdAccount == idAccount &&
                        (q.ServiceDetail.IdSection == null || q.ServiceDetail.IdSection == Guid.Empty))
                        .AsNoTracking()
                .ToList();
        }

        public Task<List<Question>> GetCompleteQuestion(Guid idServiceDetail)
        {
            Task<List<Question>> res = null;
            res = Context.Questions
                 .Include(q => q.TypePoll)
                 .Include(q => q.QuestionDetails)

                 .Where(
                     q =>
                         q.IdServiceDetail == idServiceDetail &&
                         //q.ServiceDetail.Service.IdAccount == idAccount &&
                         q.StatusRegister == CStatusRegister.Active)
                 .ToListAsync();

            return res;
        }

        public IQueryable<MyTaskQuestionsViewModel> GetQuestionFromService(Guid idService)
        {
            return Context.Questions
                .Where(q => q.ServiceDetail.IdService == idService)
                .Include(q => q.TypePoll)
                .Select(q => new MyTaskQuestionsViewModel()
                {
                    Id = q.Id,
                    Title = q.Title,
                    Order = q.Order,
                    CodeTypePoll = q.TypePoll.Code,
                    IdTypePoll = q.IdTypePoll,
                    IdServiceDetail = q.IdServiceDetail,
                    SectionTitle = q.ServiceDetail.SectionTitle,
                    SectionOrder = q.ServiceDetail.Order
                });
        }

        public List<MyTaskQuestionsViewModel> GetSubsectionQuestionFromService(Guid idService)
        {
            var query = $@"select 
                                a.Id, a.Title,a.[order],d.Code as CodeTypePoll,d.Name as NameTypePoll,a.IdTypePoll,
                                a.IdserviceDetail,c.SectionTitle, b.[Order] as SectionOrder, c.Id as IdParentSection,
                                b.SectionTitle as SubSectionTitle, b.GroupName, a.IdTypePoll, d.Name as NamePoll,
                                d.Code as CodeTypePoll,a.sequence as Sequence
                            from mardiscore.question a 
	                            inner join mardiscore.servicedetail b on a.idservicedetail=b.id
	                            inner join mardiscore.servicedetail c on b.idsection=c.id
	                            inner join mardiscore.typepoll d on a.idtypepoll=d.id
                            where c.idservice='{idService}'";
            return Context.Query<MyTaskQuestionsViewModel>(query).ToList();
        }

        public List<MyTaskQuestionsViewModel> GetQuestionFromIdquestion(Guid idquestion)
        {
            var query = $@"select 
                                a.Id, a.Title,a.[order],d.Code as CodeTypePoll,d.Name as NameTypePoll,a.IdTypePoll,
                                a.IdserviceDetail,c.SectionTitle, b.[Order] as SectionOrder, c.Id as IdParentSection,
                                b.SectionTitle as SubSectionTitle, b.GroupName, a.IdTypePoll, d.Name as NamePoll,
                                d.Code as CodeTypePoll,a.sequence as Sequence
                            from mardiscore.question a 
	                            inner join mardiscore.servicedetail b on a.idservicedetail=b.id
	                            inner join mardiscore.servicedetail c on b.idsection=c.id
	                            inner join mardiscore.typepoll d on a.idtypepoll=d.id
                            where a.id='{idquestion}'";
            return Context.Query<MyTaskQuestionsViewModel>(query).ToList();
        }

    }
}
