using System;

namespace SirenOfShame.Extruder.Mac
{
	public interface ILog {
		void Debug (string deviceNotFound);

		void Info (string startingFirmwareUpgrade);

		void Error (string readFailed, Exception ex);
	}

	public class ConsoleLog : ILog {
		public void Debug (string log)
		{
			Console.WriteLine (log);
		}
		public void Info (string log)
		{
			Console.WriteLine (log);
		}
		public void Error (string log, Exception ex)
		{
			Console.WriteLine (log + "\n" + ex);
		}
	}

	public class MyLogManager
	{
		public static ILog GetLogger (Type type)
		{
			return new ConsoleLog ();
		}

		public MyLogManager ()
		{
		}
	}
}

