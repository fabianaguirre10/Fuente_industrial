using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskListViewModel : PaginatedList
    {
        public TaskListViewModel() : base("Task", "Task", "Index")
        {
        }

        public Guid IdCampaign { get; set; }

        public List<CoreFilterDetail> FilterList { get; set; } = new List<CoreFilterDetail>();

        public List<TaskListItemViewModel> TasksList { get; set; } = new List<TaskListItemViewModel>();
    }
}
