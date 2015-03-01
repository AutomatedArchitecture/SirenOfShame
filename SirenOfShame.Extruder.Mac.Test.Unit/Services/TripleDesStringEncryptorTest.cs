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
	}
}

