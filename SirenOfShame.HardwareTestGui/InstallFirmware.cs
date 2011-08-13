using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SirenOfShame.HardwareTestGui.Properties;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using log4net;

namespace SirenOfShame.HardwareTestGui
{
    public partial class InstallFirmware : UserControl
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(InstallFirmware));

        public InstallFirmware()
        {
            InitializeComponent();
            _firmwareFile.Text = Settings.Default.InstallFirmwareLocation;
            if (Program.SirenOfShameDevice != null)
            {
                Program.SirenOfShameDevice.Connected += SirenOfShameDevice_Connected;
                Program.SirenOfShameDevice.Disconnected += SirenOfShameDevice_Disconnected;
            }
        }

        void SirenOfShameDevice_Disconnected(object sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                _deviceInfo.Text = "Disconnected";
            });
        }

        private void SirenOfShameDevice_Connected(object sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                _deviceInfo.Text = Program.GetDeviceInfoAsString();
            });
        }

        private void _browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "HEX File (*.hex)|*.hex"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _firmwareFile.Text = dlg.FileName;
            }
        }

        private void _begin_Click(object sender, EventArgs e)
        {
            Settings.Default.InstallFirmwareLocation = _firmwareFile.Text;
            Settings.Default.Save();

            var t = new Thread(UpgradeThread);
            t.Start();

            _browse.Enabled = false;
            _firmwareFile.ReadOnly = true;
            _begin.Enabled = false;
        }

        private void UpgradeThread()
        {
            try
            {
                byte[] hexFileData = File.ReadAllBytes(_firmwareFile.Text);
                using (var hexFileStream = new MemoryStream(hexFileData))
                {
                    SetStatus("Waiting for device to connect.");
                    Program.SirenOfShameDevice.PerformFirmwareUpgrade(hexFileStream, UpdateProgress);
                    SetStatus("Upgrade complete.");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error performing upgrade", ex);
                SetStatus("Error installing firmware");
                ExceptionMessageBox.Show(this, "Error Performing Upgrade", "Error Performing Upgrade", ex);
            }
            finally
            {
                this.Invoke(() =>
                {
                    _begin.Enabled = true;
                });
            }
        }

        private void SetStatus(string message)
        {
            this.Invoke(() => { _status.Text = message; });
        }

        private void UpdateProgress(int progress)
        {
            this.Invoke(() =>
            {
                _progressBar.Value = progress;
            });
        }
    }
}
