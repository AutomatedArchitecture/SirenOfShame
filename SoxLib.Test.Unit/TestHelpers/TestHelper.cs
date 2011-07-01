using System.IO;

namespace SoxLib.Test.Unit.TestHelpers
{
    public class TestHelper
    {
        public static string GetSolutionDirectory()
        {
            string dir = Path.GetDirectoryName(typeof(TestInit).Assembly.CodeBase.Substring("file:///".Length));
            dir = dir + @"\..\..\..\..\";
            dir = Path.GetFullPath(dir);
            return dir;
        }
    }
}
