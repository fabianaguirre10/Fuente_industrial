using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mardis.Engine.DataAccess.MardisCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Web.ViewModel.CustomerViewModels
{
    public class CustomerRegisterViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Nombre del Cliente")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public Guid IdStatusCustomer { get; set; }

        public List<SelectListItem> StatusCustomers { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Abreviatura")]
        public string Abbreviation { get; set; }

        [Required]
        [Display(Name = "Tipo de Cliente")]
        public Guid IdTypeCustomer { get; set; }

        public List<SelectListItem> Types { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Nombre del Contacto")]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public ICollection<Channel> Channels { get; set; } = new List<Channel>();

        public List<TypeBusiness> TypeBusinessList { get; set; } = new List<TypeBusiness>();

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public string NewChannels { get; set; } = string.Empty;

        public string NewTypeBusiness { get; set; } = string.Empty;

        public string NewCategories { get; set; } = string.Empty;

        public string DeletedChannels { get; set; } = string.Empty;

        public string DeletedTypeBusiness { get; set; } = string.Empty;

        public string DeletedCategories { get; set; } = string.Empty;
    }
}
