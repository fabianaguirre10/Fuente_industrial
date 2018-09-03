using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskViewAnswer
    {
        public Guid Idquestion { get; set; }
        public string AnswerQuestion { get; set; }
        public Guid idTask { get; set; }
        public String idAnswer { get; set; }
        public String estado { get; set; }
        
    }
}
