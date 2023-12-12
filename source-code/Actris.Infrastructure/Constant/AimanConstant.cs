using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Constant
{
    public class AimanConstant
    {
        public static string AppFK = ConfigurationManager.AppSettings["aiman:AppFK"];
        public static string XAuth = ConfigurationManager.AppSettings["aiman:XAuth"];
        public static string Uri = ConfigurationManager.AppSettings["aiman:Uri"];
        public static string MasterUserData = ConfigurationManager.AppSettings["aiman:MasterUserData"];
        public static string UserData = ConfigurationManager.AppSettings["aiman:UserData"];
        public static string UserMenu = ConfigurationManager.AppSettings["aiman:UserMenu"];
        public static string DoTransaction = ConfigurationManager.AppSettings["aiman:DoTransaction"];
        public static string XPropMasterData = ConfigurationManager.AppSettings["aiman:XPropMasterData"];
        public static string XPropUserData = ConfigurationManager.AppSettings["aiman:XPropUserData"];
        public static string XPropUserMenu = ConfigurationManager.AppSettings["aiman:XPropUserMenu"];
        public static string ConKey = ConfigurationManager.AppSettings["Con:Key"];
        public static bool ConUseEncryption = ConfigurationManager.AppSettings["Con:UseEncryption"] == "True" || ConfigurationManager.AppSettings["Con:UseEncryption"] == "true";

        public static string AppLogKey = ConfigurationManager.AppSettings["AppLog:Key"];
        public static string AppLogCode = ConfigurationManager.AppSettings["AppLog:Code"];
        public static string AppLogLink = ConfigurationManager.AppSettings["AppLog:Link"];
         
  
    }
}
