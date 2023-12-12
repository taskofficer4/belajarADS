using System.Web;
using System.Web.Mvc;

namespace Actris.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // By Default all controller required user login
            filters.Add(new AuthorizeAttribute());
        }
    }
}
