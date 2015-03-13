using System;

using Foundation;
using AppKit;

namespace SirenOfShame.Extruder.Mac
{
	public partial class MainWindowController : NSWindowController
	{
		public MainWindowController (IntPtr handle) : base (handle)
		{
		}

		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
		}

		public MainWindowController () : base ("MainWindow")
		{
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			var tdse = new TripleDesStringEncryptor ();
			var encryptedString = tdse.DecryptString ("wwwfBrRRCDxe3qSYCrri3w==");
			MainLabel.StringValue = encryptedString;
		}

		public new MainWindow Window {
			get { return (MainWindow)base.Window; }
		}
	}
}
