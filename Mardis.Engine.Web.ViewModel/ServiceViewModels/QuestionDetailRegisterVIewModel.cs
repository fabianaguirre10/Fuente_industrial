using System;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.ServiceViewModels
{
    public class QuestionDetailRegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string IdQuestion { get; set; } = Guid.Empty.ToString();

        [Required]
        [Display(Name = "Respuesta")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        [Display(Name = "Pregunta siguiente")]
        public string IdQuestionLink { get; set; }

        [Display(Name = "Peso")]
        public int Weight { get; set; }

        [Display(Name = "Estado")]
        public string StatusRegister { get; set; } = "A";
    }
}