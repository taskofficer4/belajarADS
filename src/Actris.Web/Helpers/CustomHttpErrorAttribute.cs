using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace Actris.Web.Helpers
{
    public class CustomHttpErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            return;
        }
    }
}