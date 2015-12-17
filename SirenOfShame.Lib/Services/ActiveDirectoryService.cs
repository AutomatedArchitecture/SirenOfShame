using System;
using System.DirectoryServices;
using System.Drawing;
using System.IO;

namespace SirenOfShame.Lib.Services
{
    public class ActiveDirectoryService
    {
        public Image GetUserPicture(string userName, string domain)
        {
            var directoryEntry = new DirectoryEntry("LDAP://" + domain);
            var propertiesToLoad = new[] { "thumbnailPhoto", "samaccountname" };
            var filter = string.Format("(&(SAMAccountName={0}))", userName);
            var directorySearcher = new DirectorySearcher(directoryEntry, filter, propertiesToLoad);
            var user = directorySearcher.FindOne();

            if (!user.Properties.Contains("thumbnailPhoto"))
            {
                var message = "LDAP did not contain a thumbnailPhoto property for " + userName;
                throw new Exception(message);
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
