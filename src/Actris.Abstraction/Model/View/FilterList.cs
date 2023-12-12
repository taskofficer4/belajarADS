using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.View
{
    public class FilterList
    {
        public string GridId { get; set; }
        public FilterList()
        {
            FilterItems = new List<List<FilterItem>>();
        }

        public string CurrentUser { get; set; }
        public bool IsForLookup { get; set; }

        public bool ReadOnly { get; set; }

        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }

        /// <summary>
        /// Filters to be applied to query
        /// inner list will be combined with 'or' outter list will be combined with 'and'
        /// </summary>
        public List<List<FilterItem>> FilterItems { get; set; }
        public string OrderBy { get; set; }

        public bool AnyFilter(string columnId = null)
        {
            // per column / field
            if (columnId == null)
            {
                return FilterItems.Any(o => o.Any());
            }
            // per column / field
            return  FilterItems.Any(o => o.Any(a => a.Name == columnId));

        }

        public string AdditionalParam { get; set; }

        public override string ToString()
        {
            return $"Page : {Page} | Size : {Size} | OrderBy : {OrderBy}";
        }


        public void AddIncludesFilter(string name, List<string> values)
        {
            FilterItems = new List<List<FilterItem>>();
            var filterIncludes = new List<FilterItem>();
            foreach (var val in values)
            {
                filterIncludes.Add(new FilterItem(name,val,FilterType.Includes));
            }
            FilterItems.Add(filterIncludes);
        }
    }

    public class FilterItem
    {
        public FilterItem()
        {
        }

        public FilterItem(string name, string value, FilterType type)
        {
            Name = name;
            Value = value;
            FilterType = type;
        }
        public string Name { get; set; }
        public string Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FilterType FilterType { get; set; }
    }


    public enum FilterType
    {
        Undefined = 0,
        Equal,
        NotEqual,
        BeginWith,
        NotBeginWith,
        Contains,
        NotContains,
        EndWith,
        NotEndWith,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        NotEmpty,
        Empty,
        Includes
    }
}
