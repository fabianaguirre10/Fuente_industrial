using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// ViewModel Question
    /// </summary>
    public class QuestionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid IdServiceDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TypePoll> ItemsTypePoll { get; set; }

       
    }
}
