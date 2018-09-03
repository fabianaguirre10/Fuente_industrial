using Mardis.Engine.DataAccess.MardisCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.EquipmentViewModels
{
    public class EquipmentItemViewModel
    {
        public int Id { get; set; }
        public Guid Idbranch { get; set; }

        public int IdType { get; set; }
        public string Code { get; set; }
        public string Sticker { get; set; }
        public string EQplate { get; set; }
        public string Series { get; set; }
        public string brand { get; set; }
        public string Model { get; set; }
        public virtual Equipament_status Equipament_statuss { get; set; }
        public string maker { get; set; }
        public string guide { get; set; }
        public int NDoor { get; set; }
        public int Status { get; set; }
        public string description { get; set; }

        public DateTime CreationDate { get; set; }

        public string usr_web { get; set; }
        public virtual Branch Branches { get; set; }
    }
}
