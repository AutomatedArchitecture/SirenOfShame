using System;
using System.Collections.Generic;
using System.Windows;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using log4net;

namespace WallOfShame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static ILog _log;
        private bool _mockSoS;

        public ISirenOfShameDevice SirenOfShameDevice { get; private set; }
        public bool FullScreen { get; private set; }

        public static App CurrentApp
        {
            get { return (App)Current; }
        }

        private void Init(IEnumerable<string> args)
        {
            try
            {
                FullScreen = true;
                _log = MyLogManager.GetLogger(typeof(App));
                ProcessCommandLineArguments(args);

                if (_mockSoS)
                {
                    SirenOfShameDevice = new MockSirenOfShameDevice();
                }
                else
                {
                    SirenOfShameDevice = new SirenOfShameDevice();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        private void ProcessCommandLineArguments(IEnumerable<string> args)
        {
            if (args == null)
            {
                return;
            }
            foreach (string s in args)
            {
                if (s == "/nofullscreen")
                {
                    FullScreen = false;
                }
                else if (s == "/mocksos")
                {
                    _mockSoS = true;
                }
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Init(e.Args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Siren of Shame Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
