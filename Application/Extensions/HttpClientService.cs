using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public class HttpClientService(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
    {
        private HttpClient CreateClient() => httpClientFactory!.CreateClient(Constant.HttpClientName);
        public HttpClient GetPublicClient() => CreateClient();
        public async Task<HttpClient> GetPrivateClient()
        {
            try {
                var client = CreateClient();
                var localStorageDTO = await localStorageService.GetModelFromToken();
                if (string.IsNullOrEmpty(localStorageDTO.Token))
                    return client;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Constant.HttpClientHeaderScheme, localStorageDTO.Token);
                return client;
            } catch {
                return new HttpClient();
            }
        }
    }
}
