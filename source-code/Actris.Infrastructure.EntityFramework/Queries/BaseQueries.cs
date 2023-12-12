using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Infrastructure.EntityFramework.Queries
{
    public class BaseQueries
    {
        public const string PagedQuery = "@select @filter ORDER BY @sort offset @offset rows fetch next @limit rows only";
        public const string CountQuery = "@select @filter";
    }
}
