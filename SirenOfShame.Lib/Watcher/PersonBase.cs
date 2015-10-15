using System.Windows.Forms;

namespace SirenOfShame.Lib.Watcher
{
    public abstract class PersonBase
    {
        public abstract string RawName { get; set; }
        public abstract string DisplayName { get; set; }
        public abstract int AvatarId { get; set; }

        public bool Clickable
        {
            get { return RawName != null; }
        }

        public abstract int GetAvatarId(ImageList avatarImageList);
    }
}