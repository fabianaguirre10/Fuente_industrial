using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.ServiceViewModels;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class ServiceDetailDao : ADao
    {

        private readonly QuestionDao _questionDao;
        private readonly QuestionDetailDao _questionDetailDao;
        private readonly AnswerDao _answerDao;


        public ServiceDetailDao(MardisContext mardisContext)
            : base(mardisContext)
        {
            _questionDetailDao = new QuestionDetailDao(mardisContext);
            _questionDao = new QuestionDao(mardisContext);
            _answerDao = new AnswerDao(mardisContext);
        }

        public int GetMaxOrden(Guid idService, Guid idAccount)
        {
            var itemReturn = Context.ServiceDetails.Where(tb => tb.IdService == idService &&
                                                                tb.StatusRegister == CStatusRegister.Active &&
                                                                tb.Service.IdAccount == idAccount)
                                    .Max(tb => tb.Order);

            return itemReturn;
        }

        public List<ServiceDetail> GetDetails(Guid idService, Guid idAccount)
        {
            var itemsReturns = Context.ServiceDetails
                .Include(s => s.Sections)
                    .ThenInclude(sc => sc.Questions)
                        .ThenInclude(q => q.QuestionDetails)
                                      .Where(tb => tb.IdService == idService &&
                                                  tb.StatusRegister == CStatusRegister.Active &&
                                                  tb.Service.IdAccount == idAccount &&
                                                  tb.IdSection == null
                                             )
                                             .AsNoTracking()
                                      .OrderBy(tb => tb.Order)
                                      .ToList();

            return itemsReturns;
        }
        public List<ServiceDetail> GetDetailsAggr(Guid idService, Guid idAccount)
        {
            var itemsReturns = Context.ServiceDetails

                                      .Where(tb => tb.IdService == idService &&
                                                  tb.StatusRegister == CStatusRegister.Active &&
                                                  tb.Service.IdAccount == idAccount &&
                                                  tb.IdSection == null
                                             )
                                             .AsNoTracking()
                                      .OrderBy(tb => tb.Order)
                                      .ToList();

            return itemsReturns;
        }



        public ServiceDetail GetOne(Guid idServiceDetail, Guid idAccount)
        {
            var itemReturn = Context.ServiceDetails
                                    .FirstOrDefault(tb => tb.Id == idServiceDetail &&
                                                 tb.StatusRegister == CStatusRegister.Active &&
                                                 tb.Service.IdAccount == idAccount);

            return itemReturn;
        }

        public List<ServiceDetail> GetServiceDetailBeforeOrder(Guid idService, int order, Guid idAccount)
        {
            var items =
                Context.ServiceDetails
                    .Where(tb => tb.IdService == idService &&
                                 tb.Order < order &&
                                 tb.StatusRegister == CStatusRegister.Active &&
                                 tb.Service.IdAccount == idAccount)
                    .OrderBy(s => s.Order)
                    .ToList();

            return items;
        }

        public List<ServiceDetail> GetServiceDetailAfterOrder(Guid idService, int order, Guid idAccount)
        {
            var items =
                Context.ServiceDetails
                    .Where(tb => tb.IdService == idService &&
                                 tb.Order >= order &&
                                 tb.StatusRegister == CStatusRegister.Active &&
                                 tb.Service.IdAccount == idAccount)
                    .OrderBy(s => s.Order)
                    .ToList();

            return items;
        }

        public List<ServiceDetail> GetServiceDetailsFromService(Guid idService, Guid idAccount, Guid idTask)
        {
            var consulta = Context.ServiceDetails
                  .Include(tb => tb.Sections)
                  .Where(tb => tb.IdService == idService &&
                               tb.StatusRegister == CStatusRegister.Active &&
                               tb.Service.IdAccount == idAccount)
                  .OrderBy(s => s.Order)
                  .ToList();
            return consulta.OrderBy(x => x.Order).ToList();
        }
        public ServiceDetail GetServiceDetailsFromServiceID(Guid idService, Guid idAccount, Guid idservicedetail)
        {
            var consulta = Context.ServiceDetails
                  .Include(tb => tb.Sections)
                  .Where(tb => tb.IdService == idService &&
                               tb.StatusRegister == CStatusRegister.Active &&
                               tb.Service.IdAccount == idAccount)
                  .OrderBy(s => s.Order)
                  .Where(x => x.Id == idservicedetail).ToList();
            return consulta.OrderBy(x => x.Order).FirstOrDefault();
        }

        public List<ServiceDetail> GetServiceDetailsFromServiceGeo(Guid idService, Guid idAccount)
        {
            List<ServiceDetail> seccion = new List<ServiceDetail>();
            List<ServiceDetail> Resultado = new List<ServiceDetail>();
            var consulta = Context.ServiceDetails
                  .Include(tb => tb.Sections)
                  .Where(tb => tb.IdService == idService &&
                               tb.StatusRegister == CStatusRegister.Active &&
                               tb.Service.IdAccount == idAccount)
                  .OrderBy(s => s.Order)
                  .ToList();

            Resultado = consulta;

            return Resultado.OrderBy(x => x.Order).ToList();
        }

        public List<ServiceDetail> GetServiceDetailOnlyKeys(Guid idService, Guid idAccount)
        {
            var itemsRetun = Context.ServiceDetails.Where(tb => tb.IdService == idService &&
                                                                                  tb.StatusRegister == CStatusRegister.Active &&
                                                                                  tb.Service.IdAccount == idAccount
                                                                                  )
                                                .OrderBy(tb => tb.Order)
                                                .Select(tb => new ServiceDetail { Id = tb.Id })
                                                .ToList();

            return itemsRetun;
        }

        public List<ServiceDetail> GetCompleteSection(Guid idServiceDetail, Guid idService, Guid idAccount)
        {
            return Context.ServiceDetails
                    .Include(s => s.Questions)
                        .ThenInclude(q => q.QuestionDetails)
                    .Include(a => a.Questions)
                        .ThenInclude(q => q.TypePoll)
                    .Include(s => s.Sections)
                        .ThenInclude(sc => sc.Questions)
                            .ThenInclude(q => q.QuestionDetails)
                    .Include(s => s.Sections)
                        .ThenInclude(sc => sc.Questions)
                            .ThenInclude(q => q.TypePoll)
                .Where(s => s.Service.Id == idService && s.Service.IdAccount == idAccount)
                .ToList();
        }

        public int GetServiceDetailCount(Guid idService, Guid idAccount)
        {
            return Context.ServiceDetails
                .Count(s => s.IdService == idService &&
                            s.StatusRegister == CStatusRegister.Active &&
                            s.Service.IdAccount == idAccount);
        }

        public List<ServiceDetail> GetSubSections(Guid idServiceDetail, Guid idaccount)
        {
            return Context.ServiceDetails
                .Include(s => s.Questions)
                    .ThenInclude(q => q.QuestionDetails)
                .Where(s => s.IdSection == idServiceDetail &&
                            s.Service.IdAccount == idaccount)
                .ToList();
        }
        public List<ServiceDetail> GetSubSectionsAggre(Guid idServiceDetail, Guid idaccount)
        {
            return Context.ServiceDetails
                .Where(s => s.IdSection == idServiceDetail)
                .ToList();
        }


        public List<ServiceDetailRegisterViewModel> GetChildrens(Guid idServiceDetal)
        {
            var sections = Context.ServiceDetails
                .Where(s => s.IdSection == idServiceDetal)
                .ToList();

            return sections.Select(s => new ServiceDetailRegisterViewModel()
            {
                Id = s.Id.ToString(),
                HasPhoto = s.HasPhoto,
                GroupName = s.GroupName,
                IdSection = s.IdSection,
                IdService = s.IdService.ToString(),
                IsDynamic = s.IsDynamic,
                Order = s.Order,
                SectionTitle = s.SectionTitle,
                Questions =
                        _questionDao.GetQuestion(s.Id)
                            .Select(
                                q =>
                                    new QuestionRegisterViewModel()
                                    {
                                        Id = q.Id.ToString(),
                                        IdServiceDetail = q.IdServiceDetail.ToString(),
                                        IdTypePoll = q.IdTypePoll.ToString(),
                                        Order = q.Order,
                                        StatusRegister = q.StatusRegister,
                                        Title = q.Title,
                                        Weight = q.Weight,
                                        HasPhoto = q.HasPhoto == "S",
                                        AnswerRequired = q.AnswerRequired,
                                        QuestionDetails =
                                            _questionDetailDao.GetQuestionDetails(q.Id)
                                                .Select(
                                                    qd =>
                                                        new QuestionDetailRegisterViewModel()
                                                        {
                                                            Answer = qd.Answer,
                                                            Id = qd.Id.ToString(),
                                                            IdQuestion = qd.IdQuestion.ToString(),
                                                            IdQuestionLink = qd.IdQuestionLink.ToString(),
                                                            Order = qd.Order,
                                                            Weight = qd.Weight
                                                        })
                                                .ToList()
                                    })
                            .ToList()
            })
                .ToList();
        }

        public ServiceDetail GetNotTracked(Guid idServiceDetail, Guid idAccount)
        {
            var itemReturn = Context.ServiceDetails
                                    .Include(s => s.Questions)
                                        .ThenInclude(q => q.QuestionDetails)
                                    .AsNoTracking()
                                    .FirstOrDefault(tb => tb.Id == idServiceDetail &&
                                                 tb.StatusRegister == CStatusRegister.Active// &&
                                                                                            //tb.Service.IdAccount == idAccount
                                                 );

            return itemReturn;
        }



    }
}
