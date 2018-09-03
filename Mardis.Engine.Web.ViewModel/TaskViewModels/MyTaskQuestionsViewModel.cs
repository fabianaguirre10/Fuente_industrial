using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskQuestionsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public int Weight { get; set; }

        public Guid IdTypePoll { get; set; }

        public string NamePoll { get; set; }

        public string CodeTypePoll { get; set; }

        public string Answer { get; set; }

        public bool HasPhoto { get; set; }

        public int CopyNumber { get; set; }

        public Guid IdQuestionDetail { get; set; }

        public Guid IdAnswer { get; set; }

        public bool AnswerRequired { get; set; }

        public Guid IdServiceDetail { get; set; }

        public string SectionTitle { get; set; }

        public int SectionOrder { get; set; }
        public int? sequence { get; set; }
        public int? sequenceSection { get; set ; }

        public Guid IdParentSection { get; set; }

        public string SubSectionTitle { get; set; }

        public string GroupName { get; set; }

        public List<MyTaskQuestionDetailsViewModel> QuestionDetailCollection { get; set; }
        //public List<TaskViewQuestionModelMul> DetailQuestion { get; set; }
        public override string ToString()
        {
            return SectionTitle + " " + Title;
        }
    }
}