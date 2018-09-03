using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.ViewModel.EquipmentViewModels
{
    public class EquipmentRegisterViewModel
    {
        public int Id { get; set; }
        public Guid Idbranch { get; set; }


        public int IdType { get; set; }
        public string BranchName { get; set; }
     
        [Display(Name = "Sticker")]
        public string Sticker { get; set; } 

        [Required]
        [Display(Name = "Placa")]
        public string EQplate { get; set; }
   
        [Display(Name = "Serie")]
        public string Series { get; set; }
        [Required]
        [Display(Name = "Brandeo")]
        public string brand { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string Model { get; set; }
        [Display(Name = "Número de Puertas")]
        [DataType(DataType.Duration)]
        public int NDoor { get; set; }            
        [Display(Name = "Estado")]
        public int Status { get; set; }
        public string ReturnUrl { get; set; }  
        [Display(Name = "Descripción")]
        public string description { get; set; }
        [Display(Name = "Fabricante")]
        [Required]
        public string maker { get; set; }
        
        [Display(Name = "Fecha")]
        [Range(typeof(DateTime), "1/1/1966", "1/1/2040")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public string usr_web { get; set; }
        public string aggregateuri { get; set; }

        [Display(Name = "Guia")]
        public string guide { get; set; }
        public virtual Equipament_status Equipament_statuss { get; set; }

        public virtual Branch Branches { get; set; }
        public List<EquipamentImages> EquipamentImg { get; set; }

        public List<Activity> Activities { get; set; }

    }
}
