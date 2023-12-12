using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class MdCaOverdueImpactRepository : BaseCrudRepository<MD_CorrectiveActionOverdueImpact, MdCaOverdueImpactDto, MdCaOverdueImpactDto, MdCaOverdueImpactQuery>,
     IMdCaOverdueImpactRepository
   {
      public MdCaOverdueImpactRepository(ActrisContext context, IConnectionProvider connection, MdCaOverdueImpactQuery tCrudQuery) : base(context, connection, tCrudQuery)
      {
      }

      public override async Task<MdCaOverdueImpactDto> GetOne(string id)
      {
         var result = await base.GetOne(id);
         result.Id = result.OverdueImpact;
         return result;
      }

      public override async Task Update(MdCaOverdueImpactDto dto)
      {
         var sqlCommand = $"UPDATE actris.MD_CorrectiveActionOverdueImpact " +
             $"SET OverdueImpact = '{dto.OverdueImpact}', " +
             $"    ImpactBahasa = '{dto.ImpactBahasa}' " +
             $"WHERE OverdueImpact = '{dto.Id}'";
         await _context.Database.ExecuteSqlCommandAsync(sqlCommand);
      }

      public override async Task Delete(string id)
      {
         var item = _context.MD_CorrectiveActionOverdueImpact.FirstOrDefault(o => o.OverdueImpact == id);
         if (item == null)
         {
            throw new Exception($"Data with id '{id}' not found or already deleted");
         }
         item.DataStatus = "deleted";
         await _context.SaveChangesAsync();
      }

      public bool IsExist(string overdueImpact)
      {
         return _context.MD_CorrectiveActionOverdueImpact.Any(o => o.OverdueImpact == overdueImpact);
      }
   }
}
