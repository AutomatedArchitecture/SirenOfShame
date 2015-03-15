using System;

namespace SirenOfShame.Extruder.Mac
{
	public class ApiResultBase
	{
		public bool Success { get; set; }
		public string ErrorMessage { get; set; }
		public object Result { get; set; }
	}
}

