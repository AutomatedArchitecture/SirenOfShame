using System;

namespace SirenOfShame.Extruder.Mac
{
	public enum TrayIcon
	{
		Red,
		Green,
		Question
	}

	public class SetTrayIconEventArgs
	{
		public TrayIcon TrayIcon { get; set; }
	}
}

