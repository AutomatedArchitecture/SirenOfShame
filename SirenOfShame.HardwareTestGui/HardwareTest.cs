using System;
using System.Threading;
using System.Windows.Forms;

namespace SirenOfShame.HardwareTestGui
{
    public partial class HardwareTest : Form
    {
        public HardwareTest()
        {
            InitializeComponent();
            _deviceSetup.OnInstallFirmware += OnInstallFirmware;
            _fullTest.OnRunTheGambitClick += FullTestOnOnRunTheGambitClick;
        }

        private void FullTestOnOnRunTheGambitClick(object sender, RunTheGambitClickDelegateArgs args)
        {
            _tabControl.SelectTab(0);
            _deviceSetup.RunTheGambit();
        }

        private void OnInstallFirmware(object sender, InstallFirmwareDelegateArgs args)
        {
            _tabControl.SelectTab(1);
            _installFirmwarePage.BeginInstallFirmware(OnInstallFirmwareSuccess);
        }

        private void OnInstallFirmwareSuccess()
        {
            _tabControl.SelectTab(3);
            _fullTest.TabSelected();
        }
    }
}
