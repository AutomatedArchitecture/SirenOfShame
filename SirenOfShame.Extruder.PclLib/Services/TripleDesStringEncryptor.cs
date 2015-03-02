using System.Text;
using PCLCrypto;

namespace SirenOfShame.Extruder.PclLib {
	public class TripleDesStringEncryptor {
        private readonly byte[] _key;
//        private readonly byte[] _iv;

		public TripleDesStringEncryptor(int iterations = 1000)
        {
            var key = "GSYAHAGCBDUUADIADKOPAAAW";
            var iv = Encoding.Unicode.GetBytes("USAZBGAW");
			//int iterations = iterations;
			int countBytes = 16;
			_key = NetFxCrypto.DeriveBytes.GetBytes(key, iv, iterations, countBytes);
        }

		private ICryptographicKey CreateSymmetricKey ()
		{
			var algorithm = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm (SymmetricAlgorithm.TripleDesCbcPkcs7);
			return algorithm.CreateSymmetricKey (_key);
		}

		public string EncryptString(string text) {
			if (string.IsNullOrEmpty (text))
				return null;
			ICryptographicKey symetricKey = CreateSymmetricKey ();
			var bytes = WinRTCrypto.CryptographicEngine.Encrypt (symetricKey, Encoding.UTF8.GetBytes (text));
			return System.Convert.ToBase64String (bytes);
		}

		public string DecryptString(string text) {
			if (string.IsNullOrEmpty (text))
				return null;
			byte[] input = System.Convert.FromBase64String(text);
			ICryptographicKey symetricKey = CreateSymmetricKey ();
			var bytes = WinRTCrypto.CryptographicEngine.Decrypt (symetricKey, input);
			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}
	}
}

//using System;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;
//
//namespace SirenOfShame.PclLib
//{
//    public class TripleDesStringEncryptor
//    {
//        public string EncryptString(string text)
//        {
//            ICryptoTransform transform = _provider.CreateEncryptor(_key, _iv);
//
//            if (string.IsNullOrWhiteSpace(text))
//            {
//                return null;
//            }
//            using (MemoryStream stream = new MemoryStream())
//            {
//                using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
//                {
//                    byte[] input = Encoding.UTF8.GetBytes(text);
//                    cryptoStream.Write(input, 0, input.Length);
//                    cryptoStream.FlushFinalBlock();
//                    return Convert.ToBase64String(stream.ToArray());
//                }
//            }
//        }
//
//        public string DecryptString(string text)
//        {
//            ICryptoTransform transform = _provider.CreateDecryptor(_key, _iv);
//
//            if (string.IsNullOrWhiteSpace(text))
//            {
//                return null;
//            }
//            using (MemoryStream stream = new MemoryStream())
//            {
//                using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
//                {
//                    byte[] input = Convert.FromBase64String(text);
//                    cryptoStream.Write(input, 0, input.Length);
//                    cryptoStream.FlushFinalBlock();
//                    return Encoding.UTF8.GetString(stream.ToArray());
//                }
//            }
//        }
//    }
//}