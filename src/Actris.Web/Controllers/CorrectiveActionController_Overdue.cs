using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Infrastructure.Validators;
using Actris.Web.Extensions;

namespace Actris.Web.Controllers
{
   public partial class CorrectiveActionController : Controller
   {
      public async Task<ActionResult> Overdue(string id)
      {
         var dto = await _svc.GetOne(id);
         dto.State = FormState.Overdue;
         dto.Act.FormState = FormState.Overdue;
         await LoadCaLookup(dto);
         return View("Index", dto);
      }

      public async Task< ActionResult> OverdueShowForm(string id)
      {
         var dto = new TxCaDto();
         dto.CorrectiveActionID = id;
         await LoadCaLookup(dto);
         return PartialView("_Form-OverdueAsModalContent", dto);
      }

      [HttpPost]
      public async Task<ActionResult> OverdueSubmit(TxCaDto dto)
      {
         var validator = new TxCaOverdueValidator();
         var result = await validator.ValidateAsync(dto);
         await LoadCaLookup(dto);
         if (!result.IsValid)
         {
            result.AddToModelState(ModelState);
            return PartialView("_Form-OverdueAsModalContent", dto);
         }
         await _svc.SubmitOverdue(dto);
         return new EmptyResult();
      }
   }
}
