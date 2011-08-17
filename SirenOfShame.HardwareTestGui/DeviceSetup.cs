using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SirenOfShame.HardwareTestGui.Properties;
using SirenOfShame.Lib;
using log4net;

namespace SirenOfShame.HardwareTestGui
{
    public partial class DeviceSetup : UserControl
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(DeviceSetup));

        public DeviceSetup()
        {
            InitializeComponent();
            if (!DesignMode && Program.SirenOfShameDevice != null)
            {
                _bootloaderFilename.Text = Settings.Default.BootloaderLocation;

                ResetResults();
            }
        }

        private void ResetResults()
        {
            _verifyResults.Text = "";
            _eraseResults.Text = "";
            _setFusesResult.Text = "";
            _verifyFusesResults.Text = "";
            _writeBootloaderResults.Text = "";
            _verifyBootloaderResults.Text = "";
            _output.Text = "";
        }

        private void _verify_Click(object sender, EventArgs e)
        {
            var results = RunStk500("-s");
            var success = results.Contains("Signature is 0x1E 0x95 0x8A");
            SetResult(_verifyResults, success);
        }

        private void _erase_Click(object sender, EventArgs e)
        {
            var results = RunStk500("-e");
            var success = results.Contains("Device erased");
            SetResult(_eraseResults, success);
        }

        private void _setFuses_Click(object sender, EventArgs e)
        {
            var results = RunStk500("-fD0DE -EFC");
            var success = results.Contains("Fuse bits programmed");
            SetResult(_setFusesResult, success);
        }

        private void _verifyFuses_Click(object sender, EventArgs e)
        {
            var results = RunStk500("-FD0DE -GFC");
            var success = results.Contains("Fuse bits verified successfully");
            SetResult(_verifyFusesResults, success);
        }

        private void _writeBootloader_Click(object sender, EventArgs e)
        {
            var results = RunStk500("\"-if" + _bootloaderFilename.Text + "\" -pf");
            var success = results.Contains("FLASH programmed");
            SetResult(_writeBootloaderResults, success);
        }

        private void _verifyBootloader_Click(object sender, EventArgs e)
        {
            var results = RunStk500("\"-if" + _bootloaderFilename.Text + "\" -vf");
            var success = results.Contains("FLASH verified successfully");
            SetResult(_verifyBootloaderResults, success);
        }

        private void SetResult(Label results, bool success)
        {
            if (success)
            {
                results.ForeColor = Color.Green;
                results.Text = "OK";
            }
            else
            {
                results.ForeColor = Color.Red;
                results.Text = "FAIL";
            }
        }

        private string RunStk500(string additionalParameters)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                const string defaultParameters = "-dAtmega32u2 -cUSB ";
                const string fileName = @"C:\Program Files (x86)\Atmel\AVR Tools\STK500\stk500.exe";
                string cmdLineArgs = defaultParameters + additionalParameters;

                _log.Debug("Running stk500: " + fileName + " " + cmdLineArgs);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = cmdLineArgs,
                    UseShellExecute = false,
                    ErrorDialog = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                Process process = new Process
                {
                    StartInfo = startInfo
                };
                if (!process.Start())
                {
                    throw new Exception("Could not start process \"" + fileName + "\"");
                }
                StreamReader outputReader = process.StandardOutput;
                StreamReader errorReader = process.StandardError;
                process.WaitForExit();

                string output = outputReader.ReadToEnd();
                _log.Debug("Standard Output:\n" + output);

                string error = errorReader.ReadToEnd();
                _log.Error("Standard Error:\n" + error);

                string results = output + "\n" + error;
                _output.Text = results;
                return results;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void _clearResults_Click(object sender, EventArgs e)
        {
            ResetResults();
        }

        private void _browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "HEX File (*.hex)|*.hex"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _bootloaderFilename.Text = dlg.FileName;
                Settings.Default.BootloaderLocation = dlg.FileName;
                Settings.Default.Save();
            }
        }
    }
}
