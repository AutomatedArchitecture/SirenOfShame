using System;
using System.Windows.Forms;

namespace UsbLib
{
    public class UsbDeviceNotification
    {
        private const int WM_DEVICECHANGE = 0x0219;

        public event EventHandler UsbDeviceArrived;

        public void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case WM_DEVICECHANGE:
                    OnUsbDeviceArrived();
                    break;
            }
        }

        private void OnUsbDeviceArrived()
        {
            if (UsbDeviceArrived != null)
            {
                UsbDeviceArrived(this, new EventArgs());
            }
        }
    }
}
