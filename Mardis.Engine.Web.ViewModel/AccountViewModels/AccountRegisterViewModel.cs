using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.AccountViewModels
{
    public class AccountRegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string StatusRegister { get; set; }

        public bool Selected { get; set; }
    }
}