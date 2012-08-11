using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace MockCiServerServices
{
    public class MockWatcher : WatcherBase
    {
        private static MockCiServerForm _serverForm;

        private static MockCiServerForm ServerForm
        {
            get
            {
                if (_serverForm == null)
                {
                    Thread.Sleep(10000); // wait for the splash screen to disappear
                    Application.OpenForms.Cast<Form>().FirstOrDefault().Invoke(() =>
                    {
                        _serverForm = new MockCiServerForm();
                        _serverForm.Show();
                    });
                }
                return _serverForm;
            }
        }

        public MockWatcher(SirenOfShameSettings settings, MockCiEntryPoint mockCiEntryPoint)
            : base(settings)
        {

        }

        protected override IList<BuildStatus> GetBuildStatus()
        {
            return ServerForm.GetBuildStatus();
        }

        public override void StopWatching()
        {
            ServerForm.StopWatching();
        }

        public override void Dispose()
        {

        }
    }
}