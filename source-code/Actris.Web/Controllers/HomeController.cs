using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Actris.Abstraction.Services;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Configuration;
using AppLog_Component;
using Actris.Abstraction.Model.Response;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.PeerToPeer;
using System.Security.Claims;
using Microsoft.Owin;
using DocumentFormat.OpenXml.Wordprocessing;
using Actris.Infrastructure.Constant;

namespace Actris.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Home Page";

                //Harus di sematkan di HOME
                var name = HttpContext.User.Identity.Name; 
                AppLogger _appLog = new AppLogger();
                _appLog.Applog_Insert("Home", name, AimanConstant.AppLogCode, "Description", "Success", 
                    AimanConstant.AppLogLink,AimanConstant.AppLogKey);


                return View();
            }

            //if (IsSSOEnabled())
            //{
            //    return RedirectToAction("LoginSSO");
            //}

            return View("LoginForm");
        }

        private bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == System.Web.Configuration.AuthenticationMode.None;
        }

        [Authorize]
        public ActionResult LoginSSO()
        {
            ViewBag.Title = "Home Page";
            return View("Index");
        }

        [HttpGet]
        public bool LoginForm(string userName)
        {
            if (IsSSOEnabled())
                return false;

            var user = _userService.GetUserInfo(userName);
            if (user == null)
                return false;


            HttpContext.User = new GenericPrincipal(new GenericIdentity(userName), new string[] { "user" });
            FormsAuthentication.SetAuthCookie(userName, true);
            return true;
        }
         
        [HttpPost]
        public async Task<ActionResult> LoginFormImpersonate(string account) {

            string userName = account;
            var user = await _userService.GetUserInfo(userName);
            //if (user == null)
            //    return false;
             
            var claims = new[]
                 {
                    new Claim(ClaimTypes.Name, userName) 
                };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            // Sign in the user by setting the authentication cookie
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(identity);

            // Redirect to the desired page after login (e.g., homepage)
            return RedirectToAction("Index", "Home");
             
        }

        public async Task<ActionResult> RunAs()
        {
            return View(await _userService.GetCurrentUserInfo());
        }
    }
}
