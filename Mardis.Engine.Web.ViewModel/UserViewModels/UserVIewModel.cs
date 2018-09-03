using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.UserViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string ProfileId { get; set; }

        public string PersonId { get; set; }

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public string IdTypeUser { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string PersonName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string PersonSurname { get; set; }

        [Required]
        [Display(Name = "Tipo de Documento")]
        public string TypeDocument { get; set; }

        [Required]
        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Display(Name = "Celular")]
        public string Mobile { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        
        public string UserType { get; set; }
        
        public string Account { get; set; }

        public string Avatar { get; set; }
    }
}
