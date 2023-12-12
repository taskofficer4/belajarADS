using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Infrastructure.Validators;
using Actris.Web.Extensions;

namespace Actris.Web.Controllers
{
    public partial class CorrectiveActionController : Controller
    {
        public async Task<ActionResult> NeedApproval(string id)
        {
            var dto = await _svc.GetOne(id);
            dto.State = FormState.NeedApproval;
            dto.Act.FormState = FormState.NeedApproval;
            await LoadCaLookup(dto);
            return View("Index", dto);
        }

        [HttpPost]
        public async Task<ActionResult> NeedApproval(string id, TxCaDto dto)
        {
            dto.CorrectiveActionID = id;
            TempData["Message"] = $"{dto.CorrectiveActionID} has been {dto.ApprovalAction}";
            dto.ModifiedBy = CurrentUser.GetPreferredUsername();
            //TODO: masuk ke service untuk canclled/approve/reject
            var param = await _svc.GetOne(id);
            dto.ModifiedBy = CurrentUser.GetPreferredUsername();
            dto.Pic1 = param.Pic1;
            dto.Pic2 = param.Pic2;
            if (dto.IsReject)
            {

            }
            else
            {
                if (param.FlowCode == "10.3.4")
                {
                    await _svc.SubmitApprovalCompleted(dto);
                }
                else
                {
                    await _svc.SubmitApproval(dto);
                }
            }
            return RedirectToAction("Index", "NeedApproval", new { Area = "TaskList" });
        }

    }
}
