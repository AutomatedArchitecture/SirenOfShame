// ReSharper disable InconsistentNaming
using NUnit.Framework;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Settings
{
    [TestFixture]
    public class PersonSettingTest
    {
        [Test]
        public void GetBothDisplayAndRawNames_RawNameNoDisplayName_OnlyShowsRawName()
        {
            var personSetting = new PersonSetting
                {
                    RawName = "dev\\lrichard",
                    DisplayName = "dev\\lrichard"
                };
            Assert.AreEqual("dev\\lrichard", personSetting.GetBothDisplayAndRawNames());
        }
        
        [Test]
        public void GetBothDisplayAndRawNames_NullDisplayName_OnlyShowsRawName()
        {
            var personSetting = new PersonSetting
                {
                    RawName = "dev\\lrichard",
                    DisplayName = null
                };
            Assert.AreEqual("dev\\lrichard", personSetting.GetBothDisplayAndRawNames());
        }
        
        [Test]
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
