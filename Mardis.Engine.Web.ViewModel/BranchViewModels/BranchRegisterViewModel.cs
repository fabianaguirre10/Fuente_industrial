using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchRegisterViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Código de Cliente")]
        [StringLength(50, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 3)]
        public string ExternalCode { get; set; }

        [Required]
        [Display(Name = "Nombre de local")]
        [StringLength(200, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Rótulo de local")]
        [StringLength(200, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 5)]
        public string Label { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [StringLength(20, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string StatusRegister { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Debe seleccionar un País...")]
        public string IdCountry { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Debe seleccionar una Provincia...")]
        public string IdProvince { get; set; }

        [Display(Name = "Cantón")]
        [Required(ErrorMessage = "Debe seleccionar un Cantón...")]
        public string IdDistrict { get; set; }

        [Display(Name = "Parroquia")]
        [Required(ErrorMessage = "Debe seleccionar una Parroquia...")]
        public string IdParish { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Zona...")]
        [Display(Name = "Zona")]
        public string IdSector { get; set; }

        [Required]
        [Display(Name = "Sector")]
        [StringLength(250, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string Zone { get; set; }

        [Required]
        [Display(Name = "Barrio")]
        [StringLength(250, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string Neighborhood { get; set; }

        [Required]
        [Display(Name = "Latitud")]
        //[Range(float.MinValue, float.MaxValue, ErrorMessage = "El {0} no contiene el formato correcto, por favor revisar")]
        public string Latitude { get; set; }

        [Required]
        [Display(Name = "Longitud")]
        //[Range(float.MinValue, float.MaxValue, ErrorMessage = "El {0} no contiene el formato correcto, por favor revisar")]
        public string Longitude { get; set; }

        [Required]
        [Display(Name = "Calle principal")]
        [StringLength(250, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string MainStreet { get; set; }

        [Required]
        [Display(Name = "Calle secundaria")]
        [StringLength(250, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string SecundaryStreet { get; set; }

        [Required]
        [Display(Name = "Número")]
        [StringLength(100, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string NumberBranch { get; set; }

        [Required]
        [Display(Name = "Referencia")]
        [StringLength(500, ErrorMessage = "El {0} debe contener como mínimo {2} y máximo {1} caracteres de longitud", MinimumLength = 1)]
        public string Reference { get; set; }

        public Guid? IdPersonAdministrator { get; set; }

        public Guid IdPersonOwner { get; set; }

        [Display(Name = "Personería")]
        public string OwnerTypeDocument { get; set; }

        [Required(ErrorMessage = "Necesita Ingresar el documento del dueño")]
        [Display(Name = "Documento")]
        public string OwnerDocument { get; set; }

        [Display(Name = "Nombres")]
        public string OwnerName { get; set; }

        [Display(Name = "Apellidos")]
        public string OwnerSurname { get; set; }

        [Display(Name = "Teléfono")]
        public string OwnerPhone { get; set; }

        [Display(Name = "Celular")]
        public string OwnerCellPhone { get; set; }

        [Display(Name = "¿El administrador del local es el propietario del negocio?")]
        public bool IsAdministratorOwner { get; set; }

        [Display(Name = "Nombres")]
        public string AdministratorName { get; set; }

        [Display(Name = "Apellidos")]
        public string AdministratorSurname { get; set; }

        [Required(ErrorMessage = "Necesita Ingresar el documento del administrador")]
        [Display(Name = "Documento")]
        public string AdministratorDocument { get; set; }

        [Display(Name = "Celular")]
        public string AdministratorCellPhone { get; set; }

        [Display(Name = "Teléfono")]
        public string AdministratorPhone { get; set; }

        [Display(Name = "Tipo General de Local")]
        public string TypeBusiness { get; set; }

        public List<BranchCustomersViewModel> BranchCustomers { get; set; } = new List<BranchCustomersViewModel>();

        public Guid IdCustomer { get; set; }

        public Guid IdTypeBusiness { get; set; }

        public Guid IdChannel { get; set; }

        public string SelectedAccounts { get; set; }

        public string ReturnUrl { get; set; }
    }
}
