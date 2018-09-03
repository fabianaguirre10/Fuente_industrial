using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class LogicViewModel
    {
        public string IdQuestion { get; set; }

        public string IdProductCategory { get; set; }
       
        public string IdProduct { get; set; }

        public List<LogicAnswerViewModel> ItemsAnswer { get; set; } 
    }

    /// <summary>
    /// 
    /// </summary>
    public class LogicAnswerViewModel
    {
        public string IdAnswer { get; set; }

        public string HasNext { get; set; }

        public string IdQuestionLink { get; set; }
    }

}
