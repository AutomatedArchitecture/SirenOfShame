using NUnit.Framework;
using System;
using SirenOfShame.Extruder.PclLib;

namespace SirenOfShame.Extruder.Mac.Test.Unit
{
	[TestFixture()]
	public class TripleDesStringEncryptorTest
	{
		[Test ()]
		public void Encrypt_GivenNull_ReturnsNull ()
		{
			var tdse = new TripleDesStringEncryptor ();
			Assert.IsNull(tdse.EncryptString (null));
		}

		[Test ()]
		public void Decrypt_GivenNull_ReturnsNull ()
		{
			var tdse = new TripleDesStringEncryptor ();
			Assert.IsNull(tdse.DecryptString (null));
		}

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

