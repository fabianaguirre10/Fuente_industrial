using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel.ServiceViewModels
{
    public class ServiceIndexViewModel
    {
        public List<TypeServiceListViewModel> TypeServicesList { get; set; } = new List<TypeServiceListViewModel>();

        public List<ServiceCustomerViewModel> Customers { get; set; } = new List<ServiceCustomerViewModel>();

        public List<ServiceItemViewModel> Services { get; set; } = new List<ServiceItemViewModel>();
    }
}
