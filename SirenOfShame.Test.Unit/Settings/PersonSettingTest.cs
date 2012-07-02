// ReSharper disable InconsistentNaming
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestClass]
    public class PersonSettingTest
    {
        [TestMethod]
        public void GetBothDisplayAndRawNames_RawNameNoDisplayName_OnlyShowsRawName()
        {
            var personSetting = new PersonSetting
                {
                    RawName = "dev\\lrichard",
                    DisplayName = "dev\\lrichard"
                };
            Assert.AreEqual("dev\\lrichard", personSetting.GetBothDisplayAndRawNames());
        }
        
        [TestMethod]
        public void GetBothDisplayAndRawNames_NullDisplayName_OnlyShowsRawName()
        {
            var personSetting = new PersonSetting
                {
                    RawName = "dev\\lrichard",
                    DisplayName = null
                };
            Assert.AreEqual("dev\\lrichard", personSetting.GetBothDisplayAndRawNames());
        }
        
        [TestMethod]
        public void GetBothDisplayAndRawNames_DisplayNameAndRawName_DisplaysBoth()
        {
            var personSetting = new PersonSetting
                {
                    RawName = "dev\\lrichard",
                    DisplayName = "Lee Richardson"
                };
            Assert.AreEqual("Lee Richardson (dev\\lrichard)", personSetting.GetBothDisplayAndRawNames());
        }
    }
}
