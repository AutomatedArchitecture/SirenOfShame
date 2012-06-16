using System;
using System.Linq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Services
{
    public class SosOnlineService
    {
        public const string SOS_URL = "http://localhost:3115";

        public void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string, ServerUnavailableException> onFail)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/ApiV1/VerifyCredentials", doc =>
            {
                string success = doc.Descendants("Success").First().Value;
                if (success == "true")
                {
                    onSuccess();
                } 
                else
                {
                    string errorMessage = doc.Descendants("ErrorMessage").First().Value;
                    onFail(errorMessage, null);
                }
            }, OnConnectionFail(onFail));
        }

        private static void AddSosOnlineCredentials(SirenOfShameSettings settings, WebClientXml webClientXml)
        {
            webClientXml.Add("UserName", settings.SosOnlineUsername);
            // send the encrypted version of the password since we're too cheap to support SSL at present
            webClientXml.Add("Password", settings.SosOnlinePassword);
        }

        public void AddBuilds(SirenOfShameSettings settings, string exportedBuilds, Action<DateTime> onSuccess, Action<string, ServerUnavailableException> onFail)
        {
            WebClientXml webClientXml = new WebClientXml();
            AddSosOnlineCredentials(settings, webClientXml);
            webClientXml.Add("Builds", exportedBuilds);
            webClientXml.UploadValuesAndReturnXmlAsync(SOS_URL + "/ApiV1/AddBuilds", doc =>
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
                    onFail(errorMessage, null);
                }
            }, OnConnectionFail(onFail));
        }

        private static Action<ServerUnavailableException> OnConnectionFail(Action<string, ServerUnavailableException> onFail)
        {
            return ex => onFail("Failed to connect to SoS online", ex);
        }
    }
}
