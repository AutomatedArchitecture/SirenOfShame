using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TfsRestServices
{
    public class TfsJsonService
    {
        public async Task<List<TfsJsonBuild>> GetBuildsStatuses(TfsConnectionDetails connection, Dictionary<string, string> queryParams)
        {
            return await GetFromTfs<TfsJsonBuild>(connection, "_apis/build/builds", queryParams);
        }

        public async Task<List<TfsJsonBuildDefinition>> GetBuildDefinitions(string url, string username, string password)
        {
            var connection = new TfsConnectionDetails(url, username, password);
            return await GetFromTfs<TfsJsonBuildDefinition>(connection, "_apis/build/definitions", new Dictionary<string, string>());
        }

        private async Task<List<T>> GetFromTfs<T>(TfsConnectionDetails connection, string api, Dictionary<string, string> queryParams)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                Credentials = connection.AsNetworkConnection()
            };

            HttpClient httpClient = new HttpClient(handler)
            {
                BaseAddress = connection.GetBaseAddress()
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            var credentialsBase64Encoded = connection.Base64EncodeCredentials();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentialsBase64Encoded);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var queryParamsAsString = string.Concat(queryParams.Select(i => "&" + i.Key + "=" + i.Value));
            var buildDefinitionsStr = await httpClient.GetStringAsync(api + "?api-version=2.0" + queryParamsAsString);
            var jsonWrapper = JsonConvert.DeserializeObject<TfsJsonWrapper<T>>(buildDefinitionsStr);
            return jsonWrapper.Value;
        }

        public async Task<List<TfsJsonComment>> GetComments(TfsJsonBuild tfsJsonBuild, TfsConnectionDetails connection)
        {
            var comments = await GetFromTfs<TfsJsonComment>(connection, "_apis/build/builds/" + tfsJsonBuild.Id + "/changes",
                        new Dictionary<string, string>());
            return comments;
        }
    }
}
