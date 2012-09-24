using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public enum GettingStartedClickTypeEnum
    {
        ConfigureServer,
        ConfigureSosOnline,
        NeverShowGettingStarted
    }
    
    public partial class GettingStarted : UserControl
    {
        public event GettingStartedClick OnGettingStartedClick;

        public void Initialize(SirenOfShameSettings settings)
        {
            bool anyServers = settings.CiEntryPointSettings.Any();
            _configureCiServer.ImageKey = anyServers ? "server_checkbox_checked.bmp" : "server.bmp";
        }

        public GettingStarted()
        {
            InitializeComponent();
        }

        private void InvokeOnGettingStartedClick(GettingStartedClickTypeEnum clickType)
        {
            GettingStartedClick handler = OnGettingStartedClick;
            if (handler != null) handler(this, new GettingStartedOpenDialogArgs { GettingStartedClickType = clickType });
        }

        private void ConfigureCiServerClick(object sender, System.EventArgs e)
        {
            InvokeOnGettingStartedClick(GettingStartedClickTypeEnum.ConfigureServer);
        }

        private void SosOnlineClick(object sender, System.EventArgs e)
        {
            InvokeOnGettingStartedClick(GettingStartedClickTypeEnum.ConfigureSosOnline);
        }

        private void HideCheckedChanged(object sender, System.EventArgs e)
        {
            InvokeOnGettingStartedClick(GettingStartedClickTypeEnum.NeverShowGettingStarted);
        }

    }

    public delegate void GettingStartedClick(object sender, GettingStartedOpenDialogArgs args);

    public class GettingStartedOpenDialogArgs
    {
        public GettingStartedClickTypeEnum GettingStartedClickType { get; set; }
    }
}