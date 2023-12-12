using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Validators;
using Actris.Web.Extensions;

namespace Actris.Web.Controllers
{
   [Authorize]
   public partial class CorrectiveActionController : Controller
   {
      private ITxCaService _svc;
      private ILookupService _lookupSvc;
      private IFileAttachmentService _attachmentSvc;
      private IUserService _userSvc;
      public CorrectiveActionController(ITxCaService svc, ILookupService lookupSvc, IFileAttachmentService attachmentSvc, IUserService userSvc)
      {
         _svc = svc;
         _lookupSvc = lookupSvc;
         _attachmentSvc = attachmentSvc;
         _userSvc = userSvc;
      }

      private async Task LoadCaLookup(TxCaDto dto)
      {
         ViewBag.ProjectPhase = "TX_CorrectiveAction";
         dto.Lookup.CaPriority = await _lookupSvc.GetCaPriorityList();
         dto.Lookup.OverdueImpact = await _lookupSvc.GetCaOverdueImpactList();
         if (!string.IsNullOrEmpty(dto.ResponsibleManager))
         {
            var lsManager = await _lookupSvc.GetManagerList(dto.ResponsibleDepartmentID);
            dto.Lookup.ResponsibleManager = lsManager.Items.Select(o => new LookupItem(o.EmpID, o.DisplayText())).ToLookupList();
         }

         if (!string.IsNullOrEmpty(dto.Pic1))
         {
            var lsEmploy = await _lookupSvc.GetEmployeeList(dto.Pic1);
            dto.Lookup.Pic1 = lsEmploy.Select(o => new LookupItem(o.EmpID, o.EmpName)).ToLookupList();
         }

         if (!string.IsNullOrEmpty(dto.Pic2))
         {
            var lsEmploy = await _lookupSvc.GetEmployeeList(dto.Pic2);
            dto.Lookup.Pic2 = lsEmploy.Select(o => new LookupItem(o.EmpID, o.EmpName)).ToLookupList();
         }
      }

      public async Task<JsonResult> GetManagerList(string q, int page = 1)
      {
         var result = await _lookupSvc.GetManagerList(q, page);
         return Json(result, JsonRequestBehavior.AllowGet);
      }

      public async Task<JsonResult> GetEmployeeListByParentEmpID(string empID, string q, int page = 1)
      {
         var result = await _lookupSvc.GetEmployeeListByParentEmpID(empID, q, page);
         return Json(result, JsonRequestBehavior.AllowGet);
      }

   }
}
