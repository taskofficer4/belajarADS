using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Web.Controllers;

namespace Actris.Web.Areas.TaskList.Controllers
{

	public class TaskListBaseController : BaseCrudController<TaskListDto, TaskListDto>
	{
		private ITaskListNeedActionService _needActionSvc;
		private ITaskListNeedApprovalService _needApprovalSvc;
		private ITaskListOverdueService _overdueSvc;
		protected string _currentTab;
		public TaskListBaseController(
		   ITaskListService crudSvc,
		   ITaskListNeedActionService needActionSvc,
		   ITaskListNeedApprovalService needApprovalSvc,
		   ITaskListOverdueService overdueSvc) : base(crudSvc, true)
		{
			_needActionSvc = needActionSvc;
			_needApprovalSvc = needApprovalSvc;
			_overdueSvc = overdueSvc;

		}

		public override Task<ActionResult> Create(TaskListDto model)
		{
			throw new System.NotImplementedException();
		}

		public override Task<ActionResult> Edit(TaskListDto model)
		{
			throw new System.NotImplementedException();
		}

		protected override FormDefinition DefineForm(FormState formState)
		{
			throw new System.NotImplementedException();
		}

		protected override List<ColumnDefinition> DefineGrid()
		{
			return TaskListDto.GetColumnDefinitions();
		}

		public override PartialViewResult GridList(FilterList filterList)
		{
			filterList.CurrentUser = CurrentUser.GetPreferredUsername();
			List<ColumnDefinition> columnDefinitions = DefineGrid();
			var task = Task.Run(async () => await _crudService.GetPaged(new GridParam
			{
				ColumnDefinitions = columnDefinitions,
				FilterList = filterList
			}));

			var ret = task.Result;
			filterList.TotalItems = ret.TotalItems;

			GridListModel model = new GridListModel
			{
				GridId = this.GetType().Name + "_grid",
				FilterList = filterList,
				ColumnDefinitions = columnDefinitions,
				HideActionButton = HideActionButton
			};
			model.FillRows(ret.Items);
			return PartialView("GridList", model);
		}

		public PartialViewResult TaskListTabCounter()
		{
			var currentUser = CurrentUser.GetPreferredUsername();
			var counterNeedAction = Task.Run(async () => await _needActionSvc.Count(currentUser)).Result;
			var counterNeedApproval = Task.Run(async () => await _needApprovalSvc.Count(currentUser)).Result;
			var counterOverdue = Task.Run(async () => await _overdueSvc.Count(currentUser)).Result;


			var tabCounter = new TaskListTabCounter();
			tabCounter.AllowNeedApproval = true;
			tabCounter.AllowOverdue = true;
			tabCounter.AllowNeedAction = true;

			tabCounter.NeedAction = counterNeedAction;
			tabCounter.NeedApproval = counterNeedApproval;
			tabCounter.Overdue = counterOverdue;

			tabCounter.CurrentTab = this.ControllerContext.RouteData.Values["controller"].ToString();
			return PartialView("_TaskListTabCounter", tabCounter);
		}


	}
}
