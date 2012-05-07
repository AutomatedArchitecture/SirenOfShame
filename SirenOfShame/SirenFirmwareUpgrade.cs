using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using log4net;

namespace SirenOfShame
{
    public partial class SirenFirmwareUpgrade : FormBase
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(SirenFirmwareUpgrade));

        private byte[] _hexFileData;
        private bool _upgrading;
        private int _newVersionNumber;

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        public SirenFirmwareUpgrade()
        {
            InitializeComponent();
            IocContainer.Instance.Compose(this);
        }

        private void SirenFirmwareUpgrade_Load(object sender, EventArgs e)
        {
            SosMessageBox.Show("Disconnect Device",
                               "To perform upgrade 1. Disconnect your Siren of Shame device. 2. Select file.  3. Click upgrade. 4. When the dialog says 'Please connect your Siren of Shame' then, uh yea, do it",
                               "Got it");
        }

        private int LoadHexFileData(string path)
        {
            try
            {
                using (var fileStream = File.OpenRead(path))
                {
                    var xml = XDocument.Load(fileStream);
                    if (xml.Root == null)
                    {
                        throw new Exception("No root element");
                    }

                    // version
                    var versionElem = xml.Root.Element("version");
                    if (versionElem == null)
                    {
                        throw new Exception("Could not find 'version' element in XML");
                    }
                    int version;
                    if (!int.TryParse(versionElem.Value, out version))
                    {
                        throw new Exception("Could not parse version number '" + versionElem.Value + "'");
                    }

                    // hex
                    var hexElem = xml.Root.Element("hex");
                    if (hexElem == null)
                    {
                        throw new Exception("Could not find 'hex' element in XML");
                    }
                    _hexFileData = Encoding.ASCII.GetBytes(hexElem.Value.Trim());
                    _newVersion.Text = version.ToString(CultureInfo.InvariantCulture);

                    return version;
                }
            }
            catch (Exception ex)
            {
                _newVersion.Text = "Could not load firmware";
                Log.Error("Could not load firmware file", ex);
                ExceptionMessageBox.Show(this, "Error Loading Upgrade", "Could not load upgrade file", ex);
                return 0;
            }
        }

        private void EnableAllButtons(bool enable)
        {
            _upgrade.Enabled = enable;
            _selectFile.Enabled = enable;
        }
        
        private void _upgrade_Click(object sender, EventArgs e)
        {
            if (SirenOfShameDevice.IsConnected)
            {
                SosMessageBox.Show("Disconnect",
                                   "Can't you follow directions or something?  Please disconnect your Siren of Shame, then click upgrade, then re-connect it when you are instructed to do so.  Geez.",
                                   "Ok, ok, got it");
                return;
            }
            
            EnableAllButtons(enable: false);
            _status.Text = "Waiting for connection...";
            _progressBar.Value = 0;
            DoUpgrade();
        }

        private void DoUpgrade()
        {
            _upgrading = true;
            var t = new Thread(DoUpgradeThread);
            t.Start();
        }

        private void DoUpgradeThread()
        {
            try
            {
                PerformUpgrade();
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Timeout"))
                {
                    SosMessageBox.Show("Timeout", "We never found a Siren of Shame device.  Did you connect one?", "Ok");
                }
                else
                {
                    ExceptionMessageBox.Show(this, "Error Performing Upgrade", "Error Performing Upgrade", ex);
                }
                
                Log.Error("Error performing upgrade", ex);
                _upgrading = false;
                Invoke(() =>
                {
                    EnableAllButtons(true);
                    _status.Text = "Click Upgrade when ready";
                    _progressBar.Value = 0;
                });
            }
        }

        private void PerformUpgrade()
        {
            using (var hexFileStream = new MemoryStream(_hexFileData))
            {
                SirenOfShameDevice.PerformFirmwareUpgrade(hexFileStream, UpdateProgress);
            }
            _upgrading = false;
            Invoke(() =>
            {
                Close();
                MessageBox.Show(this, "Upgrade Complete!", "Upgrade Complete!");
            });
        }

        private void UpdateProgress(int progress)
        {
            Invoke(() =>
            {
                Log.Info("Performing upgrade... " + _currentVersion.Text + " to " + _newVersion.Text + " (file size: " + _hexFileData.Length + ") progress: " + progress);
                _cancel.Enabled = false;

                _progressBar.Value = progress;
                if (progress <= 10)
                {
                    _status.Text = "Please connect your Siren of Shame now...";
                } 
                else if (progress == 100)
                {
                    _status.Text = "Upgrade Complete";
                    _upgrading = false;
                }
                else
                {
                    _status.Text = "Upgrading...";
                }
            });
        }

        private void SirenFirmwareUpgrade_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_upgrading)
            {
                MessageBox.Show(this, "In the middle of an upgrade. Click cancel or wait for the upgrade to finish.", "Cannot Close While Upgrading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }
        }

        private void _cancel_Click(object sender, EventArgs e)
        {
            if (!_upgrading)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void _selectFile_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            string fileName = openFileDialog1.FileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                _newVersionNumber = LoadHexFileData(fileName);
                var errorLoadingHexFile = _newVersionNumber == 0;
                if (errorLoadingHexFile) return;

                _upgrade.Enabled = true;

                int version = 0;
                if (SirenOfShameDevice.TryConnect())
                {
                    version = SirenOfShameDevice.FirmwareVersion;
                    _currentVersion.Text = version.ToString();
                }
                else
                {
                    _currentVersion.Text = "Not Connected";
                }

                if (_newVersionNumber == version)
                {
                    _upgrade.Enabled = false;
                    _status.Text = "Version " + version + " is already loaded";
                }
            }
        }
    }
}
