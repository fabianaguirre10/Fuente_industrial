using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskServicesViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Template { get; set; }

        public List<MyTaskServicesDetailViewModel> ServiceDetailCollection { get; set; }
    }
}
