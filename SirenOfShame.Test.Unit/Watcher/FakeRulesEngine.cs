using System;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Services;

namespace SirenOfShame.Test.Unit.Watcher
{
    class FakeRulesEngine : RulesEngine
    {
        public SosOnlineService MockSosOnlineService = new SosOnlineServiceDummy();

        public FakeRulesEngine(SirenOfShameSettings settings) : base(settings)
        {
        }

        DateTime _now = new DateTime(2010, 1, 2);

        public void SetNow(DateTime now)
        {
            _now = now;
        }
        
        protected override DateTime Now
        {
            get { return _now; }
        }

        protected override SosOnlineService SosOnlineService
        {
            get { return MockSosOnlineService; }
        }
    }
}
