using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using OpenAthens.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Net;

namespace Actris.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromDays(1)
            });
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                    Authority = ConfigurationManager.AppSettings["oidc:ServerRealm"],
                    ClientId = ConfigurationManager.AppSettings["oidc:ClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["oidc:ClientSecret"],
                    MetadataAddress = ConfigurationManager.AppSettings["oidc:Metadata"],
                    RedirectUri = ConfigurationManager.AppSettings["oidc:RedirectUri"],
                    PostLogoutRedirectUri = ConfigurationManager.AppSettings["oidc:RedirectUri"],
                    Scope = OpenIdConnectScope.OpenIdProfile,
                    GetClaimsFromUserInfoEndpoint = true,
                   
                    ResponseType = OpenIdConnectResponseType.Code,
                    UseTokenLifetime = false
                }
            );

            // handle access 3rd party https not secured
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }
    }
}
