using NUnit.Framework;

namespace SirenOfShame.Test.Unit.TestHelpers
{
    public static class AssertHelpers
    {
        public static void AreEquals(byte[] expected, int expectedOffset, byte[] found, int foundOffset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(expected[expectedOffset + i], found[foundOffset + i], "Data mismatch at location " + i + " (expected offset: " + (expectedOffset + i) + ", found offset: " + (foundOffset + i) + ")");
            }
        }
    }
}
