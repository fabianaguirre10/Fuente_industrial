using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel.ServiceViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Mardis.Engine.Converter;
using MiscUtil.Reflection;
using System.Xml.Linq;
using Mardis.Engine.Web.ViewModel.Utility;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
namespace Mardis.Engine.Business.MardisCore
{
    public class ServiceBusiness : ABusiness
    {
        #region VARIABLES & CONSTRUCTORES

        private readonly ServiceDao _serviceDao;
        private readonly ServiceDetailDao _serviceDetailDao;
        private readonly QuestionDao _questionDao;
        private readonly QuestionDetailDao _questionDetailDao;
        private readonly TypePollDao _typePollDao;
        private readonly SequenceBusiness _sequenceBusiness;
        private readonly TypeServiceDao _typeServiceDao;
        private readonly CustomerDao _customerDao;
        private readonly IList<StructXmlModel> slStructXmlModel = new List<StructXmlModel>();
        public ServiceBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _serviceDao = new ServiceDao(mardisContext);
            _serviceDetailDao = new ServiceDetailDao(mardisContext);
            _questionDao = new QuestionDao(mardisContext);
            _questionDetailDao = new QuestionDetailDao(mardisContext);
            _typePollDao = new TypePollDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
            _typeServiceDao = new TypeServiceDao(mardisContext);
            _customerDao = new CustomerDao(mardisContext);
        }

        #endregion

        public List<Service> GetService(Guid idTypeService, Guid idCustomer, Guid idAccount)
        {
            return _serviceDao.GetService(idTypeService, idCustomer, idAccount);
        }

        public Service GetOne(Guid id, Guid idAccount)
        {
            return _serviceDao.GetOne(id, idAccount);
        }
        public List<Service> GetServicebyAccount(Guid idAccount)
        {
            return _serviceDao.GetServicesByAccount(idAccount);
        }

        public List<Service> GetServicesByCustomerId(Guid idAccount, Guid idCustomer)
        {
            return _serviceDao.GetServicesByCustomerId(idAccount,
                                                        idCustomer);
        }

        public List<Service> GetServicesByChannelId(Guid idAccount, Guid idChannel)
        {
            return _serviceDao.GetServicesByChannelId(idAccount,
                                                       idChannel);
        }

        public ServiceIndexViewModel GetIndexPageInformation(string typeService, string customer, IDataProtector protector, Guid idAccount)
        {
            var idTypeService = Guid.Empty;
            var idCustomer = Guid.Empty;

            if (!string.IsNullOrEmpty(typeService))
            {
                idTypeService = Guid.Parse(protector.Unprotect(typeService));
            }

            if (!string.IsNullOrEmpty(customer))
            {
                idCustomer = Guid.Parse(protector.Unprotect(customer));
            }

            var model = new ServiceIndexViewModel
            {
                TypeServicesList = _typeServiceDao.GetAll()
                    .Select(
                        t =>
                            new TypeServiceListViewModel()
                            {
                                Id = protector.Protect(t.Id.ToString()),
                                Name = t.Name
                            })
                    .ToList(),
                Customers =
                    _customerDao.GetCustomerByTypeService(idTypeService, idAccount)
                        .Select(
                            c =>
                                new ServiceCustomerViewModel()
                                {
                                    Id = protector.Protect(c.Id.ToString()),
                                    Name = c.Name,
                                    IdTypeService = typeService
                                })
                        .ToList(),
                Services =
                    _serviceDao.GetServicesByCustomerId(idAccount, idCustomer)
                        .Select(
                            s =>
                                new ServiceItemViewModel()
                                {
                                    Id = protector.Protect(s.Id.ToString()),
                                    Name = s.Name,
                                    IdCustomer = customer,
                                    IdTypeBusiness = typeService,
                                    Icon = s.Icon,
                                    IconColor = s.IconColor
                                })
                        .ToList()
            };


            return model;
        }

        #region SECTIONS

        public void DuplicateSection(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);

            var section = GetCurrentSection(service, Guid.Parse(model.CurrentSection));

            if (section.IdSection == Guid.Empty || section.IdSection == null)
            {
                var beforeSections = service.ServiceDetails.Where(s => s.Order <= section.Order).OrderBy(s => s.Order).ToList();
                var afterSections = service.ServiceDetails.Where(s => s.Order > section.Order).OrderBy(s => s.Order).ToList();

                service.ServiceDetails.Clear();

                service.ServiceDetails.AddRange(beforeSections);

                section.Id = Guid.Empty;
                section.Questions.ForEach(q => q.Id = Guid.Empty);
                section.Questions.ForEach(q => q.QuestionDetails.ForEach(d => d.Id = Guid.Empty));

                section.Sections.ForEach(a => a.Id = Guid.Empty);
                section.Sections.ForEach(a => a.Questions.ForEach(q => q.Id = Guid.Empty));
                section.Sections.ForEach(a => a.Questions.ForEach(q => q.QuestionDetails.ForEach(d => d.Id = Guid.Empty)));

                service.ServiceDetails.Add(section);
                service.ServiceDetails.AddRange(afterSections);
            }
            else
            {
                var serviceDetail = service.ServiceDetails.FirstOrDefault(s => s.Id == section.IdSection);
                var beforeSections = serviceDetail.Sections.Where(s => s.Order <= section.Order).OrderBy(s => s.Order).ToList();
                var afterSections = serviceDetail.Sections.Where(s => s.Order > section.Order).OrderBy(s => s.Order).ToList();

                serviceDetail.Sections.Clear();
                serviceDetail.Sections.AddRange(beforeSections);

                section.Id = Guid.Empty;
                section.Questions.ForEach(q =>
                {
                    q.Id = Guid.Empty;
                    q.IdServiceDetail = Guid.Empty;
                });
                section.Questions.ForEach(q => q.QuestionDetails.ForEach(d =>
                                                                            {
                                                                                d.Id = Guid.Empty;
                                                                                d.IdQuestion = Guid.Empty;
                                                                            }));

                section = _serviceDetailDao.InsertOrUpdate(section);

                var newSection = _serviceDetailDao.GetNotTracked(section.Id, idAccount);

                if (newSection == null)
                {

                }

                newSection.Questions.ForEach(q =>
                {
                    q.ServiceDetail = null;
                    q.QuestionDetails.ForEach(d =>
                    {
                        d.Question = null;
                    });
                });

                serviceDetail.Sections.Add(newSection);
                serviceDetail.Sections.AddRange(afterSections);

                var beforeDetails =
                    service.ServiceDetails.Where(s => s.Order < serviceDetail.Order).OrderBy(s => s.Order).ToList();
                var afterDetails =
                    service.ServiceDetails.Where(s => s.Order > serviceDetail.Order).OrderBy(s => s.Order).ToList();

                service.ServiceDetails.Clear();
                service.ServiceDetails.AddRange(beforeDetails);
                service.ServiceDetails.Add(serviceDetail);
                service.ServiceDetails.AddRange(afterDetails);
            }

            //UpdateEntireService(service, idAccount);
        }

        public ServiceRegisterViewModel AddSection(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateSection(service, CService.LastSection, Guid.Empty);

            service = UpdateEntireService(service, idAccount);

            return GetServiceRegisterModel(idAccount, service.Id);
        }

        public ServiceRegisterViewModel AddSubSection(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateSubSection(service, Guid.Parse(model.CurrentSection));

            return GetServiceRegisterModel(idAccount, service.Id);
        }

        public void AddSectionBefore(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateSection(service, CService.BeforeSection, Guid.Parse(model.CurrentSection));

            UpdateEntireService(service, idAccount);
        }

        public void AddSectionAfter(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateSection(service, CService.AfterSection, Guid.Parse(model.CurrentSection));

            UpdateEntireService(service, idAccount);
        }

        public void DeleteSection(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            var section = service.ServiceDetails.FirstOrDefault(s => s.Id == Guid.Parse(model.CurrentSection));

            section.StatusRegister = CStatusRegister.Delete;
            _serviceDetailDao.InsertOrUpdate(section);
        }

        private Service GenerateSubSection(Service service, Guid currentSection)
        {
            //var order = service.ServiceDetails.FirstOrDefault(s => s.Id == idSection).Sections.Max(s => s.Order) + 1;

            var order = 1;

            var subSection =
                new ServiceDetail()
                {
                    HasPhoto = false,
                    IsDynamic = false,
                    Order = order,
                    SectionTitle = string.Empty,
                    StatusRegister = CStatusRegister.Active,
                    IdSection = currentSection,
                    Weight = 0,
                    IdService = null
                };

            _serviceDetailDao.InsertOrUpdate(subSection);

            return service;
        }

        public Service GenerateSection(Service service, string position, Guid currentSection)
        {
            var order = 0;

            var section = service.ServiceDetails.FirstOrDefault(s => s.Id == currentSection);
            switch (position)
            {
                case CService.BeforeSection:
                    if (section != null)
                    {
                        order = section.Order;
                    }
                    break;
                case CService.AfterSection:
                    if (section != null)
                    {
                        order = section.Order + 1;
                    }
                    break;
                case CService.LastSection:
                    if (section != null)
                    {
                        order = section.Order;
                    }
                    else
                    {
                        order = service.ServiceDetails.Max(s => s.Order) + 1;
                    }
                    break;
            }

            var beforeSections = service.ServiceDetails.Where(s => s.Order < order).OrderBy(s => s.Order).ToList();
            var afterSections = service.ServiceDetails.Where(s => s.Order >= order).OrderBy(s => s.Order).ToList();

            service.ServiceDetails.Clear();
            service.ServiceDetails.AddRange(beforeSections);

            service.ServiceDetails.Add(new ServiceDetail()
            {
                HasPhoto = false,
                IsDynamic = false,
                Order = order,
                SectionTitle = "",
                StatusRegister = CStatusRegister.Active,
                Weight = 0,
                IdService = service.Id
            });

            service.ServiceDetails.AddRange(afterSections);

            return service;
        }

        #endregion

        #region QUESTIONS

        public void AddQuestion(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateQuestion(service, CService.LastQuestion, Guid.Parse(model.CurrentSection), Guid.Empty);

            UpdateEntireService(service, idAccount);
        }

        public void AddQuestionBefore(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateQuestion(service, CService.BeforeQuestion, Guid.Parse(model.CurrentSection),
                Guid.Parse(model.CurrentQuestion));

            UpdateEntireService(service, idAccount);
        }

        public void AddQuestionAfter(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            service = GenerateQuestion(service, CService.AfterQuestion, Guid.Parse(model.CurrentSection),
                Guid.Parse(model.CurrentQuestion));

            UpdateEntireService(service, idAccount);
        }

        public void DeleteQuestion(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            var section = service.ServiceDetails.FirstOrDefault(s => s.Id == Guid.Parse(model.CurrentSection));
            var question = section.Questions.FirstOrDefault(q => q.Id == Guid.Parse(model.CurrentQuestion));

            question.StatusRegister = CStatusRegister.Delete;
            _questionDao.InsertOrUpdate(question);
        }

        public Service GenerateQuestion(Service service, string position, Guid currentSection, Guid currentQuestion)
        {
            var type = _typePollDao.GetByCode(CTypePoll.Open);
            //var section = service.ServiceDetails.FirstOrDefault(s => s.Id == idSection);
            var section = GetCurrentSection(service, currentSection);
            var question = section.Questions.FirstOrDefault(q => q.Id == currentQuestion);
            var order = 1;

            switch (position)
            {
                case CService.BeforeQuestion:
                    order = question.Order;
                    break;
                case CService.AfterQuestion:
                    order = question.Order + 1;
                    break;
                case CService.LastQuestion:
                    if (section.Questions.Any())
                    {
                        order = section.Questions.Max(q => q.Order) + 1;
                    }
                    break;
            }

            service = UpdateServiceQuestionSchema(service, currentSection, section, order, type);

            return service;
        }

        private static Service UpdateServiceQuestionSchema(Service service, Guid idSection, ServiceDetail section, int order,
            TypePoll type)
        {
            var beforeQuestions = section.Questions.Where(q => q.Order < order).OrderBy(q => q.Order).ToList();
            var afterQuestions = section.Questions.Where(q => q.Order >= order).OrderBy(q => q.Order).ToList();

            if (section.IdSection == Guid.Empty || section.IdSection == null)
            {
                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection).Questions.Clear();
                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection).Questions.AddRange(beforeQuestions);
                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection).Questions.Add(
                    new Question()
                    {
                        CountPhoto = 0,
                        HasPhoto = "N",
                        Order = 0,
                        StatusRegister = CStatusRegister.Active,
                        Weight = 0,
                        Title = "",
                        IdTypePoll = type.Id,
                        IdServiceDetail = idSection
                    });
                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection).Questions.AddRange(afterQuestions);

            }
            else
            {
                var serviceDetail = service.ServiceDetails.First(s => s.Id == section.IdSection);

                if (serviceDetail != null)
                {
                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                    .Questions.Clear();

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.AddRange(beforeQuestions);

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.Add(new Question()
                        {
                            CountPhoto = 0,
                            HasPhoto = "N",
                            Order = 0,
                            StatusRegister = CStatusRegister.Active,
                            Weight = 0,
                            Title = "",
                            IdTypePoll = type.Id,
                            IdServiceDetail = idSection
                        });

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.AddRange(afterQuestions);
                }
            }

            return service;
        }

        #endregion

        #region ANSWERS

        public void AddAnswer(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);

            service = GenerateAnswer(service, CService.LastAnswer, Guid.Parse(model.CurrentSection),
                Guid.Parse(model.CurrentQuestion), Guid.Empty);

            UpdateEntireService(service, idAccount);
        }


        public void AddSubAnswer(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);

            service = GenerateAnswer(service, CService.LastAnswer, Guid.Parse(model.CurrentSection),
                Guid.Parse(model.CurrentQuestion), Guid.Empty);

            UpdateEntireService(service, idAccount);
        }

        public void AddAnswerAfter(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);

            service = GenerateAnswer(service, CService.AfterAnswer, Guid.Parse(model.CurrentSection),
                Guid.Parse(model.CurrentQuestion), Guid.Parse(model.CurrentAnswer));

            UpdateEntireService(service, idAccount);
        }

        public void DeleteAnswer(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);
            var section = service.ServiceDetails.FirstOrDefault(s => s.Id == Guid.Parse(model.CurrentSection));
            var question = section.Questions.FirstOrDefault(q => q.Id == Guid.Parse(model.CurrentQuestion));
            var answer = question.QuestionDetails.FirstOrDefault(q => q.Id == Guid.Parse(model.CurrentAnswer));

            answer.StatusRegister = CStatusRegister.Delete;
            _questionDetailDao.InsertOrUpdate(answer);
        }

        private Service GenerateAnswer(Service service, string position, Guid idSection, Guid idQuestion, Guid idAnswer)
        {
            var section = GetCurrentSection(service, idSection);

            var question = section.Questions.FirstOrDefault(q => q.Id == idQuestion);
            var answer = question.QuestionDetails.FirstOrDefault(a => a.Id == idAnswer);
            var order = 1;

            switch (position)
            {
                case CService.AfterAnswer:
                    order = answer.Order + 1;
                    break;
                case CService.LastAnswer:
                    if (question.QuestionDetails.Any())
                    {
                        order = question.QuestionDetails.Max(d => d.Order) + 1;
                    }
                    break;
            }

            service = UpdateServiceQuestionDetailSchema(service, idSection, idQuestion, question, order, section);

            return service;
        }

        private static Service UpdateServiceQuestionDetailSchema(Service service, Guid idSection, Guid idQuestion, Question question, int order, ServiceDetail section)
        {
            var beforeAnswers = question.QuestionDetails.Where(q => q.Order < order).OrderBy(q => q.Order).ToList();
            var afterAnswers = question.QuestionDetails.Where(q => q.Order >= order).OrderBy(q => q.Order).ToList();

            if (section.IdSection == Guid.Empty || section.IdSection == null)
            {
                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection)
                    .Questions.FirstOrDefault(q => q.Id == idQuestion)
                    .QuestionDetails.Clear();

                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection)
                    .Questions.FirstOrDefault(q => q.Id == idQuestion)
                    .QuestionDetails.AddRange(beforeAnswers);

                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection)
                    .Questions.FirstOrDefault(q => q.Id == idQuestion)
                    .QuestionDetails.Add(new QuestionDetail()
                    {
                        Answer = "",
                        Order = 0,
                        IsNext = "N",
                        StatusRegister = CStatusRegister.Active,
                        Weight = 0,
                        IdQuestion = idQuestion
                    });

                service.ServiceDetails.FirstOrDefault(s => s.Id == idSection)
                    .Questions.FirstOrDefault(q => q.Id == idQuestion)
                    .QuestionDetails.AddRange(afterAnswers);
            }
            else
            {
                var serviceDetail = service.ServiceDetails.First(s => s.Id == section.IdSection);

                if (serviceDetail != null)
                {
                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                    .Questions.FirstOrDefault(q => q.Id == idQuestion)
                    .QuestionDetails.Clear();

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.FirstOrDefault(q => q.Id == idQuestion)
                        .QuestionDetails.AddRange(beforeAnswers);

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.FirstOrDefault(q => q.Id == idQuestion)
                        .QuestionDetails.Add(new QuestionDetail()
                        {
                            Answer = "",
                            Order = 0,
                            IsNext = "N",
                            StatusRegister = CStatusRegister.Active,
                            Weight = 0,
                            IdQuestion = idQuestion
                        });

                    serviceDetail.Sections.FirstOrDefault(s => s.Id == idSection)
                        .Questions.FirstOrDefault(q => q.Id == idQuestion)
                        .QuestionDetails.AddRange(afterAnswers);
                }
            }

            return service;
        }

        private static ServiceDetail GetCurrentSection(Service service, Guid idSection)
        {
            var section = service.ServiceDetails.FirstOrDefault(s => s.Id == idSection);

            if (section == null)
            {
                if (service.ServiceDetails.Any(s => s.Sections.Any(d => d.Id == idSection)))
                {
                    foreach (var serviceServiceDetail in service.ServiceDetails)
                    {
                        if (serviceServiceDetail.Sections.Any(sd => sd.Id == idSection))
                        {
                            section = serviceServiceDetail.Sections.FirstOrDefault(a => a.Id == idSection);
                            break;
                        }
                    }
                }
            }
            return section;
        }

        #endregion

        public Service UpdateEntireService(Service service, Guid idAccount)
        {
            var sectionOrder = 0;
            if (service.Id == Guid.Empty)
            {
                service = SaveService(service, idAccount);
            }

            foreach (var serviceDetail in service.ServiceDetails)
            {
                serviceDetail.IdService = service.Id;
                sectionOrder = UpdateSectionsInDataBase(service, serviceDetail, sectionOrder);
            }

            service.ServiceDetails.Clear();
            service.IdAccount = idAccount;

            return _serviceDao.InsertOrUpdate(service);
        }

        private int UpdateSectionsInDataBase(Service service, ServiceDetail serviceDetail, int sectionOrder)
        {
            var questionOrder = 0;

            if (serviceDetail.Id == Guid.Empty)
            {
                var q = _questionDao.InsertOrUpdate(serviceDetail);

                serviceDetail.Id = q.Id;
            }

            foreach (var question in serviceDetail.Questions)
            {

                question.IdServiceDetail = serviceDetail.Id;

                if (question.Id == Guid.Empty)
                {
                    var q = _questionDao.InsertOrUpdate(question);

                    question.Id = q.Id;
                }

                var questionDetailOrder = 0;
                foreach (var questionDetail in question.QuestionDetails)
                {
                    questionDetail.IdQuestion = question.Id;
                    questionDetail.Order = questionDetailOrder++;
                    _questionDetailDao.InsertOrUpdate(questionDetail);
                }
                question.QuestionDetails.Clear();

                question.Order = questionOrder++;
                _questionDao.InsertOrUpdate(question);
            }
            serviceDetail.Questions.Clear();

            foreach (var section in serviceDetail.Sections)
            {

                section.IdSection = serviceDetail.Id;

                if (section.Id == Guid.Empty)
                {
                    var s = _serviceDetailDao.InsertOrUpdate(section);
                    section.Id = s.Id;
                }

                foreach (var question in section.Questions)
                {

                    question.IdServiceDetail = section.Id; ;

                    if (question.Id == Guid.Empty)
                    {
                        var q = _questionDao.InsertOrUpdate(question);

                        question.Id = q.Id;
                    }

                    var questionDetailOrder = 0;
                    foreach (var questionDetail in question.QuestionDetails)
                    {
                        questionDetail.IdQuestion = question.Id;
                        questionDetail.Order = questionDetailOrder++;
                        _questionDetailDao.InsertOrUpdate(questionDetail);
                    }
                    question.QuestionDetails.Clear();

                    question.Order = questionOrder++;
                    _questionDao.InsertOrUpdate(question);
                }

                //section.IdService = service.Id;
                section.Order = sectionOrder++;
                _serviceDetailDao.InsertOrUpdate(section);
            }

            serviceDetail.Sections.Clear();

            serviceDetail.IdService = service.Id;
            serviceDetail.Order = sectionOrder++;
            _serviceDetailDao.InsertOrUpdate(serviceDetail);
            return sectionOrder;
        }

        public ServiceRegisterViewModel GetService(string idService, IDataProtector protector, Guid accountId)
        {
            var model = new ServiceRegisterViewModel();

            if (!string.IsNullOrEmpty(idService))
            {
                var serviceId = Guid.Parse(protector.Unprotect(idService));

                model = GetServiceRegisterModel(accountId, serviceId);
            }

            return model;
        }

        private ServiceRegisterViewModel GetServiceRegisterModel(Guid idAccount, Guid serviceId)
        {
            var myService = _serviceDao.GetService(serviceId, idAccount);
            var service = ConvertService.ToServiceRegisterViewModel(myService);

            return service;
        }

        public Service SaveService(Service tempService, Guid idAccount)
        {

            if (string.IsNullOrEmpty(tempService.Code))
            {
                var nextSequence = _sequenceBusiness.NextSequence(CService.SequenceCode, idAccount);

                var internalCode = nextSequence.Initial + "-" + nextSequence.SequenceCurrent;
                tempService.Code = internalCode;
            }

            tempService.IdAccount = idAccount;
            tempService.StatusRegister = CStatusRegister.Active;

            _serviceDao.InsertOrUpdate(tempService);

            return tempService;
        }

        public bool DeleteService(Guid idService, Guid idAccount)
        {
            bool isSuccess;

            var oneService = _serviceDao.GetOne(idService, idAccount);
            oneService.StatusRegister = CStatusRegister.Delete;

            _serviceDetailDao.InsertOrUpdate(oneService);

            return true;
        }
     
        public void Save(ServiceRegisterViewModel model, Guid idAccount)
        {
            var service = ConvertService.FromServiceRegisterViewModel(model);

            service.IdAccount = idAccount;

            //Cuando es un nuevo servicio
            if (service.Id == Guid.Empty)
            {
                _serviceDao.InsertOrUpdate(service);
            }
            //Cuando es un servicio existente
            else
            {
                var exService = _serviceDao.GetOne(service.Id, idAccount);

                PropertyCopy.Copy(service, exService);

                service = null;

                //_serviceDao.Context.SaveChanges();
                //_serviceDao.InsertOrUpdate(exService);
                UpdateEntireService(exService, idAccount);
            }

            //UpdateEntireService(service, idAccount);
        }

        #region xml
        public int GenerateXml(string urlXml,Guid idaccount , string name)

        {

            string path = urlXml;
            try
            {

           
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(path, false))
            {
                Sheet sheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);
                Sheet sheets2 = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(1);
                string comienzo = "";
                string nomenglatura = "";
                bool Dynamic = false;
                Boolean save = false;
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id) as WorksheetPart).Worksheet;
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                int j = 0;
                foreach (Row row in rows)
                {
                    j++;
                    if (row.RowIndex.Value != 1)
                    {

                        StructXmlModel structXmlModel = new StructXmlModel();
                        StructXmlModelQuestion structXmlModelQ = new StructXmlModelQuestion();
                        structXmlModel.Question = new List<StructXmlModelQuestion>();
          
                        int i = 0;
                        if (comienzo != "begin group")
                        {
                            ///servicios
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                i++;
                                switch (i)
                                {
                                      
                                    case 1:
                                        if (GetCellValue(doc, cell) == "begin repeat")
                                        {

                                            Dynamic = true;
                                        }
                                        if (GetCellValue(doc, cell) == "begin group")
                                        {
                                            save = true;
                                            comienzo = "begin group";
                                            structXmlModel.IsDynamic = Dynamic;
                                            structXmlModel.id = GetCellValue(doc, cell);
                                        }
                                        break;
                                    case 2:
                                        if (structXmlModel.id == "begin group")
                                        {
                                            structXmlModel.valueText = GetCellValue(doc, cell);
                                            nomenglatura = structXmlModel.valueText;
                                            structXmlModel.Question.Add(structXmlModelQ);
                                        }
                                        break;
                                    case 3:
                                        if (structXmlModel.id == "begin group")
                                          structXmlModel.QuestionText = GetCellValue(doc, cell);
                                        break;
                                 
                                }
                            }
                            if (comienzo == "begin group" && save)
                            {
                                save = false;
                                Dynamic = false;
                                slStructXmlModel.Add(structXmlModel);
                            }
                        }
                        else
                        {
                            //Preguntas
                            StructXmlModelQuestion questionList = new StructXmlModelQuestion();
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                i++;
                                if (nomenglatura != "")
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            if (comienzo == "begin group" )
                                               questionList.QuestionTipo = GetCellValue(doc, cell);
                                     
                                            if (GetCellValue(doc, cell) == "end group")
                                            {
                                                comienzo = "";
                                                nomenglatura = "";
                                            }
                                            break;
                                        case 2:
                                            if (comienzo == "begin group")
                                                questionList.valueText = GetCellValue(doc, cell);
                                            break;
                                        case 3:
                                            if (comienzo == "begin group")
                                            {
                                                questionList.id = nomenglatura;
                                                questionList.QuestionText = GetCellValue(doc, cell);
                                                if (questionList.QuestionTipo != "image" && questionList.QuestionTipo != "geopoint")
                                                {
                                                    slStructXmlModel.Where(q => q.valueText == nomenglatura).FirstOrDefault().Question.Add(questionList);
                                                    if (questionList.QuestionTipo.Contains("select_one") || questionList.QuestionTipo.Contains("select_multiple"))
                                                    {
                                                        // Respuesta 
                                                        slStructXmlModel.Where(q => q.valueText == nomenglatura)
                                                            .FirstOrDefault()
                                                            .Question.Where(d => d.valueText == questionList.valueText).First().Detail = new List<StructXmlModelQuestionDetail>();
                                                        worksheet = (doc.WorkbookPart.GetPartById(sheets2.Id) as WorksheetPart).Worksheet;
                                                        var rows2 = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                                                        Char delimiter = ' ';
                                                        String[] tipo = questionList.QuestionTipo.Split(delimiter);
                                                        foreach (Row rowchosse in rows2)
                                                        {
                                                            if (row.RowIndex.Value != 1)
                                                            {
                                                                int xi = 0;
                                                                StructXmlModelQuestionDetail structXmlModelD = new StructXmlModelQuestionDetail();
                                                                foreach (Cell chosse in rowchosse.Descendants<Cell>())
                                                                {
                                                                    xi++;
                                                                    switch (xi)
                                                                    {
                                                                        case 1:
                                                                            if (GetCellValue(doc, chosse).Trim() == tipo[1])
                                                                                structXmlModelD.id = GetCellValue(doc, chosse).Trim();
                                                                            break;
                                                                        case 2:
                                                                            if (structXmlModelD.id == tipo[1])
                                                                                structXmlModelD.valueText = GetCellValue(doc, chosse);
                                                                            break;
                                                                        case 3:
                                                                            if (structXmlModelD.id == tipo[1])
                                                                            {
                                                                                structXmlModelD.QuestionText = GetCellValue(doc, chosse);
                                                                                slStructXmlModel.Where(q => q.valueText == nomenglatura)
                                                                                .FirstOrDefault()
                                                                                   .Question.Where(d => d.valueText == questionList.valueText).First().Detail.Add(structXmlModelD);
                                                                            }
                                                                            break;

                                                                    }
                                                                }

                                                            }


                                                        }
                                                    }
                                                }
                                                else {

                                                    if (questionList.QuestionTipo == "geopoint") {
                                                        string coordenas = questionList.valueText;
                                                        string IdStruct = questionList.id;
                                                        questionList.QuestionTipo = "text";
                                                        questionList.valueText = coordenas + "_" + "LNG";
                                                        questionList.QuestionText = "Logitud";
                                                        questionList.id = IdStruct;
                                                        slStructXmlModel.Where(q => q.valueText == nomenglatura).FirstOrDefault().Question.Add(questionList);

                                                        questionList = new StructXmlModelQuestion();
                                                        questionList.id = IdStruct;
                                                        questionList.QuestionTipo = "text";
                                                        questionList.valueText = coordenas + "_" + "LAT";
                                                        questionList.QuestionText = "Latitud";
                                                        slStructXmlModel.Where(q => q.valueText == nomenglatura).FirstOrDefault().Question.Add(questionList);
                                                    }
                                                }

                                            }
                                            break;
                                    }
                                }
                            };
                        }

                    }


                }

              
                    return      _serviceDao.SaveFormAggregate(slStructXmlModel, idaccount,name);
                    }
            }
            catch (Exception)
            {

                return 0;
            }
        }
      
        private string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";
            if (cell.CellValue != null)
            {

                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }
            else
            {
                value = "NA";

            }
            return value;

        }
        #endregion
    }
}
