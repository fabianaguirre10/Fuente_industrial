using System;

namespace Mardis.Engine.Web.ViewModel.CustomerViewModels
{
    public class CustomerItemViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Type { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}