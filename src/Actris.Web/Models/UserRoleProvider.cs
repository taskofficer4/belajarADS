using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Actris.Abstraction.Services;

namespace Actris.Web.Models
{
    public class UserRoleProvider : RoleProvider
    {
        private readonly IUserService _userService;

        public UserRoleProvider()
        {
            _userService = DependencyResolver.Current.GetService<IUserService>();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
         
            var userInfo = Task.Run(async () => await _userService.GetUserInfo(username, true)).Result;
            if (userInfo?.Roles == null)
            {
                return false;
            }
            var roles = roleName.Split(',');
            return userInfo.Roles.Any(o => roles.Contains(o.Value));

        }

        public override string[] GetRolesForUser(string username)
        {
            var userInfo = Task.Run(async () => await _userService.GetUserInfo(username, true)).Result;
    
            if (userInfo?.Roles == null)
            {
                return new string[] { };
            }
            var roles = userInfo.Roles.Select(o => o.Value);
            return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}