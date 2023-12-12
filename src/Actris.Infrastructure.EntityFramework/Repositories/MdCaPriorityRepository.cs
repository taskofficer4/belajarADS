using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
   public class MdCaPriorityRepository : BaseCrudRepository<MD_CorrectiveActionPriority, MdCaPriorityDto, MdCaPriorityDto, MdCorrectiveActionPriorityQuery>, IMdCaPriorityRepository
    {
        public MdCaPriorityRepository(ActrisContext context, IConnectionProvider connection, MdCorrectiveActionPriorityQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }

        public override async Task<MdCaPriorityDto> GetOne(string id)
        {
            var result = await base.GetOne(id);
            result.Id = result.Priority;
            return result;
        }

        public override async Task Update(MdCaPriorityDto dto)
        {
            var sqlCommand = "UPDATE actris.MD_CorrectiveActionPriority " +
                "SET Priority = @priority, PriorityBahasa = @priorityBahasa, PriorityDuration = @priorityDuration " +
                "WHERE Priority = @oldId";

            var oldId = new SqlParameter("@oldId", dto.Id);
            var priority = new SqlParameter("@priority", dto.Priority);
            var priorityBahasa = new SqlParameter("@priorityBahasa", dto.PriorityBahasa);
            var priorityDuration = new SqlParameter("@priorityDuration", dto.PriorityDuration);

            await _context.Database.ExecuteSqlCommandAsync(sqlCommand, new[] { oldId, priority, priorityBahasa, priorityDuration });
        }

        public override async Task Delete(string id)
        {
            var item = _context.MD_CorrectiveActionPriority.FirstOrDefault(o => o.Priority == id);
            if (item == null)
            {
                throw new Exception($"Data with id '{id}' not found or already deleted");
            }

            item.DataStatus = "deleted";
            await _context.SaveChangesAsync();
        }

        public bool IsPriorityExist(string priority)
        {
            return _context.MD_CorrectiveActionPriority.Any(o=> o.Priority == priority);
        }
    }
}
