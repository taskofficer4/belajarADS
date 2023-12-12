using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Actris.Abstraction.Extensions
{
    public static class CurrentUser
    {
        public static bool HaveRole(string[] roles)
        {
            foreach (var role in roles)
            {
                var isHaveRole = HttpContext.Current.User.HaveRole(role);
                if (!isHaveRole)
                {
                    return false;
                }
            }
            return true;
           
        }

        public static bool HaveRole(string roles)
        {
            return HttpContext.Current.User.HaveRole(roles);
        }

        public static string GetPreferredUsername(this IPrincipal user)
        {
            Claim claim = ((ClaimsIdentity)user.Identity).FindFirst("preferred_username");
            string username =  claim?.Value;
            return username ?? user.Identity.Name;
        }

        public static string GetPreferredUsername()
        {
            return HttpContext.Current.User.GetPreferredUsername();
        }


        public static bool HaveRole(this IPrincipal user, string roles)
        {
            var username = user.GetPreferredUsername();
            return Roles.IsUserInRole(username, roles);
        }

        public  static string GetIpAddress()
        {
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}