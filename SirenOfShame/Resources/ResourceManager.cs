using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Resources2
{
    public class ResourceManager
    {
        public static IEnumerable<string> InternalAudioLocations
        {
            get { return Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(name => name.EndsWith(".wav")); }
        }

        public static IEnumerable<Sound> InternalAudioFiles
        {
            get { return InternalAudioLocations.Select(i => new Sound(i)); }
        }
    }
}
