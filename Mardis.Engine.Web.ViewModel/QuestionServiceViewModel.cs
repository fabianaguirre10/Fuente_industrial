using System;

namespace Mardis.Engine.Web.ViewModel
{
    public class OneAnswerViewModel
    {
        public Guid IdQuestionDetail { get; set; }

        public Guid IdQuestion{ get; set; }

        public int CopyNumber { get; set; }

    }

    public class OpenAnswerViewModel
    {
        //public Guid IdQuestionDetail { get; set; }

        public Guid IdQuestion { get; set; }

        public string Value { get; set; }

        public int CopyNumber { get; set; }
    }

    public class ManyAnswerViewModel
    {
        public Guid IdQuestionDetail { get; set; }

        public int CopyNumber { get; set; }
    }

}
