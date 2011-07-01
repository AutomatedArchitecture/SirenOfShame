using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Helpers
{
    /// <summary>
    /// ThreeStateTreeNode inherits from <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treenode.aspx">TreeNode</see>
    /// and adds the ability to support a third, indeterminate state as well as optionally cascading state changes to related nodes, i.e.
    /// child nodes and or parent nodes, as determined by this instance's related parent TreeView settings, CascadeNodeChecksToChildNodes and
    /// CascadeNodeChecksToParentNode.
    /// </summary>
    public class ThreeStateTreeNode : TreeNode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeNode class in addition to intializing
        /// the base class (<see cref="http://msdn2.microsoft.com/en-us/library/bk8h64c9.aspx">TreeNode Constructor</see>). 
        /// </summary>
        public ThreeStateTreeNode()
        {
            CommonConstructor();
        }

        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeNode class with a string for the text label to display in addition to intializing
        /// the base class (<see cref="http://msdn2.microsoft.com/en-us/library/ytx906df.aspx">TreeNode Constructor</see>). 
        /// </summary>
        /// <param name="text">The string for the label of the new tree node.</param>
        public ThreeStateTreeNode(string text)
            : base(text)
        {
            CommonConstructor();
        }

        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeNode class with a string for the text label to display 
        /// and an array of child ThreeStateTreeNodes in addition to intializing the base class 
        /// (<see cref="http://msdn2.microsoft.com/en-us/library/774ty506.aspx">TreeNode Constructor</see>). 
        /// </summary>
        /// <param name="text">The string for the label of the new tree node.</param>
        /// <param name="children">An array of child ThreeStateTreeNodes.</param>
        public ThreeStateTreeNode(string text, ThreeStateTreeNode[] children)
            : base(text, children)
        {
            CommonConstructor();
        }

        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeNode class with a string for the text label to display 
        /// and the selected and unselected image indexes in addition to intializing the base class 
        /// (<see cref="http://msdn2.microsoft.com/en-us/library/8dfy3k5t.aspx">TreeNode Constructor</see>). 
        /// </summary>
        /// <param name="text">The string for the label of the new tree node.</param>
        /// <param name="imageIndex">The image index of the unselected image in the parent TreeView's <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treeview.imagelist.aspx">ImageList</see>.</param>
        /// <param name="selectedImageIndex">The image index of the selected image in the parent TreeView's <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treeview.imagelist.aspx">ImageList</see>.</param>
        public ThreeStateTreeNode(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex)
        {
            CommonConstructor();
        }

        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeNode class with a string for the text label to display ,
        /// the selected and unselected image indexes, and an array of child ThreeStateTreeNodes in addition to intializing the base class 
        /// (<see cref="http://msdn2.microsoft.com/en-us/library/8dfy3k5t.aspx">TreeNode Constructor</see>). 
        /// </summary>
        /// <param name="text">The string for the label of the new tree node.</param>
        /// <param name="imageIndex">The image index of the unselected image in the parent TreeView's <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treeview.imagelist.aspx">ImageList</see>.</param>
        /// <param name="selectedImageIndex">The image index of the selected image in the parent TreeView's <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treeview.imagelist.aspx">ImageList</see>.</param>
        /// <param name="children">An array of child ThreeStateTreeNodes.</param>
        public ThreeStateTreeNode(string text, int imageIndex, int selectedImageIndex, ThreeStateTreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children)
        {
            CommonConstructor();
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Performs common initialization to all constructors.
        /// </summary>
        private void CommonConstructor()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// The current state of the checkbox.
        /// </summary>
        private CheckBoxState mState = CheckBoxState.Unchecked;

        private bool _updateCheckInternal;

        [Category("Three State TreeView"),
        Description("The current state of the node's checkbox, Unchecked, Checked, or Indeterminate"),
        DefaultValue(CheckBoxState.Unchecked),
        TypeConverter(typeof(CheckBoxState)),
        Editor("Ascentium.Research.Windows.Components.CheckBoxState", typeof(CheckBoxState))]
        public CheckBoxState State
        {
            get
            {
                if (mState == CheckBoxState.Indeterminate)
                {
                    return mState;
                }
                return Checked ? CheckBoxState.Checked : CheckBoxState.Unchecked;
            }
            set
            {
                if (mState != value)
                {
                    mState = value;
                    Checked = (mState == CheckBoxState.Checked);
                }
            }
        }

        /// <summary>
        /// Returns the 'combined' state for all siblings of a node.
        /// </summary>
        private CheckBoxState SiblingsState
        {
            get
            {
                // If parent is null, cannot have any siblings or if the parent
                // has only one child (i.e. this node) then return the state of this 
                // instance as the state.
                if ((Parent == null) || (Parent.Nodes.Count == 1))
                    return State;

                // The parent has more than one child.  Walk through parent's child
                // nodes to determine the state of all this node's siblings,
                // including this node.
                CheckBoxState state = 0;
                foreach (TreeNode node in Parent.Nodes)
                {
                    ThreeStateTreeNode child = node as ThreeStateTreeNode;
                    if (child != null)
                        state |= child.State;

                    // If the state is now indeterminate then know there
                    // is a combination of checked and unchecked nodes
                    // and no longer need to continue evaluating the rest
                    // of the sibling nodes.
                    if (state == CheckBoxState.Indeterminate)
                        break;
                }

                return (state == 0) ? CheckBoxState.Unchecked : state;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Manages state changes from one state to the next.
        /// </summary>
        /// <param name="fromState">The state upon which to base the state change.</param>
        public void Toggle(CheckBoxState fromState)
        {
            switch (fromState)
            {
                case CheckBoxState.Unchecked:
                    {
                        State = CheckBoxState.Checked;
                        break;
                    }
                default:
                    {
                        State = CheckBoxState.Unchecked;
                        break;
                    }
            }

            UpdateStateOfRelatedNodes();
        }

        /// <summary>
        /// Manages state changes from one state to the next.
        /// </summary>
        public new void Toggle()
        {
            Toggle(State);
        }

        /// <summary>
        /// Manages updating related child and parent nodes of this instance.
        /// </summary>
        public void UpdateStateOfRelatedNodes()
        {
            if (_updateCheckInternal)
            {
                return;
            }

            ThreeStateTreeView tv = TreeView as ThreeStateTreeView;
            if ((tv != null) && tv.CheckBoxes)
            {
                tv.BeginUpdate();

                // If want to cascade checkbox state changes to child nodes of this node and
                // if the current state is not intermediate, update the state of child nodes.
                if (State != CheckBoxState.Indeterminate)
                    UpdateChildNodeState();

                UpdateParentNodeState(true);

                tv.EndUpdate();
            }
        }

        /// <summary>
        /// Recursiveley update child node's state based on the state of this node.
        /// </summary>
        private void UpdateChildNodeState()
        {
            ThreeStateTreeNode child;
            foreach (TreeNode node in Nodes)
            {
                // It is possible node is not a ThreeStateTreeNode, so check first.
                if (node is ThreeStateTreeNode)
                {
                    child = node as ThreeStateTreeNode;
                    child.State = State;
                    child.Checked = (State != CheckBoxState.Unchecked);
                    child.UpdateChildNodeState();
                }
            }
        }

        /// <summary>
        /// Recursiveley update parent node state based on the current state of this node.
        /// </summary>
        private void UpdateParentNodeState(bool isStartingPoint)
        {
            // If isStartingPoint is false, then know this is not the initial call
            // to the recursive method as we want to force on the first time
            // this is called to set the instance's parent node state based on
            // the state of all the siblings of this node, including the state
            // of this node.  So, if not the startpoint (!isStartingPoint) and
            // the state of this instance is indeterminate (CheckBoxState.Indeterminate)
            // then know to set all subsequent parents to the indeterminate
            // state.  However, if not in an indeterminate state, then still need
            // to evaluate the state of all the siblings of this node, including the state
            // of this node before setting the state of the parent of this instance.

            ThreeStateTreeNode parent = Parent as ThreeStateTreeNode;
            if (parent != null)
            {
                CheckBoxState state;

                // Determine the new state
                if (!isStartingPoint && (State == CheckBoxState.Indeterminate))
                    state = CheckBoxState.Indeterminate;
                else
                    state = SiblingsState;

                // Update parent state if not the same.
                if (parent.State != state)
                {
                    parent.State = state;
                    _updateCheckInternal = true;
                    parent.Checked = (state != CheckBoxState.Unchecked);
                    _updateCheckInternal = false;
                    parent.UpdateParentNodeState(false);
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// The available states for a ThreeStateCheckBox.
    /// </summary>
    [Flags]
    public enum CheckBoxState
    {
        Unchecked = 1,
        Checked = 2,
        Indeterminate = Unchecked | Checked
    }
}
