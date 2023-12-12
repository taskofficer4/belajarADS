using Dapper;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;
using Actris.Abstraction.Enum;
using static Dapper.SqlMapper;
using System.Data.Entity;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
    public abstract class BaseCrudRepository<TEntity,TDto,TGridModel,TCrudQuery> : BaseRepository  
        where TCrudQuery : BaseCrudQuery
        where TEntity : class
        where TDto :  BaseDtoAutoMapper<TEntity>

    {
        private TCrudQuery _crudQuery;
        
        public BaseCrudRepository(ActrisContext context, IConnectionProvider connection, TCrudQuery tCrudQuery) 
            : base(context, connection)
        {
            _crudQuery = tCrudQuery;
            
        }

        public virtual async Task Create(TDto dto)
        {
            var entity = dto.ToEntity();
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = System.Data.Entity.EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TDto> GetOne(string id) 
        {
            TEntity result = await _context.Set<TEntity>().FindAsync(id);
            TDto dto =  (TDto)Activator.CreateInstance(typeof(TDto), result);
            return dto;
        }

        public virtual async Task<TDto> GetOne(int id)
        {
            TEntity result = await _context.Set<TEntity>().FindAsync(id);
            TDto dto = (TDto)Activator.CreateInstance(typeof(TDto), result);
            return dto;
        }

        public virtual async Task Delete(int id)
        {
            var item = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(item);
          await   _context.SaveChangesAsync();

        }

        public virtual async Task Delete(string id)
        {
            var item = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(item);
            await _context.SaveChangesAsync();

        }

        public virtual async Task<LookupList> GetAdaptiveFilterList(string columnId, ColumnType columnType)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };
            using (var conn = OpenConnection())
            {

                var queryString = _crudQuery.AdaptiveFilterQuery.Replace("@columnName", columnId);               
                if (columnType == ColumnType.Date)
                {
                    var items =  await conn.QueryAsync<DateTime?>(queryString);

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item.HasValue ? item?.ToString("dd-MMM-yyyy") : "[Empty]",
                        Value = item.HasValue ? item?.ToString("yyyy-MM-dd") : ""
                    }).ToList();
                }
                else
                {
                    var items = await conn.QueryAsync<string>(queryString);
                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }

            }
            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();
            return result;
        }

     

        public virtual async Task<Paged<TGridModel>> GetPaged(GridParam param)
        {
            return await GetPaged<TGridModel>(
               _crudQuery.SelectPagedQuery,
               _crudQuery.CountQuery,

               param.ColumnDefinitions,
               param.FilterList,
               _crudQuery.SelectPagedQueryAdditionalWhere);
        }

        /// <summary>
        /// Default update : semua field akan terupdate
        /// Jika beda behaviour, lebih baik overide method ini
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual async Task Update(TDto dto)
        {
            TEntity model = dto.ToEntity();
            _context.Set<TEntity>().Attach(model);
            var entry = _context.Entry(model);
            
            entry.State = System.Data.Entity.EntityState.Modified;

            //by default this column not modified
            var properties = model.GetType().GetProperties();

            if (properties.Any(o=>o.Name == "CreatedAt"))
            {
                entry.Property("CreatedAt").IsModified = false;
            }

            if (properties.Any(o => o.Name == "CreatedBy"))
            {
                entry.Property("CreatedBy").IsModified = false;
            }

            if (properties.Any(o => o.Name == "DataStatus"))
            {
                entry.Property("DataStatus").IsModified = false;
            }

            if (properties.Any(o => o.Name == "CreatedDate"))
            {
                entry.Property("CreatedDate").IsModified = false;
            }
            await _context.SaveChangesAsync();
        }


        public virtual async Task<string> GetLookupText(int id)
        {
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(_crudQuery.LookupTextQuery,id);
                var lookupText = await connection.QueryFirstOrDefaultAsync<string>(querySQL);
                if (lookupText == null)
                {
                    lookupText = "";
                }
                return lookupText;
            }
        }
    }
}
