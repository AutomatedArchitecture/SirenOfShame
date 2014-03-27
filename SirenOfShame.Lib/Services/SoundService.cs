using System.IO;
using System.Media;

namespace SirenOfShame.Lib.Services
{
    public class SoundService
    {
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
            player.Play();
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
    }
}
