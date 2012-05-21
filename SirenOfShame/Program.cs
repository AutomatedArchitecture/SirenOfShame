using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Microsoft.VisualBasic.ApplicationServices;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Services;
using System.Reflection;

namespace SirenOfShame
{
    public class Program : WindowsFormsApplicationBase
    {
        private static ILog _log;
        private bool _startMinimized;
        private static MainForm _form;

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                RealMain(args);
            }
            catch (ReflectionTypeLoadException ex)
            {
                var firstError = ex.LoaderExceptions.FirstOrDefault();
                string message = firstError == null ? ex.Message : firstError.Message;
                _log.Error(message, ex);
                MessageBox.Show(message, "Siren of Shame Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                if (ex.Message.StartsWith("Access to the path") && ex.Message.EndsWith("is denied."))
                {
                    MessageBox.Show("There is a second instance of Siren of Shame running on this machine.  Please close the other instance and restart.");
                    return;
                }
                MessageBox.Show(ex.ToString(), "Siren of Shame Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RealMain(string[] args)
        {
            try
            {
                _log = MyLogManager.GetLogger(typeof(Program));
                Application.ThreadException += ApplicationThreadException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                new Program().Run(args);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            _log.Error("Global error", e.Exception);

            // see http://social.msdn.microsoft.com/Forums/en/vbgeneral/thread/628ba445-bdcf-4a9c-abcd-ac6e34b19a0d
            bool splashscreenBug = (e.Exception is InvalidOperationException) && e.Exception.Message == "Invoke or BeginInvoke cannot be called on a control until the window handle has been created.";
            if (splashscreenBug)
                return;

            ExceptionMessageBox.Show(null, "Drat", "Something crazy just happened.", e.Exception);
        }

        public static MainForm Form
        {
            get { return _form; }
        }

        public Program()
        {
            IsSingleInstance = true;
            EnableVisualStyles = true;
            StartupNextInstance += SirenOfShameApplication_StartupNextInstance;
        }

        private void SirenOfShameApplication_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            MainForm.Show();
            MainForm.WindowState = FormWindowState.Normal;
            e.BringToForeground = true;
        }

        protected override bool OnInitialize(ReadOnlyCollection<string> args)
        {
            bool mockSoS = false;
            bool showSplash = true;
            _startMinimized = false;
            if (args.Any(s => s == "/nosplash"))
            {
                showSplash = false;
            }
            if (args.Any(s => s == "/mocksos"))
            {
                mockSoS = true;
            }
            if (args.Any(s => s == "/min"))
            {
                _startMinimized = true;
            }

            _log.Debug(string.Format("OnInitialize() starting; mockSos = {0}; showSplash = {1}, startMinimized = {2}", mockSoS, showSplash, _startMinimized));

            // todo: we shouldn't need to do this
            IocContainer.Instance.Register(typeof(AudioFileService), new AudioFileService());
            IocContainer.Instance.Register(typeof(LedFileService), new LedFileService());

            if (mockSoS)
            {
                IocContainer.Instance.Register(typeof(ISirenOfShameDevice), new MockSirenOfShameDevice());
            }
            else
            {
                IocContainer.Instance.Register(typeof(ISirenOfShameDevice), new SirenOfShameDevice());
            }
            IocContainer.Instance.GetExport<ISirenOfShameDevice>().TryConnect();

            IocContainer.Instance.TryLogAssemblyVersions();

            if (showSplash)
            {
                SplashScreen = new SplashScreen();
            }
            return base.OnInitialize(args);
        }

        protected override void OnCreateMainForm()
        {
            MainForm = _form = new MainForm();
            if (_startMinimized)
            {
                MainForm.WindowState = FormWindowState.Minimized;
            }
            base.OnCreateMainForm();
        }
    }
}
