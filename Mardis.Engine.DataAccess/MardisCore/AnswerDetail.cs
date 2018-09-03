using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("AnswerDetail", Schema = "MardisCore")]
    public class AnswerDetail : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdQuestionDetail { get; set; }

        [ForeignKey("IdQuestionDetail")]
        public QuestionDetail QuestionDetail { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public Guid IdAnswer { get; set; }

        public int CopyNumber { get; set; }

        public string AnswerValue { get; set; }

        public byte[] AnswerMultimedia { get; set; }

        [ForeignKey("IdAnswer")]
        public Answer Answer { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;
    }
}
