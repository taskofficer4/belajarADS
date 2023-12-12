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
        public async Task<ActionResult> NeedAction(string id)
        {
            var dto = await _svc.GetOne(id);
            dto.State = FormState.NeedAction;
            dto.Act.FormState = FormState.NeedAction;
            await LoadCaLookup(dto);
            return View("Index", dto);
        }


        [HttpPost]
        public async Task<ActionResult> NeedAction(string id, TxCaDto dto)
        {
            dto.State = FormState.NeedAction;
            dto.CorrectiveActionID = id;
            var param = await _svc.GetOne(id);
            var validator = new TxCaNeedActionValidator();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var existingDto = await _svc.GetOne(id);
                existingDto.State = FormState.NeedAction;
                existingDto.Act.FormState = FormState.NeedAction;

                existingDto.Attachments = dto.Attachments;
                existingDto.FollowUpPlan = dto.FollowUpPlan;
                existingDto.CompletionDate = dto.CompletionDate;

                result.AddToModelState(ModelState);
                await LoadCaLookup(existingDto);
                return View("Index", existingDto);
            }

            if (!dto.IsSubmit)
            {
                TempData["Message"] = $"'{dto.CorrectiveActionID}' has been saved";
                await _svc.SaveFollowUp(dto);
            }
            else
            {
                TempData["Message"] = $"'{dto.CorrectiveActionID}' has been submited";
                dto.ModifiedBy = CurrentUser.GetPreferredUsername();
                if (param.FlowCode == "10.3.1" || param.FlowCode == "10.3.2.1")
                {
                    await _svc.SubmitForCompletion(dto);
                }
                else
                {
                    await _svc.SubmitFollowUp(dto);
                }
                
            }

            return RedirectToAction("Index", "NeedAction", new { Area = "TaskList" });
        }


        public ActionResult ProposedDueDateEdit(string id)
        {
            var dto = new TxCaDto();
            dto.CorrectiveActionID = id;
            return PartialView("_Form-ProposedDueDate", dto);
        }

        [HttpPost]
        public async Task<ActionResult> ProposedDueDateEdit(TxCaDto dto)
        {
            var validator = new TxCaProposeDueDateValidator(FormState.Create);
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return PartialView("_Form-ProposedDueDate", dto);
            }
            await _svc.SubmitProposedDueDate(dto);
            return new EmptyResult();
        }
    }
}
