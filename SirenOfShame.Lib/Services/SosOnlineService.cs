using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

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

        public void AddBuilds(SirenOfShameSettings settings, string exportedBuilds, Action<DateTime> onSuccess, Action<string> onFail)
        {
            WebClientXml webClientXml = new WebClientXml();
            webClientXml.Add("UserName", settings.SosOnlineUsername);
            //// todo: Send password encrypted, don't decrypt
            webClientXml.Add("Password", settings.GetSosOnlinePassword());
            webClientXml.Add("Builds", exportedBuilds);
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/api/AddBuilds", doc =>
            {
                string success = doc.Descendants("Success").First().Value;
                if (success == "true")
                {
                    string newHighWaterMarkStr = doc.Descendants("NewHighWaterMark").First().Value;
                    DateTime newHighWaterMark = new DateTime(long.Parse(newHighWaterMarkStr));
                    onSuccess(newHighWaterMark);
                }
                else
                {
                    string errorMessage = doc.Descendants("ErrorMessage").First().Value;
                    onFail(errorMessage);
                }
            });
        }
    }
}
