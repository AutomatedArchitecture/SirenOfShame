namespace SirenOfShame
{
    partial class UserList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._users = new System.Windows.Forms.ListView();
            this.User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Reputation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._editUserName = new System.Windows.Forms.ToolStripMenuItem();
            this._hideUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._showAllUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this._userMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _users
            // 
            this._users.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this._users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.User,
            this.Reputation});
            this._users.Dock = System.Windows.Forms.DockStyle.Fill;
            this._users.LabelEdit = true;
            this._users.Location = new System.Drawing.Point(0, 0);
            this._users.Name = "_users";
            this._users.Size = new System.Drawing.Size(172, 194);
            this._users.TabIndex = 1;
            this._users.UseCompatibleStateImageBehavior = false;
            this._users.View = System.Windows.Forms.View.Details;
            this._users.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.UsersAfterLabelEdit);
            this._users.SelectedIndexChanged += new System.EventHandler(this.UsersSelectedIndexChanged);
            this._users.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UsersMouseUp);
            // 
            // User
            // 
            this.User.Text = "User";
            this.User.Width = 80;
            // 
            // Reputation
            // 
            this.Reputation.Text = "Reputation";
            this.Reputation.Width = 75;
            // 
            // _userMenu
            // 
            this._userMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._editUserName,
            this._hideUser,
            this.toolStripSeparator1,
            this._showAllUsers});
            this._userMenu.Name = "_userMenu";
            this._userMenu.Size = new System.Drawing.Size(130, 76);
            // 
            // _editUserName
            // 
            this._editUserName.Name = "_editUserName";
            this._editUserName.Size = new System.Drawing.Size(152, 22);
            this._editUserName.Text = "Edit Name";
            this._editUserName.Click += new System.EventHandler(this.EditUserNameClick);
            // 
            // _hideUser
            // 
            this._hideUser.Name = "_hideUser";
            this._hideUser.Size = new System.Drawing.Size(152, 22);
            this._hideUser.Text = "Hidden";
            this._hideUser.Click += new System.EventHandler(this.HideUserClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // _showAllUsers
            // 
            this._showAllUsers.CheckOnClick = true;
            this._showAllUsers.Name = "_showAllUsers";
            this._showAllUsers.Size = new System.Drawing.Size(152, 22);
            this._showAllUsers.Text = "Show All";
            this._showAllUsers.CheckedChanged += new System.EventHandler(this.ShowAllUsersCheckedChanged);
            // 
            // UserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this._users);
            this.Name = "UserList";
            this.Size = new System.Drawing.Size(172, 194);
            this._userMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _users;
        private System.Windows.Forms.ColumnHeader User;
        private System.Windows.Forms.ColumnHeader Reputation;
        private System.Windows.Forms.ContextMenuStrip _userMenu;
        private System.Windows.Forms.ToolStripMenuItem _editUserName;
        private System.Windows.Forms.ToolStripMenuItem _hideUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _showAllUsers;
        private System.IO.Ports.SerialPort serialPort1;
    }
}
