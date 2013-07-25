using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SirenOfShame.Lib.Device;

namespace SirenOfShame.CLI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.Error.WriteLine("Usage: <program.exe> [led pattern index] [audio pattern index]");
            Console.Error.Write("Connecting device...");
            var deviceController = new DeviceController();
            while (false == deviceController.TryConnect())
            {
                Console.Error.Write(".");
                Thread.Sleep(new TimeSpan(0, 0, 1));
            }
            Console.Error.WriteLine();

            int? lightIndex = TryGetIntArg(args, 0);
            int? audioIndex = TryGetIntArg(args, 1);
            deviceController.PlayAlarm(new TimeSpan(0, 0, 10), lightIndex, audioIndex);
            return 0;
        }

        private static int? TryGetIntArg(string[] args, int index)
        {
            var lightIndexStr = TryGetArrayItem(args, index, null);
            int? lightIndex = null;
            if (false == String.IsNullOrWhiteSpace(lightIndexStr))
            {
                lightIndex = Int32.Parse(lightIndexStr);
            }
            return lightIndex;
        }

        private static string TryGetArrayItem(string[] args, int index, string defaultValue)
        {
            if (args.Length > index) {
                return args[index];
            }
            return defaultValue;
        }
    }
}
