using Actris.Abstraction.Model.Aiman;
using Actris.Abstraction.Services;
using Actris.Infrastructure.Constant;
using Actris.Infrastructure.HttpUtils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace Actris.Infrastructure.Services
{
    public class AimanApiService : IAimanApiService
    {
        private readonly HttpContextBase _httpContext;
        private static CacheService _cache = CacheService.GetInstance(nameof(AimanApiService));
        private CacheItemPolicy CacheExpiration => new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) };

        public AimanApiService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Default body untuk hit api ke aiman
        /// </summary>
        /// <typeparam name="TAimanResponse"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="xUserProp"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        private async Task<TAimanResponse> BaseGetFromApi<TAimanResponse>(string endpoint, string xUserProp, string whereCondition = "1=1") where TAimanResponse : class
        {
            var xUserAuth = AimanConstant.XAuth;

            void AddHeaders(HttpRequestMessage request)
            {
                request.Headers.Add("X-User-Prop", xUserProp);
                request.Headers.Add("X-User-Auth", xUserAuth);
            }

            try
            {
                // URL ENDPOINT
                UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                {
                    Path = endpoint
                };

                // PARAMETER
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["WhereCondition"] = whereCondition;

                uriBuilder.Query = query.ToString();
                var response = await HttpRequester.Http.GetAsJson<HttpResponse<TAimanResponse>>(uriBuilder.Uri, AddHeaders);
                if (response.IsSuccessful)
                {
                    return response.Value.Object;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public async Task<List<AimanDirectorate>> GetDirectorate(string whereCondition = "1=1")
        {
            return await _cache.GetFromCacheAsync(
              nameof(GetDirectorate),
              whereCondition, x => BaseGetFromApi<List<AimanDirectorate>>(AimanConstant.EndpointGetDirectorate, AimanConstant.XPropGetDirectorate, whereCondition),
              CacheExpiration
          );
        }

        public async Task<List<AimanDivision>> GetDivision()
        {
            return await BaseGetFromApi<List<AimanDivision>>(AimanConstant.EndpointGetDivision, AimanConstant.XPropGetDivision);
        }

        public async Task<List<AimanSubDivision>> GetSubDivision()
        {
            return await BaseGetFromApi<List<AimanSubDivision>>(AimanConstant.EndpointGetSubDivision, AimanConstant.XPropGetSubDivision);
        }

        public async Task<List<AimanCompany>> GetCompany()
        {
            return await BaseGetFromApi<List<AimanCompany>>(AimanConstant.EndpointGetCompany, AimanConstant.XPropGetCompany);
        }

        public async Task<List<AimanDepartment>> GetDepartment()
        {
            return await BaseGetFromApi<List<AimanDepartment>>(AimanConstant.EndpointGetDepartment, AimanConstant.XPropGetDepartment);
        }

        public async Task<List<AimanWorkingArea>> GetWorkingArea()
        {
            return await BaseGetFromApi<List<AimanWorkingArea>>(AimanConstant.EndpointGetWorkingArea, AimanConstant.XPropGetWorkingArea);
        }

        public async Task<List<AimanLocationCompany>> GetLocationCompany()
        {
            return await BaseGetFromApi<List<AimanLocationCompany>>(AimanConstant.EndpointGetLocationCompany, AimanConstant.XPropGetLocationCompany);
        }

        public async Task<List<AimanEmployee>> GetAllMasterEmployee(string whereCondition)
        {
            return await BaseGetFromApi<List<AimanEmployee>>(AimanConstant.EndpointGetAllMasterEmployee, AimanConstant.XPropGetAllMasterEmployee,whereCondition);
        }


        public async Task<List<AimanPsaHierarchy>> GetPSAHierarchy(string whereCondition)
        {
            return await BaseGetFromApi<List<AimanPsaHierarchy>>(AimanConstant.EndpointGetPSAHierarchy, AimanConstant.XPropGetPSAHierarchy, whereCondition);
        }
    }
}
