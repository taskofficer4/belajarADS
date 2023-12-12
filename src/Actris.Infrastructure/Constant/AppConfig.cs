using System.Configuration;

namespace Actris.Infrastructure.Constant
{
    public class AppConfig
    {
        public static bool IsProduction = ConfigurationManager.AppSettings["apps:IsProduction"] == "true";
   
    }
}
