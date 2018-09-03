using System;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskRegisterModelView
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public Guid IdBranch { get; set; }

        public string BranchName { get; set; }

        public string Description { get; set; }

        public Guid IdCampaign { get; set; }

        public string NameCampaign { get; set; }

        public Guid IdStatusTask { get; set; }

        public string StatusTaskName { get; set; }

        public Guid IdMerchant { get; set; }

        public string NameMerchant { get; set; }

    }
}
