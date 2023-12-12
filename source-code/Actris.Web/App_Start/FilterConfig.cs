using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Actris.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            if (IsSSOEnabled())
            {
                // By Default all controller required user login (when using sso login)
                filters.Add(new AuthorizeAttribute());
            }



        }

        private static bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == System.Web.Configuration.AuthenticationMode.None;
        }
    }
}
