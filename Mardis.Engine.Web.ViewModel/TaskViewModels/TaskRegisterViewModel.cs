using System;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskRegisterViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Campaña")]
        public Guid IdCampaign { get; set; }

        [Display(Name = "Campaña asociada")]
        public string CampaignName { get; set; }

        [Required]
        [Display(Name = "Fecha de Ejecución")]
        [Range(typeof(DateTime), "1/1/1966", "1/1/2040")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Asignado a")]
        public Guid IdMerchant { get; set; }

        [Required]
        [Display(Name = "Local")]
        public Guid IdBranch { get; set; }

        public string BranchName { get; set; }
        
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public Guid IdStatusTask { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Display(Name = "Ruta")]
        public string Route { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Identificador Aggregate")]
        public string AggregateUri { get; set; }

        [Display(Name = "Código Cliente")]
        public string ExternalCode { get; set; }

        public Guid? UserValidator { get; set; }

        public DateTime DateValidation { get; set; }=DateTime.Now;
    }
}
