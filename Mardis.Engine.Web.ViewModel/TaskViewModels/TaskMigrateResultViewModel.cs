using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskMigrateResultViewModel
    {
        public string Code { get; set; }
        public string User { get; set; }
        public string description { get; set; }
        public string Element { get; set; }
        public string type { get; set; }
        public int line { get; set; }
    }
}
