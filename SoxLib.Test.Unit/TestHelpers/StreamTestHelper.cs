using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoxLib.Helpers;

namespace SoxLib.Test.Unit.TestHelpers
{
    public static class StreamTestHelper
    {
        public static void AssertAreEqual(Stream expected, Stream found, int tolerance)
        {
            var expectedByteArray = expected.ReadToEnd();
            var foundByteArray = found.ReadToEnd();
            Assert.AreEqual(expectedByteArray.Length, foundByteArray.Length, "Lengths do not match");
            for (int i = 0; i < expectedByteArray.Length; i++)
            {
                if (Math.Abs(expectedByteArray[i] - foundByteArray[i]) > tolerance)
                {
                    Assert.AreEqual(expectedByteArray[i], foundByteArray[i], "Data mismatch at location " + i);
                }
            }
        }
    }
}
