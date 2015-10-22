using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class AvatarPicker : Form
    {
        private readonly ImageList _avatarImageList;
        private readonly PersonSetting _personSetting;
        private bool _okToClose;
        public event AvatarClicked OnAvatarClicked;

        private void InvokeOnOnAvatarClicked()
        {
            var handler = OnAvatarClicked;
            if (handler != null) handler(this, new AvatarClickedArgs());
        }

        public AvatarPicker(ImageList avatarImageList, PersonSetting personSetting)
        {
            _okToClose = true;
            _avatarImageList = avatarImageList;
            _personSetting = personSetting;
            InitializeComponent();
            emailTextbox.Text = personSetting.Email;
            if (personSetting.Email != null)
            {
                _gravatar.SetImage(personSetting, avatarImageList);
                tabControl1.SelectedIndex = 1;
            }
            if (personSetting.AvatarImageName != null)
            {
                _croppedCustom.ImageLocation = Path.Combine(SirenOfShameSettings.GetAvatarsFolder(),
                    personSetting.AvatarImageName);
            }
            int avatarCount = SirenOfShameSettings.AVATAR_COUNT;
            for (int i = 0; i < avatarCount; i++)
            {
                Avatar avatar = new Avatar();
                avatar.SetImage(i, avatarImageList);
                avatar.Click += AvatarOnClick;
                flowLayoutPanel1.Controls.Add(avatar);
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            if (!DesignMode && _okToClose)
            {
                Close();
                Dispose();
            }
        }

        private void AvatarOnClick(object sender, EventArgs eventArgs)
        {
            Avatar avatar = (Avatar) sender;
            SelectAvatarAndClose(avatar.ImageIndex);
        }

        private void SelectAvatarAndClose(int? avatarId)
        {
            _personSetting.AvatarId = avatarId;
            InvokeOnOnAvatarClicked();
            Close();
            Dispose();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            var gravatarService = new GravatarService();
            var avatarId = gravatarService.DownloadGravatarFromEmailAndAddToImageList(emailTextbox.Text, _avatarImageList);
            _gravatar.SetImage(avatarId, _avatarImageList);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _personSetting.Email = emailTextbox.Text;
            SelectAvatarAndClose(null);
        }

        private void SelectImage_Click(object sender, EventArgs e)
        {
            SelectCustomImage();
        }

        private void CustomPictureBox_Click(object sender, EventArgs e)
        {
            SelectCustomImage();
        }

        private void SaveCustomImage_Click(object sender, EventArgs e)
        {
            var avatarsFolder = SirenOfShameSettings.GetAvatarsFolder();
            var newFileName = Guid.NewGuid() + ".png";
            var combine = Path.Combine(avatarsFolder, newFileName);
            _croppedCustom.Image.Save(combine);
            _personSetting.AvatarImageName = newFileName;
            SelectAvatarAndClose(null);
        }

        private void SelectCustomImage()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG",
                RestoreDirectory = true
            };

            _okToClose = false;
            try
            {
                var dialogResult = openFileDialog1.ShowDialog();
                if (dialogResult != DialogResult.OK) return;
            }
            finally
            {
                _okToClose = true;
            }
            
            var image = Image.FromFile(openFileDialog1.FileName);
            _croppedCustom.Image = Resize(image);
        }

        /// <summary>
        /// http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp
        /// </summary>
        private new Bitmap Resize(Image image)
        {
            int width = 48;
            int height = 48;
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }

    public delegate void AvatarClicked(object sender, AvatarClickedArgs args);

    public class AvatarClickedArgs
    {
    }
}
