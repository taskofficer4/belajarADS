

using System;
using System.Linq;

using System.Web.Mvc;
using Actris.Abstraction.Extensions;

namespace Actris.Web.Helpers
{
    /// <summary>
    /// Untuk validasi apakah user yang sedang login memiliki role untuk askses suatu controller atau tidak
    /// return nya bukan ke login page melainkan ke forbidden
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoles : AuthorizeAttribute
    {
        private string[] _requiredRoles { get; set; }

        public AuthorizeRoles(params object[] requiredRole)
        {
            this._requiredRoles = requiredRole.Select(p => (string)p).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;

            foreach (var role in this._requiredRoles)
                if (CurrentUser.HaveRole(role))
                {
                    authorized = true;
                    break;
                }

            if (!authorized)
            {
                context.Result = new ViewResult
                {
                    ViewName = "Forbidden"
                };
                return;
            }
        }
    }
}