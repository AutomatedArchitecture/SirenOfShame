using NUnit.Framework;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Test.Unit.Extruder.Services
{
    [TestFixture]
    public class TripleDESStringEncryptorTest
    {
        [Test]
        public void SimpleWordDecryptsToSameValueEncrypted()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("password");
            var decrypted = encryptor.DecryptString(encrypted);
            Assert.AreEqual("password", decrypted);
        }

        [Test]
        public void SimpleWordEncryptsToKnownValue()
        {
            var encryptor = new TripleDesStringEncryptor();
            var encrypted = encryptor.EncryptString("password");
            Assert.AreEqual("wwwfBrRRCDxe3qSYCrri3w==", encrypted);
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
