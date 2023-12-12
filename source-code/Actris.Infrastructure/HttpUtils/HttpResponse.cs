using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Infrastructure.HttpUtils
{
    public class HttpResponse<T> where T : class
    {
        public bool Status { get; set; }
        public T Object { get; set; }
        public string Message { get; set; }
    }
}
