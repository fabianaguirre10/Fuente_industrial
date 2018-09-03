using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.ServiceViewModels
{
    public class ServiceRegisterViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Nombre del Servicio")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tipo de Servicio")]
        public string IdTypeService { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public string IdCustomer { get; set; }

        [Required]
        [Display(Name = "Canal")]
        public string IdChannel { get; set; }

        //public List<SelectListItem> ChannelList { get; set; }

        [Required]
        [Display(Name = "Tìtulo")]
        public string PollTitle { get; set; }

        public List<ServiceDetailRegisterViewModel> ServiceDetailList { get; set; } = new List<ServiceDetailRegisterViewModel>();

        public string CurrentSection { get; set; }

        public string CurrentQuestion { get; set; }

        public string CurrentAnswer { get; set; }

        public string StatusRegister { get; set; } = "A";

        [Required]
        [Display(Name = "Ícono")]
        public string Icon { get; set; }

        [Required]
        [Display(Name = "Color de Ícono")]
        public string IconColor { get; set; }

        [Required]
        [Display(Name = "Plantilla")]
        public string Template { get; set; }
    }
}
