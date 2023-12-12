using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class LookupRepository : BaseRepository, ILookupRepository
   {

      public LookupRepository(ActrisContext context, IConnectionProvider connection)
       : base(context, connection)
      {
      }

      public async Task<LookupList> GetActSourceList()
      {
         var query = _context.MD_ActionTrackingSource.Where(o => o.DataStatus != "deleted").OrderBy(o => o.Source);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.ToKeyString(), o.Source)).ToLookupList();
         return result;
      }

      public async Task<LookupList> GetCaPriorityList()
      {
         var query = _context.MD_CorrectiveActionPriority.Where(o => o.DataStatus != "deleted").OrderBy(o => o.Priority);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.Priority, o.Priority)).ToLookupList();
         return result;
      }

      public async Task<LookupList> GetDirectorateList(string hierLvl2)
      {
         var query = _context.VW_PSAHierarchy
            .Where(o => o.EntityType == "WK" && o.HierLvl2 == hierLvl2);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.DirectorateID, o.DirectorateDesc)).ToLookupList();
         return result;
      }

      public async Task<LookupList> GetDivisiZonaList(string hierLvl2, string directorateID)
      {
         var query = _context.VW_PSAHierarchy
            .Where(o => o.EntityType == "WK" && o.HierLvl2 == hierLvl2 && o.DirectorateID == directorateID);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.DivisionID, o.DivisionDesc)).ToLookupList();
         return result;
      }


      public async Task<LookupList> GetCompanyList(string hierLvl2, string directorateID, string divisionID)
      {
         var query = _context.VW_PSAHierarchy
            .Where(o => o.EntityType == "WK" && o.HierLvl2 == hierLvl2 && o.DirectorateID == directorateID && o.DivisionID == divisionID);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.CompanyCode, o.CompanyName)).ToLookupList();
         return result;
      }

      public async Task<LookupList> GetWilayahKerjaList(string hierLvl2, string directorateID, string divisionID, string companyCode)
      {
         var query = _context.VW_PSAHierarchy
            .Where(o => o.EntityType == "WK" && o.HierLvl2 == hierLvl2 && o.DirectorateID == directorateID && o.DivisionID == divisionID && o.CompanyCode == companyCode);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.CompanyCode, o.CompanyName)).ToLookupList();
         return result;
      }

        public async Task<LookupList> GetCaOverdueImpactList()
        {
         var query = _context.MD_CorrectiveActionOverdueImpact.Where(o => o.DataStatus != "deleted").OrderBy(o => o.OverdueImpact);
         var ls = await query.ToListWithNoLockAsync();
         var result = ls.Select(o => new LookupItem(o.OverdueImpact, o.OverdueImpact)).ToLookupList();
         return result;
      }
    }
}

