using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoxLib.Test.Unit
{
    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            BasicConfigurator.Configure();
        }
    }
}
