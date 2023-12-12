using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using System.Collections.Generic;
using System.Configuration;
using Antlr.Runtime.Misc;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Services;
using System.Web.Configuration;
using System.Security.Claims;
using System.Security.Principal;
using Actris.Abstraction.Extensions;

namespace Actris.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        private bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == System.Web.Configuration.AuthenticationMode.None;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (IsSSOEnabled())
            {
                return RedirectToAction("Index", "Home");
            }

            return View("LoginForm");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> LoginForm(string userName)
        {
            if (IsSSOEnabled())
                return new JsonResult
                {
                    Data = BaseJsonResult.Error("SSO IS ENABLE"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };


            var user = await _userService.GetUserInfo(userName);
            if (user == null)
                return new JsonResult
                {
                    Data = BaseJsonResult.Error($"User '{userName}' Not Found"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };


            HttpContext.User = new GenericPrincipal(new GenericIdentity(userName), new string[]
            {
                "preferred_username",
            });
            FormsAuthentication.SetAuthCookie(userName, true);
            return new JsonResult
            {
                Data = BaseJsonResult.Ok(null),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult LogOff()
        {

            if (Request.IsAuthenticated)
            {
                CacheService.ClearAllCache();
                FormsAuthentication.SignOut();
                var authTypes = HttpContext.GetOwinContext().Authentication.GetAuthenticationTypes();
                HttpContext.GetOwinContext().Authentication.SignOut(authTypes.Select(t => t.AuthenticationType).ToArray());
            }
            return Redirect("/");
        }

        public async Task<ActionResult> Index()
        {
            return View(await _userService.GetCurrentUserInfo());
        }

        [Authorize]
        public PartialViewResult UserHeader()
        {

            var user = Task.Run(async () => await _userService.GetCurrentUserInfo()).Result;

            // handle if user null from MADAM API
            // just show SSO Username
            if (user == null)
            {
                user = new User
                {
                    Name = CurrentUser.GetPreferredUsername()
                };
            }
            return PartialView("Layout/_Header", user);
        }

        [Authorize]
        public PartialViewResult UserMenu()
        {
            var user = Task.Run(async () => await _userService.GetCurrentUserInfo()).Result;

            // remove ini kalo menu dari madam udah siap
            user.UserMenu = dummyMenus();
            if (user != null)
            {
                replaceRootUrl(user.UserMenu);
            }
            return PartialView("Layout/_SidebarUserMenu", user);
        }


        private List<UserMenu> dummyMenus()
        {
            var menu = new List<UserMenu>
            {
                new UserMenu{Caption= "Home",  Link= "/Home", Icon="fa-solid fa-house"},
                new UserMenu
                {
                    Caption = "Master Data",
                    Icon = "fa-regular fa-folder",
                    Child = new List<UserMenu>()
                    {
                        new UserMenu{Caption= "ACT Source",  Link= "MasterData/ActSource", Icon="fa-regular fa-folder"},
                        new UserMenu{Caption= "Priority Level",  Link=  "MasterData/CaPriority", Icon="fa-regular fa-folder"},
                        new UserMenu{Caption= "Impact Analysis",  Link= "MasterData/ImpactAnalysis", Icon="fa-regular fa-folder"},
                        new UserMenu{Caption= "Overdue Impact",  Link= "MasterData/CaOverdueImpact", Icon="fa-regular fa-folder"},
                    }
                },
                new UserMenu{Caption= "Action Tracking Report",  Link= "/ActionTrackingReport", Icon="fa-regular fa-folder"},
            };

            return menu;
        }


        private void replaceRootUrl(List<UserMenu> userMenus)
        {
            var rootUrl = GetBaseUrl();
            foreach (var menu in userMenus)
            {
                if (menu.Link != null && !menu.Link.Contains("http"))
                {
                    menu.Link = rootUrl.TrimEnd('/') + "/" + menu.Link.TrimStart('/');
                    menu.Link = menu.Link.Replace("//PHE_ACTRIS", "/PHE_ACTRIS");
                    menu.Link = menu.Link.Replace("//phe_actris", "/PHE_ACTRIS");
                }
                if (menu.Child != null && menu.Child.Any())
                {
                    replaceRootUrl(menu.Child);
                }
            }
        }

        public string GetBaseUrl()
        {
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, appUrl);
            return baseUrl.TrimEnd('/');
        }
    }
}
