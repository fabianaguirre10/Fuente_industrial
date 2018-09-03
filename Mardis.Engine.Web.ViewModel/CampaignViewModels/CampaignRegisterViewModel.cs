using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignRegisterViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Nombre de la Campaña")]
        public string Name { get; set; }

        [Display(Name = "Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Estado")]
        public Guid IdStatusCampaign { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public Guid IdCustomer { get; set; }

        [Required]
        [Display(Name = "Canal")]
        public Guid IdChannel { get; set; }

        [Required]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1966", "1/1/2040")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Fecha de finalización")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1966", "1/1/2040")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Fecha de alcance")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1966", "1/1/2040")]
        public DateTime RangeDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Supervisor")]
        public Guid IdSupervisor { get; set; }

        [Required]
        [Display(Name = "Comentarios")]
        public string Comment { get; set; }

        public string NewServices { get; set; }

        public string DeletedServices { get; set; }

        public List<Service> ServiceList { get; set; } = new List<Service>();
    }
}
