namespace SirenOfShame.Resources2
{
    public class AudioFile
    {
        public AudioFile()
        {

        }

        public AudioFile(string location)
        {
            Location = location;
            DisplayName = LocationToDisplayName(location);
        }

        public static string LocationToDisplayName(string location)
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

        public string Location { get; set; }
        public string DisplayName { get; set; }
    }
}