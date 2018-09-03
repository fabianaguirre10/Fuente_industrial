using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("QuestionDetail", Schema = "MardisCore")]
    public class QuestionDetail : IEntity
    {

        public QuestionDetail()
        {
            AnswerDetails = new HashSet<AnswerDetail>();
        }

        public Guid Id { get; set; }
        public Guid IdQuestion { get; set; }
        public int Order { get; set; }
        public int Weight { get; set; }
        public string Answer { get; set; }
        public string StatusRegister { get; set; }
        public string IsNext { get; set; }
        public Guid? IdQuestionLink { get; set; }
        public string IdQuestionRequired { get; set; }
        public string Aggregatefield { get; set; }
        [ForeignKey("IdQuestion")]
        public virtual Question Question { get; set; }

        [ForeignKey("IdQuestionLink")]
        public Question QuestionLink { get; set; }
        

        public ICollection<AnswerDetail> AnswerDetails { get; set; }

        public override string ToString()
        {
            return Order + " " + Answer;
        }
    }
}
