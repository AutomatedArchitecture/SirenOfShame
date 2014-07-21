using System.Net;
using System.Threading.Tasks;
using SirenOfShame.Extruder.Models;

namespace SirenOfShame.Extruder.Services
{
    public class SosOnlineService
    {
        public async Task<ApiResultBase> ConnectExtruder(ConnectExtruderModel connectExtruderModel)
        {
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(connectExtruderModel);
            const string url = "http://localhost:3115/ApiV1/ConnectExtruder";
            var resultStr = await webClient.UploadStringTaskAsync(url, "POST", data);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResultBase>(resultStr);
            return result;
        }
    }
}
