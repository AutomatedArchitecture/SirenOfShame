namespace SirenOfShame.Configuration
{
    sealed partial class AddRule
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRule));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._when = new System.Windows.Forms.ComboBox();
            this._build = new System.Windows.Forms.ComboBox();
            this._add = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._whoIsCustom = new System.Windows.Forms.RadioButton();
            this._whoIsAnyone = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._inheritAlerts = new System.Windows.Forms.RadioButton();
            this._modalDialog = new System.Windows.Forms.RadioButton();
            this._trayAlert = new System.Windows.Forms.RadioButton();
            this._noAlert = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._testAudio = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._testLights = new System.Windows.Forms.Button();
            this._audio = new System.Windows.Forms.ComboBox();
            this._lights = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this._turnOnLights = new System.Windows.Forms.RadioButton();
            this._inheritLightSetting = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this._customLightsDuration = new System.Windows.Forms.RadioButton();
            this._playLightsUntilBuildPasses = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this._lightsDurationTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this._turnOnAudio = new System.Windows.Forms.RadioButton();
            this._inheritAudio = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this._customAudioDuration = new System.Windows.Forms.RadioButton();
            this._playAudioUntilBuildPasses = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this._audioDurationTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._who = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "For the following build:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "When the following happens:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "And the person who did it is:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Then notify me:";
            // 
            // _when
            // 
            this._when.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._when.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._when.FormattingEnabled = true;
            this._when.Location = new System.Drawing.Point(163, 12);
            this._when.Name = "_when";
            this._when.Size = new System.Drawing.Size(220, 21);
            this._when.TabIndex = 4;
            // 
            // _build
            // 
            this._build.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._build.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._build.FormattingEnabled = true;
            this._build.Location = new System.Drawing.Point(137, 39);
            this._build.Name = "_build";
            this._build.Size = new System.Drawing.Size(246, 21);
            this._build.TabIndex = 5;
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._add.Location = new System.Drawing.Point(230, 375);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(75, 23);
            this._add.TabIndex = 11;
            this._add.Text = "Add";
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this.AddClick);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.Location = new System.Drawing.Point(311, 375);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 12;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // _whoIsCustom
            // 
            this._whoIsCustom.AutoSize = true;
            this._whoIsCustom.Location = new System.Drawing.Point(117, 87);
            this._whoIsCustom.Name = "_whoIsCustom";
            this._whoIsCustom.Size = new System.Drawing.Size(14, 13);
            this._whoIsCustom.TabIndex = 15;
            this._whoIsCustom.UseVisualStyleBackColor = true;
            this._whoIsCustom.CheckedChanged += new System.EventHandler(this.WhoIsCustomCheckedChanged);
            // 
            // _whoIsAnyone
            // 
            this._whoIsAnyone.AutoSize = true;
            this._whoIsAnyone.Checked = true;
            this._whoIsAnyone.Location = new System.Drawing.Point(50, 85);
            this._whoIsAnyone.Name = "_whoIsAnyone";
            this._whoIsAnyone.Size = new System.Drawing.Size(61, 17);
            this._whoIsAnyone.TabIndex = 16;
            this._whoIsAnyone.TabStop = true;
            this._whoIsAnyone.Text = "Anyone";
            this._whoIsAnyone.UseVisualStyleBackColor = true;
            this._whoIsAnyone.CheckedChanged += new System.EventHandler(this.WhoIsAnyoneCheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this._inheritAlerts);
            this.panel1.Controls.Add(this._modalDialog);
            this.panel1.Controls.Add(this._trayAlert);
            this.panel1.Controls.Add(this._noAlert);
            this.panel1.Location = new System.Drawing.Point(15, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 55);
            this.panel1.TabIndex = 17;
            // 
            // _inheritAlerts
            // 
            this._inheritAlerts.AutoSize = true;
            this._inheritAlerts.Checked = true;
            this._inheritAlerts.Location = new System.Drawing.Point(34, 3);
            this._inheritAlerts.Name = "_inheritAlerts";
            this._inheritAlerts.Size = new System.Drawing.Size(142, 17);
            this._inheritAlerts.TabIndex = 3;
            this._inheritAlerts.TabStop = true;
            this._inheritAlerts.Text = "Inherit notification setting";
            this._inheritAlerts.UseVisualStyleBackColor = true;
            // 
            // _modalDialog
            // 
            this._modalDialog.AutoSize = true;
            this._modalDialog.Location = new System.Drawing.Point(180, 26);
            this._modalDialog.Name = "_modalDialog";
            this._modalDialog.Size = new System.Drawing.Size(149, 17);
            this._modalDialog.TabIndex = 2;
            this._modalDialog.Text = "By opening a modal dialog";
            this._modalDialog.UseVisualStyleBackColor = true;
            // 
            // _trayAlert
            // 
            this._trayAlert.AutoSize = true;
            this._trayAlert.Location = new System.Drawing.Point(35, 26);
            this._trayAlert.Name = "_trayAlert";
            this._trayAlert.Size = new System.Drawing.Size(128, 17);
            this._trayAlert.TabIndex = 1;
            this._trayAlert.Text = "With a quick tray alert";
            this._trayAlert.UseVisualStyleBackColor = true;
            // 
            // _noAlert
            // 
            this._noAlert.AutoSize = true;
            this._noAlert.Location = new System.Drawing.Point(180, 3);
            this._noAlert.Name = "_noAlert";
            this._noAlert.Size = new System.Drawing.Size(102, 17);
            this._noAlert.TabIndex = 0;
            this._noAlert.Text = "Do not notify me";
            this._noAlert.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "And if a Siren of Shame is connected:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this._testAudio);
            this.panel2.Controls.Add(this._testLights);
            this.panel2.Controls.Add(this._audio);
            this.panel2.Controls.Add(this._lights);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(15, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 165);
            this.panel2.TabIndex = 19;
            // 
            // _testAudio
            // 
            this._testAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._testAudio.FlatAppearance.BorderSize = 0;
            this._testAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._testAudio.ImageKey = "Play.png";
            this._testAudio.ImageList = this.imageList1;
            this._testAudio.Location = new System.Drawing.Point(347, 26);
            this._testAudio.Name = "_testAudio";
            this._testAudio.Size = new System.Drawing.Size(21, 21);
            this._testAudio.TabIndex = 28;
            this._testAudio.UseVisualStyleBackColor = true;
            this._testAudio.Click += new System.EventHandler(this.TestAudioClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Play.png");
            this.imageList1.Images.SetKeyName(1, "Stop.png");
            // 
            // _testLights
            // 
            this._testLights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._testLights.FlatAppearance.BorderSize = 0;
            this._testLights.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._testLights.ImageKey = "Play.png";
            this._testLights.ImageList = this.imageList1;
            this._testLights.Location = new System.Drawing.Point(347, 111);
            this._testLights.Name = "_testLights";
            this._testLights.Size = new System.Drawing.Size(21, 21);
            this._testLights.TabIndex = 29;
            this._testLights.UseVisualStyleBackColor = true;
            this._testLights.Click += new System.EventHandler(this.TestLightsClick);
            // 
            // _audio
            // 
            this._audio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._audio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._audio.FormattingEnabled = true;
            this._audio.Items.AddRange(new object[] {
            "Pattern #1",
            "Pattern #2",
            "Pattern #3"});
            this._audio.Location = new System.Drawing.Point(35, 26);
            this._audio.Name = "_audio";
            this._audio.Size = new System.Drawing.Size(306, 21);
            this._audio.TabIndex = 13;
            // 
            // _lights
            // 
            this._lights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lights.FormattingEnabled = true;
            this._lights.Items.AddRange(new object[] {
            "Pattern #1",
            "Pattern #2",
            "Pattern #3"});
            this._lights.Location = new System.Drawing.Point(35, 111);
            this._lights.Name = "_lights";
            this._lights.Size = new System.Drawing.Size(306, 21);
            this._lights.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this._turnOnLights);
            this.panel6.Controls.Add(this._inheritLightSetting);
            this.panel6.Location = new System.Drawing.Point(3, 88);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(368, 27);
            this.panel6.TabIndex = 27;
            // 
            // _turnOnLights
            // 
            this._turnOnLights.AutoSize = true;
            this._turnOnLights.Location = new System.Drawing.Point(152, 3);
            this._turnOnLights.Name = "_turnOnLights";
            this._turnOnLights.Size = new System.Drawing.Size(182, 17);
            this._turnOnLights.TabIndex = 25;
            this._turnOnLights.Text = "Turn on the following light patten:";
            this._turnOnLights.UseVisualStyleBackColor = true;
            this._turnOnLights.CheckedChanged += new System.EventHandler(this.TurnOnLightsCheckedChanged);
            // 
            // _inheritLightSetting
            // 
            this._inheritLightSetting.AutoSize = true;
            this._inheritLightSetting.Checked = true;
            this._inheritLightSetting.Location = new System.Drawing.Point(36, 3);
            this._inheritLightSetting.Name = "_inheritLightSetting";
            this._inheritLightSetting.Size = new System.Drawing.Size(110, 17);
            this._inheritLightSetting.TabIndex = 11;
            this._inheritLightSetting.TabStop = true;
            this._inheritLightSetting.Text = "Inherit light setting";
            this._inheritLightSetting.UseVisualStyleBackColor = true;
            this._inheritLightSetting.CheckedChanged += new System.EventHandler(this.InheritLightSettingCheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this._customLightsDuration);
            this.panel4.Controls.Add(this._playLightsUntilBuildPasses);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this._lightsDurationTextBox);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(40, 137);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(325, 26);
            this.panel4.TabIndex = 24;
            // 
            // _customLightsDuration
            // 
            this._customLightsDuration.AutoSize = true;
            this._customLightsDuration.Location = new System.Drawing.Point(154, 4);
            this._customLightsDuration.Name = "_customLightsDuration";
            this._customLightsDuration.Size = new System.Drawing.Size(14, 13);
            this._customLightsDuration.TabIndex = 24;
            this._customLightsDuration.UseVisualStyleBackColor = true;
            // 
            // _playLightsUntilBuildPasses
            // 
            this._playLightsUntilBuildPasses.AutoSize = true;
            this._playLightsUntilBuildPasses.Checked = true;
            this._playLightsUntilBuildPasses.Location = new System.Drawing.Point(23, 2);
            this._playLightsUntilBuildPasses.Name = "_playLightsUntilBuildPasses";
            this._playLightsUntilBuildPasses.Size = new System.Drawing.Size(125, 17);
            this._playLightsUntilBuildPasses.TabIndex = 23;
            this._playLightsUntilBuildPasses.TabStop = true;
            this._playLightsUntilBuildPasses.Text = "Until the build passes";
            this._playLightsUntilBuildPasses.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(233, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "seconds";
            // 
            // _lightsDurationTextBox
            // 
            this._lightsDurationTextBox.Location = new System.Drawing.Point(199, 2);
            this._lightsDurationTextBox.Name = "_lightsDurationTextBox";
            this._lightsDurationTextBox.Size = new System.Drawing.Size(28, 20);
            this._lightsDurationTextBox.TabIndex = 20;
            this._lightsDurationTextBox.Text = "5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "for";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this._turnOnAudio);
            this.panel5.Controls.Add(this._inheritAudio);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(368, 27);
            this.panel5.TabIndex = 26;
            // 
            // _turnOnAudio
            // 
            this._turnOnAudio.AutoSize = true;
            this._turnOnAudio.Location = new System.Drawing.Point(159, 3);
            this._turnOnAudio.Name = "_turnOnAudio";
            this._turnOnAudio.Size = new System.Drawing.Size(139, 17);
            this._turnOnAudio.TabIndex = 25;
            this._turnOnAudio.Text = "Play the following audio:";
            this._turnOnAudio.UseVisualStyleBackColor = true;
            this._turnOnAudio.CheckedChanged += new System.EventHandler(this.TurnOnAudioCheckedChanged);
            // 
            // _inheritAudio
            // 
            this._inheritAudio.AutoSize = true;
            this._inheritAudio.Checked = true;
            this._inheritAudio.Location = new System.Drawing.Point(36, 3);
            this._inheritAudio.Name = "_inheritAudio";
            this._inheritAudio.Size = new System.Drawing.Size(117, 17);
            this._inheritAudio.TabIndex = 11;
            this._inheritAudio.TabStop = true;
            this._inheritAudio.Text = "Inherit audio setting";
            this._inheritAudio.UseVisualStyleBackColor = true;
            this._inheritAudio.CheckedChanged += new System.EventHandler(this.InheritAudioSettingsCheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this._customAudioDuration);
            this.panel3.Controls.Add(this._playAudioUntilBuildPasses);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this._audioDurationTextBox);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(40, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(325, 29);
            this.panel3.TabIndex = 23;
            // 
            // _customAudioDuration
            // 
            this._customAudioDuration.AutoSize = true;
            this._customAudioDuration.Location = new System.Drawing.Point(154, 6);
            this._customAudioDuration.Name = "_customAudioDuration";
            this._customAudioDuration.Size = new System.Drawing.Size(14, 13);
            this._customAudioDuration.TabIndex = 21;
            this._customAudioDuration.UseVisualStyleBackColor = true;
            // 
            // _playAudioUntilBuildPasses
            // 
            this._playAudioUntilBuildPasses.AutoSize = true;
            this._playAudioUntilBuildPasses.Checked = true;
            this._playAudioUntilBuildPasses.Location = new System.Drawing.Point(23, 3);
            this._playAudioUntilBuildPasses.Name = "_playAudioUntilBuildPasses";
            this._playAudioUntilBuildPasses.Size = new System.Drawing.Size(125, 17);
            this._playAudioUntilBuildPasses.TabIndex = 20;
            this._playAudioUntilBuildPasses.TabStop = true;
            this._playAudioUntilBuildPasses.Text = "Until the build passes";
            this._playAudioUntilBuildPasses.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "seconds";
            // 
            // _audioDurationTextBox
            // 
            this._audioDurationTextBox.Location = new System.Drawing.Point(199, 2);
            this._audioDurationTextBox.Name = "_audioDurationTextBox";
            this._audioDurationTextBox.Size = new System.Drawing.Size(28, 20);
            this._audioDurationTextBox.TabIndex = 17;
            this._audioDurationTextBox.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "for";
            // 
            // _who
            // 
            this._who.FormattingEnabled = true;
            this._who.Location = new System.Drawing.Point(137, 84);
            this._who.Name = "_who";
            this._who.Size = new System.Drawing.Size(243, 21);
            this._who.TabIndex = 30;
            // 
            // AddRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 409);
            this.Controls.Add(this._who);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._whoIsAnyone);
            this.Controls.Add(this._whoIsCustom);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._add);
            this.Controls.Add(this._build);
            this.Controls.Add(this._when);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddRule";
            this.Text = "Add Rule";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _when;
        private System.Windows.Forms.ComboBox _build;
        private System.Windows.Forms.Button _add;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.RadioButton _whoIsCustom;
        private System.Windows.Forms.RadioButton _whoIsAnyone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton _modalDialog;
        private System.Windows.Forms.RadioButton _trayAlert;
        private System.Windows.Forms.RadioButton _noAlert;
        private System.Windows.Forms.RadioButton _inheritAlerts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox _lights;
        private System.Windows.Forms.ComboBox _audio;
        private System.Windows.Forms.RadioButton _inheritAudio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _audioDurationTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox _lightsDurationTextBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton _customLightsDuration;
        private System.Windows.Forms.RadioButton _playLightsUntilBuildPasses;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton _customAudioDuration;
        private System.Windows.Forms.RadioButton _playAudioUntilBuildPasses;
        private System.Windows.Forms.RadioButton _turnOnAudio;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton _turnOnLights;
        private System.Windows.Forms.RadioButton _inheritLightSetting;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button _testAudio;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button _testLights;
        private System.Windows.Forms.ComboBox _who;
    }
}