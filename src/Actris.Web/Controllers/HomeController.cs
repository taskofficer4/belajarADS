using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Configuration;
using Actris.Abstraction.Services;
using System.Security.Claims;
using Actris.Abstraction.Model.Response;
using Actris.Infrastructure.Services;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;
using DocumentFormat.OpenXml.EMMA;
using Actris.Abstraction.Enum;
using Actris.Web.Helpers;

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
            return View();
        }

        [AllowAnonymous]
        public ActionResult ClearCache()
        {
          CacheService.ClearAllCache();
          return Content("Clear Cache OK");
        }
    }
}
