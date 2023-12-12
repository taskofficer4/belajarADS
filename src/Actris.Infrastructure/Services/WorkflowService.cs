using Actris.Abstraction.Services;
using Actris.Infrastructure.Constant;
using Actris.Infrastructure.HttpUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.Aiman;

namespace Actris.Infrastructure.Services
{
   public class WorkflowService : IWorkflowService
   {
      private readonly HttpContextBase _httpContext;
      public WorkflowService()
      {

      }
      public WorkflowService(HttpContextBase httpContext)
      {
         _httpContext = httpContext;
      }


      internal async Task<TAimanResponse> BaseGetFromApi<TAimanResponse>(string TransNo) where TAimanResponse : class
      {
         try
         {
            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
            uriBuilder.Path = AimanConstant.EndpointMadamGetAllStatusApprover;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["appId"] = AimanConstant.HsseAppId;
            query["transNo"] = TransNo;
            uriBuilder.Query = query.ToString() ?? string.Empty;

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

      internal async Task<TAimanResponse> BaseDoTrx<TAimanResponse>(TrxMadam.DoTrx trx)
      where TAimanResponse : class
      {
         try
         {
            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri);
            uriBuilder.Path = AimanConstant.EndpointMadamTrx;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["appId"] = AimanConstant.HsseAppId;
            query["transNo"] = !string.IsNullOrEmpty(trx.TransNo) ? trx.TransNo : "1";
            query["companyCode"] = trx.CompanyCode != string.Empty ? trx.CompanyCode : "1";
            query["action"] = !string.IsNullOrEmpty(trx.Action) ? trx.Action : "Submit";

            //Diisi pertama kali saat submit
            query["startWF"] = "4303AECD-152C-4524-9F31-CC28A07E18B0";
            query["actionFor"] = !string.IsNullOrEmpty(trx.ActionFor) ? trx.ActionFor : "1";
            query["actionBy"] = !string.IsNullOrEmpty(trx.ActionBy) ? trx.ActionBy : "1";
            query["source"] = "Web Aplication";
            query["notes"] = trx.Notes;
            query["additionalData"] = trx.AdditionalData;
            uriBuilder.Query = query.ToString() ?? string.Empty;

            var response = await HttpRequester.Http.PostAsJson<HttpResponse<TAimanResponse>>(uriBuilder.Uri, AddHeaders);
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

      void AddHeaders(HttpRequestMessage request)
      {
         request.Headers.Add("appId", AimanConstant.HsseAppId);
         var xAuth = AimanConstant.HsseUserAuth;
         request.Headers.Add("X-User-Auth", xAuth);
      }

      public async Task<TrxMadam.Object> GetMadamStatusInfo(string whereCondition)
      {
         return await BaseGetFromApi<TrxMadam.Object>(whereCondition);
      }

      public async Task<TrxMadam.ResponseTrx> DoMadamTrx(TrxMadam.DoTrx trx)
      {
         return await BaseDoTrx<TrxMadam.ResponseTrx>(trx);
      }


   }
}