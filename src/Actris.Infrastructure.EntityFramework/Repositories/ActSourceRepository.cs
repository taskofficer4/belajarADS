using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using Actris.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
    public class ActSourceRepository : BaseCrudRepository<MD_ActionTrackingSource, MdActSourceDto, MdActSourceDto, ActSourceQuery>, IMdActSourceRepository
    {
        private static object createProcess = new object();
        public ActSourceRepository(ActrisContext context, IConnectionProvider connection, ActSourceQuery tCrudQuery) : base(context, connection, tCrudQuery)
        {
        }

        public override async Task<MdActSourceDto> GetOne(string id)
        {
            var dto = new MdActSourceDto();
            dto.FromKeyString(id);

            var result = await _context
                .MD_ActionTrackingSource
                .FirstOrDefaultAsync(o =>
                o.Source == dto.Source && 
                o.DirectorateID == dto.DirectorateID &&
                o.DivisionID == dto.DivisionID && 
                o.SubDivisionID == dto.SubDivisionID &&
                o.DepartmentID == dto.DepartmentID);

            return new MdActSourceDto(result);
        }


        public override async Task Delete(string id)
        {
            var dto = new MdActSourceDto();
            dto.FromKeyString(id);
            var item = await _context.MD_ActionTrackingSource
              .FirstOrDefaultAsync(o =>
              o.Source == dto.Source &&
              o.DirectorateID == dto.DirectorateID &&
              o.DivisionID == dto.DivisionID &&
              o.SubDivisionID == dto.SubDivisionID &&
              o.DepartmentID == dto.DepartmentID);

            if (item == null)
            {
                throw new Exception($"Data with id '{id}' not found or already deleted");
            }
            item.DataStatus = "deleted";
            await _context.SaveChangesAsync();
        }

        public override async Task Update(MdActSourceDto dto)
        {
            var oldDto = new MdActSourceDto();
            oldDto.FromKeyString(dto.CompositeKey);

            // pake native sql command karena EF ga support rubah primary key
            var sqlCommand = "UPDATE actris.MD_ActionTrackingSource " +
                $"SET Source = '{dto.Source}', " +
                $"SourceBahasa = '{dto.SourceBahasa}', " +
                $"DirectorateID = '{dto.DirectorateID}', " +
                $"DirectorateDesc = '{dto.DirectorateDesc}', " +
                $"DivisionID = '{dto.DivisionID}', " +
                $"DivisionDesc = '{dto.DivisionDesc}', " +
                $"SubDivisionID = '{dto.SubDivisionID}', " +
                $"SubDivisionDesc = '{dto.SubDivisionDesc}', " +
                $"DepartmentID = '{dto.DepartmentID}', " +
                $"DepartmentDesc = '{dto.DepartmentDesc}' " +

                "WHERE " +
                $"Source = '{oldDto.Source}' AND " +
                $"DirectorateID = '{oldDto.DirectorateID}' AND " +
                $"DivisionID = '{oldDto.DivisionID}' AND " +
                $"SubDivisionID = '{oldDto.SubDivisionID}' AND " +
                $"DepartmentID = '{oldDto.DepartmentID}'";

            await _context.Database.ExecuteSqlCommandAsync(sqlCommand);
        }
    }
}
