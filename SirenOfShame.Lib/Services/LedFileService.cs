using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace SirenOfShame.Lib.Services
{
    [Export(typeof(LedFileService))]
    public class LedFileService
    {
        public byte[] Read(string fileName)
        {
            try
            {
                string fileData = File.ReadAllText(fileName);
                return TextToPattern(fileData);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not read LED data from file '" + fileName + "'", ex);
            }
        }

        public byte[] TextToPattern(string text)
        {
            IEnumerable<byte[]> data = GetRows(text);
            MemoryStream result = new MemoryStream();
            foreach (var d in data)
            {
                result.Write(d, 0, d.Length);
            }
            return result.ToArray();
        }

        public IEnumerable<byte[]> GetRows(string text)
        {
            return text
                .Split('\n')
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => i.Trim(new[] { '\r', ' ' }))
                .Select(r => r
                    .Split(',')
                    .Where(z => !string.IsNullOrWhiteSpace(z))
                    .Select(r2 => Convert.ToByte(r2.Trim()))
                    .ToArray())
                    .Select(r => r.Length == 5 ? r.Concat(new byte[] { 0 }).ToArray() : r)
                .Where(i => i.Length == 6);
        }

        public TimeSpan GetLength(string fileName)
        {
            string fileData = File.ReadAllText(fileName);
            return TimeSpan.FromSeconds(GetRows(fileData).Count() * 0.1);
        }
    }
}
