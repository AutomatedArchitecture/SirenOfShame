using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class NapoleonTest
    {
        [TestMethod]
        public void CurrentUserHas100MorePointsThanAnyoneElse()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser", TotalBuilds = 110, FailedBuilds = 0 };
            var people = new List<PersonSetting> { 
                personSetting,
                new PersonSetting { RawName = "someoneElse", TotalBuilds = 10, FailedBuilds = 0 },
                new PersonSetting { RawName = "someoneElseElse", TotalBuilds = 5, FailedBuilds = 0 },
            };
            Assert.IsTrue(new Napoleon(personSetting, people).HasJustAchieved());
        }
        
        [TestMethod]
        public void OnlyOnePersonOnProject()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser", TotalBuilds = 110, FailedBuilds = 0 };
            var people = new List<PersonSetting> { 
                personSetting,
            };
            Assert.IsFalse(new Napoleon(personSetting, people).HasJustAchieved());
        }
        
        [TestMethod]
        public void CurrentUserHas99MorePointsThanAnyoneElse()
        {
            PersonSetting personSetting = new PersonSetting { RawName = "currentUser", TotalBuilds = 110, FailedBuilds = 0 };
            var people = new List<PersonSetting> { 
                personSetting,
                new PersonSetting { RawName = "someoneElse", TotalBuilds = 11, FailedBuilds = 0 },
                new PersonSetting { RawName = "someoneElseElse", TotalBuilds = 5, FailedBuilds = 0 },
            };
            Assert.IsFalse(new Napoleon(personSetting, people).HasJustAchieved());
        }
    }
}
