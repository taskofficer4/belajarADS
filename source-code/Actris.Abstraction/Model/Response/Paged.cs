using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.Response
{
    public class Paged<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}
