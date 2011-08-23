using System;
using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame.Lib
{
    public partial class WavViewControl : UserControl
    {
        private byte[] _data;
        private int? _selectionStart;
        private int? _selectionEnd;
        private double _zoom = 1.0;
        private readonly Pen _wavPen;
        private readonly Pen _wavPenInverse;
        private readonly Pen _selectionPen;
        private Brush _selectionRectBrush;

        public WavViewControl()
        {
            InitializeComponent();
            _wavPen = new Pen(Color.FromArgb(227, 222, 14));
            _wavPenInverse = new Pen(Color.FromArgb(0, 0, 0));
            _selectionPen = new Pen(Color.FromArgb(227, 222, 14));
            _selectionRectBrush = new SolidBrush(Color.FromArgb(227, 222, 14));
            MouseWheel += OnMouseWheel;
        }

        public void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                _zoom = _zoom * 2.0;
            }
            else
            {
                _zoom = _zoom / 2.0;
            }
            if (_zoom > 4.0)
            {
                _zoom = 4.0;
            }
            if (_zoom < 0.03125)
            {
                _zoom = 0.03125;
            }
            RecalculateScroll();
        }

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; RecalculateScroll(); }
        }

        private void RecalculateScroll()
        {
            if (_data == null)
            {
                return;
            }

            _hscroll.Maximum = _data.Length + 100;
            _hscroll.SmallChange = 10;
            _hscroll.LargeChange = (int)(Width / _zoom);
            if (_hscroll.LargeChange >= _hscroll.Maximum)
            {
                _hscroll.Value = 0;
                _hscroll.LargeChange = _hscroll.Maximum;
            }
            _image.Invalidate();
        }

        public int? SelectionStart
        {
            get { return _selectionStart; }
            set { _selectionStart = value; _image.Invalidate(); }
        }

        public int? SelectionEnd
        {
            get { return _selectionEnd; }
            set { _selectionEnd = value; _image.Invalidate(); }
        }

        public double Zoom
        {
            get { return _zoom; }
            set { _zoom = value; RecalculateScroll(); }
        }

        private void _image_Paint(object sender, PaintEventArgs e)
        {
            if (_data == null)
            {
                return;
            }

            Point ptLast = GetXY(-1);
            for (int i = 0; i < _data.Length; i++)
            {
                Point pt = GetXY(i);
                Pen pen = _wavPen;
                if (SelectionStart != null && SelectionEnd != null)
                {
                    if (SelectionStart.Value == i)
                    {
                        Point ptEnd = GetXY(SelectionEnd.Value);
                        e.Graphics.FillRectangle(_selectionRectBrush, pt.X, 0, ptEnd.X - pt.X, Height);
                    }
                    if (i >= SelectionStart.Value && i <= SelectionEnd.Value)
                    {
                        pen = _wavPenInverse;
                    }
                }
                else if (SelectionStart != null && SelectionStart.Value == i)
                {
                    e.Graphics.DrawLine(_selectionPen, pt.X, 0, pt.X, Height);
                }
                else if (SelectionEnd != null && SelectionEnd.Value == i)
                {
                    e.Graphics.DrawLine(_selectionPen, pt.X, 0, pt.X, Height);
                }
                e.Graphics.DrawLine(pen, ptLast, pt);
                ptLast = pt;
            }
        }

        private Point GetXY(int i)
        {
            if (i < 0)
            {
                return new Point(-1, _image.Height / 2);
            }
            if (i >= _data.Length)
            {
                return new Point(_image.Width + 1, _image.Height / 2);
            }
            double val = _data[i] / 255.0;
            int y = (int)(_image.Height - (_image.Height * val));
            int x = (int)((i - _hscroll.Value) * _zoom);
            return new Point(x, y);
        }

        private int GetIndex(Point pt)
        {
            return (int)((pt.X / _zoom) + _hscroll.Value);
        }

        private void _hscroll_Scroll(object sender, ScrollEventArgs e)
        {
            _image.Invalidate();
        }

        private void _image_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateSelection(e);
        }

        private void _image_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateSelection(e);
        }

        private void _image_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateSelection(e);
        }

        private void UpdateSelection(MouseEventArgs e)
        {
            int x = Math.Min(Width, Math.Max(0, e.Location.X));
            int y = e.Location.Y;
            var idx = GetIndex(new Point(x, y));
            if (e.Button == MouseButtons.Left)
            {
                SelectionStart = idx;
            }
            else if (e.Button == MouseButtons.Right)
            {
                SelectionEnd = idx;
            }
        }
    }
}
