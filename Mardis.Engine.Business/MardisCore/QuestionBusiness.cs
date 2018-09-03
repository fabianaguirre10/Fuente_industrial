using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business.MardisCore
{
    /// <summary>
    /// Gestión de Preguntas
    /// </summary>
    public class QuestionBusiness : ABusiness
    {

        private readonly TypePollDao _typePollDao;
        private readonly QuestionDao _questionDao;
        private readonly AnswerDao _answerDao;
        private readonly AnswerDetailDao _answerDetailDao;

        public QuestionBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _typePollDao = new TypePollDao(mardisContext);
            _questionDao = new QuestionDao(mardisContext);
            _answerDao = new AnswerDao(mardisContext);
            _answerDetailDao = new AnswerDetailDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idServiceDetail"></param>
        /// <returns></returns>
        public List<Question> GetQuestion(Guid idServiceDetail)
        {
            return _questionDao.GetQuestion(idServiceDetail);
        }

        /// <summary>
        /// Obtiene el máximo
        /// </summary>
        /// <param name="idServiceDetail"></param>
        /// <returns></returns>
        public int GetMax(Guid idServiceDetail)
        {
            return _questionDao.GetMax(idServiceDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <returns></returns>
        public Question GetOne(Guid idQuestion)
        {
            return _questionDao.GetOne(idQuestion);
        }

        /// <summary>
        /// Adicionar Pregunta
        /// </summary>
        /// <param name="idServiceDetail"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private Question AddQuestion(Guid idServiceDetail, int position = -1)
        {
            var oneQuestion = new Question
            {
                IdServiceDetail = idServiceDetail,
                TypePoll = _typePollDao.GetByCode(CTypePoll.Many)
            };

            oneQuestion.IdTypePoll = oneQuestion.TypePoll.Id;
            oneQuestion.HasPhoto = CQuestion.NoHasPhotos;
            oneQuestion.CountPhoto = 0;

            if (-1 == position)
            {
                oneQuestion.Order = GetMax(idServiceDetail) + 1;
            }
            else
            {
                oneQuestion.Order = position;
            }

            oneQuestion.StatusRegister = CStatusRegister.Active;
            oneQuestion.Title = CService.InsertQuestion;
            oneQuestion.Weight = 0;

            return oneQuestion;
        }

        /// <summary>
        /// Insertar Pregunta
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <param name="idServiceDetail"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Question InsertQuestion(Guid idQuestion, Guid idServiceDetail, string action)
        {
            Question returnQuestion = null;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {

                    var questionCurent = _questionDao.GetOne(idQuestion);
                    int position = -1;
                    var itemsUpdate = new List<Question>();

                    switch (action)
                    {
                        case CService.LastQuestion:
                            position = -1;
                            break;
                        case CService.BeforeQuestion:
                            position = questionCurent.Order;
                            itemsUpdate = _questionDao.GetQuestionBeforeOrder(questionCurent.IdServiceDetail,
                                                                              questionCurent.Order);
                            break;
                        case CService.AfterQuestion:
                            position = questionCurent.Order + 1;
                            itemsUpdate = _questionDao.GetQuestionAfterOrder(questionCurent.IdServiceDetail,
                                                                             questionCurent.Order);
                            break;
                    }

                    foreach (var itemTemp in itemsUpdate)
                    {
                        itemTemp.Order = itemTemp.Order + 1;
                    }

                    returnQuestion = AddQuestion(idServiceDetail, position);

                    Context.Questions.Add(returnQuestion);
                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return returnQuestion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <returns></returns>
        public bool DeleteQuestion(Guid idQuestion)
        {
            bool isSuccess;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var oneQuestion = _questionDao.GetOne(idQuestion);

                    oneQuestion.StatusRegister = CStatusRegister.Delete;

                    Context.Questions.Add(oneQuestion);
                    Context.Entry(oneQuestion).State = EntityState.Modified;
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

        public List<SelectListItem> GetQuestionTolink(string service, Guid idAccount, IDataProtector protector)
        {

            var idService = Guid.Parse(service);

            return
                _questionDao.GetQuestionsToLink(idService, idAccount)
                    .Select(q => new SelectListItem() { Text = q.Title, Value = q.Id.ToString() })
                    .ToList();
        }

        private List<MyTaskQuestionsViewModel> GetAnsweredQuestions(List<MyTaskQuestionsViewModel> questions, Guid idTask, int copyNumber, Guid idAccount)
        {
            foreach (var question in questions)
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

            return questions;
        }
    }
}
