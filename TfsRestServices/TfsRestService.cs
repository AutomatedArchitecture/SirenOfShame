using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsRestBuildDefinition : MyBuildDefinition
    {
        public TfsRestBuildDefinition() {  }

        public TfsRestBuildDefinition(TfsJsonBuildDefinition jsonBuildDefinition)
        {
            Id = jsonBuildDefinition.Id.ToString();
            Name = jsonBuildDefinition.Definition.Name;
        }

        public override string Id { get; }
        public override string Name { get; }
    }

    public class TfsJsonWrapper
    {
        public TfsJsonBuildDefinition[] Value { get; set; }
    }

    public class TfsJsonBuildDefinition
    {
        public int Id { get; set; }
        public TfsJsonBuildDefinitionDefinition Definition { get; set; }
    }

    public class TfsJsonBuildDefinitionDefinition
    {
        public string Name { get; set; }
    }

    internal class TfsRestService
    {
        public async Task<List<TfsRestBuildDefinition>> GetProjects(string url, string username, string password)
        {
            HttpClient httpClient = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            var betterUrl = url + (url.EndsWith("/") ? "" : "/");
            httpClient.BaseAddress = new Uri(betterUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var buildDefinitionsStr = await httpClient.GetStringAsync("_apis/build/builds?api-version=2.0");
            var jsonWrapper = JsonConvert.DeserializeObject<TfsJsonWrapper>(buildDefinitionsStr);
            return jsonWrapper.Value.Select(i => new TfsRestBuildDefinition(i)).ToList();
        }
    }
}