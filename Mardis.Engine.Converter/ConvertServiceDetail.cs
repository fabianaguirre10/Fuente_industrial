using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.TaskViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertServiceDetail
    {
        public static MyTaskServicesDetailViewModel ToMyTaskServicesDetailViewModel(ServiceDetail section)
        {
            return new MyTaskServicesDetailViewModel()
            {
                Id = section.Id,
                HasPhoto = section.HasPhoto,
                IsDynamic = section.IsDynamic,
                NumberOfCopies = section.NumberOfCopies,
                Order = section.Order,
                SectionTitle = section.SectionTitle,
                Weight = section.Weight,

            };
        }

        public static List<MyTaskServicesDetailViewModel> ToMyTaskServicesDetailViewModelList(List<ServiceDetail> sections)
        {
            return sections.Select(s => new MyTaskServicesDetailViewModel()
            {
                NumberOfCopies = s.NumberOfCopies,
                GroupName = s.GroupName,
                HasPhoto = s.HasPhoto,
                Id = s.Id,
                SectionTitle = s.SectionTitle,
                IsDynamic = s.IsDynamic,
                Order = s.Order,
                Weight = s.Weight,
                QuestionCollection = s.Questions.Where(q => q.StatusRegister == CStatusRegister.Active).OrderBy(q => q.Order).Select(q => new MyTaskQuestionsViewModel()
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
                              q.QuestionDetails.Where(qd => qd.StatusRegister == CStatusRegister.Active)
                                  .OrderBy(qd => qd.Order)
                                  .Select(qd => new MyTaskQuestionDetailsViewModel()
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
                }).ToList(),
                Sections = s.Sections.Where(ss => ss.StatusRegister == CStatusRegister.Active).OrderBy(ss => ss.Order).Select(ss => new MyTaskServicesDetailViewModel()
                {
                    NumberOfCopies = ss.NumberOfCopies,
                    GroupName = ss.GroupName,
                    HasPhoto = ss.HasPhoto,
                    Id = ss.Id,
                    SectionTitle = ss.SectionTitle,
                    IsDynamic = ss.IsDynamic,
                    Order = ss.Order,
                    Weight = ss.Weight,
                    QuestionCollection =
                                ss.Questions.Where(q => q.StatusRegister == CStatusRegister.Active)
                                    .OrderBy(q => q.Order)
                                    .Select(q => new MyTaskQuestionsViewModel()
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
                                                          q.QuestionDetails.Where(qd => qd.StatusRegister == CStatusRegister.Active)
                                                              .OrderBy(qd => qd.Order)
                                                              .Select(qd => new MyTaskQuestionDetailsViewModel()
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
                                            }).ToList(),
                }).ToList()
            }).ToList();
        }
    }
}
