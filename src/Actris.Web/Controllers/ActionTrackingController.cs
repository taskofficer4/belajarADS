
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Validators;
using Actris.Web.Extensions;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;

namespace Actris.Web.Controllers
{

   public class ActionTrackingController : Controller
   {
      private ITxActService _svc;
      private ILookupService _lookupSvc;
      private IFileAttachmentService _attachmentSvc;
      private IUserService _userSvc;
      public ActionTrackingController(ITxActService svc, ILookupService lookupSvc, IFileAttachmentService attachmentSvc, IUserService userSvc)
      {
         _svc = svc;
         _lookupSvc = lookupSvc;
         _attachmentSvc = attachmentSvc;
         _userSvc = userSvc;
      }

      public async Task<ActionResult> Create()
      {
         var model = new TxActDto();
         await LoadLookupList(model);
         model.TypeShuRegion = await CurrentUserTypeShuRegion();
         return View("Create", model);
         //return View("CreateCopy", model);
        }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public virtual async Task<ActionResult> Create(TxActDto dto)
      {
         dto.FormState = FormState.Create;
         var validator = new ActionTrackingValidator(FormState.Create);
         var result = await validator.ValidateAsync(dto);
         await LoadLookupList(dto);
         dto.TypeShuRegion = await CurrentUserTypeShuRegion();
         if (result.IsValid)
         {
            dto.CreatedBy = User.GetPreferredUsername();
            dto.CreatedDate = DateHelper.WibNow;
            dto.FillLookupDesc();
            await _svc.Create(dto);

            TempData["Message"] = $"Data has been created with id '{dto.ActionTrackingID}'";
            return RedirectToAction("Index", "ActionTrackingList");
         }
         result.AddToModelState(ModelState);
         return View("Create", dto);
      }

      public async Task<ActionResult> Edit(string id)
      {
         var dto = await _svc.GetOne(id);
         dto.FormState = FormState.Edit;
         await LoadLookupList(dto);

         return View("Create", dto);
      }

      public async Task<ActionResult> View(string id)
      {
         var dto = await _svc.GetOne(id);
         dto.FormState = FormState.View;
         await LoadLookupList(dto);
         return View("Create", dto);
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public virtual async Task<ActionResult> Edit(TxActDto dto)
      {
         dto.FormState = FormState.Create;
         var validator = new ActionTrackingValidator(FormState.Create);
         var result = await validator.ValidateAsync(dto);
         await LoadLookupList(dto);
         if (result.IsValid)
         {
            var currentUser = await _userSvc.GetCurrentUserInfo();
            dto.ModifiedBy = User.GetPreferredUsername();
            dto.ModifiedDate = DateHelper.WibNow;
            dto.FillLookupDesc();

            await _svc.Update(dto);
            if (dto.IsSubmit)
            {
               TempData["Message"] = $"{dto.ActionTrackingID} has been submited";
            }
            else
            {
               TempData["Message"] = $"{dto.ActionTrackingID} has been updated";
            }
          
            return RedirectToAction("Index", "ActionTrackingList");
         }
         result.AddToModelState(ModelState);

         return View("Create", dto);
      }

      //[HttpGet]
      //public ActionResult View(string id)
      //{
      //    ActViewDto data = _svc.GetActionTrackingView(id);
      //    return View("View/View",data);
      //}


      protected async Task LoadLookupList(TxActDto dto)
      {
         // FROM PARAM LIST
         dto.Lookup.ActSource = await _lookupSvc.GetActSourceList();

         // saling berkaitan dropdown ini
         var currentUser = await _userSvc.GetCurrentUserInfo();
         dto.Lookup.DirectorateRegional = await _lookupSvc.GetDirectorateList(currentUser.HierLvl2);
         dto.Lookup.DivisionZona = await _lookupSvc.GetDivisiZonaList(currentUser.HierLvl2,dto.DirectorateRegionalID);
         dto.Lookup.Company = await _lookupSvc.GetCompanyList(currentUser.HierLvl2, dto.DirectorateRegionalID,dto.DivisiZonaID);
         dto.Lookup.WilayahKerja = await _lookupSvc.GetWilayahKerjaList(currentUser.HierLvl2, dto.DirectorateRegionalID, dto.DivisiZonaID, dto.CompanyCode);
         dto.Lookup.LocationCompany = await _lookupSvc.GetLocationCompanyList(dto.CompanyCode);


         dto.Lookup.LocationStatus = new LookupList
         {
            Items = new List<LookupItem>()
                {
                    new LookupItem {
                        Text = $"Within operation boundary",
                        Value = $"1",
                    },
                     new LookupItem {
                        Text = $"Outside operation boundary / public area",
                        Value = $"2",
                    }
                }
         };

         ViewBag.ProjectPhase = "TX_ActionTracking";
      }

      private async Task<string> CurrentUserTypeShuRegion()
      {
         var currentUser = await _userSvc.GetCurrentUserInfo();
         if (currentUser != null)
         {
            return currentUser.TypeShuRegion;
         }
         return "";
      }

      private JsonResult JsonResult(object result) => new JsonResult
      {
         Data = result,
         JsonRequestBehavior = JsonRequestBehavior.AllowGet
      };

      [HttpGet]
      public async Task<JsonResult> GetDivisionList(string directorateID)
      {
         var currentUser = await _userSvc.GetCurrentUserInfo();
         var result = await _lookupSvc.GetDivisiZonaList(currentUser.HierLvl2, directorateID);
         return JsonResult(result);
      }

      [HttpGet]
      public async Task<JsonResult> GetCompanyList(string directorateID, string divisionID)
      {
         var currentUser = await _userSvc.GetCurrentUserInfo();
         var result = await _lookupSvc.GetCompanyList(currentUser.HierLvl2, directorateID, divisionID);
         return JsonResult(result);
      }

      [HttpGet]
      public async Task<JsonResult> GetWilayahKerjaList(string directorateID, string divisionID, string companyCode)
      {
         var currentUser = await _userSvc.GetCurrentUserInfo();
         var result = await _lookupSvc.GetWilayahKerjaList(currentUser.HierLvl2, directorateID, divisionID, companyCode);
         return JsonResult(result);
      }

      [HttpGet]
      public async Task<JsonResult> GetLocationCompanyList(string companyCode)
      {
         var result = await _lookupSvc.GetLocationCompanyList(companyCode);
         return JsonResult(result);
      }



     


      #region CA FORM
      public async Task<PartialViewResult> CreateCa()
      {
         var dto = new TxCaDto();
         dto.State = FormState.Create;
         await LoadCaLookup(dto);

         return PartialView("_CaForm", dto);
      }

      [HttpPost]
      public async Task<ActionResult> CreateCa(TxCaDto dto, List<TxCaDto> caList)
      {
         dto.State = FormState.Create;
         var validator = new TxCaValidator(FormState.Create);
         var result = await validator.ValidateAsync(dto);
         if (result.IsValid)
         {
            if (caList == null)
            {
               caList = new List<TxCaDto>();
            }
            dto.CorrectiveActionID = "Draft";
            dto.Status = "Open";

            if (!string.IsNullOrEmpty(dto.Pic1)) dto.Pic1EmpName = (await _lookupSvc.GetEmployeeList(dto.Pic1)).First().EmpName;
            if (!string.IsNullOrEmpty(dto.Pic2)) dto.Pic2EmpName = (await _lookupSvc.GetEmployeeList(dto.Pic2)).First().EmpName;

            caList.Add(dto);

            // re index
            var i = 0;
            foreach (var ca in caList)
            {
               ca.Index = i++;
            }
            return PartialView("_CaList", caList);
         }
         result.AddToModelState(ModelState);
         await LoadCaLookup(dto);

         return PartialView("_CaForm", dto);
      }

      [HttpPost]
      public ActionResult DeleteCa(List<TxCaDto> caList, int index)
      {
         caList.RemoveAt(index);

         // re index
         var i = 0;
         foreach (var ca in caList)
         {
            ca.Index = i++;
         }
         return PartialView("_CaList", caList);
      }


      /// <summary>
      /// Untuk View CA 
      /// Di render di backend
      /// </summary>
      /// <param name="dto"></param>
      /// <returns></returns>
      [HttpPost]
      public async Task<ActionResult> ViewCa(TxCaDto dto)
      {
         dto.State = FormState.View;
         await LoadCaLookup(dto);
         return PartialView("_CaForm", dto);
      }

      /// <summary>
      /// Untuk Show Edit Form CA 
      /// Di render di backend
      /// </summary>
      /// <param name="dto"></param>
      /// <returns></returns>
      [HttpPost]
      public async Task<ActionResult> ShowEditFormCa(TxCaDto dto)
      {
         dto.State = FormState.Edit;
         await LoadCaLookup(dto);
         return PartialView("_CaForm", dto);
      }

      [HttpPost]
      public async Task<ActionResult> EditCa(TxCaDto dto, List<TxCaDto> caList)
      {
         dto.State = FormState.Edit;
         var validator = new TxCaValidator(FormState.Create);
         var result = await validator.ValidateAsync(dto);
         if (result.IsValid)
         {
            caList.Insert(dto.Index, dto);
            caList.RemoveAt(dto.Index + 1);

            return PartialView("_CaList", caList);
         }
         result.AddToModelState(ModelState);
         await LoadCaLookup(dto);
         return PartialView("_CaForm", dto);
      }

      private async Task LoadCaLookup(TxCaDto dto)
      {
         ViewBag.ProjectPhase = "TX_CorrectiveAction";
         dto.Lookup.CaPriority = await _lookupSvc.GetCaPriorityList();
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
         var result = await _lookupSvc.GetEmployeeListByParentEmpID(empID,q, page);
         return Json(result, JsonRequestBehavior.AllowGet);
      }

      #endregion

   }
}
