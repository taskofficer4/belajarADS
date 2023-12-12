using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Repositories
{
    public interface ITxAttachmentRepository
    {
        Task Create(FileAttachmentDto model);
        Task<List<FileAttachmentDto>> GetList(string referenceCode);
        Task UpdateReferenceCode(FileAttachmentDto model);
        Task Sync(string projectPhase, string referenceCode, List<FileAttachmentDto> listAttachment);
        Task Delete(string referenceCode, string filepath);
    }
}
