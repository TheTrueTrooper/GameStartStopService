namespace GameStartStopService
{
    partial class AttendantConsole
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
            this.Lab_MachinesToStart = new System.Windows.Forms.Label();
            this.Bu_Start = new System.Windows.Forms.Button();
            this.CheLisBox_ConnectedMachines = new System.Windows.Forms.CheckedListBox();
            this.GroBox_GameSelector = new System.Windows.Forms.GroupBox();
            this.Lab_Description = new System.Windows.Forms.Label();
            this.Lab_PlayTime = new System.Windows.Forms.Label();
            this.Lab_Cost = new System.Windows.Forms.Label();
            this.RicTexBox_Description = new System.Windows.Forms.RichTextBox();
            this.PicBox_GameImage = new System.Windows.Forms.PictureBox();
            this.Lab_GameSelectionList = new System.Windows.Forms.Label();
            this.LisBox_GameSelectionList = new System.Windows.Forms.ListBox();
            this.Lab_GameName = new System.Windows.Forms.Label();
            this.Bu_Stop = new System.Windows.Forms.Button();
            this.RicTexBox_Log = new System.Windows.Forms.RichTextBox();
            this.Lab_EventLog = new System.Windows.Forms.Label();
            this.LisBox_RunningMachines = new System.Windows.Forms.ListBox();
            this.Lab_RunningMachines = new System.Windows.Forms.Label();
            this.GroBox_GameSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_GameImage)).BeginInit();
            this.SuspendLayout();
            // 
            // Lab_MachinesToStart
            // 
            this.Lab_MachinesToStart.AutoSize = true;
            this.Lab_MachinesToStart.Location = new System.Drawing.Point(12, 13);
            this.Lab_MachinesToStart.Name = "Lab_MachinesToStart";
            this.Lab_MachinesToStart.Size = new System.Drawing.Size(107, 13);
            this.Lab_MachinesToStart.TabIndex = 1;
            this.Lab_MachinesToStart.Text = "Connected machines";
            // 
            // Bu_Start
            // 
            this.Bu_Start.Location = new System.Drawing.Point(15, 725);
            this.Bu_Start.Name = "Bu_Start";
            this.Bu_Start.Size = new System.Drawing.Size(1524, 23);
            this.Bu_Start.TabIndex = 2;
            this.Bu_Start.Text = "Start Button";
            this.Bu_Start.UseVisualStyleBackColor = true;
            this.Bu_Start.Click += new System.EventHandler(this.Bu_Start_Click);
            // 
            // CheLisBox_ConnectedMachines
            // 
            this.CheLisBox_ConnectedMachines.CheckOnClick = true;
            this.CheLisBox_ConnectedMachines.FormattingEnabled = true;
            this.CheLisBox_ConnectedMachines.Location = new System.Drawing.Point(15, 44);
            this.CheLisBox_ConnectedMachines.Name = "CheLisBox_ConnectedMachines";
            this.CheLisBox_ConnectedMachines.Size = new System.Drawing.Size(369, 469);
            this.CheLisBox_ConnectedMachines.TabIndex = 3;
            this.CheLisBox_ConnectedMachines.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheLisBox_ConnectedMachines_ItemCheck);
            // 
            // GroBox_GameSelector
            // 
            this.GroBox_GameSelector.Controls.Add(this.Lab_Description);
            this.GroBox_GameSelector.Controls.Add(this.Lab_PlayTime);
            this.GroBox_GameSelector.Controls.Add(this.Lab_Cost);
            this.GroBox_GameSelector.Controls.Add(this.RicTexBox_Description);
            this.GroBox_GameSelector.Controls.Add(this.PicBox_GameImage);
            this.GroBox_GameSelector.Controls.Add(this.Lab_GameSelectionList);
            this.GroBox_GameSelector.Controls.Add(this.LisBox_GameSelectionList);
            this.GroBox_GameSelector.Controls.Add(this.Lab_GameName);
            this.GroBox_GameSelector.Location = new System.Drawing.Point(400, 29);
            this.GroBox_GameSelector.Name = "GroBox_GameSelector";
            this.GroBox_GameSelector.Size = new System.Drawing.Size(764, 491);
            this.GroBox_GameSelector.TabIndex = 7;
            this.GroBox_GameSelector.TabStop = false;
            this.GroBox_GameSelector.Text = "Game Selection";
            // 
            // Lab_Description
            // 
            this.Lab_Description.AutoSize = true;
            this.Lab_Description.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Description.Location = new System.Drawing.Point(6, 251);
            this.Lab_Description.Name = "Lab_Description";
            this.Lab_Description.Size = new System.Drawing.Size(102, 19);
            this.Lab_Description.TabIndex = 7;
            this.Lab_Description.Text = "Description";
            this.Lab_Description.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Lab_PlayTime
            // 
            this.Lab_PlayTime.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_PlayTime.Location = new System.Drawing.Point(247, 454);
            this.Lab_PlayTime.Name = "Lab_PlayTime";
            this.Lab_PlayTime.Size = new System.Drawing.Size(239, 24);
            this.Lab_PlayTime.TabIndex = 6;
            this.Lab_PlayTime.Text = "PlayTime(ms):";
            this.Lab_PlayTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // Lab_Cost
            // 
            this.Lab_Cost.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Cost.Location = new System.Drawing.Point(6, 454);
            this.Lab_Cost.Name = "Lab_Cost";
            this.Lab_Cost.Size = new System.Drawing.Size(235, 24);
            this.Lab_Cost.TabIndex = 5;
            this.Lab_Cost.Text = "Cost:";
            this.Lab_Cost.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // RicTexBox_Description
            // 
            this.RicTexBox_Description.Enabled = false;
            this.RicTexBox_Description.Location = new System.Drawing.Point(10, 283);
            this.RicTexBox_Description.Name = "RicTexBox_Description";
            this.RicTexBox_Description.Size = new System.Drawing.Size(476, 168);
            this.RicTexBox_Description.TabIndex = 4;
            this.RicTexBox_Description.Text = "";
            // 
            // PicBox_GameImage
            // 
            this.PicBox_GameImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PicBox_GameImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PicBox_GameImage.Location = new System.Drawing.Point(10, 58);
            this.PicBox_GameImage.Name = "PicBox_GameImage";
            this.PicBox_GameImage.Size = new System.Drawing.Size(476, 190);
            this.PicBox_GameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBox_GameImage.TabIndex = 3;
            this.PicBox_GameImage.TabStop = false;
            // 
            // Lab_GameSelectionList
            // 
            this.Lab_GameSelectionList.AutoSize = true;
            this.Lab_GameSelectionList.Location = new System.Drawing.Point(492, 16);
            this.Lab_GameSelectionList.Name = "Lab_GameSelectionList";
            this.Lab_GameSelectionList.Size = new System.Drawing.Size(101, 13);
            this.Lab_GameSelectionList.TabIndex = 2;
            this.Lab_GameSelectionList.Text = "Game Selection List";
            // 
            // LisBox_GameSelectionList
            // 
            this.LisBox_GameSelectionList.FormattingEnabled = true;
            this.LisBox_GameSelectionList.Location = new System.Drawing.Point(495, 58);
            this.LisBox_GameSelectionList.Name = "LisBox_GameSelectionList";
            this.LisBox_GameSelectionList.Size = new System.Drawing.Size(263, 420);
            this.LisBox_GameSelectionList.TabIndex = 1;
            this.LisBox_GameSelectionList.SelectedIndexChanged += new System.EventHandler(this.LisBox_GameSelectionList_SelectedIndexChanged);
            // 
            // Lab_GameName
            // 
            this.Lab_GameName.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_GameName.Location = new System.Drawing.Point(6, 16);
            this.Lab_GameName.Name = "Lab_GameName";
            this.Lab_GameName.Size = new System.Drawing.Size(480, 39);
            this.Lab_GameName.TabIndex = 0;
            this.Lab_GameName.Text = "label1";
            this.Lab_GameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Bu_Stop
            // 
            this.Bu_Stop.Enabled = false;
            this.Bu_Stop.Location = new System.Drawing.Point(15, 754);
            this.Bu_Stop.Name = "Bu_Stop";
            this.Bu_Stop.Size = new System.Drawing.Size(1524, 23);
            this.Bu_Stop.TabIndex = 8;
            this.Bu_Stop.Text = "Stop Button";
            this.Bu_Stop.UseVisualStyleBackColor = true;
            this.Bu_Stop.Click += new System.EventHandler(this.Bu_Stop_Click);
            // 
            // RicTexBox_Log
            // 
            this.RicTexBox_Log.Enabled = false;
            this.RicTexBox_Log.Location = new System.Drawing.Point(15, 571);
            this.RicTexBox_Log.Name = "RicTexBox_Log";
            this.RicTexBox_Log.Size = new System.Drawing.Size(1524, 137);
            this.RicTexBox_Log.TabIndex = 8;
            this.RicTexBox_Log.Text = "";
            // 
            // Lab_EventLog
            // 
            this.Lab_EventLog.AutoSize = true;
            this.Lab_EventLog.Location = new System.Drawing.Point(12, 544);
            this.Lab_EventLog.Name = "Lab_EventLog";
            this.Lab_EventLog.Size = new System.Drawing.Size(56, 13);
            this.Lab_EventLog.TabIndex = 10;
            this.Lab_EventLog.Text = "Event Log";
            // 
            // LisBox_RunningMachines
            // 
            this.LisBox_RunningMachines.FormattingEnabled = true;
            this.LisBox_RunningMachines.Location = new System.Drawing.Point(1170, 41);
            this.LisBox_RunningMachines.Name = "LisBox_RunningMachines";
            this.LisBox_RunningMachines.Size = new System.Drawing.Size(369, 472);
            this.LisBox_RunningMachines.TabIndex = 11;
            // 
            // Lab_RunningMachines
            // 
            this.Lab_RunningMachines.AutoSize = true;
            this.Lab_RunningMachines.Location = new System.Drawing.Point(1167, 13);
            this.Lab_RunningMachines.Name = "Lab_RunningMachines";
            this.Lab_RunningMachines.Size = new System.Drawing.Size(96, 13);
            this.Lab_RunningMachines.TabIndex = 12;
            this.Lab_RunningMachines.Text = "Running Machines";
            // 
            // AttendantConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 794);
            this.Controls.Add(this.Lab_RunningMachines);
            this.Controls.Add(this.LisBox_RunningMachines);
            this.Controls.Add(this.Lab_EventLog);
            this.Controls.Add(this.RicTexBox_Log);
            this.Controls.Add(this.Bu_Stop);
            this.Controls.Add(this.GroBox_GameSelector);
            this.Controls.Add(this.CheLisBox_ConnectedMachines);
            this.Controls.Add(this.Bu_Start);
            this.Controls.Add(this.Lab_MachinesToStart);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1570, 889);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1570, 833);
            this.Name = "AttendantConsole";
            this.Text = "Attendant Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AttendantConsole_FormClosing);
            this.GroBox_GameSelector.ResumeLayout(false);
            this.GroBox_GameSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_GameImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_MachinesToStart;
        private System.Windows.Forms.Button Bu_Start;
        private System.Windows.Forms.CheckedListBox CheLisBox_ConnectedMachines;
        private System.Windows.Forms.GroupBox GroBox_GameSelector;
        private System.Windows.Forms.Label Lab_Description;
        private System.Windows.Forms.Label Lab_PlayTime;
        private System.Windows.Forms.Label Lab_Cost;
        private System.Windows.Forms.RichTextBox RicTexBox_Description;
        private System.Windows.Forms.PictureBox PicBox_GameImage;
        private System.Windows.Forms.Label Lab_GameSelectionList;
        private System.Windows.Forms.ListBox LisBox_GameSelectionList;
        private System.Windows.Forms.Label Lab_GameName;
        private System.Windows.Forms.Button Bu_Stop;
        private System.Windows.Forms.RichTextBox RicTexBox_Log;
        private System.Windows.Forms.Label Lab_EventLog;
        private System.Windows.Forms.ListBox LisBox_RunningMachines;
        private System.Windows.Forms.Label Lab_RunningMachines;
    }
}