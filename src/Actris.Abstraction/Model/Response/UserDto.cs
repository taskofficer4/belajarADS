using Actris.Abstraction.Model.Aiman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actris.Abstraction.Model.Response
{
   public class User
   {
      public string EmpId { get; set; }
      public string EmpAccount { get; set; }
      public string Name { get; set; }
      public string AuthCompany { get; set; }
      public string DefaultCompany { get; set; }


      // current user Employee data 
      public AimanEmployee Employee { get; set; }
      public string TypeShuRegion { get; set; }
      public List<UserRoleDto> Roles { get; set; }
      public List<UserRoleDto> UserGroup { get; set; }
      public string HierLvl2
      {
         get
         {
            if (UserGroup != null && UserGroup.Any())
            {
               return UserGroup.First().Value;
            }
            return null;
         }
      }
      public List<UserMenu> UserMenu { get; set; }
   }

   public class UserRoleDto
   {
      public string Key { get; set; }
      public string Value { get; set; }

      public UserRoleDto()
      {

      }

      public UserRoleDto(string roleName)
      {
         Key = roleName;
         Value = roleName;
      }
   }

   public class UserDataDto
   {
      public string AuthUserApp { get; set; }
      public List<UserRoleDto> Roles { get; set; }
      public List<UserRoleDto> UserGroup { get; set; }
   }

   public class UserMenu
   {
      public string Caption { get; set; }
      public string Name { get; set; }
      public int Sort { get; set; }
      public string Link { get; set; }

      public string Icon { get; set; }
      public string Area { get; set; }
      public List<UserMenu> Child { get; set; }

   }

   public class MasterUserData : AimanEmployee
   {
   }
}
