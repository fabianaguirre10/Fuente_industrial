using System;
using System.Collections.Generic;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskPerCampaignViewModel : PaginatedList
    {
        public TaskPerCampaignViewModel() : base("Task", "Campaign", "TasksPerCampaign")
        {
        }

        public TaskPerCampaignViewModel(string actionName, string controllerName) : base("Task", controllerName, actionName)
        {
        }

        public Guid IdCampaign { get; set; }

        public List<MyTaskItemViewModel> PendingTasksList { get; set; }

        public List<MyTaskItemViewModel> StartedTasksList { get; set; }

        public List<MyTaskItemViewModel> ImplementedTasksList { get; set; }

        public List<MyTaskItemViewModel> NotImplementedTasksList { get; set; }

        public int CountImplementedTasks { get; set; }

        public int CountNotImplementedTasks { get; set; }

        public int CountStartedTasks { get; set; }

        public int CountPendingTasks { get; set; }

        public List<MyStatusTaskViewModel> tasks { get; set; }


    }
}
