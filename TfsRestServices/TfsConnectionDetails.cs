using System;
using System.Net;
using System.Text;
using SirenOfShame.Lib.Settings;

namespace TfsRestServices
{
    public class TfsConnectionDetails
    {
        public TfsConnectionDetails(CiEntryPointSetting ciEntryPointSetting)
        {
            BaseUrl = ciEntryPointSetting.Url;
            Username = ciEntryPointSetting.UserName;
            Password = ciEntryPointSetting.GetPassword();
        }

        public TfsConnectionDetails() {  }

        public TfsConnectionDetails(string url, string username, string password)
        {
            BaseUrl = url;
            Username = username;
            Password = password;
        }

        private string BaseUrl { get; }
        private string Username { get; }
        private string Password { get; }

        public Uri GetBaseAddress()
        {
            var withTrailingSlash = BaseUrl + (BaseUrl.EndsWith("/") ? "" : "/");
            return new Uri(withTrailingSlash);
        }

        public string Base64EncodeCredentials()
        {
            var byteArray = Encoding.ASCII.GetBytes(Username + ":" + Password);
            var base64String = Convert.ToBase64String(byteArray);
            return base64String;
        }

        public NetworkCredential AsNetworkConnection()
        {
            return new NetworkCredential(Username, Password);
        }
    }
}