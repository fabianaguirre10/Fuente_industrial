using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.ServiceViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertService
    {
        public static Service FromServiceRegisterViewModel(ServiceRegisterViewModel model)
        {
            return new Service()
            {
                Id = (!string.IsNullOrEmpty(model.Id)) ? Guid.Parse(model.Id) : Guid.Empty,
                Name = model.Name,
                Code = model.Code,
                CreationDate = model.DateCreation,
                IdCustomer = Guid.Parse(model.IdCustomer),
                IdChannel = Guid.Parse(model.IdChannel),
                IdTypeService = Guid.Parse(model.IdTypeService),
                PollTitle = model.PollTitle,
                StatusRegister = model.StatusRegister,
                Icon = model.Icon,
                IconColor = model.IconColor,
                Template = model.Template,
                ServiceDetails = model.ServiceDetailList.Select(s => new ServiceDetail()
                {
                    Id = (!string.IsNullOrEmpty(s.Id))? Guid.Parse(s.Id):Guid.Empty,
                    Order = s.Order,
                    StatusRegister = s.StatusRegister,
                    SectionTitle = s.SectionTitle,
                    Weight = s.Weight,
                    IsDynamic = s.IsDynamic,
                    IdService = (!string.IsNullOrEmpty(s.IdService)) ? Guid.Parse(s.IdService):Guid.Empty,
                    HasPhoto = s.HasPhoto,
                    GroupName = s.GroupName,
                    IdSection = s.IdSection,
                    Sections = GetSectionChildrens(s),
                    Questions = s.Questions.Select(q => new Question()
                    {
                        Id = (!string.IsNullOrEmpty(q.Id)) ? Guid.Parse(q.Id):Guid.Empty,
                        IdServiceDetail = (!string.IsNullOrEmpty(q.IdServiceDetail)) ? Guid.Parse(q.IdServiceDetail):Guid.Empty,
                        Title = q.Title,
                        StatusRegister = q.StatusRegister,
                        Order = q.Order,
                        Weight = q.Weight,
                        IdTypePoll = Guid.Parse(q.IdTypePoll),
                        HasPhoto = q.HasPhoto ? "S" : "N",
                        CountPhoto = 0,
                        AnswerRequired = q.AnswerRequired,
                        QuestionDetails = q.QuestionDetails.Select(qd => new QuestionDetail()
                        {
                            Id = (!string.IsNullOrEmpty(qd.Id)) ? Guid.Parse(qd.Id):Guid.Empty,
                            IdQuestion = (!string.IsNullOrEmpty(qd.IdQuestion)) ? Guid.Parse(qd.IdQuestion):Guid.Empty,
                            Order = qd.Order,
                            Weight = qd.Weight,
                            Answer = qd.Answer,
                            IdQuestionLink = (!string.IsNullOrEmpty(qd.IdQuestionLink)) ? (Guid?) Guid.Parse(qd.IdQuestionLink) : null,
                            IsNext = "N",
                            StatusRegister = qd.StatusRegister
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }

        private static List<ServiceDetail> GetSectionChildrens(ServiceDetailRegisterViewModel s)
        {
            if (s.Sections == null)
            {
                return new List<ServiceDetail>();
            }
            return s.Sections.Select(x => new ServiceDetail()
            {
                GroupName = x.GroupName,
                HasPhoto = x.HasPhoto,
                Id = (!string.IsNullOrEmpty(x.Id)) ? Guid.Parse(x.Id):Guid.Empty,
                IdSection = x.IdSection,
                IdService = string.IsNullOrEmpty(x.IdService) ? (Guid?)null : Guid.Parse(x.IdService),
                IsDynamic = x.IsDynamic,
                Order = x.Order,
                SectionTitle = x.SectionTitle,
                Weight = x.Weight,
                StatusRegister = x.StatusRegister,
                Questions = x.Questions.Select(q => new Question()
                {
                    Id = (!string.IsNullOrEmpty(q.Id)) ? Guid.Parse(q.Id):Guid.Empty,
                    IdServiceDetail = (!string.IsNullOrEmpty(q.IdServiceDetail)) ? Guid.Parse(q.IdServiceDetail):Guid.Empty,
                    Title = q.Title,
                    StatusRegister = q.StatusRegister,
                    Order = q.Order,
                    Weight = q.Weight,
                    IdTypePoll = Guid.Parse(q.IdTypePoll),
                    HasPhoto = q.HasPhoto ? "S" : "N",
                    CountPhoto = 0,
                    AnswerRequired = q.AnswerRequired,
                    QuestionDetails = q.QuestionDetails.Select(qd => new QuestionDetail()
                    {
                        Id = (!string.IsNullOrEmpty(qd.Id)) ? Guid.Parse(qd.Id):Guid.Empty,
                        IdQuestion = (!string.IsNullOrEmpty(qd.IdQuestion)) ? Guid.Parse(qd.IdQuestion):Guid.Empty,
                        Order = qd.Order,
                        Weight = qd.Weight,
                        Answer = qd.Answer,
                        IdQuestionLink = (!string.IsNullOrEmpty(qd.IdQuestionLink)) ? Guid.Parse(qd.IdQuestionLink) : (Guid?)null,
                        IsNext = "N",
                        StatusRegister = qd.StatusRegister
                    }).ToList()
                }).ToList()
            })
            .ToList();
        }

        public static ServiceRegisterViewModel ToServiceRegisterViewModel(Service service)
        {
            return new ServiceRegisterViewModel()
            {
                Code = service.Code,
                CurrentSection = string.Empty,
                CurrentAnswer = string.Empty,
                CurrentQuestion = string.Empty,
                DateCreation = service.CreationDate,
                Icon = service.Icon,
                Id = service.Id.ToString(),
                IconColor = service.IconColor,
                StatusRegister = service.StatusRegister,
                IdChannel = service.IdChannel.ToString(),
                IdCustomer = service.IdCustomer.ToString(),
                IdTypeService = service.IdTypeService.ToString(),
                Name = service.Name,
                PollTitle = service.PollTitle,
                Template = service.Template,
                ServiceDetailList = service.ServiceDetails.Where(s => s.StatusRegister == CStatusRegister.Active).OrderBy(s => s.Order).Select(s => new ServiceDetailRegisterViewModel()
                {
                    Id = s.Id.ToString(),
                    StatusRegister = s.StatusRegister,
                    Weight = s.Weight,
                    IdService = s.IdService.ToString(),
                    GroupName = s.GroupName,
                    IdSection = s.IdSection,
                    Order = s.Order,
                    HasPhoto = s.HasPhoto,
                    IsDynamic = s.IsDynamic,
                    SectionTitle = s.SectionTitle,
                    Questions = s.Questions.Where(q => q.StatusRegister == CStatusRegister.Active).OrderBy(Q => Q.Order).Select(q => new QuestionRegisterViewModel()
                    {
                        AnswerRequired = q.AnswerRequired,
                        HasPhoto = q.HasPhoto.IndexOf("S", StringComparison.Ordinal) >= 0,
                        Id = q.Id.ToString(),
                        StatusRegister = q.StatusRegister,
                        Weight = q.Weight,
                        Order = q.Order,
                        Title = q.Title,
                        IdServiceDetail = q.IdServiceDetail.ToString(),
                        IdTypePoll = q.IdTypePoll.ToString(),
                        QuestionDetails = q.QuestionDetails.Where(qd => qd.StatusRegister == CStatusRegister.Active).OrderBy(qd => qd.Order).Select(qd => new QuestionDetailRegisterViewModel()
                        {
                            Id = qd.Id.ToString(),
                            StatusRegister = qd.StatusRegister,
                            Weight = qd.Weight,
                            Order = qd.Order,
                            IdQuestionLink = qd.IdQuestionLink.ToString(),
                            Answer = qd.Answer,
                            IdQuestion = qd.IdQuestion.ToString()
                        }).ToList()
                    }).ToList(),
                    Sections = s.Sections.Where(sc => sc.StatusRegister == CStatusRegister.Active).OrderBy(sc => sc.Order).Select(sc => new ServiceDetailRegisterViewModel()
                    {
                        Id = sc.Id.ToString(),
                        StatusRegister = sc.StatusRegister,
                        Weight = sc.Weight,
                        IdService = sc.IdService.ToString(),
                        GroupName = sc.GroupName,
                        IdSection = sc.IdSection,
                        Order = sc.Order,
                        HasPhoto = sc.HasPhoto,
                        IsDynamic = sc.IsDynamic,
                        SectionTitle = sc.SectionTitle,
                        Questions = sc.Questions.Where(q => q.StatusRegister == CStatusRegister.Active).OrderBy(q => q.Order).Select(q => new QuestionRegisterViewModel()
                        {
                            AnswerRequired = q.AnswerRequired,
                            HasPhoto = q.HasPhoto.IndexOf("S", StringComparison.Ordinal) >= 0,
                            Id = q.Id.ToString(),
                            StatusRegister = q.StatusRegister,
                            Weight = q.Weight,
                            Order = q.Order,
                            Title = q.Title,
                            IdServiceDetail = q.IdServiceDetail.ToString(),
                            IdTypePoll = q.IdTypePoll.ToString(),
                            QuestionDetails = q.QuestionDetails.Where(qd => qd.StatusRegister == CStatusRegister.Active).OrderBy(qd => qd.Order).Select(qd => new QuestionDetailRegisterViewModel()
                            {
                                Id = qd.Id.ToString(),
                                StatusRegister = qd.StatusRegister,
                                Weight = qd.Weight,
                                Order = qd.Order,
                                IdQuestionLink = qd.IdQuestionLink.ToString(),
                                Answer = qd.Answer,
                                IdQuestion = qd.IdQuestion.ToString()
                            }).ToList()
                        }).ToList(),
                    }).ToList()
                }).ToList()
            };
        }
    }
}
