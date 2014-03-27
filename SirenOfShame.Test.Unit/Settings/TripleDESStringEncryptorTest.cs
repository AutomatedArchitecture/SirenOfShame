using NUnit.Framework;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestFixture]
    public class TripleDESStringEncryptorTest
    {
        [Test]
        public void Simple()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("password");
            var decrypted = encryptor.DecryptString(encrypted);
            Assert.AreEqual("password", decrypted);
        }
        
        [Test]
        public void UnusualChars()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("!@#$%^&*()- +=");
            var decrypted = encryptor.DecryptString(encrypted);
            Assert.AreEqual("!@#$%^&*()- +=", decrypted);
        }
    }
}
