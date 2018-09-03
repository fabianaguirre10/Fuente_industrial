using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskServicesDetailViewModel
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public int Weight { get; set; }

        public string SectionTitle { get; set; }

        public bool IsDynamic { get; set; }

        public bool HasPhoto { get; set; }

        public int NumberOfCopies { get; set; }

        public int CopyNumber { get; set; }

        public string GroupName { get; set; }

        public Guid? IdSection { get; set; }

        public List<MyTaskServicesDetailViewModel> Sections { get; set; }

        public List<MyTaskQuestionsViewModel> QuestionCollection { get; set; }
    }
}