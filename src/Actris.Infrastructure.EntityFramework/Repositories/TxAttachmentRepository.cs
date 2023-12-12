using Actris.Abstraction.Extensions;
using Actris.Abstraction.Helper;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
    public class TxAttachmentRepository : BaseRepository, ITxAttachmentRepository
    {
        public TxAttachmentRepository(ActrisContext context, IConnectionProvider connection) : base(context, connection)
        {
        }

        public async Task Create(FileAttachmentDto model)
        {
            var entity = model.ToEntity();
            entity.CreatedDate = DateHelper.WibNow;

            _context.TX_Attachment.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Sync(string projectPhase, string referenceCode, List<FileAttachmentDto> listAttachment)
        {
            var listPathDontDelete = new List<string>();
            foreach (var attachment in listAttachment)
            {
                attachment.ReferenceCode = referenceCode;
                listPathDontDelete.Add(attachment.FilePath);
                await UpdateReferenceCode(attachment);
            }

            // delete yang ga ada di list ui
            var toBeDelete = _context.TX_Attachment.Where(o => 
               o.ProjectPhase == projectPhase &&
               o.ReferenceCode == referenceCode && 
               !listPathDontDelete.Contains(o.FilePath)
             );
            if (toBeDelete.Any())
            {
                _context.TX_Attachment.RemoveRange(toBeDelete);
                await _context.SaveChangesAsync();
            }
          
        }

        public async Task Delete(string referenceCode, string filePath)
        {
            var item = _context.TX_Attachment.FirstOrDefault(o=>o.ReferenceCode == referenceCode && o.FilePath == filePath);
            if (item != null)
            {
                _context.TX_Attachment.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FileAttachmentDto>> GetList(string referenceCode)
        {
            var ls = await _context.TX_Attachment
                .Where(o => o.ReferenceCode == referenceCode)
                .OrderBy(o=>o.CreatedDate)
                .ToListWithNoLockAsync();
            if (ls.Any())
            {
                var result = ls.Select(o => new FileAttachmentDto(o)).ToList();

                foreach (var item in result)
                {
                    item.FileUrl = FileHelperExtension.GetFileUrl(item.FilePath);
                }
                return result;
            }

            return new List<FileAttachmentDto>();
        }

        public async Task UpdateReferenceCode(FileAttachmentDto model)
        {
            var anyDraft = _context.TX_Attachment
                .AsNoTracking()
                .Any(o => o.ReferenceCode == "DRAFT" &&
                o.FilePath == model.FilePath);
            if (anyDraft)
            {
                // pake native sql command karena EF ga support rubah primary key
                var sqlCommand = "UPDATE dbo.TX_Attachment " +
                $"SET ReferenceCode = '{model.ReferenceCode}' " +
                "WHERE " +
                $"ReferenceCode = 'DRAFT' AND " +
                $"FilePath = '{model.FilePath}'";

                await _context.Database.ExecuteSqlCommandAsync(sqlCommand);
            }
        }
    }
}
