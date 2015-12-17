using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using log4net;
using SirenOfShame.Lib;
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
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(AvatarPicker));
        private readonly ImageService _imageService;

        private void InvokeOnOnAvatarClicked()
        {
            var handler = OnAvatarClicked;
            if (handler != null) handler(this, new AvatarClickedArgs());
        }

        public AvatarPicker(ImageList avatarImageList, PersonSetting personSetting)
        {
            _imageService = new ImageService();
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
                tabControl1.SelectedIndex = 2;
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
                CloseAndDispose();
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
            CloseAndDispose();
        }

        private void CloseAndDispose()
        {
            Close();
            Dispose();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailTextbox.Text)) return;

            var gravatarService = new GravatarService();
            var avatarId = gravatarService.DownloadGravatarFromEmailAndAddToImageList(emailTextbox.Text, _avatarImageList);
            _gravatar.SetImage(avatarId, _avatarImageList);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailTextbox.Text))
            {
                CloseAndDispose();
                return;
            }

            _personSetting.Email = emailTextbox.Text;
            SelectAvatarAndClose(null);
        }

        private void SelectImage_Click(object sender, EventArgs e)
        {
            SelectCustomImage();
        }

        private void SaveCustomImage_Click(object sender, EventArgs e)
        {
            if (_croppedCustom.Image == null)
            {
                CloseAndDispose();
                return;
            }
            var avatarsFolder = SirenOfShameSettings.GetAvatarsFolder();
            var newFileName = Guid.NewGuid() + ".png";
            var combine = Path.Combine(avatarsFolder, newFileName);
            _croppedCustom.Image.Save(combine);
            _personSetting.AvatarImageName = newFileName;
            _personSetting.AvatarImageUploaded = false;
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
            _croppedCustom.Image = _imageService.Resize(image);
        }

        private void ImportFromAd_Click(object sender, EventArgs e)
        {
            try
            {
                var activeDirectoryService = new ActiveDirectoryService();
                var picture = activeDirectoryService.GetUserPicture(_adUser.Text, _adDomain.Text);
                _croppedCustom.Image = _imageService.Resize(picture);
            }
            catch (Exception ex)
            {
                _log.Error("Error importing from active directory", ex);
                SetErrorMessage(ex);
            }
        }

        private void SetErrorMessage(Exception ex)
        {
            _errorMessage.Text = ex.ToString();
            _errorMessage.Visible = true;
        }

        private void CroppedCustom_Click(object sender, EventArgs e)
        {
            SelectCustomImage();
        }

        private async void GetFromUrl_Click(object sender, EventArgs e)
        {
            try
            {
                var requestUri = new Uri(_url.Text);
                HttpClient client = new HttpClient();
                var imageStream = await client.GetStreamAsync(requestUri);
                Image picture = Image.FromStream(imageStream);
                _croppedCustom.Image = _imageService.Resize(picture);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
            }
        }
    }

    public delegate void AvatarClicked(object sender, AvatarClickedArgs args);

    public class AvatarClickedArgs
    {
    }
}
