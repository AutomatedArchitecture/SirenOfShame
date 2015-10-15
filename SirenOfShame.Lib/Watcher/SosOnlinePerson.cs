using System.Windows.Forms;

namespace SirenOfShame.Lib.Watcher
{
    public class SosOnlinePerson : PersonBase
    {
        public override string RawName { get; set; }
        public override string DisplayName { get; set; }
        public int AvatarId { get; set; }
        public override int GetAvatarId(ImageList avatarImageList)
        {
            return AvatarId;
        }
    }
}