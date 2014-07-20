using System.Net;
using System.Threading.Tasks;
using SirenOfShame.Extruder.Models;

namespace SirenOfShame.Extruder.Services
{
    public class SosOnlineService
    {
        public async Task ConnectExtruder(ConnectExtruderModel connectExtruderModel)
        {
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(connectExtruderModel);
            const string url = "http://localhost:3115/ApiV1/ConnectExtruder";
            await webClient.UploadStringTaskAsync(url, "POST", data);
        }
    }
}
