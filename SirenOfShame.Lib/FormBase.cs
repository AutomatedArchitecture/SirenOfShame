using System;
using System.Windows.Forms;

namespace SirenOfShame
{
    public class FormBase : Form
    {
        public void Invoke(Action a)
        {
            if (InvokeRequired)
            {
                Invoke((Delegate)a);
            }
            else
            {
                a();
            }
        }

    }
}
