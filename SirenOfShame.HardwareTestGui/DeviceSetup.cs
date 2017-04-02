// ReSharper disable InconsistentNaming
using System;
using System.ComponentModel;
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
        public event InstallFirmwareDelegate OnInstallFirmware;

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
            Verify();
        }

        private bool Verify()
        {
            var results = RunStk500("info");
            var success = results.Contains("Signature:  0x1e958a");
            SetResult(_verifyResults, success);
            return success;
        }

        private void _erase_Click(object sender, EventArgs e)
        {
            Erase();
        }

        private bool Erase()
        {
            var results = RunStk500("chiperase");
            var success = results.Contains("Chiperase completed successfully");
            SetResult(_eraseResults, success);
            return success;
        }

        private void _setFuses_Click(object sender, EventArgs e)
        {
            SetFuses();
        }

        private bool SetFuses()
        {
            // Set Fuse LOW=DE High=D0 Extended = FC
            var results = RunStk500("write -fs --values 0xded0fc");
            var success = results.Contains("Write completed successfully.");
            SetResult(_setFusesResult, success);
            return success;
        }

        private void _verifyFuses_Click(object sender, EventArgs e)
        {
            VerifyFuses();
        }

        private bool VerifyFuses()
        {
            var results = RunStk500("verify -fs --values ded0fc");
            var success = results.Contains("Verification OK");
            SetResult(_verifyFusesResults, success);
            return success;
        }

        private void _writeBootloader_Click(object sender, EventArgs e)
        {
            WriteBootloader();
        }

        private bool WriteBootloader()
        {
            // File format: "Intel Extended HEX"; Program device type = "FLASH"
            var results = RunStk500("program -fl -f \"" + _bootloaderFilename.Text + "\"");
            var success = results.Contains("Programming completed successfully.");
            SetResult(_writeBootloaderResults, success);
            return success;
        }

        private void _verifyBootloader_Click(object sender, EventArgs e)
        {
            VerifyBootloader();
        }

        private bool VerifyBootloader()
        {
            var results = RunStk500("verify -fl -f \"" + _bootloaderFilename.Text + "\"");
            var success = results.Contains("Verification OK");
            SetResult(_verifyBootloaderResults, success);
            return success;
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
                const string defaultParameters = "-t avrispmk2 -d Atmega32u2 -i ISP ";
                const string fileName = @"C:\Program Files (x86)\Atmel\Studio\7.0\atbackend\atprogram.exe";
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
                try
                {
                    if (!process.Start())
                    {
                        throw new Exception("Could not start process \"" + fileName + "\"");
                    }
                } catch (Win32Exception)
                {
                    throw new Exception("Need to install AVR Command Line Tools: http://www.atmel.no/beta_ware/AVRCommandLineTools/AVRCommandLineTools.exe");
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

        private void _runGambit_Click(object sender, EventArgs e)
        {
            RunTheGambit();
        }

        public void RunTheGambit()
        {
            ResetResults();
            if (!Verify()) return;
            if (!Erase()) return;
            if (!SetFuses()) return;
            if (!VerifyFuses()) return;
            if (!WriteBootloader()) return;
            if (OnInstallFirmware != null) OnInstallFirmware(this, new InstallFirmwareDelegateArgs());
        }
    }

    public delegate void InstallFirmwareDelegate(object sender, InstallFirmwareDelegateArgs args);

    public class InstallFirmwareDelegateArgs
    {
    }
}
