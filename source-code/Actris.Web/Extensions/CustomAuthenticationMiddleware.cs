using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
namespace ASPNetMVC.Web.Extensions
{



    public class CustomAuthenticationMiddleware : OwinMiddleware
    {
        public CustomAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Check if the user is trying to log in
            if (context.Request.Path == new PathString("/Account/Login") && context.Request.Method == "POST")
            ;{
                string username = "";//;context.Request.Form["username"];
                string password = "";//context.Request.Form["password"];

                // Perform your custom authentication logic here
                if (IsAuthenticated(username, password))
                {
                    // Authentication successful, create a claims identity for the user
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

                    // Sign in the user
                    context.Authentication.SignIn(identity);

                    // Redirect to the desired page after login (e.g., homepage)
                    context.Response.Redirect("/");
                    return;
                }
                else
                {
                    // Authentication failed, display an error message or redirect back to the login page
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            // Call the next middleware in the pipeline
            await Next.Invoke(context);
        }

        private bool IsAuthenticated(string username, string password)
        {
            // Implement your custom authentication logic here
            // For example, check the username and password against a database of users
            // Return true if authenticated, otherwise return false
            // Replace this with your actual authentication logic
            return username == "testuser" && password == "password";
        }
    }
}