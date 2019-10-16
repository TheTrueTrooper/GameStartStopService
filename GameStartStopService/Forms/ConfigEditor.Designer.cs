namespace GameStartStopService
{
    partial class ConfigEditor
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
            this.Lab_MasterServerURL = new System.Windows.Forms.Label();
            this.Lab_ServerLogOutput = new System.Windows.Forms.Label();
            this.GroBox_ServerOptions = new System.Windows.Forms.GroupBox();
            this.Lab_ServerMode = new System.Windows.Forms.Label();
            this.LisBox_ServerMode = new System.Windows.Forms.ListBox();
            this.TexBox_MachineGUID = new System.Windows.Forms.TextBox();
            this.Lab_MachineGUID = new System.Windows.Forms.Label();
            this.TexBox_ServerLogOutput = new System.Windows.Forms.TextBox();
            this.TexBox_MasterServerURL = new System.Windows.Forms.TextBox();
            this.GroBox_Credentials = new System.Windows.Forms.GroupBox();
            this.TexBox_Password = new System.Windows.Forms.TextBox();
            this.TexBox_UserName = new System.Windows.Forms.TextBox();
            this.Lab_UserName = new System.Windows.Forms.Label();
            this.Lab_Password = new System.Windows.Forms.Label();
            this.GroBox_LocalOptions = new System.Windows.Forms.GroupBox();
            this.TexBox_MasterStarterMasterPort = new System.Windows.Forms.TextBox();
            this.Lab_MasterStarterMasterPort = new System.Windows.Forms.Label();
            this.LisBox_CardMode = new System.Windows.Forms.ListBox();
            this.Lab_CardMode = new System.Windows.Forms.Label();
            this.TexBox_MachineName = new System.Windows.Forms.TextBox();
            this.TexBox_LocalLogOutput = new System.Windows.Forms.TextBox();
            this.TexBox_MasterStarterMasterLoc = new System.Windows.Forms.TextBox();
            this.LisBox_GameStarterMode = new System.Windows.Forms.ListBox();
            this.Lab_MasterStarterMasterLoc = new System.Windows.Forms.Label();
            this.Lab_StarterMode = new System.Windows.Forms.Label();
            this.Lab_MachineName = new System.Windows.Forms.Label();
            this.Lab_LocalLogOutput = new System.Windows.Forms.Label();
            this.But_Save = new System.Windows.Forms.Button();
            this.GroBox_ServerOptions.SuspendLayout();
            this.GroBox_Credentials.SuspendLayout();
            this.GroBox_LocalOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lab_MasterServerURL
            // 
            this.Lab_MasterServerURL.AutoSize = true;
            this.Lab_MasterServerURL.Location = new System.Drawing.Point(6, 70);
            this.Lab_MasterServerURL.Name = "Lab_MasterServerURL";
            this.Lab_MasterServerURL.Size = new System.Drawing.Size(170, 13);
            this.Lab_MasterServerURL.TabIndex = 0;
            this.Lab_MasterServerURL.Text = "Master Server URL (ASP.Net App)";
            // 
            // Lab_ServerLogOutput
            // 
            this.Lab_ServerLogOutput.AutoSize = true;
            this.Lab_ServerLogOutput.Location = new System.Drawing.Point(6, 94);
            this.Lab_ServerLogOutput.Name = "Lab_ServerLogOutput";
            this.Lab_ServerLogOutput.Size = new System.Drawing.Size(166, 13);
            this.Lab_ServerLogOutput.TabIndex = 1;
            this.Lab_ServerLogOutput.Text = "Server Log Output (ASP.Net App)";
            // 
            // GroBox_ServerOptions
            // 
            this.GroBox_ServerOptions.Controls.Add(this.Lab_ServerMode);
            this.GroBox_ServerOptions.Controls.Add(this.LisBox_ServerMode);
            this.GroBox_ServerOptions.Controls.Add(this.TexBox_MachineGUID);
            this.GroBox_ServerOptions.Controls.Add(this.Lab_MachineGUID);
            this.GroBox_ServerOptions.Controls.Add(this.TexBox_ServerLogOutput);
            this.GroBox_ServerOptions.Controls.Add(this.TexBox_MasterServerURL);
            this.GroBox_ServerOptions.Controls.Add(this.GroBox_Credentials);
            this.GroBox_ServerOptions.Controls.Add(this.Lab_MasterServerURL);
            this.GroBox_ServerOptions.Controls.Add(this.Lab_ServerLogOutput);
            this.GroBox_ServerOptions.Location = new System.Drawing.Point(12, 12);
            this.GroBox_ServerOptions.Name = "GroBox_ServerOptions";
            this.GroBox_ServerOptions.Size = new System.Drawing.Size(602, 221);
            this.GroBox_ServerOptions.TabIndex = 3;
            this.GroBox_ServerOptions.TabStop = false;
            this.GroBox_ServerOptions.Text = "ASP.NET Server Configs";
            // 
            // Lab_ServerMode
            // 
            this.Lab_ServerMode.AutoSize = true;
            this.Lab_ServerMode.Location = new System.Drawing.Point(6, 26);
            this.Lab_ServerMode.Name = "Lab_ServerMode";
            this.Lab_ServerMode.Size = new System.Drawing.Size(107, 13);
            this.Lab_ServerMode.TabIndex = 8;
            this.Lab_ServerMode.Text = "Service Server Mode";
            // 
            // LisBox_ServerMode
            // 
            this.LisBox_ServerMode.FormattingEnabled = true;
            this.LisBox_ServerMode.Items.AddRange(new object[] {
            "ConnectToServer",
            "NoServerDemoMode"});
            this.LisBox_ServerMode.Location = new System.Drawing.Point(180, 26);
            this.LisBox_ServerMode.Name = "LisBox_ServerMode";
            this.LisBox_ServerMode.Size = new System.Drawing.Size(412, 30);
            this.LisBox_ServerMode.TabIndex = 8;
            // 
            // TexBox_MachineGUID
            // 
            this.TexBox_MachineGUID.Location = new System.Drawing.Point(180, 117);
            this.TexBox_MachineGUID.Name = "TexBox_MachineGUID";
            this.TexBox_MachineGUID.Size = new System.Drawing.Size(412, 20);
            this.TexBox_MachineGUID.TabIndex = 11;
            // 
            // Lab_MachineGUID
            // 
            this.Lab_MachineGUID.AutoSize = true;
            this.Lab_MachineGUID.Location = new System.Drawing.Point(6, 120);
            this.Lab_MachineGUID.Name = "Lab_MachineGUID";
            this.Lab_MachineGUID.Size = new System.Drawing.Size(78, 13);
            this.Lab_MachineGUID.TabIndex = 10;
            this.Lab_MachineGUID.Text = "Machine GUID";
            // 
            // TexBox_ServerLogOutput
            // 
            this.TexBox_ServerLogOutput.Location = new System.Drawing.Point(180, 91);
            this.TexBox_ServerLogOutput.Name = "TexBox_ServerLogOutput";
            this.TexBox_ServerLogOutput.Size = new System.Drawing.Size(412, 20);
            this.TexBox_ServerLogOutput.TabIndex = 9;
            // 
            // TexBox_MasterServerURL
            // 
            this.TexBox_MasterServerURL.Location = new System.Drawing.Point(180, 67);
            this.TexBox_MasterServerURL.Name = "TexBox_MasterServerURL";
            this.TexBox_MasterServerURL.Size = new System.Drawing.Size(412, 20);
            this.TexBox_MasterServerURL.TabIndex = 8;
            // 
            // GroBox_Credentials
            // 
            this.GroBox_Credentials.Controls.Add(this.TexBox_Password);
            this.GroBox_Credentials.Controls.Add(this.TexBox_UserName);
            this.GroBox_Credentials.Controls.Add(this.Lab_UserName);
            this.GroBox_Credentials.Controls.Add(this.Lab_Password);
            this.GroBox_Credentials.Location = new System.Drawing.Point(7, 146);
            this.GroBox_Credentials.Name = "GroBox_Credentials";
            this.GroBox_Credentials.Size = new System.Drawing.Size(587, 69);
            this.GroBox_Credentials.TabIndex = 4;
            this.GroBox_Credentials.TabStop = false;
            this.GroBox_Credentials.Text = "Server Credentials Configs";
            // 
            // TexBox_Password
            // 
            this.TexBox_Password.Location = new System.Drawing.Point(173, 37);
            this.TexBox_Password.Name = "TexBox_Password";
            this.TexBox_Password.Size = new System.Drawing.Size(412, 20);
            this.TexBox_Password.TabIndex = 11;
            // 
            // TexBox_UserName
            // 
            this.TexBox_UserName.Location = new System.Drawing.Point(173, 13);
            this.TexBox_UserName.Name = "TexBox_UserName";
            this.TexBox_UserName.Size = new System.Drawing.Size(412, 20);
            this.TexBox_UserName.TabIndex = 10;
            // 
            // Lab_UserName
            // 
            this.Lab_UserName.AutoSize = true;
            this.Lab_UserName.Location = new System.Drawing.Point(6, 16);
            this.Lab_UserName.Name = "Lab_UserName";
            this.Lab_UserName.Size = new System.Drawing.Size(60, 13);
            this.Lab_UserName.TabIndex = 0;
            this.Lab_UserName.Text = "User Name";
            // 
            // Lab_Password
            // 
            this.Lab_Password.AutoSize = true;
            this.Lab_Password.Location = new System.Drawing.Point(6, 40);
            this.Lab_Password.Name = "Lab_Password";
            this.Lab_Password.Size = new System.Drawing.Size(53, 13);
            this.Lab_Password.TabIndex = 1;
            this.Lab_Password.Text = "Password";
            // 
            // GroBox_LocalOptions
            // 
            this.GroBox_LocalOptions.Controls.Add(this.TexBox_MasterStarterMasterPort);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_MasterStarterMasterPort);
            this.GroBox_LocalOptions.Controls.Add(this.LisBox_CardMode);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_CardMode);
            this.GroBox_LocalOptions.Controls.Add(this.TexBox_MachineName);
            this.GroBox_LocalOptions.Controls.Add(this.TexBox_LocalLogOutput);
            this.GroBox_LocalOptions.Controls.Add(this.TexBox_MasterStarterMasterLoc);
            this.GroBox_LocalOptions.Controls.Add(this.LisBox_GameStarterMode);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_MasterStarterMasterLoc);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_StarterMode);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_MachineName);
            this.GroBox_LocalOptions.Controls.Add(this.Lab_LocalLogOutput);
            this.GroBox_LocalOptions.Location = new System.Drawing.Point(6, 239);
            this.GroBox_LocalOptions.Name = "GroBox_LocalOptions";
            this.GroBox_LocalOptions.Size = new System.Drawing.Size(602, 230);
            this.GroBox_LocalOptions.TabIndex = 4;
            this.GroBox_LocalOptions.TabStop = false;
            this.GroBox_LocalOptions.Text = "Local Configs";
            // 
            // TexBox_MasterStarterMasterPort
            // 
            this.TexBox_MasterStarterMasterPort.Location = new System.Drawing.Point(186, 86);
            this.TexBox_MasterStarterMasterPort.Name = "TexBox_MasterStarterMasterPort";
            this.TexBox_MasterStarterMasterPort.Size = new System.Drawing.Size(412, 20);
            this.TexBox_MasterStarterMasterPort.TabIndex = 11;
            this.TexBox_MasterStarterMasterPort.TextChanged += new System.EventHandler(this.TexBox_MasterStarterMasterPort_TextChanged);
            // 
            // Lab_MasterStarterMasterPort
            // 
            this.Lab_MasterStarterMasterPort.AutoSize = true;
            this.Lab_MasterStarterMasterPort.Location = new System.Drawing.Point(6, 89);
            this.Lab_MasterStarterMasterPort.Name = "Lab_MasterStarterMasterPort";
            this.Lab_MasterStarterMasterPort.Size = new System.Drawing.Size(130, 13);
            this.Lab_MasterStarterMasterPort.TabIndex = 10;
            this.Lab_MasterStarterMasterPort.Text = "Master Starter Master Port";
            // 
            // LisBox_CardMode
            // 
            this.LisBox_CardMode.FormattingEnabled = true;
            this.LisBox_CardMode.Items.AddRange(new object[] {
            "UseCard",
            "NoCardNeededDemoMode"});
            this.LisBox_CardMode.Location = new System.Drawing.Point(184, 183);
            this.LisBox_CardMode.Name = "LisBox_CardMode";
            this.LisBox_CardMode.Size = new System.Drawing.Size(414, 30);
            this.LisBox_CardMode.TabIndex = 9;
            // 
            // Lab_CardMode
            // 
            this.Lab_CardMode.AutoSize = true;
            this.Lab_CardMode.Location = new System.Drawing.Point(4, 183);
            this.Lab_CardMode.Name = "Lab_CardMode";
            this.Lab_CardMode.Size = new System.Drawing.Size(97, 13);
            this.Lab_CardMode.TabIndex = 8;
            this.Lab_CardMode.Text = "Card Reader Mode";
            // 
            // TexBox_MachineName
            // 
            this.TexBox_MachineName.Location = new System.Drawing.Point(186, 13);
            this.TexBox_MachineName.Name = "TexBox_MachineName";
            this.TexBox_MachineName.Size = new System.Drawing.Size(412, 20);
            this.TexBox_MachineName.TabIndex = 7;
            // 
            // TexBox_LocalLogOutput
            // 
            this.TexBox_LocalLogOutput.Location = new System.Drawing.Point(186, 37);
            this.TexBox_LocalLogOutput.Name = "TexBox_LocalLogOutput";
            this.TexBox_LocalLogOutput.Size = new System.Drawing.Size(412, 20);
            this.TexBox_LocalLogOutput.TabIndex = 6;
            // 
            // TexBox_MasterStarterMasterLoc
            // 
            this.TexBox_MasterStarterMasterLoc.Location = new System.Drawing.Point(186, 60);
            this.TexBox_MasterStarterMasterLoc.Name = "TexBox_MasterStarterMasterLoc";
            this.TexBox_MasterStarterMasterLoc.Size = new System.Drawing.Size(412, 20);
            this.TexBox_MasterStarterMasterLoc.TabIndex = 5;
            // 
            // LisBox_GameStarterMode
            // 
            this.LisBox_GameStarterMode.FormattingEnabled = true;
            this.LisBox_GameStarterMode.Items.AddRange(new object[] {
            "SingleGameStarter",
            "MultiSocketStarterMaster",
            "MultiSocketStarterSlave",
            "AttendantChargeDeskOnly"});
            this.LisBox_GameStarterMode.Location = new System.Drawing.Point(186, 120);
            this.LisBox_GameStarterMode.Name = "LisBox_GameStarterMode";
            this.LisBox_GameStarterMode.Size = new System.Drawing.Size(412, 56);
            this.LisBox_GameStarterMode.TabIndex = 4;
            this.LisBox_GameStarterMode.SelectedIndexChanged += new System.EventHandler(this.LisBox_GameStarterMode_SelectedIndexChanged);
            // 
            // Lab_MasterStarterMasterLoc
            // 
            this.Lab_MasterStarterMasterLoc.AutoSize = true;
            this.Lab_MasterStarterMasterLoc.Location = new System.Drawing.Point(6, 63);
            this.Lab_MasterStarterMasterLoc.Name = "Lab_MasterStarterMasterLoc";
            this.Lab_MasterStarterMasterLoc.Size = new System.Drawing.Size(158, 13);
            this.Lab_MasterStarterMasterLoc.TabIndex = 3;
            this.Lab_MasterStarterMasterLoc.Text = "Master Starter Master IP or URL";
            // 
            // Lab_StarterMode
            // 
            this.Lab_StarterMode.AutoSize = true;
            this.Lab_StarterMode.Location = new System.Drawing.Point(6, 121);
            this.Lab_StarterMode.Name = "Lab_StarterMode";
            this.Lab_StarterMode.Size = new System.Drawing.Size(99, 13);
            this.Lab_StarterMode.TabIndex = 2;
            this.Lab_StarterMode.Text = "Game Starter Mode";
            // 
            // Lab_MachineName
            // 
            this.Lab_MachineName.AutoSize = true;
            this.Lab_MachineName.Location = new System.Drawing.Point(6, 16);
            this.Lab_MachineName.Name = "Lab_MachineName";
            this.Lab_MachineName.Size = new System.Drawing.Size(79, 13);
            this.Lab_MachineName.TabIndex = 0;
            this.Lab_MachineName.Text = "Machine Name";
            // 
            // Lab_LocalLogOutput
            // 
            this.Lab_LocalLogOutput.AutoSize = true;
            this.Lab_LocalLogOutput.Location = new System.Drawing.Point(6, 40);
            this.Lab_LocalLogOutput.Name = "Lab_LocalLogOutput";
            this.Lab_LocalLogOutput.Size = new System.Drawing.Size(89, 13);
            this.Lab_LocalLogOutput.TabIndex = 1;
            this.Lab_LocalLogOutput.Text = "Local Log Output";
            // 
            // But_Save
            // 
            this.But_Save.Location = new System.Drawing.Point(2, 475);
            this.But_Save.Name = "But_Save";
            this.But_Save.Size = new System.Drawing.Size(602, 23);
            this.But_Save.TabIndex = 5;
            this.But_Save.Text = "Save";
            this.But_Save.UseVisualStyleBackColor = true;
            this.But_Save.Click += new System.EventHandler(this.But_Save_Click);
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 520);
            this.Controls.Add(this.But_Save);
            this.Controls.Add(this.GroBox_LocalOptions);
            this.Controls.Add(this.GroBox_ServerOptions);
            this.Name = "ConfigEditor";
            this.Text = "ConfigEditor";
            this.Deactivate += new System.EventHandler(this.ConfigEditor_Deactivate);
            this.Leave += new System.EventHandler(this.ConfigEditor_Leave);
            this.GroBox_ServerOptions.ResumeLayout(false);
            this.GroBox_ServerOptions.PerformLayout();
            this.GroBox_Credentials.ResumeLayout(false);
            this.GroBox_Credentials.PerformLayout();
            this.GroBox_LocalOptions.ResumeLayout(false);
            this.GroBox_LocalOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Lab_MasterServerURL;
        private System.Windows.Forms.Label Lab_ServerLogOutput;
        private System.Windows.Forms.GroupBox GroBox_ServerOptions;
        private System.Windows.Forms.GroupBox GroBox_LocalOptions;
        private System.Windows.Forms.Label Lab_StarterMode;
        private System.Windows.Forms.Label Lab_MachineName;
        private System.Windows.Forms.Label Lab_LocalLogOutput;
        private System.Windows.Forms.GroupBox GroBox_Credentials;
        private System.Windows.Forms.Label Lab_UserName;
        private System.Windows.Forms.Label Lab_Password;
        private System.Windows.Forms.Label Lab_MasterStarterMasterLoc;
        private System.Windows.Forms.ListBox LisBox_GameStarterMode;
        private System.Windows.Forms.Button But_Save;
        private System.Windows.Forms.TextBox TexBox_ServerLogOutput;
        private System.Windows.Forms.TextBox TexBox_MasterServerURL;
        private System.Windows.Forms.TextBox TexBox_Password;
        private System.Windows.Forms.TextBox TexBox_UserName;
        private System.Windows.Forms.TextBox TexBox_MachineName;
        private System.Windows.Forms.TextBox TexBox_LocalLogOutput;
        private System.Windows.Forms.TextBox TexBox_MasterStarterMasterLoc;
        private System.Windows.Forms.TextBox TexBox_MachineGUID;
        private System.Windows.Forms.Label Lab_MachineGUID;
        private System.Windows.Forms.Label Lab_ServerMode;
        private System.Windows.Forms.ListBox LisBox_ServerMode;
        private System.Windows.Forms.ListBox LisBox_CardMode;
        private System.Windows.Forms.Label Lab_CardMode;
        private System.Windows.Forms.TextBox TexBox_MasterStarterMasterPort;
        private System.Windows.Forms.Label Lab_MasterStarterMasterPort;
    }
}