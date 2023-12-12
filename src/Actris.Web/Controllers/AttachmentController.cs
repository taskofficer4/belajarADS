using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using System.Collections.Generic;
using System.Configuration;
using Antlr.Runtime.Misc;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Services;
using System.Web.Configuration;
using System.Security.Claims;
using System.Security.Principal;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using System;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Actris.Web.Controllers
{
   [Authorize]
   public class AttachmentController : Controller
   {
      private readonly IFileAttachmentService _attachmentSvc;


      public AttachmentController(IFileAttachmentService attachmentSvc)
      {
         _attachmentSvc = attachmentSvc;
      }

      public PartialViewResult AttachmentSection(string projectPhase, string documentType, List<FileAttachmentDto> attachmentList, bool viewOnly = false)
      {
         var attachmentListFiltered = attachmentList.Where(o=>o.ProjectPhase== projectPhase).ToList();

         if (viewOnly)
         {
            return PartialView("_Form-AttachmentViewOnly", attachmentListFiltered);
         }
         ViewBag.ProjectPhase = projectPhase;
         ViewBag.DocumentType = documentType;
         ViewBag.ViewOnly = viewOnly;
         return PartialView("_Form-Attachment", attachmentListFiltered);
      }

      public async Task<PartialViewResult> UploadFile(HttpPostedFileBase file, string projectPhase, string documentType, string description, List<FileAttachmentDto> existingList = null)
      {
         ViewBag.FromActionUpload = true;
         if (existingList == null)
         {
            existingList = new List<FileAttachmentDto>();
         }

         try
         {
            FileAttachmentDto fileDto = new FileAttachmentDto
            {
               FolderToUpload = Server.MapPath("~/Upload"),
               CreatedBy = User.GetPreferredUsername(),
               DocumentType = documentType,
               ProjectPhase = projectPhase,
               FileDescription = description,
            };

            var result = await _attachmentSvc.Upload(file, fileDto, 5);
            if (result.Success)
            {
               existingList.Add(fileDto);
            }
            else
            {
               ModelState.AddModelError("File", result.Message);
            }

         }
         catch (Exception e)
         {
            ModelState.AddModelError("File", e.Message);
         }

         ViewBag.ProjectPhase = projectPhase;
         ViewBag.DocumentType = documentType;
         return PartialView("_Form-Attachment", existingList);
      }
   }
}
