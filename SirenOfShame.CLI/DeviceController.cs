using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SirenOfShame.Lib.Device;

namespace SirenOfShame.CLI
{
    public class DeviceController
    {
        ISirenOfShameDevice _sirenOfShameDevice;

        public DeviceController() {
            this._sirenOfShameDevice = new SirenOfShameDevice();
            this._sirenOfShameDevice.Connected += sirenOfShameDevice_Connected;
            this._sirenOfShameDevice.Disconnected += sirenOfShameDevice_Disconnected;

        }

        public void PlayAlarm(TimeSpan timeSpan, int? ledPatternIndex, int? audioPatternIndex)
        {
            var ledPatterns = this._sirenOfShameDevice.LedPatterns.ToArray();
            if (ledPatternIndex.HasValue)
            {
                this._sirenOfShameDevice.PlayLightPattern(ledPatterns[ledPatternIndex.Value % ledPatterns.Length], timeSpan);
            }
            var audioPattern = this._sirenOfShameDevice.AudioPatterns.ToArray();
            if (audioPatternIndex.HasValue)
            {
                this._sirenOfShameDevice.PlayAudioPattern(audioPattern[audioPatternIndex.Value % audioPattern.Length], timeSpan);
            }
        }

        public bool TryConnect()
        {
            return this._sirenOfShameDevice.TryConnect();
        }

        public static void Log(string message)
        {
            Console.Error.WriteLine(message);
        }


        protected void sirenOfShameDevice_Disconnected(object sender, EventArgs e)
        {
            Log("Device disconnected.");
        }

        protected void sirenOfShameDevice_Connected(object sender, EventArgs e)
        {
            Log("Device connected.");
            var deviceInfo = _sirenOfShameDevice.ReadDeviceInfo();
            Log(deviceInfo.ToString());


        }
    }
}
