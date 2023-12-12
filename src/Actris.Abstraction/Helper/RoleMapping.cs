using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actris.Abstraction.Enum;
using Actris.Abstraction.Model.Response;

namespace Actris.Abstraction.Helper
{
    /// <summary>
    /// fungsi ini untuk map role dari SSO ke application role dengan lebih spesifik
    /// - ROle ini yang akan digunakan untuk hide/show button
    /// - Role ini yang akan digunakan untk guard di controller action, untuk menjaga yang nembak via url meskipun menu tidak ada
    /// </summary>
    public static class RoleMapping
    {

        public static void Process(List<UserRoleDto> userRoles)
        {
            if (userRoles == null)
            {
                return;
            }
            List<string> appRoles = new List<string>();
            if (userRoles.Any(o => o.Value == "ADMINISTRATOR"))
            {

                appRoles.Add(AppRole.HOME);
             

            }

            if (userRoles.Any(o => o.Value == "CREATOR_BIN" || o.Value == "CREATOR_BAN"))
            {
                appRoles.Add(AppRole.HOME);
               

            }

            if (userRoles.Any(o => o.Value == "CREATOR_BV"))
            {
                appRoles.Add(AppRole.HOME);
             

            }

            if (userRoles.Any(o => o.Value == "CREATOR_BV_PARTNERSHIP"))
            {
                appRoles.Add(AppRole.HOME);
               
            }
            else if (userRoles.Any(o => o.Value == "VIEWER_UBD"))
            {
                appRoles.Add(AppRole.HOME);
               
            }
            appRoles = appRoles.Distinct().ToList();
            var userRole = appRoles.Select(o => new UserRoleDto(o.ToString()));
            userRoles.AddRange(userRole);
        }
    }
}
