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

      public static string Uri = ConfigurationManager.AppSettings["aiman:Uri"];


      public static string AppFK = ConfigurationManager.AppSettings["aiman:AppFK"];
      public static string XAuth = ConfigurationManager.AppSettings["aiman:XAuth"];
      public static string MasterUserData = ConfigurationManager.AppSettings["aiman:MasterUserData"];
      public static string UserData = ConfigurationManager.AppSettings["aiman:UserData"];
      public static string UserMenu = ConfigurationManager.AppSettings["aiman:UserMenu"];
      public static string XPropUserData = ConfigurationManager.AppSettings["aiman:XPropUserData"];
      public static string XPropUserMenu = ConfigurationManager.AppSettings["aiman:XPropUserMenu"];


      public static string EndpointUserData = ConfigurationManager.AppSettings["aiman:EndpointUserData"];
      public static string EndpointGetAppMenu = ConfigurationManager.AppSettings["aiman:EndpointGetAppMenu"];
      public static string ConKey = ConfigurationManager.AppSettings["Con:Key"];


      // GetDirectorate
      public static string EndpointGetDirectorate = ConfigurationManager.AppSettings["aiman:EndpointGetDirectorate"];
      public static string XPropGetDirectorate = ConfigurationManager.AppSettings["aiman:XPropGetDirectorate"];

      // GetDivision
      public static string EndpointGetDivision = ConfigurationManager.AppSettings["aiman:EndpointGetDivision"];
      public static string XPropGetDivision = ConfigurationManager.AppSettings["aiman:XPropGetDivision"];

      // GetSubDivision
      public static string EndpointGetSubDivision = ConfigurationManager.AppSettings["aiman:EndpointGetSubDivision"];
      public static string XPropGetSubDivision = ConfigurationManager.AppSettings["aiman:XPropGetSubDivision"];

      // GetDepartment
      public static string EndpointGetDepartment = ConfigurationManager.AppSettings["aiman:EndpointGetDepartment"];
      public static string XPropGetDepartment = ConfigurationManager.AppSettings["aiman:XPropGetDepartment"];

      // GetCompany
      public static string EndpointGetCompany = ConfigurationManager.AppSettings["aiman:EndpointGetCompany"];
      public static string XPropGetCompany = ConfigurationManager.AppSettings["aiman:XPropGetCompany"];

      // GetWorkingArea
      public static string EndpointGetWorkingArea = ConfigurationManager.AppSettings["aiman:EndpointGetWorkingArea"];
      public static string XPropGetWorkingArea = ConfigurationManager.AppSettings["aiman:XPropGetWorkingArea"];

      // GetLocationByCompany
      public static string EndpointGetLocationCompany = ConfigurationManager.AppSettings["aiman:EndpointGetLocationCompany"];
      public static string XPropGetLocationCompany = ConfigurationManager.AppSettings["aiman:XPropGetLocationCompany"];

      // GetLocationByCompany
      public static string EndpointGetAllMasterEmployee = ConfigurationManager.AppSettings["aiman:EndpointGetAllMasterEmployee"];
      public static string XPropGetAllMasterEmployee = ConfigurationManager.AppSettings["aiman:XPropGetAllMasterEmployee"];


      // GetPSAHierarchy
      public static string EndpointGetPSAHierarchy = ConfigurationManager.AppSettings["aiman:EndpointGetPSAHierarchy"];
      public static string XPropGetPSAHierarchy = ConfigurationManager.AppSettings["aiman:XPropGetPSAHierarchy"];

      // MadamTrx
      public static string HsseAppId = ConfigurationManager.AppSettings["aiman:MadamAppId"];
      public static string EndpointMadamTrx = ConfigurationManager.AppSettings["aiman:EndpointMadamTrx"];
      public static string HsseUserAuth = ConfigurationManager.AppSettings["aiman:MadamUserAuth"];

      //MadamGetAllStatusApprover
      public static string EndpointMadamGetAllStatusApprover = ConfigurationManager.AppSettings["aiman:EndpointMadamGetAllStatusApprover"];
   }
}
