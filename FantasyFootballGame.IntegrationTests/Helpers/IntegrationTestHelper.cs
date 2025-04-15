using FantasyFootballGame.IntegrationTests.Extensions;
using System.Net.Http.Headers;


namespace FantasyFootballGame.IntegrationTests.Helpers
{
    public static class IntegrationTestHelper
    {
        public static async Task<HttpResponseMessage> CreateItem(HttpClient client,string url, object dto, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = dto.ReadAsJsonContent()
            };
            AddDefaultHeaders(request, token);
            return await client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> DeleteItem(HttpClient client,string url, int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"${url}/{id}");
            AddDefaultHeaders(request, token);
            return await client.SendAsync(request);
        }

        public static void AddDefaultHeaders(HttpRequestMessage request, string token)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("User-Agent", "Integration-Tests");
        }
    }
}
