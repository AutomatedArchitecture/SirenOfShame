using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SirenOfShame
{
    // Adapted from: 
    class WaterMarkTextBox : TextBox
    {
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private string _watermarkText;
        public string WaterMarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                SetWatermark(_watermarkText);
            }
        }

        private void SetWatermark(string watermarkText)
        {
            SendMessage(Handle, EM_SETCUEBANNER, 0, watermarkText);
        }          
    }
}
