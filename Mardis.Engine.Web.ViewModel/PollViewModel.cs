using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class PollViewModel
    {
        public PollViewModel()
        {
            ItemsQuestion = new List<QuestionPollViewModel>();
        }

        public Guid IdSection { get; set; }
        public string Title { get; set; }
        public string Weight { get; set; }

        public bool IsDynamic { get; set; }

       public List<QuestionPollViewModel> ItemsQuestion { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class QuestionPollViewModel
    {
        public QuestionPollViewModel()
        {
            ItemsAnswer = new List<AnswerPollViewModel>();
        }

        public Guid IdQuestion { get; set; }

        public string Title { get; set; }

        public Guid IdTypePoll { get; set; }

        public string Weight { get; set; }

        public string HasPhotos { get; set; }

        public string CountPhotos { get; set; }

        public List<AnswerPollViewModel> ItemsAnswer { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AnswerPollViewModel
    {
        public Guid IdAnswer { get; set; }
        public string Title { get; set; }
        public string Weight { get; set; }

    }

}
