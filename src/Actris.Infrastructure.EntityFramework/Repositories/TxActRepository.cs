using Actris.Abstraction.Enum;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class TxActRepository :
       BaseCrudRepository<TX_ActionTracking,
           TxActDto,
           TxActDto,
           TxActionTrackingQuery>,
       ITxActRepository
   {
      private static object _lock = new object();
      private static object _lockSubmit = new object();
      public TxActRepository(ActrisContext context, IConnectionProvider connection, TxActionTrackingQuery tCrudQuery) : base(context, connection, tCrudQuery)
      {
      }

      public override async Task Create(TxActDto dto)
      {
         lock (_lock)
         {
            // SAVE ACT
            dto.ActionTrackingID = GenerateNewId();
            var actEntity = dto.ToEntity();
            actEntity.ObservationID = "dummy";

            if (dto.IsSubmit)
            {
               actEntity.Status = ActStatusEnum.Open;
            }
            else
            {
               actEntity.Status = ActStatusEnum.Draft;
            }
            _context.TX_ActionTracking.Add(actEntity);
            _context.SaveChanges();
         }
      }

      public List<TxCaDto> GetCaList(string actRef)
      {
         var ls = _context.TX_CorrectiveAction.Where(o => o.ActionTrackingID == actRef && o.DataStatus != "deleted").OrderBy(o => o.CreatedDate).ToList();
         return ls.Select(o => new TxCaDto(o)).ToList();
      }

      public override async Task Update(TxActDto dto)
      {
         if (dto.IsSubmit)
         {
            dto.Status = ActStatusEnum.Open;
         }
         var actEntity = dto.ToEntity();

         actEntity.ObservationID = "dummy";
         var entry = _context.Entry(actEntity);
         entry.State = System.Data.Entity.EntityState.Modified;

         if (!dto.IsSubmit)
         {
            entry.Property(o => o.Status).IsModified = false;
         }
         entry.Property(o => o.TypeShuRegion).IsModified = false;
         entry.Property(o => o.CreatedBy).IsModified = false;
         entry.Property(o => o.CreatedDate).IsModified = false;
         entry.Property(o => o.ObservationID).IsModified = false;
         entry.Property(o => o.DataStatus).IsModified = false;
         await _context.SaveChangesAsync();
         _context.SaveChanges();
      }

      private string GenerateNewId()
      {
         var now = DateHelper.WibNow;
         var prefixId = $"ACT-{now:yyyyMMdd}-";
         var lastId = _context.TX_ActionTracking
             .Where(o => o.ActionTrackingID.StartsWith(prefixId))
             .OrderByDescending(o => o.ActionTrackingID)
             .FirstOrDefault()?.ActionTrackingID;

         var lastNumber = 1;
         if (!string.IsNullOrEmpty(lastId))
         {
            var lastNumberStr = lastId.Replace(prefixId, "");
            lastNumber = int.Parse(lastNumberStr);
            lastNumber++;
         }

         return $"{prefixId}{lastNumber:D4}";

      }

        public async Task SaveWorkflowResponse(TxActDto dto)
        {
            var currentStatus = dto.Status;
            var existing = _context.TX_ActionTracking.FirstOrDefault(o => o.ActionTrackingID == dto.ActionTrackingID);
            if (existing != null)
            {
                existing.Status = currentStatus;
                existing.ModifiedDate = DateHelper.WibNow;
                existing.ModifiedBy = dto.ModifiedBy;
                await _context.SaveChangesAsync();
            }

        }

    }
}
