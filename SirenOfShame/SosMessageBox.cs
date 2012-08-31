using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class SosMessageBox : Form
    {
        private bool _button1Clicked;
        private bool _button2Clicked;
        
        public static DialogResult Show(string title, string body, string button1Text, DialogResult button1DialogResult = DialogResult.OK)
        {
            return Show(title, body, button1Text, button1DialogResult, null, DialogResult.Cancel);
        }

        public static DialogResult Show(string title, string body, string button1Text, DialogResult button1DialogResult, string button2Text, DialogResult button2DialogResult)
        {
            SosMessageBox msg = new SosMessageBox
            {
                Text = title,
                _body = {Text = body},
                _button1 = {Text = button1Text},
                _button2 = {Visible = !string.IsNullOrEmpty(button2Text), Text = button2Text}
            };
            msg._button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            msg._button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            msg.ShowDialog();

            if (msg._button1Clicked) return button1DialogResult;
            if (msg._button2Clicked) return button2DialogResult;
            
            return DialogResult.Cancel;
        }

        private SosMessageBox()
        {
            InitializeComponent();

            Activate();
            Focus();
        }

        private void Button1Click(object sender, System.EventArgs e)
        {
            _button1Clicked = true;
            Close();
        }

        private void Button2Click(object sender, System.EventArgs e)
        {
            _button2Clicked = true;
            Close();
        }
    }
}
