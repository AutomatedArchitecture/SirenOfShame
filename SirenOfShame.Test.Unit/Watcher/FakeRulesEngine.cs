using System;
using SirenOfShame.Lib.Network;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    class FakeRulesEngine : RulesEngine
    {
        private readonly FakeWebClient _fakeWebClient = new FakeWebClient();

        public FakeRulesEngine(SirenOfShameSettings settings) : base(settings)
        {
        }

        public FakeWebClient WebClient
        {
            get { return _fakeWebClient; }
        }

        protected override SosWebClient GetWebClient()
        {
            return _fakeWebClient;
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
    }
}
