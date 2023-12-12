using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Constant;
using Actris.Infrastructure.HttpUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Actris.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpContextBase _httpContext;
        private readonly MemoryCache _cache;

        public UserService(HttpContextBase httpContext, MemoryCache cache)
        {
            _httpContext = httpContext;
            _cache = cache;
        }

        public string GetCurrentUserName()
        {
            string userName;
            if (IsSSOEnabled())
            {
                userName = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                userName = _httpContext.User.Identity.Name;
            }
            return userName;
        }

        public async Task<User> GetCurrentUserInfo()
        {
            string userName = GetCurrentUserName();

            if (string.IsNullOrEmpty(userName))
                return null;

            User userInfo = _cache.Get(userName) as User;
            if (userInfo != null)
                return userInfo;

            userInfo = await GetUserInfo(userName);
            _cache.Set(userName, userInfo, null);
            return userInfo;
        }


        private bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == AuthenticationMode.None;
        }

        public async Task<User> GetUserInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var masterUserData = await GetMasterData(userName);
            User ret = new User();

            if (masterUserData == null)
            {
                ret.EmpId = "0";
                ret.EmpAccount = userName;
                ret.Name = userName;
                ret.Email = userName;
                //return null;
            }
            else
            {
                ret.EmpId = masterUserData.EmpID;
                ret.EmpAccount = masterUserData.EmpAccount;
                ret.Name = masterUserData.EmpName;
                ret.Email = masterUserData.EmpEmail;
            }
            var userDataDto = await GetUserData(userName);
            if (userDataDto != null)
            {
                ret.Roles = userDataDto.Roles;
                ret.UserMenu = await GetUserMenu(userDataDto.AuthUserApp);
            }
            return ret;
        }

        private async Task<List<UserMenu>> GetUserMenu(string authUserApp)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
                uriBuilder.Path = AimanConstant.UserMenu;
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["userAuthAppFK"] = authUserApp;
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserMenu>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropUserMenu));
                if (data.IsSuccessful)
                {
                    return data.Value.Object;
                }
            }
            catch
            {
            }
            return new List<UserMenu>();
        }

        private async Task<UserDataDto> GetUserData(string userName)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
                uriBuilder.Path = AimanConstant.UserData;
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["appFk"] = AimanConstant.AppFK;
                query["username"] = userName;
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserDataDto>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropUserData));
                if (data.IsSuccessful)
                {
                    return data.Value.Object.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private async Task<MasterUserData> GetMasterData(string username)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
                uriBuilder.Path = AimanConstant.MasterUserData;
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["WhereCondition"] = $"EmpAccount='{username}'";
                uriBuilder.Query = query.ToString() ?? string.Empty;
                var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<MasterUserData>>>(uriBuilder.Uri, (req) => AddHeaders(req, AimanConstant.XPropMasterData));
                if (data.IsSuccessful)
                {
                    return data.Value.Object.FirstOrDefault();
                }
                return new MasterUserData();
            }
            catch
            {
                return null;
            }
        }

        private void AddHeaders(HttpRequestMessage request, string prop)
        {
            request.Headers.Add("X-User-Prop", prop);
            request.Headers.Add("X-User-Auth", AimanConstant.XAuth);
        }
    }
}
