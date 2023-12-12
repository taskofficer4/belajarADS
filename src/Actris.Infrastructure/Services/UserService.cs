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
using Actris.Abstraction.Helper;
using Actris.Abstraction.Model.Aiman;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Constant;
using Actris.Infrastructure.HttpUtils;

namespace Actris.Infrastructure.Services
{
   public class UserService : IUserService
   {
      private ILookupService _lookupSvc;
      private readonly HttpContextBase _httpContext;
      private static CacheService _cache = CacheService.GetInstance(nameof(UserService));

      private IAimanApiService _aimanApiSvc;


      public UserService(HttpContextBase httpContext, ILookupService lookupSvc, IAimanApiService aimanApiSvc)
      {
         _httpContext = httpContext;
         _lookupSvc = lookupSvc;
         _aimanApiSvc = aimanApiSvc;
      }

      public async Task<User> GetCurrentUserInfo()
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

         return await GetUserInfo(userName, true);
      }

      private bool IsSSOEnabled()
      {
         AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
         return authenticationSection.Mode == AuthenticationMode.None;
      }

      public async Task<User> GetUserInfo(string userName, bool useCache = false)
      {
         if (!useCache)
         {
            _cache.Clear(nameof(GetUserInfo), userName);
         }
         return await _cache.GetFromCacheAsync(
             nameof(GetUserInfo),
             userName, x => GetUserInfoToApi(userName),
             new CacheItemPolicy
             {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15)
             }
         );
      }

      private async Task<User> GetUserInfoToApi(string userName)
      {
         if (string.IsNullOrEmpty(userName))
            return null;

         var whereCondition = $"[EmpAccount]='{userName}'";
         var aimanEmployee = (await _aimanApiSvc.GetAllMasterEmployee(whereCondition)).FirstOrDefault();

         if (aimanEmployee == null)
         {
            aimanEmployee = new AimanEmployee
            {
               EmpID = "0000",
               EmpAccount = userName
            };
         }

         User ret = new User();
         ret.Employee = aimanEmployee;
         ret.EmpId = aimanEmployee.EmpID;
         ret.EmpAccount = aimanEmployee.EmpAccount;
         ret.Name = aimanEmployee.EmpAccount;

         var userDataDto = await GetUserData(userName);
         if (userDataDto != null)
         {
            ret.Roles = userDataDto.Roles;
            ret.UserGroup = userDataDto.UserGroup;
            ret.UserMenu = await GetUserMenu(userDataDto.AuthUserApp);
            RoleMapping.Process(ret.Roles);
         }
         else
         {
            ret.UserMenu = new List<UserMenu>();
            ret.Roles = new List<UserRoleDto> { new UserRoleDto
                {
                    Value = "UNKNOWN_ROLE"
                } };
         }


         // set SHU/REGIONAL
         if (aimanEmployee?.DirectorateID != null)
         {
            // get info shu / regional
            var directorate = (await _aimanApiSvc.GetDirectorate($"[DirectorateID]='{aimanEmployee.DirectorateID}'")).FirstOrDefault();
            if (directorate != null)
            {
               ret.TypeShuRegion = directorate.Directorate_Org_Cat;
            }
         }

         return ret;
      }

      private async Task<List<UserMenu>> GetUserMenu(string authUserApp)
      {
         try
         {
            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
            uriBuilder.Path = AimanConstant.EndpointGetAppMenu;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["userAuthAppFK"] = authUserApp;
            uriBuilder.Query = query.ToString() ?? string.Empty;

            var xpropUserMenu = AimanConstant.XPropUserMenu;
            var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserMenu>>>(uriBuilder.Uri, (req) => AddHeaders(req, xpropUserMenu));
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
            uriBuilder.Path = AimanConstant.EndpointUserData;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["appFk"] = AimanConstant.AppFK;
            query["username"] = userName;
            uriBuilder.Query = query.ToString() ?? string.Empty;

            var xpropUserData = AimanConstant.XPropUserData;
            var data = await HttpRequester.Http.GetAsJson<HttpResponse<List<UserDataDto>>>(uriBuilder.Uri, (req) => AddHeaders(req, xpropUserData));
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

      private void AddHeaders(HttpRequestMessage request, string prop)
      {
         request.Headers.Add("X-User-Prop", prop);

         var xAuth = AimanConstant.XAuth;
         request.Headers.Add("X-User-Auth", xAuth);
      }


   }
}
