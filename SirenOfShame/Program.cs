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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
