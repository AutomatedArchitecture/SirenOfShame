using System.IO;

namespace SoxLib.Helpers
{
    public static class StreamHelper
    {
        public static void WriteToFile(this Stream stream, string fileName)
        {
            using (Stream output = File.Open(fileName, FileMode.Create))
            {
                stream.WriteToStream(output);
            }
        }

        public static void WriteToStream(this Stream input, Stream output)
        {
            byte[] buffer = new byte[1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static byte[] ReadToEnd(this Stream stream)
        {
            MemoryStream result = new MemoryStream();
            stream.WriteToStream(result);
            return result.ToArray();
        }
    }
}
