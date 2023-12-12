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
    public class ImpactAnalysisRepository : BaseCrudRepository<MD_CorrectiveActionImpactAnalysis, ImpactAnalysisDto, ImpactAnalysisDto, ImpactAnalysisQuery>, IImpactAnalysisRepository
    {
        public ImpactAnalysisRepository(ActrisContext context, IConnectionProvider connection, ImpactAnalysisQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }

        private static object createProcess = new object();
        public override async Task Create(ImpactAnalysisDto dto)
        {
            // Agar tidak ada duplikasi id yg sama, di locak proses create tidak boleh paralel
            lock (createProcess)
            {
                dto.CorrectiveActionImpactAnalysisID = GenerateNewId();
                Task.Run(async () => await base.Create(dto));
            }
        }


        private string GenerateNewId()
        {

            var prefixId = "ACPR";
            var lastId = _context.MD_CorrectiveActionImpactAnalysis
                .Where(o => o.CorrectiveActionImpactAnalysisID.StartsWith(prefixId))
                .OrderByDescending(o => o.CorrectiveActionImpactAnalysisID)
                .FirstOrDefault()?.CorrectiveActionImpactAnalysisID;

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
            var item = _context.MD_CorrectiveActionImpactAnalysis.FirstOrDefault(o => o.CorrectiveActionImpactAnalysisID == id);
            if (item == null)
            {
                throw new Exception($"Data with id '{id}' not found or already deleted");
            }
            item.DataStatus = "deleted";
            await _context.SaveChangesAsync();
        }
    }
}
