using System;
using System.Collections.Generic;
using System.IO;

namespace WavToC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputFile = args[0];
            string outputFile = args[1];

            List<string> data = new List<string>();
            using (var input = File.OpenRead(inputFile))
            {
                int b;
                while ((b = input.ReadByte()) >= 0)
                {
                    string s = Convert.ToString(b, 16);
                    s = s.PadLeft(2, '0');
                    data.Add("0x" + s);
                }
            }

            using (var output = File.Open(outputFile, FileMode.Create))
            using (var outputWriter = new StreamWriter(output))
            {
                outputWriter.WriteLine("/******************** Pattern 0 ********************/");
                outputWriter.WriteLine("prog_uint8_t internalAudioPattern_0[] PROGMEM = {");
                for (int i = 0; i < data.Count; i += 16)
                {
                    for (int j = 0; j < 16 && i + j < data.Count; j++)
                    {
                        outputWriter.Write(data[i + j] + ", ");
                    }
                    outputWriter.WriteLine();
                }
                outputWriter.WriteLine("};");
                outputWriter.WriteLine("#define internalAudioPattern_0_length " + data.Count);
                outputWriter.WriteLine("#define internalAudioPattern_0_delay  1000");
            }
        }
    }
}
