using System.Net.Http.Json;

namespace sRequestBase
{
    public abstract class RequestBase
    {
#if NET6_0_OR_GREATER
        protected HttpClient _httpClient = new HttpClient();
#endif
#if NETCOREAPP3_1_OR_GREATER

#endif

        public string Domain { get; set; }
        public string Url { get; set; }

        public RequestBase()
        {
            Domain = String.Empty;
            Url = String.Empty;
        }

        private  void PostformRequest( )
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<HttpResponseMessage> SendPostForm(string urlextra,HttpContent content)
        {
            PostformRequest();
            string urlPosition = String.Concat(Domain,Url,urlextra);
            return await _httpClient.PostAsync(urlPosition, content);

        }

    }
}