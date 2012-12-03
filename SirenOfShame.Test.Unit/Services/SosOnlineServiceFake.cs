using SirenOfShame.Lib.Services;
using SirenOfShame.Test.Unit.Watcher;

namespace SirenOfShame.Test.Unit.Services
{
    public class SosOnlineServiceFake : SosOnlineService
    {
        public FakeWebClient FakeWebClient = new FakeWebClient();

        protected override Lib.Network.SosWebClient GetWebClient()
        {
            return FakeWebClient;
        }
    }
}
