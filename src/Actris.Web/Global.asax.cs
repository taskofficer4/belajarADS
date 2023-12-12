using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Actris.Web.Providers;

namespace Actris.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            AutoMapperConfig.RegisterGlobalMapping();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            //ValueProviderFactories.Factories.Add(new JsonNetActionValueProvider());

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            // Untuk generate label secara otomatis
            ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) =>
            {
                if (member != null)
                {
                    var propertyName = member.Name;
                    if (propertyName.EndsWith("ParID"))
                    {
                        propertyName = propertyName.Replace("ParID", "");
                    }
                    else if (propertyName.EndsWith("ID"))
                    {
                        propertyName = propertyName.Replace("ID", "");

                    }

                    return Regex.Replace(propertyName, "(\\B[A-Z])", " $1");
                }
                return null;
            };
        }


    }
}
