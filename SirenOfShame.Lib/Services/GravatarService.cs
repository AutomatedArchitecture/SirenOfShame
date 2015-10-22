using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Services
{
    public class GravatarService
    {
        private const int DEFAULT_IMAGE_SIZE = 48;

        public int DownloadImageFromWebAndAddToImageList(string imageUrl, ImageList avatarImageList)
        {
            if (avatarImageList.Images.ContainsKey(imageUrl))
                return avatarImageList.Images.IndexOfKey(imageUrl);

            int avatarId;
            var webClient = new WebClient();
            byte[] imageRaw = webClient.DownloadData(imageUrl);
            using (MemoryStream gravatarMs = new MemoryStream(imageRaw))
            {
                Image gravatarImage = Image.FromStream(gravatarMs);
                avatarImageList.Images.Add(imageUrl, gravatarImage);
                avatarId = avatarImageList.Images.Count - 1;
            }
            return avatarId;
        }

        public int DownloadGravatarFromEmailAndAddToImageList(string email, ImageList avatarImageList)
        {
            var imgSrc = GetImgSrc(email, DEFAULT_IMAGE_SIZE);
            return DownloadImageFromWebAndAddToImageList(imgSrc, avatarImageList);
        }

        public static string GetImgSrc(string email, int size)
        {
            string hash = GetMd5Hash(email);
            var rand = new Random(string.IsNullOrEmpty(email) ? 0 : email[0]).Next(1, 8);
            return string.Format("http://www.gravatar.com/avatar/{0}?s={1}&d=http%3A%2F%2Fsirenofshame.com%2FImages%2Fdude{2}.jpg",
                hash,
                size,
                rand);
        }

        private static string GetMd5Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(input);
            bs = md5.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
    }
}
