using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Actris.Infrastructure.HttpUtils
{
    public sealed class HttpRequester
    {
        private readonly HttpClient _client;
        //private static HttpService instance = null;
        public event EventHandler<AfterResponseEventArgs> AfterResponseEventHandler;
        public event EventHandler<OnErrorEventArgs> OnErrorEventHandler;
        public static readonly HttpRequester Http = new HttpRequester();
        //public static HttpService http
        //{
        //    get
        //    {
        //        if (instance == null) instance = new HttpService();
        //        return instance;
        //    }
        //}

        private HttpRequester()
        {
            _client = new HttpClient();
        }

        public async Task<HttpServiceResult<T>> GetAsJson<T>(Uri uri, Action<HttpRequestMessage> action = null) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);

                action?.Invoke(request);
                var response = await _client.SendAsync(request);
                OnAfterResponseEventHandler(new AfterResponseEventArgs
                {
                    Response = response
                });
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var g = JsonSerializer.Deserialize<T>(result);
                    return HttpServiceResult<T>.Ok(g, (int)response.StatusCode);
                }

                var failedJson = JsonSerializer.Deserialize<ErrorCode>(result); 

                return failedJson != null
                    ? HttpServiceResult<T>.Fail(failedJson.Message, failedJson.Code ?? "400", (int)response.StatusCode)
                    : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
            }
            catch (Exception e)
            {
                ErrorEventHandler(new OnErrorEventArgs(e));
                throw;
            }
        }

        private void OnAfterResponseEventHandler(AfterResponseEventArgs e)
        {
            var handler = AfterResponseEventHandler;
            handler?.Invoke(this, e);
        }

        private void ErrorEventHandler(OnErrorEventArgs e)
        {
            var handler = OnErrorEventHandler;
            handler?.Invoke(this, e);
        }
    }
}
