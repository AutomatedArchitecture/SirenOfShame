using System;

namespace UsbLib
{
    [Flags]
    public enum DiGetClassFlags : uint
    {
        Default = 0x00000001,  // only valid with DeviceInterface
        Present = 0x00000002,
        AllClasses = 0x00000004,
        Profile = 0x00000008,
        DeviceInterface = 0x00000010,
    }
}