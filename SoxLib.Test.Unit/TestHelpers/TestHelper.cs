using System;
using System.IO;

namespace SoxLib.Test.Unit.TestHelpers
{
    public class TestHelper
    {
        public static string GetSolutionDirectory()
        {
            string dir = Path.GetFullPath(Path.GetDirectoryName(typeof(TestInit).Assembly.CodeBase.Substring("file:///".Length)));
            for (int i = 0; i < 100; i++)
            {
                if (File.Exists(Path.Combine(dir, "SirenOfShame.sln")))
                {
                    return dir;
                }
                dir = Path.GetFullPath(dir + @"\..");
            }
            throw new Exception("Could not find solution dir");
        }
    }
}
