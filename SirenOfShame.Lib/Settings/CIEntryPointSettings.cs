using System;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class CiEntryPointSettings
    {
        public string Url { get; set; }

        public string UserName { get; set; }

        public string EncryptedPassword { get; set; }

        public void SetPassword(string value)
        {
            EncryptedPassword = new TripleDESStringEncryptor().EncryptString(value);
        }

        public string GetPassword()
        {
            return new TripleDESStringEncryptor().DecryptString(EncryptedPassword);
        }
    }
}
