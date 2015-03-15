// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SirenOfShame.Extruder.Mac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		WebKit.WebView Browser { get; set; }

		[Outlet]
		AppKit.NSButton GoButton { get; set; }

		[Outlet]
		AppKit.NSTextField MainLabel { get; set; }

		[Outlet]
		AppKit.NSSecureTextField Password { get; set; }

		[Outlet]
		AppKit.NSTextField Username { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Browser != null) {
				Browser.Dispose ();
				Browser = null;
			}

			if (MainLabel != null) {
				MainLabel.Dispose ();
				MainLabel = null;
			}

			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}

			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}

			if (GoButton != null) {
				GoButton.Dispose ();
				GoButton = null;
			}
		}
	}
}
