using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class QuestionDetailBusiness : ABusiness
    {
        readonly QuestionDetailDao _questionDetailDao;
        readonly QuestionDao _questionDao;

        public QuestionDetailBusiness(MardisContext mardisContext)
               : base(mardisContext)
        {
            _questionDetailDao = new QuestionDetailDao(mardisContext);
            _questionDao = new QuestionDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <returns></returns>
        public int GetMax(Guid idQuestion)
        {
            return _questionDetailDao.GetMax(idQuestion);
        }


        public List<QuestionDetail> GetQuestionDetails(Guid idQuestion)
        {
            return _questionDetailDao.GetQuestionDetails(idQuestion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public QuestionDetail AddAnswer(Guid idQuestion, int position = -1)
        {
            var oneQuestionDetail = new QuestionDetail
            {
                IdQuestion = idQuestion,
                IsNext = CQuestionDetail.Yes
            };


            if (-1 == position)
            {
                oneQuestionDetail.Order = GetMax(idQuestion) + 1;
            }
            else
            {
                oneQuestionDetail.Order = position;
            }

            oneQuestionDetail.Answer = string.Empty;
            oneQuestionDetail.StatusRegister = CStatusRegister.Active;

            return oneQuestionDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestionDetail"></param>
        /// <returns></returns>
        public QuestionDetail GetOne(Guid idQuestionDetail)
        {
            return _questionDetailDao.GetOne(idQuestionDetail);
        }

        /// <summary>
        /// Dame respuestas con pregunta
        /// </summary>
        /// <param name="idQuestionDetail"></param>
        /// <returns></returns>
        public QuestionDetail GetOneWithQuestion(Guid idQuestionDetail)
        {
            return _questionDetailDao.GetOneWithQuestion(idQuestionDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idQuestion"></param>
        /// <param name="idQuestionDetail"></param>
        /// <param name="actionAnswer"></param>
        /// <returns></returns>
        public QuestionDetail InsertQuestionDetail(Guid idQuestion, Guid idQuestionDetail, string actionAnswer)
        {
            var itemReturn = new QuestionDetail();

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var questionDetailCurrent = _questionDetailDao.GetOne(idQuestionDetail);
                    var position = -1;
                    var itemsUpdate = new List<QuestionDetail>();

                    switch (actionAnswer)
                    {
                        case CService.LastAnswer:
                            position = -1;
                            break;
                        case CService.AfterAnswer:
                            position = questionDetailCurrent.Order + 1;
                            itemsUpdate = _questionDetailDao.GetQuestionDetailAfterOrder(idQuestion,
                                                                                  questionDetailCurrent.Order);
                            break;
                    }

                    foreach (var itemTemp in itemsUpdate)
                    {
                        itemTemp.Order = itemTemp.Order + 1;
                    }

                    itemReturn = AddAnswer(idQuestion, position);

                    Context.QuestionDetails.Add(itemReturn);
                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return itemReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idService"></param>
        /// <returns></returns>
        public List<QuestionDetail> GetQuestionDetailByService(Guid idService)
        {
            var itemsReturn = _questionDetailDao.GetQuestionDetailByService(idService);

            return itemsReturn;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsQuestionDetail"></param>
        /// <returns></returns>
        public Boolean SaveQuestionDetail(List<LogicViewModel> itemsQuestionDetail)
        {
            bool isSuccess;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {

                    foreach (var itemTempLogic in itemsQuestionDetail)
                    {

                        var oneQuestion = _questionDao.GetOne(new Guid(itemTempLogic.IdQuestion));

                        if (!string.IsNullOrEmpty(itemTempLogic.IdProductCategory))
                        {
                            oneQuestion.IdProductCategory = new Guid(itemTempLogic.IdProductCategory);
                        }
                        else
                        {
                            oneQuestion.IdProductCategory = null;
                        }

                        if (!string.IsNullOrEmpty(itemTempLogic.IdProduct))
                        {
                            oneQuestion.IdProduct = new Guid(itemTempLogic.IdProduct);
                        }
                        else
                        {
                            oneQuestion.IdProduct = null;
                        }


                        Context.Questions.Add(oneQuestion);
                        Context.Entry(oneQuestion).State = EntityState.Modified;


                        if (0 < itemTempLogic.ItemsAnswer.Count)
                        {
                            foreach (var itemAnswer in itemTempLogic.ItemsAnswer)
                            {
                                var oneAnswer = _questionDetailDao.GetOne(new Guid(itemAnswer.IdAnswer));

                                oneAnswer.IsNext = itemAnswer.HasNext;

                                if (!string.IsNullOrEmpty(itemAnswer.IdQuestionLink))
                                {
                                    oneAnswer.IdQuestionLink = new Guid(itemAnswer.IdQuestionLink);
                                }

                                Context.QuestionDetails.Add(oneAnswer);
                                Context.Entry(oneAnswer).State = EntityState.Modified;
                            }
                        }


                    }

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAnswer"></param>
        /// <returns></returns>
        public bool DeleteAnswer(Guid idAnswer)
        {
            var isSuccess = true;

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var oneAnswer = _questionDetailDao.GetOne(idAnswer);

                    oneAnswer.StatusRegister = CStatusRegister.Delete;

                    Context.QuestionDetails.Add(oneAnswer);
                    Context.Entry(oneAnswer).State = EntityState.Modified;
                    Context.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();

                    isSuccess = false;
                }
            }

            return isSuccess;
        }
    }
}
