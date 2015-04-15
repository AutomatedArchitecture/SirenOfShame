using System;

namespace SirenOfShame.Extruder.Mac
{
	public interface ILog {
		void Debug (string message);

		void Info (string message);

		void Error (string message, Exception ex);

		void Error (string message);
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
		public void Error (string message)
		{
			Console.WriteLine (message);
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

