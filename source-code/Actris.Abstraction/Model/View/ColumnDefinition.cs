using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.View
{
    public class ColumnDefinition
    {

        public ColumnDefinition(string name, string id, ColumnType type = ColumnType.String)
        {
            Name = name;
            Id = id;
            Type = type;
        }

        public ColumnDefinition(string name, string id, ColumnType type, string joinColumnKey)
        {
            Name = name;
            Id = id;
            Type = type;
            _joinColumnKey = joinColumnKey;
        }

        public string Name { get; set; }
        private string _joinColumnKey { get; set; }
        public string Id { get; set; }
        public ColumnType Type { get; set; }

        /// <summary>
        /// Dipake kalo ada case where ke join column contoh
        /// where d.DepartementName
        /// </summary>
        /// <returns></returns>
        public string GetColumnName()
        {
            if (!string.IsNullOrEmpty(_joinColumnKey))
            {
                return _joinColumnKey;
            }
            else
            {
                return Id;
            }
        }
    }

    public enum ColumnType
    {
        String = 0,
        Number,
        Date,
        DateTime,
        DateYear,
        Html,
        Id,
        Link,
        Switch,
        LinkList,
        Image,
        RichText,
        Percentage,
        Hidden
    }
}
