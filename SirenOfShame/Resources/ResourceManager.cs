using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SirenOfShame.Resources2
{
    public class ResourceManager
    {
        public static IEnumerable<string> InternalAudioLocations
        {
            get { return Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(name => name.EndsWith(".wav")); }
        }

        public static IList<AudioFile> InternalAudioFiles
        {
            get { return InternalAudioLocations.Select(i => new AudioFile(i)).ToList(); }
        }
    }
}
