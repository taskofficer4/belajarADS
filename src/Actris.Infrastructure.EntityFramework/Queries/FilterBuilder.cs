using System;
using Actris.Abstraction.Enum;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Actris.Abstraction.Model.View;

namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class FilterBuilder
    {
        public static string BuildFilter(List<ColumnDefinition> columns, List<List<FilterItem>> param, string additionalWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" where (");

            var filterCount = 0;
            for (int i = 0; i < param.Count; i++)
            {
              

                List<FilterItem> paramItem = param[i];
                sb.Append(i == 0 ? "" : " AND ");
                for (int j = 0; j < paramItem.Count; j++)
                {
                    FilterItem filter = paramItem[j];
                    var column = columns.FirstOrDefault(o => o.Id == filter.Name);
                    var columnName = filter.Name;

                    if (column != null)
                    {
                        columnName = column.GetColumnName();
                    }
                  

                    if (columnName == "AnyField")
                    {
                        var queryAnyField = BuildAnyFieldContain(columns, filter.Value);
                        sb.Append(queryAnyField);
                        filterCount++;
                        break;
                    }

                    sb.Append(j == 0 ? "(" : " OR ");

                    switch (filter.FilterType)
                    {
                        case FilterType.Equal:
                            if (column?.Type == ColumnType.Date || column?.Type == ColumnType.DateTime)
                            {
                                var startDate = DateTime.ParseExact(filter.Value, "yyyy-MM-dd", new DateTimeFormatInfo());
                                var endDate = startDate.AddDays(1);
                                sb.Append($"{columnName} BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'");
                            }
                            else
                            {
                                sb.Append($"{columnName} = '{filter.Value}'");
                            }
                            
                            break;
                        case FilterType.NotEqual:
                            sb.Append($"{columnName} != '{filter.Value}'");
                            break;
                        case FilterType.BeginWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.NotBeginWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('{filter.Value}%')");
                            break;
                        case FilterType.Contains:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.NotContains:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}%')");
                            break;
                        case FilterType.EndWith:
                            sb.Append($"LOWER({columnName}) LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.NotEndWith:
                            sb.Append($"LOWER({columnName}) NOT LIKE LOWER('%{filter.Value}')");
                            break;
                        case FilterType.LessThan:
                            sb.Append($"{columnName} < '{filter.Value}'");
                            break;
                        case FilterType.GreaterThan:
                            sb.Append($"{columnName} > '{filter.Value}'");
                            break;
                        case FilterType.LessThanOrEqual:
                            sb.Append($"{columnName} <= '{filter.Value}'");
                            break;
                        case FilterType.GreaterThanOrEqual:
                            sb.Append($"{columnName} >= '{filter.Value}'");
                            break;
                        case FilterType.NotEmpty:
                            sb.Append($"{columnName} <> ''");
                            break;
                        case FilterType.Empty:
                            sb.Append($"ISNULL({columnName},'') = ''");
                            break;
                        case FilterType.Includes:
                            if (string.IsNullOrWhiteSpace(filter.Value))
                            {
                                sb.Append($"{columnName} = '{filter.Value}'");
                                sb.Append($" OR {columnName} IS NULL");
                            }
                            else
                            {
                                if (column?.Type == ColumnType.Date || column?.Type == ColumnType.DateTime)
                                {
                                    var startDate = DateTime.ParseExact(filter.Value,"yyyy-MM-dd",new DateTimeFormatInfo());
                                    var endDate = startDate.AddDays(1);
                                    sb.Append($"{columnName} BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'");
                                }
                                else
                                {
                                    sb.Append($"{columnName} = '{filter.Value}'");
                                }
                               
                            }
                            break;
                        default:
                        case FilterType.Undefined:
                            throw new System.Exception($"Cannot build {filter.FilterType}, of {filter.Name} : {filter.Value}");
                    }
                    filterCount++;
                    if (j == paramItem.Count - 1)
                    {
                        sb.Append(')');
                    }
                }
            }

            if (!string.IsNullOrEmpty(additionalWhere))
            {
                if (filterCount > 0)
                {
                    sb.Append($" AND ");
                }
                sb.Append($" {additionalWhere} ");
                filterCount++;
            }

            if (filterCount == 0)
            {
                return "";
            }

            sb.Append(')');
            return sb.ToString();
        }

        private static string BuildAnyFieldContain(List<ColumnDefinition> columns, string keywords)
        {
            var listFilter = new List<string>();
            var listKeywords = keywords.Trim().Split(' ');

            foreach (var column in columns)
            {
                // per kalimat
                listFilter.Add($"LOWER({column.GetColumnName()}) LIKE LOWER('%{keywords}%')\n");

                // per kata
                //foreach (var keyword in listKeywords)
                //{
                //    listFilter.Add($"LOWER({column.GetColumnName()}) LIKE LOWER('%{keyword}%')\n");
                //}
            }
            return  $"({string.Join(" OR ", listFilter)})";
        }
    }
}
