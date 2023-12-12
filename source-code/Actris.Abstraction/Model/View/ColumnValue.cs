using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.View
{

    public class GridRow
    {
        public int No { get; set; }
        public string Id { get; set; }
        public List<GridColumn> Columns { get; set; }
    }

    public class GridColumn
    {
        public string FieldId { get; set; }
        public ColumnType Type { get; set; }
        public string Value { get; set; }

        public string Text { get; set; }
    }


}
