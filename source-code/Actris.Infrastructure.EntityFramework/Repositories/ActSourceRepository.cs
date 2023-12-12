using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
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
    public class ActSourceRepository : BaseCrudRepository<MD_ActionTrackingSource, ActSourceDto, ActSourceDto, ActSourceQuery>, IActSourceRepository
    {
        private static object createProcess = new object();
        public ActSourceRepository(ActrisContext context, IConnectionProvider connection, ActSourceQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }

        public override async Task Create(ActSourceDto dto)
        {
            // Agar tidak ada duplikasi id yg sama, di locak proses create tidak boleh paralel
            lock (createProcess)
            {
                dto.ActionTrackingSourceID = GenerateNewId();
                Task.Run(async () => await base.Create(dto));
            }
        }

        
        private string GenerateNewId()
        {

            var prefixId = "ACTS";
            var lastId = _context.MD_ActionTrackingSource
                .Where(o => o.ActionTrackingSourceID.StartsWith(prefixId))
                .OrderByDescending(o => o.ActionTrackingSourceID)
                .FirstOrDefault()?.ActionTrackingSourceID;

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
            var item = _context.MD_ActionTrackingSource.FirstOrDefault(o => o.ActionTrackingSourceID == id);
            if (item == null)
            {
                throw new Exception($"Data with id '{id}' not found or already deleted");
            }
            item.DataStatus = "deleted";
            await _context.SaveChangesAsync();
        }
    }
}
