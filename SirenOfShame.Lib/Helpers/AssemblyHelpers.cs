using System;
using System.IO;
using System.Reflection;

namespace SirenOfShame.Lib.Helpers
{
    public static class AssemblyHelpers
    {
        const int PeHeaderOffset = 60;
        const int LinkerTimestampOffset = 8;

        public static DateTime GetLinkerTimestamp(this Assembly assembly)
        {
            string filePath = assembly.Location;
            return GetTimestamp(filePath);
        }

        public static DateTime GetTimestamp(string filePath)
        {
            byte[] b = new byte[2048];
            Stream s = null;

            try
            {
                s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = BitConverter.ToInt32(b, PeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
    }
}
