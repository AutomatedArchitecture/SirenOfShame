using System;
using System.Collections.Specialized;
using System.Net;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Services
{
    public class SosOnlineService
    {
        public const string SOS_URL = "http://localhost:3115";
        const string AUTHENTICATION_SUCCESS = "success";

        public void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string> onFail)
        {
            WebClient webClient = new WebClient();
            webClient.UploadValuesCompleted += (s, uploadEventArgs) =>
            {
                // todo: more error handeling when authenticating
                byte[] result = uploadEventArgs.Result;
                string resultAsStr = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                if (resultAsStr == AUTHENTICATION_SUCCESS)
                {
                    onSuccess();
                }
                else
                {
                    onFail(resultAsStr);
                }
            };

            NameValueCollection data = new NameValueCollection();
            data["UserName"] = settings.SosOnlineUsername;
            // todo: Send password encrypted, don't decrypt
            data["Password"] = settings.GetSosOnlinePassword();
            webClient.UploadValuesAsync(new Uri(SOS_URL + "/api/VerifyCredentials"), "POST", data);
        }
    }
}
