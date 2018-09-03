using System;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskQuestionDetailsViewModel
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public int Weight { get; set; }

        public Guid IdQuestion { get; set; }

        public string Answer { get; set; }

        //[Range(typeof(bool), "false", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Checked { get; set; } = false;

        public string IsNext { get; set; }

        public Guid IdQuestionLink { get; set; }
        public String IdQuestionRequired { get; set; }

        public int CopyNumber { get; set; }

        public Guid IdServiceDetail { get; set; }

        public string SelectedAnswer { get; set; }

        public override string ToString()
        {
            return Answer;
        }
    }
}