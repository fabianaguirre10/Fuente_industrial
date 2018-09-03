using System;
using System.Collections.Generic;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mardis.Engine.Business.MardisCore
{
    /// <summary>
    /// Negocio de Servicio de Detalles
    /// </summary>
    public class ServiceDetailBusiness : ABusiness
    {
        private readonly ServiceDetailDao _serviceDetailDao;
        private readonly QuestionDao _questionDao;
        private readonly QuestionDetailDao _questionDetailDao;
        private readonly AnswerDao _answerDao;
        private readonly AnswerDetailDao _answerDetailDao;

        public ServiceDetailBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _serviceDetailDao = new ServiceDetailDao(mardisContext);
            _questionDao = new QuestionDao(mardisContext);
            _questionDetailDao = new QuestionDetailDao(mardisContext);
            _answerDao = new AnswerDao(mardisContext);
            _answerDetailDao = new AnswerDetailDao(mardisContext);
        }

        public int GetMaxOrden(Guid idService, Guid idAccount)
        {
            return _serviceDetailDao.GetMaxOrden(idService, idAccount);
        }

        public List<ServiceDetail> GetDetails(Guid idService, Guid idAccount)
        {
            return _serviceDetailDao.GetDetails(idService, idAccount);
        }
        public List<ServiceDetail> GetDetailsAggre(Guid idService, Guid idAccount)
        {
            return _serviceDetailDao.GetDetailsAggr(idService, idAccount);
        }
        public List<ServiceDetail> GetSubSectionsAggre(Guid idServiceDetail, Guid idaccount)
        {
            return _serviceDetailDao.GetSubSectionsAggre(idServiceDetail, idaccount);
        }

        public ServiceDetail AddServiceDetail(Guid idService, Guid idAccount, int position = -1)
        {
            var itemReturn = new ServiceDetail { IdService = idService };


            if (-1 == position)
            {
                itemReturn.Order = GetMaxOrden(idService, idAccount) + 1;
            }
            else
            {
                itemReturn.Order = position;
            }

            itemReturn.StatusRegister = CStatusRegister.Active;
            itemReturn.SectionTitle = CService.InsertSection;
            itemReturn.Weight = 0;

            return itemReturn;
        }

        public ServiceDetail GetOne(Guid idServiceDetail, Guid idAccount)
        {
            return _serviceDetailDao.GetOne(idServiceDetail, idAccount);
        }

        public ServiceDetail InsertSection(Guid idServiceDetail, Guid idService, string action, Guid idAccount)
        {
            ServiceDetail returnItem = null;

            using (var transaction = Context.Database.BeginTransaction())
            {
                var serviceDetailCurrent = _serviceDetailDao.GetOne(idServiceDetail, idAccount);
                var position = -1;
                var itemsUpdate = new List<ServiceDetail>();

                try
                {

                    switch (action)
                    {
                        case CService.LastSection:
                            position = -1;
                            break;
                        case CService.BeforeSection:
                            position = serviceDetailCurrent.Order;
                            itemsUpdate = _serviceDetailDao.GetServiceDetailBeforeOrder(idService,
                                                                                   serviceDetailCurrent.Order, idAccount);
                            break;
                        case CService.AfterSection:
                            position = serviceDetailCurrent.Order + 1;
                            itemsUpdate = _serviceDetailDao.GetServiceDetailAfterOrder(idService,
                                                                                  serviceDetailCurrent.Order, idAccount);
                            break;
                    }

                    foreach (var itemTemp in itemsUpdate)
                    {
                        itemTemp.Order = itemTemp.Order + 1;
                    }

                    returnItem = AddServiceDetail(idService, idAccount, position);

                    Context.ServiceDetails.Add(returnItem);
                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }

            }

            return returnItem;
        }

        public List<MyTaskServicesDetailViewModel> GetPollSections(Guid idTask, Guid idService, Guid idAccount)
        {

            var sections = _serviceDetailDao.GetCompleteSection(idTask, idService, idAccount);

            var answers = _answerDao.GetAllAnswers(idTask);

            var model = ConvertServiceDetail.ToMyTaskServicesDetailViewModelList(sections.OrderBy(s => s.Order).ToList());

            model = GetAnsweredSections(model, answers);

            return model;
        }

        public List<MyTaskServicesDetailViewModel> GetAnsweredSections(List<MyTaskServicesDetailViewModel> model, List<Answer> answers)
        {
            int pos = 0;
            answers = answers.Distinct().ToList();
            List<MyTaskServicesDetailViewModel> Section = new List<MyTaskServicesDetailViewModel>();
            foreach (var answer in answers)
            {
               
                foreach (var answerDetail in answer.AnswerDetails)
                {
                    //tomar todas las secciones lista para tomar todas las secciones duplicadas
                    Section = model.Where(m => m.Id == answer.IdServiceDetail).ToList();

                    if (Section.Count == 0)
                    {
                        //buscar subsecciones
                        foreach (var section in model)
                        {

                            foreach (var x in section.Sections)
                            {
                                if (x.Id == answer.IdServiceDetail)
                                {
                                    Section.Add(x);
                                }
                            }
                        }
                    }


                    //listado para tomar todas las preguntas porque lista porque toma las preguntas de las secciones duplicadas
                    List<MyTaskQuestionsViewModel> questionList = new List<MyTaskQuestionsViewModel>();
                    MyTaskQuestionsViewModel pregunta = new MyTaskQuestionsViewModel();
                    foreach (var n in Section)
                    {
                        pregunta = new MyTaskQuestionsViewModel();
                        //if (n.QuestionCollection.Where(x=> x.Id == answer.IdQuestion).FirstOrDefault().CodeTypePoll == CTypePoll.Many)
                        //{
                        //    pregunta = n?.QuestionCollection.Where(q => q.QuestionDetailCollection.Where(y=>y.IdQuestion == answer.IdQuestion && y.Id==answerDetail.IdQuestionDetail).First().Id == answerDetail.IdQuestionDetail).FirstOrDefault();                           //pregunta = n?.QuestionCollection.Where(q => q.Id == answer.IdQuestion &&).FirstOrDefault();
                        //}
                        //else {
                        //    pregunta = n?.QuestionCollection.Where(q => q.Id == answer.IdQuestion).FirstOrDefault();
                        //}
                        pregunta = n?.QuestionCollection.Where(q => q.Id == answer.IdQuestion).FirstOrDefault();
                        questionList.Add(pregunta);


                    }



                    //recorremos todas las preguntas lista porque puede existir varias preguntas con el mismo id

                    int i = 0;
                    var questionmodifica = questionList;
                    for (i = 0; i < questionList.Count(); i++)
                    {
                        if (questionList[i] != null)
                        {
                            if (questionList.Count > 1)
                                answer.Question.sequence = i;
                            else
                                answer.Question.sequence = 0;


                            var question = Section[i]?.QuestionCollection.FirstOrDefault(q => q.Id == answer.IdQuestion);
                            var answerquestion = answers.Where(x => x.IdQuestion == questionList[i].Id && (x.sequenceSection == i + 1 || x.sequenceSection == 0 || x.sequenceSection == null)).ToList();

                            //if (answer.Question.TypePoll.Code == CTypePoll.Many) {
                            //     question = Section[i]?.QuestionCollection.FirstOrDefault(q => q.QuestionDetailCollection.Where(y => y.IdQuestion == answer.IdQuestion && y.Id == answerDetail.IdQuestionDetail).First().Id == answerDetail.IdQuestionDetail);
                            //     answerquestion = answers.Where(x => x.IdQuestion == questionList[i].Id && (x.sequenceSection == 1)).ToList();
                            //    answerquestion = answers.Where(x => x.IdQuestion == questionList[i].Id && (x.sequenceSection == 2)).ToList();

                            //}
                         

                         
                            if (answerquestion.Count() > 0)
                            {
                                var resp = answerquestion[0].AnswerDetails.ToList();

                                if (question != null)
                                {

                                    switch (answer.Question.TypePoll.Code)
                                    {
                                        case CTypePoll.One:
                                            question.IdQuestionDetail = (Guid)resp[0].IdQuestionDetail;
                                            question.IdAnswer = answerquestion[0].Id;
                                            break;
                                        case CTypePoll.Open:
                                            question.Answer = resp[0].AnswerValue;
                                            question.IdAnswer = answerquestion[0].Id;
                                            break;

                                        case CTypePoll.Many:
                                            question.IdQuestionDetail = (Guid)resp[0].IdQuestionDetail;
                                            question.IdAnswer = answerquestion[0].Id;
                                            if (question.QuestionDetailCollection.Where(x => x.Id == answerDetail.IdQuestionDetail).Count() > 0)
                                                question.QuestionDetailCollection.Where(x => x.Id == answerDetail.IdQuestionDetail).First().Checked=true ;
                                            break;
                                    }
                                }
                            }
                          
                        }



                    }
                }
           
                if (answer.AnswerDetails.Count() == 0 && answer.Question.TypePoll.Code == CTypePoll.Many)
                {
                    var  Sectiona = model.Where(m => m.Id == answer.IdServiceDetail);
                    var question =Sectiona.FirstOrDefault().QuestionCollection.FirstOrDefault(q => q.Id == answer.IdQuestion);
                    question.IdAnswer = answer.Id;
                }



            }


            return model;
        }

        private void LoadQuestion(Guid idTask, Guid idAccount, MyTaskServicesDetailViewModel section)
        {
            foreach (var question in section.QuestionCollection)
            {
                var answer = _answerDao.GetAnswerValueByQuestion(question.Id, idTask, idAccount);

                if (answer == null) continue;

                if (question.CodeTypePoll == CTypePoll.Open)
                {
                    var answers = _answerDao.GetAnswerListByQuestion(question.Id, idTask, idAccount);
                    foreach (
                        var answerDetail in
                        answers.SelectMany(
                            answerItem =>
                                answerItem.AnswerDetails.Where(answerDetail => answerDetail.CopyNumber == question.CopyNumber)))
                    {
                        question.Answer = answerDetail.AnswerValue;
                    }
                }

                if (question.CodeTypePoll == CTypePoll.One)
                {
                    foreach (var item in answer.AnswerDetails)
                    {
                        if ((item.IdQuestionDetail != Guid.Empty && item.IdQuestionDetail != null) &&
                            item.CopyNumber == question.CopyNumber)
                        {
                            question.IdQuestionDetail = (Guid)item.IdQuestionDetail;
                            question.IdAnswer = item.IdAnswer;
                        }
                    }
                }

                if (question.CodeTypePoll == CTypePoll.Image)
                {
                    var answers = _answerDao.GetAnswerListByQuestion(question.Id, idTask, idAccount);
                    foreach (
                        var answerDetail in
                        answers.SelectMany(
                            answerItem =>
                                answerItem.AnswerDetails.Where(answerDetail => answerDetail.CopyNumber == question.CopyNumber)))
                    {
                        question.Answer = answerDetail.AnswerValue;
                    }
                }

                foreach (var questionDetail in
                    from questionDetail in question.QuestionDetailCollection
                    let answerDetail =
                    _answerDetailDao.GetAnswerDetail(questionDetail.Id, questionDetail.CopyNumber, answer.Id, idAccount)
                    where answerDetail != null
                    select questionDetail)
                {
                    questionDetail.Checked = true;
                    break;
                }
            }
        }

        public bool SavePoll(List<PollViewModel> itemsPoll, Guid idAccount)
        {
            bool isSuccess;

            using (var transaction = Context.Database.BeginTransaction())
            {

                try
                {
                    foreach (var itemSection in itemsPoll)
                    {
                        var tempSection =
                                         _serviceDetailDao.GetOne(itemSection.IdSection, idAccount);

                        tempSection.SectionTitle = itemSection.Title;
                        tempSection.Weight = IntegerUtil.GetValue(itemSection.Weight);
                        tempSection.IsDynamic = itemSection.IsDynamic;

                        Context.ServiceDetails.Add(tempSection);
                        Context.Entry(tempSection).State = EntityState.Modified;

                        foreach (var itemQuestion in itemSection.ItemsQuestion)
                        {
                            var tempQuestion = _questionDao.GetOne(itemQuestion.IdQuestion);

                            tempQuestion.Title = itemQuestion.Title;
                            tempQuestion.IdTypePoll = itemQuestion.IdTypePoll;
                            tempQuestion.Weight = IntegerUtil.GetValue(itemQuestion.Weight);
                            tempQuestion.HasPhoto = itemQuestion.HasPhotos;
                            tempQuestion.CountPhoto = IntegerUtil.GetValue(itemQuestion.CountPhotos);

                            Context.Questions.Add(tempQuestion);
                            Context.Entry(tempQuestion).State = EntityState.Modified;


                            foreach (var itemAnswer in itemQuestion.ItemsAnswer)
                            {
                                var tempAnswer = _questionDetailDao.GetOne(itemAnswer.IdAnswer);

                                tempAnswer.Answer = itemAnswer.Title;
                                tempAnswer.Weight = IntegerUtil.GetValue(itemAnswer.Weight);

                                Context.QuestionDetails.Add(tempAnswer);
                                Context.Entry(tempAnswer).State = EntityState.Modified;
                            }

                        }

                    }

                    Context.SaveChanges();

                    transaction.Commit();

                    isSuccess = true;
                }
                catch
                {
                    isSuccess = false;
                    transaction.Rollback();
                }
            }


            return isSuccess;
        }

        public bool DeleteServiceDetail(Guid isServiceDetail, Guid idAccount)
        {
            bool isSuccess;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var oneSection = _serviceDetailDao.GetOne(isServiceDetail, idAccount);

                    oneSection.StatusRegister = CStatusRegister.Delete;

                    Context.ServiceDetails.Add(oneSection);
                    Context.Entry(oneSection).State = EntityState.Modified;
                    Context.SaveChanges();

                    transaction.Commit();

                    isSuccess = true;
                }
                catch
                {
                    transaction.Rollback();

                    isSuccess = false;
                }
            }

            return isSuccess;
        }

        public ServiceDetail Save(ServiceDetail serviceDetail)
        {
            return _serviceDetailDao.InsertOrUpdate(serviceDetail);
        }

    }
}
