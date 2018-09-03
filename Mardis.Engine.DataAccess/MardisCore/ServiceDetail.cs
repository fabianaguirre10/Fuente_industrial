using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("ServiceDetail", Schema = "MardisCore")]
    public class ServiceDetail : IEntity
    {

        public ServiceDetail()
        {
            Questions = new List<Question>();
            Answers = new HashSet<Answer>();
            ServiceDetailTasks = new List<ServiceDetailTask>();
            Sections = new List<ServiceDetail>();
        }

        public Guid Id { get; set; }

        public Guid? IdService { get; set; }

        public int Order { get; set; }

        public string StatusRegister { get; set; }

        public string SectionTitle { get; set; }

        public int Weight { get; set; }

        public bool HasPhoto { get; set; } = false;

        public bool IsDynamic { get; set; } = false;

        public string GroupName { get; set; }

        public int NumberOfCopies { get; set; } = 1;

        public Guid? IdSection { get; set; }

        [ForeignKey("IdSection")]
        public ServiceDetail ParentSection { get; set; }

        [ForeignKey("IdService")]
        public virtual Service Service { get; set; }

        public List<Question> Questions { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public List<ServiceDetailTask> ServiceDetailTasks { get; set; }

        public List<ServiceDetail> Sections { get; set; }

        public override string ToString()
        {
            return Order + " " +SectionTitle;
        }
    }
}
