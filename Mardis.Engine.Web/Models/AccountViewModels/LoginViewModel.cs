using System;
using System.ComponentModel.DataAnnotations;
using Mardis.Engine.Web.ViewModel;

namespace Mardis.Engine.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public static implicit operator LoginViewModel(ViewModel.LoginViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
