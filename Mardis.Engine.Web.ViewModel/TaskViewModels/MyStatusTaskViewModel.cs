using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
  public  class MyStatusTaskViewModel
    {
        public List<MyTaskItemViewModel> TasksList { get; set; }
        public int CountTasks { get; set; }

        public int order { get; set; }

        public string type { get; set; }

        public string color { get; set; }
    }
}
