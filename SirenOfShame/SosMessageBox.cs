using System;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class SosMessageBox : Form
    {
        public bool Button1Clicked;
        public bool Button2Clicked;
        
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
            msg.Activate();
            msg.Focus();

            if (msg.Button1Clicked) return button1DialogResult;
            if (msg.Button2Clicked) return button2DialogResult;
            
            return DialogResult.Cancel;
        }
        
        public SosMessageBox()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, System.EventArgs e)
        {
            Button1Clicked = true;
            Close();
        }

        private void Button2Click(object sender, System.EventArgs e)
        {
            Button2Clicked = true;
            Close();
        }
    }
}
