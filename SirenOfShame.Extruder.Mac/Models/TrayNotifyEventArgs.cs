using System;

namespace SirenOfShame.Extruder.Mac
{
	public enum SosToolTipIcon
	{
		None = 0,
		Info = 1,
		Warning = 2,
		Error = 3,
	}

	public class TrayNotifyEventArgs
	{
		public string Title { get; set; }

		public string TipText { get; set; }

		public SosToolTipIcon TipIcon { get; set; }
	}
}

