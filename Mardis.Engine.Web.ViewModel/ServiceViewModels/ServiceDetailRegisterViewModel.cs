using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.ServiceViewModels
{
    public class ServiceDetailRegisterViewModel
    {
        public string Id { get; set; }

        //[Required]
        [Display(Name = "Servicio")]
        public string IdService { get; set; }

        [Required]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string StatusRegister { get; set; } = "A";

        [Required]
        [Display(Name = "Título de Sección")]
        public string SectionTitle { get; set; }

        [Required]
        [Display(Name = "Peso")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "Sección Dinámica")]
        public bool IsDynamic { get; set; }

        [Required]
        [Display(Name = "Tiene Foto")]
        public bool HasPhoto { get; set; }

        [Display(Name = "Grupo")]
        public string GroupName { get; set; }

        public Guid? IdSection { get; set; }

        public List<ServiceDetailRegisterViewModel> Sections { get; set; }= new List<ServiceDetailRegisterViewModel>();

        public List<QuestionRegisterViewModel> Questions { get; set; } = new List<QuestionRegisterViewModel>();
    }
}