using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using log4net;

namespace SirenOfShame.Lib
{
    public partial class ExceptionMessageBox : Form
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(ExceptionMessageBox));

        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        private Exception _realException;

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
                _realException = exception,
            };
            dlg._exception.Visible = false;
            if (owner != null)
                dlg.ShowDialog(owner);
            else
                dlg.ShowDialog();
        }

        private void OkClick(object sender, EventArgs e)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters["OperatingSystem"] = Environment.OSVersion.ToString();
            parameters["DotNetVersion"] = Environment.Version.ToString();
            parameters["SosVersion"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
            WebClient webClient = new WebClient();
            webClient.UploadValues("http://www.sirenofshame.com/ReportError", parameters);
            Close();
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
