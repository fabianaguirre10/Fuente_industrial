using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mardis.Engine.Web.ViewModel.ServiceViewModels
{
    public class QuestionRegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Sección")]
        public string IdServiceDetail { get; set; } = Guid.Empty.ToString();

        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string StatusRegister { get; set; } = "A";

        [Required]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        [Required]
        [Display(Name = "Peso")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "Tipo de Pregunta")]
        public string IdTypePoll { get; set; }

        [Required]
        [Display(Name = "Tiene Foto")]
        public bool HasPhoto { get; set; }

        [Required]
        [Display(Name = "Requiere Respuesta")]
        public bool AnswerRequired { get; set; }

        public List<QuestionDetailRegisterViewModel> QuestionDetails { get; set; } = new List<QuestionDetailRegisterViewModel>();
    }
}