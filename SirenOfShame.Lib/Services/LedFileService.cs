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

        public static byte[] TextToPattern(string text)
        {
            IEnumerable<string> rows = text.Split('\n').Select(i => i.Trim(new[] { '\r', ' ' }));
            var data = rows
                .Select(r => r.Split(',')
                    .Where(z => !string.IsNullOrWhiteSpace(z))
                    .Select(r2 => Convert.ToByte(r2.Trim())).ToArray());
            MemoryStream result = new MemoryStream();
            foreach (var d in data)
            {
                result.Write(d, 0, d.Length);
            }
            return result.ToArray();
        }
    }
}
