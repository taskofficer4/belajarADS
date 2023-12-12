using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
    public class CaOverdueImpactRepository : BaseCrudRepository<MD_CorrectiveActionOverdueImpact, CaOverdueImpactDto, CaOverdueImpactDto, CaOverdueImpactQuery>, ICaOverdueImpactRepository
    {
        public CaOverdueImpactRepository(ActrisContext context, IConnectionProvider connection, CaOverdueImpactQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }
        private static object createProcess = new object();
        public override async Task Create(CaOverdueImpactDto dto)
        {
            // Agar tidak ada duplikasi id yg sama, di locak proses create tidak boleh paralel
            lock (createProcess)
            {
                dto.CorrectiveActionOverdueImpactID = GenerateNewId();
                Task.Run(async () => await base.Create(dto));
            }
        }


        private string GenerateNewId()
        {

            var prefixId = "CAOV";
            var lastId = _context.MD_CorrectiveActionOverdueImpact
                .Where(o => o.CorrectiveActionOverdueImpactID.StartsWith(prefixId))
                .OrderByDescending(o => o.CorrectiveActionOverdueImpactID)
                .FirstOrDefault()?.CorrectiveActionOverdueImpactID;

            var lastNumber = 1;
            if (!string.IsNullOrEmpty(lastId))
            {
                var lastNumberStr = lastId.Replace(prefixId, "");
                lastNumber = int.Parse(lastNumberStr);
                lastNumber++;
            }

            return $"{prefixId}{lastNumber:D10}";

        }


      

        public override async Task Delete(string id)
        {
            var item = _context.MD_CorrectiveActionOverdueImpact.FirstOrDefault(o => o.CorrectiveActionOverdueImpactID == id);
            if (item == null)
            {
                throw new Exception($"Data with id '{id}' not found or already deleted");
            }

            item.DataStatus = "deleted";
            await _context.SaveChangesAsync();
        }
    }
}
