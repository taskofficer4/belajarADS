using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Response;
using System.Threading.Tasks;
using System.Web;

namespace Actris.Abstraction.Services
{
    public interface IFileAttachmentService
    {
        Task<BaseJsonResult> Upload(HttpPostedFileBase file, FileAttachmentDto dto, int maxFileSizeMb, bool isImageOnly = false);
        //Task SetSubTransactionID(int? fileID, string transactionID, string subTransactionID, string updatedBy);
        //Task SetSubTransactionID(List<FileAttachmentDto> fileAttachmentList, string transactionID, string subTransactionID, string updatedBy);
        //Task<FileAttachmentDto> GetOne(int id);
        //Task<FileAttachmentDto> GetOneByActId(string actId);
        //Task<FileAttachmentDto> GetOneByCaId(string caId);
        //Task<List<FileAttachmentDto>> GetList(string actId, string caId);
    }
}
