using System;

namespace Mardis.Engine.Web.ViewModel.DashBoardViewModels
{
    public class DashBoardMerchantViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int TaskCount { get; set; }

        public string PercentImplementedTasks { get; set; }

        public string PercentNotImplementedTasks { get; set; }

        public string PercentStartedTasks { get; set; }

        public string PercentPendingTasks { get; set; }
    }
}
