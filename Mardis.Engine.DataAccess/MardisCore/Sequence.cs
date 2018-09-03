using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Secuencias que tiene el Sistema Engine
    /// </summary>
    [Table("Sequence", Schema = "MardisCore")]
    public class Sequence : IEntity, ISoftDelete
    {

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Initial { get; set; }
        public int SequenceCurrent { get; set; }
        public string StatusRegister { get; set; }
        public int ControlSequence { get; set; }
        public System.Guid IdAccount { get; set; }


        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }

        public override string ToString()
        {
            return Initial + "-" + SequenceCurrent;
        }
    }
}
