using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SirenOfShame
{
    public partial class SplashScreen : Form
    {
        private double m_dblOpacityIncrement = .05;
        private const double m_dblOpacityDecrement = .1;
        private const int TIMER_INTERVAL = 10;
        private readonly Timer timer1 = new Timer();

        public void CloseForm()
        {
            m_dblOpacityIncrement = -m_dblOpacityDecrement;
        }

        public SplashScreen()
        {
            Opacity = .0;
            timer1.Tick += Timer1Tick;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            InitializeComponent();
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            if (!IsHandleCreated)
            {
                return;
            }
            if (Disposing || IsDisposed) return;
            Invoke((Action)(() =>
            {
                if (Disposing || IsDisposed) return;
                if (m_dblOpacityIncrement > 0)
                {
                    if (Opacity < 1)
                        Opacity += m_dblOpacityIncrement;
                }
                else
                {
                    if (Opacity > 0)
                        Opacity += m_dblOpacityIncrement;
                    else
                        Close();
                }
            }));
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            ClientSize = BackgroundImage.Size;
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            // Make window transparent
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // WS_EX_TRANSPARENT
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Do not paint the background
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint background image
            if (BackgroundImage != null)
            {
                Bitmap bmp = new Bitmap(BackgroundImage);
                bmp.MakeTransparent(Color.White);
                e.Graphics.DrawImage(bmp, 0, 0);
            }
        }
    }
}
