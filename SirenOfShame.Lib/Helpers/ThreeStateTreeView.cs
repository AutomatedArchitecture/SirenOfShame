using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Helpers
{
    public sealed class ThreeStateTreeView : TreeView
    {
        private const int WM_PAINT = 0x000F;
        private readonly Bitmap _bmpIndterminate;

        public ThreeStateTreeView()
        {
            Size szBox = new Size(20, 20);

            _bmpIndterminate = new Bitmap(szBox.Width, szBox.Height);

            var g = Graphics.FromImage(_bmpIndterminate);
            CheckBoxRenderer.DrawCheckBox(g, new Point(0, 0), System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
            g.Dispose();
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                int y = 0;
                Graphics g = CreateGraphics();

                if (Nodes.Count > 0)
                    PaintNodes(Nodes[0], g, ref y);
                g.Dispose();
            }
        }

        private void PaintNodes(TreeNode node, Graphics g, ref int y)
        {
            ThreeStateTreeNode nodeThree = node as ThreeStateTreeNode;
            if (nodeThree != null && nodeThree.State == CheckBoxState.Indeterminate)
            {
                Point ptTL = new Point(node.Bounds.X - Indent + 6, node.Bounds.Y + 1);
                g.DrawImageUnscaled(_bmpIndterminate, ptTL);
            }
            if (node.IsExpanded)
            {
                y += ItemHeight;
                PaintNodes(node.FirstNode, g, ref y);
            }
            if (node.NextNode != null)
            {
                y += ItemHeight;
                PaintNodes(node.NextNode, g, ref y);
            }
        }
    }
}
