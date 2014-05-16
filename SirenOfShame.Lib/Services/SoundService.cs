﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using log4net;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Services
{
    public class SoundService
    {
        private readonly ILog _log = MyLogManager.GetLogger(typeof (SoundService));
        readonly AudioFileService _audioFileService = new AudioFileService();

        public static string InternalAudioLocationToDisplayName(string location)
        {
            string displayName = location;
            if (displayName.StartsWith("SirenOfShame.Resources.Audio-"))
            {
                displayName = displayName.Substring(29);
            }
            displayName = displayName.Replace("-", " ");
            if (displayName.EndsWith(".wav"))
            {
                displayName = displayName.Substring(0, displayName.Length - 4);
            }
            return displayName;
        }

        private bool IsInternal(string location)
        {
            return location.StartsWith("SirenOfShame.Resource");
        }

        public void Play(string location)
        {
            var player = GetSoundPlayer(location);
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        private SoundPlayer GetSoundPlayer(string location)
        {
            if (IsInternal(location))
            {
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(location);
                return new SoundPlayer(s);
            }
            
            return new SoundPlayer(location);
        }

        private string GetSoundsDirAndEnsureExists()
        {
            var sosAppDataFolder = SirenOfShameSettings.GetSosAppDataFolder();
            var soundsDir = Path.Combine(sosAppDataFolder, "Sounds");
            if (!Directory.Exists(soundsDir))
            {
                Directory.CreateDirectory(soundsDir);
            }
            return soundsDir;
        }

        public Sound AddSound(SirenOfShameSettings settings, string fileNameAndPath, string fileNameAndExtension)
        {
            var existingSound = settings.Sounds.FirstOrDefault(i => i.DisplayName == fileNameAndExtension);
            if (existingSound != null) DeleteSound(settings, existingSound);
            
            var soundsDir = GetSoundsDirAndEnsureExists();
            var fileNameAsWav = Path.ChangeExtension(fileNameAndExtension, "wav");
            var destinationFileNameAndPath = Path.Combine(soundsDir, fileNameAsWav);

            CopyOrConvertToWav(fileNameAndPath, destinationFileNameAndPath);

            Sound sound = new Sound
            {
                Location = destinationFileNameAndPath,
                DisplayName = fileNameAndExtension
            };

            settings.Sounds.Add(sound);
            settings.Save();
            return sound;
        }

        private void CopyOrConvertToWav(string sourceFileNameAndPath, string destinationFileNameAndPath)
        {
            var isWav = "wav".Equals(Path.GetExtension(sourceFileNameAndPath), StringComparison.CurrentCultureIgnoreCase);
            if (isWav)
            {
                File.Copy(sourceFileNameAndPath, destinationFileNameAndPath);
            }
            else
            {
                _audioFileService.ConvertToWav(sourceFileNameAndPath, destinationFileNameAndPath, highQuality: true);
            }
        }

        public void DeleteSound(SirenOfShameSettings settings, Sound sound)
        {
            try
            {
                settings.Sounds.Remove(sound);
                settings.Save();

                var sosAppDataFolder = SirenOfShameSettings.GetSosAppDataFolder();
                var fileIsInOurFolder = sound.Location.StartsWith(sosAppDataFolder);
                // should be, but we can't assume someone hasn't tampered with the settings file
                if (fileIsInOurFolder)
                {
                    File.Delete(sound.Location);
                }
            }
            catch (Exception ex)
            {
                _log.Warn("Unable to delete file", ex);
            }
        }

        public void Play(Sound sound)
        {
            Play(sound.Location);
        }
    }
}
