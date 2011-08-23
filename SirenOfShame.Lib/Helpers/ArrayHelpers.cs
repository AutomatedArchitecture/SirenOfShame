using System;

namespace SirenOfShame.Lib.Helpers
{
    public static class ArrayHelpers
    {
        public static T[] Repeat<T>(T[] data, int times)
        {
            T[] result = new T[data.Length * times];
            for (int i = 0; i < times; i++)
            {
                Array.Copy(data, 0, result, i * data.Length, data.Length);
            }
            return result;
        }
    }
}
