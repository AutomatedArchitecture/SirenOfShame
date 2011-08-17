using System;
using System.IO;
using log4net;

namespace TeensyHidBootloaderLib
{
    public class TeensyHidBootloaderProgrammer
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IntelHexFile));
        private readonly int _codeSize;
        private readonly int _blockSize;

        public TeensyHidBootloaderProgrammer(McuType mcuType)
        {
            GetMcuParameters(mcuType, out _codeSize, out _blockSize);
        }

        public void Program(Stream hexFileStream, bool waitForDeviceToAppear, bool rebootAfterProgramming, TimeSpan timeOut, Action<int> progressFunc)
        {
            IntelHexFile hexFile = new IntelHexFile(hexFileStream);
            Log.DebugFormat("Read: {0} bytes, {1}% usage", hexFile.ByteCount, (double)hexFile.ByteCount / _codeSize * 100.0);

            Log.Debug("Opening device");
            using (var teensyDevice = new TeensyDevice(waitForDeviceToAppear, _blockSize, timeOut))
            {
                progressFunc(10);
                Log.Debug("Programming");
                Program(teensyDevice, hexFile, i => progressFunc(10 + (int)(i * 80.0 / 100.0)));
                progressFunc(90);

                // reboot to the user's new code
                if (rebootAfterProgramming)
                {
                    Reboot(teensyDevice);
                }
                progressFunc(100);
            }
        }

        private void Reboot(TeensyDevice teensyDevice)
        {
            byte[] buf = new byte[260];
            Log.Debug("Rebooting");
            buf[0] = 0xFF;
            buf[1] = 0xFF;
            teensyDevice.Write(buf, _blockSize + 2, 250);
        }

        private void Program(TeensyDevice teensyDevice, IntelHexFile hexFile, Action<int> progressFunc)
        {
            byte[] buf = new byte[260];
            int addr;
            bool first_block = true;
            for (addr = 0; addr < _codeSize; addr += _blockSize)
            {
                if (addr > 0 && !hexFile.BytesWithinRange(addr, addr + _blockSize - 1))
                {
                    // don't waste time on blocks that are unused,
                    // but always do the first one to erase the chip
                    continue;
                }
                Log.DebugFormat("addr: 0x{0:x} ({0}), length: 0x{1:x} ({1})", addr, _blockSize);
                if (_codeSize < 0x10000)
                {
                    buf[0] = (byte)(addr & 255);
                    buf[1] = (byte)((addr >> 8) & 255);
                }
                else
                {
                    buf[0] = (byte)((addr >> 8) & 255);
                    buf[1] = (byte)((addr >> 16) & 255);
                }
                hexFile.GetData(addr, _blockSize, buf, 2);
                teensyDevice.Write(buf, _blockSize + 2, first_block ? 3000 : 250);
                first_block = false;

                progressFunc((int)((double)addr / _codeSize * 100.0));
            }
        }

        private void GetMcuParameters(McuType mcuType, out int codeSize, out int blockSize)
        {
            switch (mcuType)
            {
                case McuType.ATMega32u2:
                case McuType.ATMega32u4:
                    codeSize = 32 * 1024;
                    blockSize = 128;
                    break;
                default:
                    throw new ArgumentException("Unhandled MCU type", "mcuType");
            }
        }
    }
}
