using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Question", Schema = "MardisCore")]
    public class Question : IEntity
    {
        public Question()
        {
            QuestionDetails = new List<QuestionDetail>();
            Answers = new HashSet<Answer>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid IdServiceDetail { get; set; }

        public string Title { get; set; }

        public string StatusRegister { get; set; }

        public int Order { get; set; }

        public int Weight { get; set; }

        public Guid IdTypePoll { get; set; }

        public string HasPhoto { get; set; }
        public string Aggregatefield { get; set; }

        public int CountPhoto { get;set; }

        public Guid? IdProductCategory { get; set; }

        public Guid? IdProduct { get; set; }


        [ForeignKey("IdTypePoll")]
        public TypePoll TypePoll { get; set; }

        [ForeignKey("IdServiceDetail")]
        public ServiceDetail ServiceDetail { get; set; }

        [InverseProperty("Question")]
        public List<QuestionDetail> QuestionDetails { get; set; }

        public ICollection<Answer> Answers { get; set; }

        [ForeignKey("IdProductCategory")]
        public ProductCategory ProductCategory { get; set; }

        [ForeignKey("IdProduct")]
        public Product Product { get; set; }

        public bool AnswerRequired { get; set; } = false;
        public int? sequence { get; set; }

        [NotMapped]
        public List<Product> ItemsProducts { get; set; }

        public override string ToString()
        {
            return Order + " " + Title;
        }
    }
}
