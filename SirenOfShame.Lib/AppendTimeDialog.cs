using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SirenOfShame.Lib
{
    public partial class AppendTimeDialog : Form
    {
        public AppendTimeDialog()
        {
            InitializeComponent();
        }

        public TimeSpan TimeSpan
        {
            get { return new TimeSpan(0, 0, (int) _minutes.Value, (int) _seconds.Value, (int) _millis.Value); }
        }

        private void _seconds_ValueChanged(object sender, EventArgs e)
        {
            if (_seconds.Value == 60)
            {
                _minutes.Value++;
                _seconds.Value = 0;
            }
            else if (_seconds.Value == -1)
            {
                if (_minutes.Value == 0)
                {
                    _seconds.Value = 0;
                }
                else
                {
                    _minutes.Value--;
                    _seconds.Value = 59;
                }
            }
        }

        private void _millis_ValueChanged(object sender, EventArgs e)
        {
            if (_millis.Value == 1000)
            {
                _seconds.Value++;
                _millis.Value = 0;
            }
            else if (_millis.Value == -1)
            {
                if (_seconds.Value == 0)
                {
                    _millis.Value = 0;
                }
                else
                {
                    _seconds.Value--;
                    _millis.Value = 999;
                }
            }
        }
    }
}
