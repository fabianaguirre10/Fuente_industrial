using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// View Model de Login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage ="Email es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no es válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        /// <summary>
        /// Recuerdame
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
