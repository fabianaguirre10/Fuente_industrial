using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("StatusTask",Schema = "MardisCore")]
    public class StatusTask : IEntity, ISoftDelete
    {
        public StatusTask()
        {
            Tasks = new HashSet<TaskCampaign>();
            EstadoTarea = new HashSet<StatusTaskAccount>();
            statusTasksuser = new HashSet<StatustaskUser>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }
        public string color { get; set; }
        public ICollection<TaskCampaign> Tasks { get; set; }
        public ICollection<StatusTaskAccount> EstadoTarea { get; set; }

        public ICollection<StatustaskUser> statusTasksuser { get; set; }
    }
}
