using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Request;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Web.Controllers;

namespace Actris.Web.Areas.TaskList.Controllers
{

	public class NeedApprovalController : TaskListBaseController
	{
		private ITxCaService _caSvc;
		public NeedApprovalController(ITaskListNeedApprovalService crudSvc,
		   ITaskListNeedActionService needActionSvc,
		   ITaskListNeedApprovalService needApprovalSvc,
		   ITaskListOverdueService overdueSvc,
		   ITxCaService caSvc) : base(crudSvc, needActionSvc, needApprovalSvc, overdueSvc)
		{
			_caSvc = caSvc;
		}

		protected override List<ColumnDefinition> DefineGrid()
		{
			return TaskListDto.NeedApprovalGetColumnDefinitions();
		}

		[HttpPost]
		public async Task<ActionResult> BulkApproval(List<TxCaDto> listCa)
		{
			var listResult = new List<ApprovalResult>();
			foreach (var ca in listCa)
			{
				// result message per item
				ApprovalResult result;

				try
				{
					ca.ModifiedBy = CurrentUser.GetPreferredUsername();
					await _caSvc.SubmitApproval(ca);

					result = new ApprovalResult(ca);
					result.Success = true;
				}
				catch (System.Exception e)
				{
					result = new ApprovalResult(ca);
					result.Success = false;
					result.ErrorMessage = e.Message;
				}
				listResult.Add(result);
			}
			return PartialView("_BulkApprovalResult", listResult);
		}
	}
}
