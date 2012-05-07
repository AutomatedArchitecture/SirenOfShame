using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using log4net;

namespace SirenOfShame.Lib
{
    public partial class ExceptionMessageBox : Form
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ExceptionMessageBox));
        SirenOfShameSettings _settings = SirenOfShameSettings.GetAppSettings();

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        private Exception _realException;
        private NameValueCollection _parameters;

        public ExceptionMessageBox()
        {
            IocContainer.Instance.Compose(this);
            InitializeComponent();
            ClientSize = new Size(ClientSize.Width, _ok.Bottom + 10);
        }

        public static void Show(Control owner, string title, string message, Exception exception)
        {
            if (owner != null && owner.InvokeRequired)
            {
                owner.Invoke((Action)(() => DoShow(owner, title, message, exception)));
            }
            else
            {
                DoShow(owner, title, message, exception);
            }
        }

        private static void DoShow(Control owner, string title, string message, Exception exception)
        {
            var dlg = new ExceptionMessageBox
            {
                Text = title,
                _message = { Text = message },
                _exception = { Text = exception.ToString() },
                _realException = exception
            };
            dlg._parameters = dlg.GetExceptionDetailsAndLog();
            dlg._exception.Visible = false;
            if (owner != null)
                dlg.ShowDialog(owner);
            else
                dlg.ShowDialog();
        }

        private void OkClick(object sender, EventArgs e)
        {
            try
            {
                var url = "http://www.sirenofshame.com/ReportError";
                _log.Info("Sending exception to: " + url);
                WebClient webClient = new WebClient();
                if (!string.IsNullOrEmpty(_contact.Text))
                {
                    _parameters.Add("ContactInfo", _contact.Text);
                }
                webClient.UploadValues(url, _parameters);
                MessageBox.Show(this, "Your error has been sent. We'll get right on that.", "Error Sent");
                _log.Debug("Exception sent");
            }
            catch (Exception exOuter)
            {
                _log.Error("Failed to send message", exOuter);
            }
            Close();
        }

        private NameValueCollection GetExceptionDetailsAndLog()
        {
            NameValueCollection parameters = new NameValueCollection();
            try
            {
                parameters["OperatingSystem"] = Environment.OSVersion.ToString();
                parameters["DotNetVersion"] = Environment.Version.ToString();
                parameters["SosVersion"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                parameters["SoftwareInstanceId"] = string.Format("{0}", _settings.SoftwareInstanceId);
                try
                {
                    parameters["DeviceConnected"] = SirenOfShameDevice.IsConnected.ToString();
                    if (SirenOfShameDevice.IsConnected)
                    {
                        parameters["FirmwareVersion"] = SirenOfShameDevice.FirmwareVersion.ToString();
                        parameters["HardwareVersion"] = SirenOfShameDevice.HardwareVersion.ToString();
                        parameters["HardwareType"] = SirenOfShameDevice.HardwareType.ToString();
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("Failed to find device info when sending error", ex);
                }
                parameters["ErrorDate"] = DateTime.Now.ToString();
                parameters["ErrorMessage"] = _realException.Message;
                parameters["StackTrace"] = _realException.ToString();
            }
            catch (Exception ex)
            {
                _log.Error("Could not get all parameters", ex);
            }

            foreach (var parameterName in parameters.AllKeys)
            {
                _log.Info(parameterName + ": " + parameters[parameterName]);
            }

            return parameters;
        }

        private void ShowMoreClick(object sender, EventArgs e)
        {
            _exception.Visible = true;
            _showMore.Visible = false;
            ClientSize = new Size(ClientSize.Width, 270);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
