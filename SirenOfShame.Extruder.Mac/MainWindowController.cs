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
			GoButton.Activated += GoButtonClicked;
		}

		private string GetEncryptedPassword() {
			var tdse = new TripleDesStringEncryptor ();
			string unencryptedPassword = Password.StringValue;
			return tdse.EncryptString(unencryptedPassword);
		}

		void GoButtonClicked (object sender, EventArgs e)
		{
			var url = new NSUrl ("http://www.sirenofshame.com/Mobile/App");
			var extruderName = "LEE-XPS";
			var encryptedPassword = GetEncryptedPassword();
			var credentialsAsString = "username=" + Username.StringValue + "&encryptedpassword=" + encryptedPassword + "&extruderName=" + extruderName;
			NSMutableUrlRequest request = new NSMutableUrlRequest (url);
			NSData bodyNsData = new NSData (Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentialsAsString)), NSDataBase64DecodingOptions.None);
			request.Body = bodyNsData;
			request.HttpMethod = "POST";
			request["content-type"] = "application/x-www-form-urlencoded";

			Browser.MainFrame.LoadRequest(request);
		}

		public new MainWindow Window {
			get { return (MainWindow)base.Window; }
		}
	}
}
