using System;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchCustomersViewModel
    {
        public Guid Id { get; set; }

        public Guid IdBranch { get; set; }

        public Guid IdCustomer { get; set; }

        public string NameCustomer { get; set; }

        public  Guid IdTypeBusiness { get; set; }

        public string NameTypeBusiness { get; set; }

        public Guid IdChannel { get; set; }

        public string NameChannel { get; set; }

        public bool Selected { get; set; } = false;
    }
}