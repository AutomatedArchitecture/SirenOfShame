using System;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using log4net;

namespace SirenOfShame.Lib.Services
{
    public class ActiveDirectoryService
    {
        private readonly ILog _log = MyLogManager.GetLogger(typeof (ActiveDirectoryService));

        public Image GetUserPicture(string userName, string domain)
        {
            var directoryEntry = new DirectoryEntry("LDAP://" + domain);
            var propertiesToLoad = new[] { "thumbnailPhoto", "samaccountname" };
            var filter = $"(&(SAMAccountName={userName}))";
            var directorySearcher = new DirectorySearcher(directoryEntry, filter, propertiesToLoad);
            var user = directorySearcher.FindOne();

            if (user == null)
            {
                _log.Warn($"Could not find user '{userName}' in active directory");
                return null;
            }

            if (!user.Properties.Contains("thumbnailPhoto"))
            {
                var message = "LDAP did not contain a thumbnailPhoto property for " + userName;
                _log.Warn(message);
                return null;
            }
            var bytes = user.Properties["thumbnailPhoto"][0] as byte[];
            if (bytes == null) return null;
            using (var ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                return image;
            }
        }

    }
}
