using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Helpers;
using System.Web;
using System;
using System.Threading.Tasks;
using Actris.Abstraction.Model.Response;
using System.IO;
using Serilog;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Helper;

namespace Actris.Infrastructure.Services
{
    public class FileAttachmentService : IFileAttachmentService
    {
        private readonly ITxAttachmentRepository _repo;

        public FileAttachmentService(ITxAttachmentRepository repo)
        {
            _repo = repo;
        }
        public async Task<BaseJsonResult> Upload(HttpPostedFileBase file, FileAttachmentDto dto, int maxFileSizeMb, bool isImageOnly = false)
        {
            // validasi
            if (file == null)
            {
                return BaseJsonResult.Error("File is required");
            }

       
           
            if (file.ContentLength <= 0)
            {
                return BaseJsonResult.Error("File size must large than 0 Byte");
            }

            if (isImageOnly)
            {
                if (!file.FileName.IsImage())
                {
                    return BaseJsonResult.Error($"File extension must {string.Join(" ", FileHelperExtension.AllowedImages)}");
                }
            }
            else
            {
                if (!file.FileName.IsAllowedFiles())
                {
                    return BaseJsonResult.Error($"File extension must {string.Join(" ", FileHelperExtension.AllowedFiles)}");
                }

            }

            var oneMB = 1 * 1028 * 1028;
            var maxFileSize = maxFileSizeMb * oneMB;
            if (file.ContentLength > maxFileSize)
            {
                return BaseJsonResult.Error($"File size must lower than {maxFileSizeMb} MB. Selected file : {file.ContentLength.ToPrettySize()}");
            }

            

            // CREATE FOLDER
            var dateNow = DateHelper.WibNow;
    
            var folder = $"{dateNow:yyyy}\\{dateNow:MM}\\{dateNow:dd}\\{dateNow:HHmmss.fff}";
            Directory.CreateDirectory(dto.FolderToUpload + "/" + folder);

            // FOLDER + FILENAME
            string fileName = Path.GetFileName(file.FileName);
            var filePath = folder + "\\" + fileName;

            // PHYSIC PATH
            string _physicalyPath = Path.Combine(dto.FolderToUpload, filePath);

            // UPLOAD PHYSIC FILE
            file.SaveAs(_physicalyPath);

            dto.ReferenceCode = "DRAFT";
            dto.FilePath = filePath;
            dto.CreatedDate = DateHelper.WibNow;
            dto.FileName= fileName;
            dto.FileSize = file.ContentLength;
            dto.FileType = fileName.GetFileExtension();
            dto.CreatedBy = CurrentUser.GetPreferredUsername();

            
            await _repo.Create(dto);

            BuildFileUrl(dto);
            return BaseJsonResult.Ok(new
            {
                FileName = dto.FileName,
                FileSize = dto.FileSize?.ToPrettySize(),
                FileUrl = dto.FileUrl
            });
        }

        public void BuildFileUrl(FileAttachmentDto dto)
        {
            if (dto != null)
            {
                if (!string.IsNullOrEmpty(dto.FilePath))
                {
                    dto.FileUrl = FileHelperExtension.GetFileUrl(dto.FilePath);
                }

            }

        }
    }
}
