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
    }
}
