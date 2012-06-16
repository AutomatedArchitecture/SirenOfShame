using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestClass]
    public class TripleDESStringEncryptorTest
    {
        [TestMethod]
        public void Simple()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("password");
            var decrypted = encryptor.DecryptString(encrypted);
            Assert.AreEqual("password", decrypted);
        }
        
        [TestMethod]
        public void UnusualChars()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("!@#$%^&*()- +=");
            var decrypted = encryptor.DecryptString(encrypted);
            Assert.AreEqual("!@#$%^&*()- +=", decrypted);
        }
    }
}
