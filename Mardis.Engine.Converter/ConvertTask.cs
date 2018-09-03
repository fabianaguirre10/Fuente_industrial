using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.Converter.Comparer;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Convert;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;
using Mardis.Engine.Web.ViewModel.TaskViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertTask
    {

        public static List<MyTaskItemViewModel> ConvertTaskToMyTaskViewItemModel(List<TaskCampaign> taskCampaignList)
        {
            return taskCampaignList
                .Select(taskCampaign =>
                                new MyTaskItemViewModel()
                                {
                                    Id = taskCampaign.Id,
                                    BranchName = taskCampaign.Branch.Name,
                                    CampaignName = taskCampaign.Campaign.Name,
                                    StartDate = taskCampaign.StartDate,
                                    BranchExternalCode = taskCampaign.Branch.ExternalCode,
                                    BranchMardisCode = taskCampaign.Branch.Code,
                                    Route = taskCampaign.Route,
                                    Code = taskCampaign.Code,
                                    Longitude = taskCampaign.Branch.LenghtBranch,
                                    Latitude = taskCampaign.Branch.LatitudeBranch,
                                    BranchId = taskCampaign.IdBranch
                                })
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskCampaign"></param>
        /// <returns></returns>
        public static TaskRegisterModelView ConvertTaskToTaskRegisterModelView(TaskCampaign taskCampaign)
        {
            return new TaskRegisterModelView()
            {
                Id = taskCampaign.Id,
                BranchName = taskCampaign.Branch.Name,
                Code = taskCampaign.Code,
                Description = taskCampaign.Description,
                IdBranch = taskCampaign.IdBranch,
                IdCampaign = taskCampaign.IdCampaign,
                IdMerchant = taskCampaign.IdMerchant,
                IdStatusTask = taskCampaign.IdStatusTask,
                NameCampaign = taskCampaign.Campaign.Name,
                NameMerchant = taskCampaign.Merchant.Profile.Name
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskCampaign"></param>
        /// <returns></returns>
        public static MyTaskViewModel ToMyTaskViewModel(TaskCampaign taskCampaign)
        {
            if (taskCampaign.IdTaskNoImplementedReason == null)
            {
                taskCampaign.IdTaskNoImplementedReason = Guid.Empty;
            }

            return new MyTaskViewModel()
            {
                IdTask = taskCampaign.Id,
                TaskCode = taskCampaign.Code,
                IdCampaign = taskCampaign.IdCampaign,
                IdAccount = taskCampaign.IdAccount,
                CampaignName = taskCampaign.Campaign.Name,
                IdCustomer = taskCampaign.Campaign.IdCustomer,
                CustomerName = taskCampaign.Campaign.Customer.Name,
                CustomerCode = taskCampaign.Campaign.Customer.Code,
                IdMerchant = taskCampaign.IdMerchant,
                MerchantName = taskCampaign.Merchant.Person.Name,
                MerchantSurname = taskCampaign.Merchant.Person.SurName,
                DateCreation = taskCampaign.DateCreation,
                StartDate = taskCampaign.StartDate,
                IdBranch = taskCampaign.IdBranch,
                BranchName = taskCampaign.Branch.Name,
                BranchLongitude = taskCampaign.Branch.LenghtBranch,
                BranchLatitude = taskCampaign.Branch.LatitudeBranch,
                IdStatusTask = taskCampaign.IdStatusTask,
                StatusTaskName = taskCampaign.StatusTask.Name,
                BranchOwnerName = taskCampaign.Branch.PersonOwner.Name,
                BranchCity = taskCampaign.Branch.District.Name,
                BranchSector = taskCampaign.Branch.Zone,
                BranchParish = taskCampaign.Branch.Parish.Name,
                BranchNeighborhood = taskCampaign.Branch.Neighborhood,
                BranchMainStreet = taskCampaign.Branch.MainStreet,
                BranchSecundaryStreet = taskCampaign.Branch.SecundaryStreet,
                BranchReference = taskCampaign.Branch.Reference,
                IdTaskNotImplementedReason = (Guid)taskCampaign.IdTaskNoImplementedReason,
                CommentTaskNotImplemented = taskCampaign.CommentTaskNoImplemented,
                Description = taskCampaign.Description,
                //TODO: Revisar acceso para revisar estos datos
                ServiceCollection = GetServiceCollection(taskCampaign.Campaign.CampaignServices),
                BranchTypeBusiness = taskCampaign.Branch.TypeBusiness,
                BranchExternalCode = taskCampaign.Branch.ExternalCode,
                BranchMardisCode = taskCampaign.Branch.Code,
                Route = taskCampaign.Route,
                BranchLabel = taskCampaign.Branch.Label,
                BranchOwnerPhone = taskCampaign.Branch.PersonOwner.Phone,
                BranchOwnerMobile = taskCampaign.Branch.PersonOwner.Mobile,
                BranchProvince = taskCampaign.Branch.Province.Name,
                BranchOwnerDocument = taskCampaign.Branch.PersonOwner.Document
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignServices"></param>
        /// <returns></returns>
        private static List<MyTaskServicesViewModel> GetServiceCollection(List<CampaignServices> campaignServices)
        {
            var returnList = new List<MyTaskServicesViewModel>();

            foreach (var campaignService in campaignServices)
            {

                var serviceDetailList = new List<MyTaskServicesDetailViewModel>();

                foreach (var serviceDetail in campaignService.Service.ServiceDetails.Where(sd => sd.StatusRegister == CStatusRegister.Active).OrderBy(sd => sd.Order))
                {
                    var count = 0;
                    serviceDetail.ServiceDetailTasks =
                        serviceDetail.ServiceDetailTasks.Distinct(new DistinctServiceDetailTaskComparer()).ToList();
                    //Al menos debe iterar una vez
                    do
                    {
                        var questionList = new List<MyTaskQuestionsViewModel>();
                        foreach (var question in serviceDetail.Questions.Distinct(new DistinctQuestionComparer()).OrderBy(q => q.Order))
                        {
                            var questionDetailList = GetQuestionDetailList(question, count);

                            questionList.Add(CreateQuestion(question, questionDetailList, count));
                        }

                        serviceDetailList.Add(GetQuestionDetail(serviceDetail, questionList, count));

                        count++;
                    } while (count < serviceDetail.ServiceDetailTasks.Count);

                }
                returnList.Add(new MyTaskServicesViewModel()
                {
                    Code = campaignService.Service.Code,
                    Id = campaignService.Service.Id,
                    Name = campaignService.Service.Name,
                    Template = campaignService.Service.Template,
                    ServiceDetailCollection = serviceDetailList
                });
            }

            return returnList;
        }

        private static MyTaskServicesDetailViewModel GetQuestionDetail(ServiceDetail serviceDetail, List<MyTaskQuestionsViewModel> questionList, int count)
        {
            return new MyTaskServicesDetailViewModel()
            {
                Id = serviceDetail.Id,
                Order = serviceDetail.Order,
                QuestionCollection = questionList,
                SectionTitle =
                    count > 0 ? serviceDetail.SectionTitle + " (" + (count + 1) + ")" : serviceDetail.SectionTitle,
                Weight = serviceDetail.Weight,
                IsDynamic = serviceDetail.IsDynamic,
                NumberOfCopies = serviceDetail.ServiceDetailTasks.Count,
                CopyNumber = count,
                HasPhoto = serviceDetail.HasPhoto,
                Sections = serviceDetail.Sections.OrderBy(s => s.Order).Select(s => new MyTaskServicesDetailViewModel()
                {
                    Id = s.Id,
                    HasPhoto = s.HasPhoto,
                    IsDynamic = s.IsDynamic,
                    Order = s.Order,
                    Weight = s.Weight,
                    SectionTitle = s.SectionTitle,
                    GroupName = s.GroupName,
                    QuestionCollection = s.Questions.OrderBy(q => q.Order).Select(q => new MyTaskQuestionsViewModel()
                    {
                        Id = q.Id,
                        Order = q.Order,
                        HasPhoto = q.HasPhoto.IndexOf("S", StringComparison.Ordinal) >= 0,
                        Weight = q.Weight,
                        AnswerRequired = q.AnswerRequired,
                        IdTypePoll = q.IdTypePoll,
                        CodeTypePoll = q.TypePoll.Code,
                        Title = q.Title,
                        Answer = string.Empty,
                        NamePoll = q.TypePoll.Name,
                        QuestionDetailCollection = q.QuestionDetails.OrderBy(qd => qd.Order).Select(qd => new MyTaskQuestionDetailsViewModel()
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
                    }).ToList()
                }).ToList()
            };
        }

        private static List<MyTaskQuestionDetailsViewModel> GetQuestionDetailList(Question question, int count)
        {
            return question.QuestionDetails
                .Distinct(new DistinctQuestionDetaiComparer())
                .Select(questionDetail => new MyTaskQuestionDetailsViewModel()
                {
                    Answer = questionDetail.Answer,
                    Checked = false,
                    Id = questionDetail.Id,
                    IdQuestion = questionDetail.IdQuestion,
                    IdQuestionLink = questionDetail.IdQuestionLink ?? Guid.Empty,
                    IsNext = questionDetail.IsNext,
                    Order = questionDetail.Order,
                    Weight = questionDetail.Weight,
                    CopyNumber = count,
                    IdServiceDetail = question.IdServiceDetail
                })
                .OrderBy(q => q.Order)
                .ToList();
        }

        private static MyTaskQuestionsViewModel CreateQuestion(Question question, List<MyTaskQuestionDetailsViewModel> questionDetailList, int count)
        {
            return new MyTaskQuestionsViewModel()
            {
                Answer = string.Empty,
                CodeTypePoll = question.TypePoll.Code,
                Id = question.Id,
                IdTypePoll = question.IdTypePoll,
                NamePoll = question.TypePoll.Name,
                Order = question.Order,
                QuestionDetailCollection = questionDetailList,
                Title = question.Title,
                Weight = question.Weight,
                HasPhoto = question.HasPhoto == "S",
                CopyNumber = count,
                AnswerRequired = question.AnswerRequired
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskList"></param>
        /// <returns></returns>
        public static List<CampaignBranchesViewModel> ConvertTaskListToCampaignBranchesViewModelList(
            List<TaskCampaign> taskList)
        {
            return taskList
                .Select(t =>
                    new CampaignBranchesViewModel()
                    {
                        Date = t.StartDate,
                        Id = t.Branch.Id,
                        Name = t.Branch.Name,
                        Status = t.StatusTask.Name,
                        Code = t.Branch.ExternalCode,
                        Latitude = t.Branch.LatitudeBranch,
                        Longitude = t.Branch.LenghtBranch,
                        Started = CTask.StatusStarted,
                        Pending = CTask.StatusPending,
                        Implemented = CTask.StatusImplemented,
                        NotImplemented = CTask.StatusNotImplemented
                    })
                .ToList();
        }

        public static List<GeoPositionViewModel> ConvertTaskPerCampaignViewModelToGeoPositionViewModelList(
            TaskPerCampaignViewModel taskList)
        {
            var resultList = new List<GeoPositionViewModel>();

            resultList.AddRange(taskList.PendingTasksList.Select(t =>
                                        new GeoPositionViewModel()
                                        {
                                            IconUrl = CImages.RedMarker,
                                            Latitude = t.Latitude.Replace(",", "."),
                                            Longitude = t.Longitude.Replace(",", "."),
                                            Title = t.BranchName,
                                            IdTask = t.Id.ToString()
                                        }));

            resultList.AddRange(taskList.StartedTasksList.Select(t =>
                                        new GeoPositionViewModel()
                                        {
                                            IconUrl = CImages.OrangeMarker,
                                            Latitude = t.Latitude.Replace(",", "."),
                                            Longitude = t.Longitude.Replace(",", "."),
                                            Title = t.BranchName,
                                            IdTask = t.Id.ToString()
                                        }));

            resultList.AddRange(taskList.ImplementedTasksList.Select(t =>
                                        new GeoPositionViewModel()
                                        {
                                            IconUrl = CImages.GreenMarker,
                                            Latitude = t.Latitude.Replace(",", "."),
                                            Longitude = t.Longitude.Replace(",", "."),
                                            Title = t.BranchName,
                                            IdTask = t.Id.ToString()
                                        }));

            resultList.AddRange(taskList.NotImplementedTasksList.Select(t =>
                                        new GeoPositionViewModel()
                                        {
                                            IconUrl = CImages.BlueMarker,
                                            Latitude = t.Latitude.Replace(",", "."),
                                            Longitude = t.Longitude.Replace(",", "."),
                                            Title = t.BranchName,
                                            IdTask = t.Id.ToString()
                                        }));

            return resultList;
        }

        public static TaskCampaign FromTaskRegisterViewModel(TaskRegisterViewModel model)
        {
            return new TaskCampaign()
            {
                Description = model.Description,
                DateModification = DateTime.Now,
                Id = model.Id,
                IdBranch = model.IdBranch,
                StartDate = model.StartDate,
                IdCampaign = model.IdCampaign,
                IdMerchant = model.IdMerchant,
                IdStatusTask = model.IdStatusTask,
                Code = model.Code,
                DateCreation = model.DateCreation.Year>2010? model.DateCreation:DateTime.Now,
                Route = model.Route,
                AggregateUri = model.AggregateUri,
                ExternalCode = model.ExternalCode,
                UserValidator = model.UserValidator,
                DateValidation = model.DateValidation.Year > 2010 ? model.DateCreation : DateTime.Now
            };
        }

        public static TaskRegisterViewModel ToTaskRegisterViewModel(TaskCampaign task)
        {
            return new TaskRegisterViewModel()
            {
                BranchName = task.Branch.Name,
                Description = task.Description,
                Id = task.Id,
                IdBranch = task.IdBranch,
                IdCampaign = task.IdCampaign,
                IdMerchant = task.IdMerchant,
                StartDate = task.StartDate,
                IdStatusTask = task.IdStatusTask,
                Code = task.Code,
                DateCreation = task.DateCreation,
                Route = task.Route,
                AggregateUri = task.AggregateUri,
                UserValidator = task.UserValidator,
                ExternalCode = task.ExternalCode,
                DateValidation = task.DateValidation
            };
        }

    }
}
