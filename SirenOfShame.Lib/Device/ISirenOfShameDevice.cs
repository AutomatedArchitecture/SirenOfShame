using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SirenOfShame.Lib.Device.SdCardFileSystem;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Device
{
    /// <summary>
    /// Interface representing the SoS device.
    /// <br/>
    /// To create a real instance of the device <code>ISirenOfShameDevice device = new SirenOfShameDevice();</code>
    /// <br/>
    /// To create a mock device <code>ISirenOfShameDevice device = new MockSirenOfShameDevice();</code>
    /// <br/>
    /// To automatically connect when the device is plugged in see <see cref="WndProc"/>.
    /// <seealso cref="SirenOfShameDevice"/>
    /// <seealso cref="MockSirenOfShameDevice"/>
    /// </summary>
    public interface ISirenOfShameDevice
    {
        /// <summary>
        /// Returns the connection state of the SoS device. 
        /// 
        /// If you are not calling <see cref="WndProc"/> this will not automatically update when the device is plugged in
        /// you will need to call <see cref="TryConnect"/>. If you are calling <see cref="WndProc"/> this property will
        /// update automatically when the device is plugged in.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets a list of audio patterns from the device.
        /// </summary>
        IEnumerable<AudioPattern> AudioPatterns { get; }

        /// <summary>
        /// Gets a list of LED patterns from the device.
        /// </summary>
        IEnumerable<LedPattern> LedPatterns { get; }

        /// <summary>
        /// Gets the firmware version of the connected SoS device.
        /// </summary>
        int FirmwareVersion { get; }

        /// <summary>
        /// Gets the hardware type of the connected SoS device. See <see cref="HardwareType"/> for a list of types.
        /// </summary>
        HardwareType HardwareType { get; }

        /// <summary>
        /// Gets the hardware version of the connected SoS device. This number will not change unless the hardware changes.
        /// </summary>
        int HardwareVersion { get; }

        /// <summary>
        /// Event fired when the SoS device is connected. This can be as a result of calling <see cref="TryConnect"/> or
        /// from a USB connection notification which requires calling <see cref="WndProc"/>.
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// Event fired when the SoS device is disconnected.
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Event fired while uploading audio/led patterns
        /// </summary>
        event UploadProgressEventHandler UploadProgress;
        
        /// <summary>
        /// Event fired when the upload of audio/led patterns has completed
        /// </summary>
        event UploadCompletedEventHandler UploadCompleted;
        
        /// <summary>
        /// Plays the given preloaded audio pattern on the device for the given amount of time. See <see cref="AudioPatterns"/>
        /// to get a list of audio patterns that can be passed to this method. This method can also stop a currently running pattern
        /// by passing in null to both parmeters.
        /// </summary>
        /// <param name="pattern">The audio pattern to play. This pattern must be retrieved from <see cref="AudioPatterns"/>.  null, if you would like to stop that pattern.</param>
        /// <param name="duration">The duration to play the audio.  null, if you would like to stop that pattern.</param>
        void PlayAudioPattern(AudioPattern pattern, TimeSpan? duration);

        /// <summary>
        /// Stops the currently playing audio.
        /// </summary>
        void StopAudioPattern();

        /// <summary>
        /// Plays the given preloaded LED pattern on the device for the given amount of time. See <see cref="LedPatterns"/>
        /// to get a list of LED patterns that can be passed to this method. This method can also stop a currently running pattern
        /// by passing in null to both parameters.
        /// </summary>
        /// <param name="pattern">The LED pattern to play. This pattern must be retrieved from <see cref="LedPatterns"/>. null, if you would like to stop the pattern.</param>
        /// <param name="duration">The duration to play the LED pattern. null, if you would like to stop that pattern.</param>
        void PlayLightPattern(LedPattern pattern, TimeSpan? duration);

        /// <summary>
        /// Stops the currently playing LED pattern.
        /// </summary>
        void StopLightPattern();

        /// <summary>
        /// Checks for USB connection notifications. This method must be called in order to receive automatic connection
        /// notifications when a SoS device is plugged in.
        /// 
        /// <code>
        /// public class MyCoolAppForm : Form {
        ///    ...
        ///    protected override void WndProc(ref Message m) {
        ///       SirenOfShameDevice.WndProc(ref m);
        ///       base.WndProc(ref m);
        ///    }
        ///    ...
        /// }
        /// </code>
        /// </summary>
        /// <param name="message">The message passed from the form.</param>
        void WndProc(ref Message message);

        /// <summary>
        /// For <see cref="HardwareType"/>.Pro this method will allow you to upload custome audio and LED patterns to the device.
        /// This method is syncronous and provies a progress callback to update progress bars, etc.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="audioPatterns">The list of audio patterns to upload.</param>
        /// <param name="ledPatterns">The list of LED patterns to upload.</param>
        Task UploadCustomPatternsAsync(SirenOfShameSettings settings, IList<AudioPatternSetting> audioPatterns, IList<UploadLedPattern> ledPatterns);

        /// <summary>
        /// Try to connect to a SoS device. Returns true if successful.
        /// This method will also fire the <see cref="Connected"/> event if it is successful.
        /// </summary>
        /// <returns>True, if successful.</returns>
        bool TryConnect();

        /// <summary>
        /// Upgrades the firmware of a connected device.
        /// </summary>
        /// <param name="hexFileStream">HEX file, AVR compiler output.</param>
        /// <param name="progressFunc">Call back method to get the progress of the upgrade. This method will be called with values 0-100 for percentage complete.</param>
        void PerformFirmwareUpgrade(Stream hexFileStream, Action<int> progressFunc);

        /// <summary>
        /// Reads additional device information. See <see cref="SirenOfShameInfo"/> for a description of what may be returned.
        /// </summary>
        /// <returns>A <see cref="SirenOfShameInfo"/> filled in with additional device information.</returns>
        SirenOfShameInfo ReadDeviceInfo();

        /// <summary>
        /// Forces a disconnect of the device.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Manually control the LEDs on the device.
        /// </summary>
        /// <param name="manualControlData">Manual control data.</param>
        void ManualControl(ManualControlData manualControlData);
    }
}
