using System;
using System.IO;
using log4net;

namespace TeensyHidBootloaderLib
{
    public class IntelHexFile
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IntelHexFile));

        /// <summary>
        /// the maximum flash image size we can support
        /// chips with larger memory may be used, but only this
        /// much intel-hex data can be loaded into memory! 
        /// </summary>
        public const int MaxMemorySize = 0x10000;

        private readonly byte[] _firmwareImage = new byte[MaxMemorySize];
        private readonly byte[] _firmwareMask = new byte[MaxMemorySize];
        private bool _endRecordSeen;
        private int _byteCount;
        private uint _extendedAddr;

        public int ByteCount { get { return _byteCount; } }

        public IntelHexFile(Stream stream)
        {
            int i, lineno = 0;

            _byteCount = 0;
            _endRecordSeen = false;
            for (i = 0; i < MaxMemorySize; i++)
            {
                _firmwareImage[i] = 0xFF;
                _firmwareMask[i] = 0;
            }
            _extendedAddr = 0;

            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineno++;
                    try
                    {
                        ParseHexLine(line);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Parse error line " + lineno, ex);
                    }
                    if (_endRecordSeen)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// from ihex.c, at http://www.pjrc.com/tech/8051/pm2_docs/intel-hex.html
        /// parses a line of intel hex code, stores the data in bytes[]
        /// and the beginning address in addr, and returns a 1 if the
        /// line was valid, or a 0 if an error occured.  The variable
        /// num gets the number of bytes that were stored into bytes[]
        /// </summary>
        private void ParseHexLine(string line)
        {
            int cksum, i;
            string ptr = line;

            int num = 0;
            if (line.Length < 11)
            {
                throw new Exception("Invalid line length. Expected at least 11 found " + line.Length);
            }
            if (line[0] != ':')
            {
                throw new Exception("Invalid start of line. Expected ':' found '" + line[0] + "'");
            }
            ptr = ptr.Substring(1);
            int len = ReadHex(ref ptr, 2);
            if (line.Length < (11 + (len * 2)))
            {
                throw new Exception("Invalid line length. Expected at least " + (11 + (len * 2)) + " found " + line.Length);
            }
            int addr = ReadHex(ref ptr, 4);
            Log.Debug("Line: length=" + len + " Addr=" + addr);
            int code = ReadHex(ref ptr, 2);
            if (addr + _extendedAddr + len >= MaxMemorySize)
            {
                throw new Exception("Address out of range. Address: " + (addr + _extendedAddr + len) + " expected less than " + MaxMemorySize);
            }
            int sum = (len & 255) + ((addr >> 8) & 255) + (addr & 255) + (code & 255);
            if (code != 0)
            {
                if (code == 1)
                {
                    _endRecordSeen = true;
                    return;
                }
                if (code == 2 && len == 2)
                {
                    i = ReadHex(ref ptr, 4);
                    sum += ((i >> 8) & 255) + (i & 255);
                    cksum = ReadHex(ref ptr, 2);
                    if ((((sum & 255) + (cksum & 255)) & 255) != 0)
                    {
                        throw new Exception("Invalid checksum " + cksum);
                    }
                    _extendedAddr = (uint)(i << 4);
                    Log.DebugFormat("ext addr = {0:08X}", _extendedAddr);
                }
                if (code == 4 && len == 2)
                {
                    i = ReadHex(ref ptr, 4);
                    sum += ((i >> 8) & 255) + (i & 255);
                    cksum = ReadHex(ref ptr, 2);
                    if ((((sum & 255) + (cksum & 255)) & 255) != 0)
                    {
                        throw new Exception("Invalid checksum " + cksum);
                    }
                    _extendedAddr = (uint)(i << 16);
                    Log.DebugFormat("ext addr = {0:08X}", _extendedAddr);
                }
                throw new Exception("No data line");
            }
            _byteCount += len;
            while (num != len)
            {
                i = ReadHex(ref ptr, 2);
                i &= 255;
                _firmwareImage[addr + _extendedAddr + num] = (byte)i;
                _firmwareMask[addr + _extendedAddr + num] = 1;
                ptr += 2;
                sum += i;
                (num)++;
                if (num >= 256)
                {
                    throw new Exception("To many bytes on a line");
                }
            }
            cksum = ReadHex(ref ptr, 2);
            if ((((sum & 255) + (cksum & 255)) & 255) != 0)
            {
                throw new Exception("Invalid checksum " + cksum);
            }
        }

        private int ReadHex(ref string line, int count)
        {
            if (line.Length < count)
            {
                throw new Exception("Not enough characters to read");
            }
            string hex = line.Substring(0, count);
            line = line.Substring(count);
            return Convert.ToInt32(hex, 16);
        }

        public bool BytesWithinRange(int begin, int end)
        {
            int i;

            if (begin < 0 || begin >= MaxMemorySize ||
               end < 0 || end >= MaxMemorySize)
            {
                return false;
            }
            for (i = begin; i <= end; i++)
            {
                if (_firmwareMask[i] != 0) return true;
            }
            return false;
        }

        public void GetData(int addr, int len, byte[] bytes, int offset)
        {
            int i;

            if (addr < 0 || len < 0 || addr + len >= MaxMemorySize)
            {
                for (i = 0; i < len; i++)
                {
                    bytes[i + offset] = 255;
                }
                return;
            }
            for (i = 0; i < len; i++)
            {
                if (_firmwareMask[addr] != 0)
                {
                    bytes[i + offset] = _firmwareImage[addr];
                }
                else
                {
                    bytes[i + offset] = 255;
                }
                addr++;
            }
        }
    }
}
