using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.EntityFramework.Queries;

namespace Actris.Infrastructure.EntityFramework.Repositories
{
	public class BaseRepository
	{
		protected readonly ActrisContext _context;
		private readonly IConnectionProvider _connection;

		public BaseRepository(ActrisContext context, IConnectionProvider connection)
		{
			_context = context;
			_connection = connection;
		}

		protected async Task<Paged<TPaged>> GetPaged<TPaged>(string selectQuery, string countQuery, List<ColumnDefinition> columnDefinitions, FilterList filterList, string additionalWhere = null)
		{
			using (var connection = OpenConnection())
			{
				if (additionalWhere != null)
				{
					if (filterList.CurrentUser != null)
					{
						additionalWhere = additionalWhere.Replace("@CurrentUser", filterList.CurrentUser);
					}
				}

				var filterSql = FilterBuilder.BuildFilter(columnDefinitions, filterList.FilterItems, additionalWhere);

				var metaData = InsertMetaData(selectQuery, filterSql, filterList.Page, filterList.Size, filterList.OrderBy);

				var querySQL = BuildQuery(BaseQueries.PagedQuery, metaData);
				var items = await connection.QueryAsync<TPaged>(querySQL);

				var count = 0;
				if (!string.IsNullOrEmpty(countQuery))
				{
					metaData["select"] = countQuery;
					querySQL = BuildQuery(BaseQueries.CountQuery, metaData);
					count = await connection.QueryFirstAsync<int>(querySQL);
				}


				return new Paged<TPaged>()
				{
					TotalItems = count,
					Items = items
				};
			}
		}

		protected string BuildQuery(string sql, Dictionary<string, object> values)
		{
			var query = sql;
			foreach (var item in values)
			{
				query = query.Replace($"@{item.Key}", item.Value?.ToString());
			}
			return query.ToString();
		}

		protected virtual Dictionary<string, object> InsertMetaData(string selectQuery, string filterSql, int page, int size, string sort)
		{
			var metaData = new Dictionary<string, object>
			{
				{ "select", selectQuery },
				{ "sort", sort },
				{ "offset", ((page - 1) * size) },
				{ "limit", size },
				{ "filter", filterSql }
			};

			return metaData;
		}

		protected async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(string query)
		{
			using (var connection = OpenConnection())
			{
				return await connection.QueryAsync<TResult>(query);
			}
		}

		protected IDbConnection OpenConnection()
		{
			SqlConnection conn;
			var connectionString = _connection.GetConnectionString();
			conn = new SqlConnection(connectionString);
			conn.Open();
			return conn;
		}

		protected async Task<LookupList> BaseGetAdaptiveFilter(string query, string columnId, ColumnType columnType)
		{
			var result = new LookupList
			{
				ColumnId = columnId
			};

			using (var connection = OpenConnection())
			{
				if (columnType == ColumnType.Date ||
					columnType == ColumnType.DateTime)
				{

					var items = await connection.QueryAsync<DateTime?>(query);

					result.Items = items.Select(item => new LookupItem
					{
						Text = item.HasValue ? item?.ToString("dd-MMM-yyyy") : "[Empty]",
						Value = item.HasValue ? item?.ToString("yyyy-MM-dd") : ""
					}).ToList();
				}
				else if (columnType == ColumnType.Percentage)
				{

					var items = await connection.QueryAsync<decimal?>(query);

					result.Items = items.Select(item => new LookupItem
					{
						Text = item.HasValue ? item?.ToString("P") : "[Empty]",
						Value = item.HasValue ? item?.ToString() : ""
					}).ToList();
				}
				else
				{
					var items = await connection.QueryAsync<string>(query);
					result.Items = items.Select(item => new LookupItem
					{
						Text = item,
						Value = string.IsNullOrEmpty(item) ? null : item
					}).GroupBy(o => o.Value).Select(o => o.First()).ToList();
				}

			}
			result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();
			return result;
		}
	}
}
