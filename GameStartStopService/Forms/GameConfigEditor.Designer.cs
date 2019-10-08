namespace GameStartStopService
{
    partial class GameConfigEditor
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
            this.TreVie_GamesView = new System.Windows.Forms.TreeView();
            this.But_ReplaceSelectGame = new System.Windows.Forms.Button();
            this.But_DeleteSelected = new System.Windows.Forms.Button();
            this.But_AddGame = new System.Windows.Forms.Button();
            this.But_AddExeToSelectedGame = new System.Windows.Forms.Button();
            this.But_Save = new System.Windows.Forms.Button();
            this.GroBox_GameEditor = new System.Windows.Forms.GroupBox();
            this.CheBox_IsDeleted = new System.Windows.Forms.CheckBox();
            this.CheBox_IsActive = new System.Windows.Forms.CheckBox();
            this.TexBox_MinNumberOfShares = new System.Windows.Forms.TextBox();
            this.Lab_MinNumberOfShares = new System.Windows.Forms.Label();
            this.TexBox_MaxNumberOfShares = new System.Windows.Forms.TextBox();
            this.Lab_MaxNumberOfShares = new System.Windows.Forms.Label();
            this.Lab_Description = new System.Windows.Forms.Label();
            this.TexBox_GUID = new System.Windows.Forms.TextBox();
            this.TexBox_Name = new System.Windows.Forms.TextBox();
            this.TexBox_ImagePath = new System.Windows.Forms.TextBox();
            this.TexBox_GamePath = new System.Windows.Forms.TextBox();
            this.TexBox_PlayTime = new System.Windows.Forms.TextBox();
            this.TexBox_CostToPlay = new System.Windows.Forms.TextBox();
            this.TexBox_StartOptions = new System.Windows.Forms.TextBox();
            this.Lab_GameStartOptions = new System.Windows.Forms.Label();
            this.Lab_CostToPlay = new System.Windows.Forms.Label();
            this.Lab_PlayTime = new System.Windows.Forms.Label();
            this.Lab_GamePath = new System.Windows.Forms.Label();
            this.Lab_ImagePath = new System.Windows.Forms.Label();
            this.Lab_Name = new System.Windows.Forms.Label();
            this.Lab_GUID = new System.Windows.Forms.Label();
            this.GroBox_OptionalExesEditor = new System.Windows.Forms.GroupBox();
            this.TexBox_ExeOpt = new System.Windows.Forms.TextBox();
            this.TexBox_Delay = new System.Windows.Forms.TextBox();
            this.Lab_ExeStartOptions = new System.Windows.Forms.Label();
            this.TexBox_ExePath = new System.Windows.Forms.TextBox();
            this.Lab_Delay = new System.Windows.Forms.Label();
            this.Lab_ExePath = new System.Windows.Forms.Label();
            this.But_ReplaceSelectOptionalExe = new System.Windows.Forms.Button();
            this.RicTexBox_Description = new System.Windows.Forms.RichTextBox();
            this.CheBox_IsDeletedOpExe = new System.Windows.Forms.CheckBox();
            this.TexBox_ExeGUID = new System.Windows.Forms.TextBox();
            this.Lab_ExeGUID = new System.Windows.Forms.Label();
            this.GroBox_GameEditor.SuspendLayout();
            this.GroBox_OptionalExesEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreVie_GamesView
            // 
            this.TreVie_GamesView.Location = new System.Drawing.Point(12, 12);
            this.TreVie_GamesView.Name = "TreVie_GamesView";
            this.TreVie_GamesView.Size = new System.Drawing.Size(736, 446);
            this.TreVie_GamesView.TabIndex = 0;
            this.TreVie_GamesView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreVie_GamesView_AfterSelect);
            // 
            // But_ReplaceSelectGame
            // 
            this.But_ReplaceSelectGame.Location = new System.Drawing.Point(13, 464);
            this.But_ReplaceSelectGame.Name = "But_ReplaceSelectGame";
            this.But_ReplaceSelectGame.Size = new System.Drawing.Size(118, 42);
            this.But_ReplaceSelectGame.TabIndex = 1;
            this.But_ReplaceSelectGame.Text = "Replace Selected Game";
            this.But_ReplaceSelectGame.UseVisualStyleBackColor = true;
            this.But_ReplaceSelectGame.Click += new System.EventHandler(this.But_ReplaceSelectGame_Click);
            // 
            // But_DeleteSelected
            // 
            this.But_DeleteSelected.Location = new System.Drawing.Point(261, 464);
            this.But_DeleteSelected.Name = "But_DeleteSelected";
            this.But_DeleteSelected.Size = new System.Drawing.Size(118, 42);
            this.But_DeleteSelected.TabIndex = 2;
            this.But_DeleteSelected.Text = "Delete Selected";
            this.But_DeleteSelected.UseVisualStyleBackColor = true;
            this.But_DeleteSelected.Click += new System.EventHandler(this.But_DeleteSelected_Click);
            // 
            // But_AddGame
            // 
            this.But_AddGame.Location = new System.Drawing.Point(385, 464);
            this.But_AddGame.Name = "But_AddGame";
            this.But_AddGame.Size = new System.Drawing.Size(118, 42);
            this.But_AddGame.TabIndex = 3;
            this.But_AddGame.Text = "Add Game";
            this.But_AddGame.UseVisualStyleBackColor = true;
            this.But_AddGame.Click += new System.EventHandler(this.But_AddGame_Click);
            // 
            // But_AddExeToSelectedGame
            // 
            this.But_AddExeToSelectedGame.Location = new System.Drawing.Point(509, 464);
            this.But_AddExeToSelectedGame.Name = "But_AddExeToSelectedGame";
            this.But_AddExeToSelectedGame.Size = new System.Drawing.Size(118, 42);
            this.But_AddExeToSelectedGame.TabIndex = 4;
            this.But_AddExeToSelectedGame.Text = "Add Exe To Selected Game";
            this.But_AddExeToSelectedGame.UseVisualStyleBackColor = true;
            this.But_AddExeToSelectedGame.Click += new System.EventHandler(this.But_AddExeToSelectedGame_Click);
            // 
            // But_Save
            // 
            this.But_Save.Location = new System.Drawing.Point(633, 464);
            this.But_Save.Name = "But_Save";
            this.But_Save.Size = new System.Drawing.Size(118, 42);
            this.But_Save.TabIndex = 5;
            this.But_Save.Text = "Save";
            this.But_Save.UseVisualStyleBackColor = true;
            this.But_Save.Click += new System.EventHandler(this.But_Save_Click);
            // 
            // GroBox_GameEditor
            // 
            this.GroBox_GameEditor.Controls.Add(this.RicTexBox_Description);
            this.GroBox_GameEditor.Controls.Add(this.CheBox_IsDeleted);
            this.GroBox_GameEditor.Controls.Add(this.CheBox_IsActive);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_MinNumberOfShares);
            this.GroBox_GameEditor.Controls.Add(this.Lab_MinNumberOfShares);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_MaxNumberOfShares);
            this.GroBox_GameEditor.Controls.Add(this.Lab_MaxNumberOfShares);
            this.GroBox_GameEditor.Controls.Add(this.Lab_Description);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_GUID);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_Name);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_ImagePath);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_GamePath);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_PlayTime);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_CostToPlay);
            this.GroBox_GameEditor.Controls.Add(this.TexBox_StartOptions);
            this.GroBox_GameEditor.Controls.Add(this.Lab_GameStartOptions);
            this.GroBox_GameEditor.Controls.Add(this.Lab_CostToPlay);
            this.GroBox_GameEditor.Controls.Add(this.Lab_PlayTime);
            this.GroBox_GameEditor.Controls.Add(this.Lab_GamePath);
            this.GroBox_GameEditor.Controls.Add(this.Lab_ImagePath);
            this.GroBox_GameEditor.Controls.Add(this.Lab_Name);
            this.GroBox_GameEditor.Controls.Add(this.Lab_GUID);
            this.GroBox_GameEditor.Location = new System.Drawing.Point(13, 512);
            this.GroBox_GameEditor.Name = "GroBox_GameEditor";
            this.GroBox_GameEditor.Size = new System.Drawing.Size(736, 358);
            this.GroBox_GameEditor.TabIndex = 6;
            this.GroBox_GameEditor.TabStop = false;
            this.GroBox_GameEditor.Text = "Game Editor";
            // 
            // CheBox_IsDeleted
            // 
            this.CheBox_IsDeleted.AutoSize = true;
            this.CheBox_IsDeleted.Location = new System.Drawing.Point(110, 335);
            this.CheBox_IsDeleted.Name = "CheBox_IsDeleted";
            this.CheBox_IsDeleted.Size = new System.Drawing.Size(74, 17);
            this.CheBox_IsDeleted.TabIndex = 20;
            this.CheBox_IsDeleted.Text = "Is Deleted";
            this.CheBox_IsDeleted.UseVisualStyleBackColor = true;
            // 
            // CheBox_IsActive
            // 
            this.CheBox_IsActive.AutoSize = true;
            this.CheBox_IsActive.Location = new System.Drawing.Point(9, 335);
            this.CheBox_IsActive.Name = "CheBox_IsActive";
            this.CheBox_IsActive.Size = new System.Drawing.Size(67, 17);
            this.CheBox_IsActive.TabIndex = 17;
            this.CheBox_IsActive.Text = "Is Active";
            this.CheBox_IsActive.UseVisualStyleBackColor = true;
            // 
            // TexBox_MinNumberOfShares
            // 
            this.TexBox_MinNumberOfShares.Location = new System.Drawing.Point(111, 37);
            this.TexBox_MinNumberOfShares.Name = "TexBox_MinNumberOfShares";
            this.TexBox_MinNumberOfShares.Size = new System.Drawing.Size(619, 20);
            this.TexBox_MinNumberOfShares.TabIndex = 19;
            this.TexBox_MinNumberOfShares.TextChanged += new System.EventHandler(this.TexBox_MinNumberOfShares_TextChanged);
            // 
            // Lab_MinNumberOfShares
            // 
            this.Lab_MinNumberOfShares.AutoSize = true;
            this.Lab_MinNumberOfShares.Location = new System.Drawing.Point(6, 40);
            this.Lab_MinNumberOfShares.Name = "Lab_MinNumberOfShares";
            this.Lab_MinNumberOfShares.Size = new System.Drawing.Size(60, 13);
            this.Lab_MinNumberOfShares.TabIndex = 18;
            this.Lab_MinNumberOfShares.Text = "Min Shares";
            // 
            // TexBox_MaxNumberOfShares
            // 
            this.TexBox_MaxNumberOfShares.Location = new System.Drawing.Point(111, 63);
            this.TexBox_MaxNumberOfShares.Name = "TexBox_MaxNumberOfShares";
            this.TexBox_MaxNumberOfShares.Size = new System.Drawing.Size(619, 20);
            this.TexBox_MaxNumberOfShares.TabIndex = 17;
            this.TexBox_MaxNumberOfShares.TextChanged += new System.EventHandler(this.TexBox_MaxNumberOfShares_TextChanged);
            // 
            // Lab_MaxNumberOfShares
            // 
            this.Lab_MaxNumberOfShares.AutoSize = true;
            this.Lab_MaxNumberOfShares.Location = new System.Drawing.Point(6, 66);
            this.Lab_MaxNumberOfShares.Name = "Lab_MaxNumberOfShares";
            this.Lab_MaxNumberOfShares.Size = new System.Drawing.Size(63, 13);
            this.Lab_MaxNumberOfShares.TabIndex = 16;
            this.Lab_MaxNumberOfShares.Text = "Max Shares";
            // 
            // Lab_Description
            // 
            this.Lab_Description.AutoSize = true;
            this.Lab_Description.Location = new System.Drawing.Point(6, 244);
            this.Lab_Description.Name = "Lab_Description";
            this.Lab_Description.Size = new System.Drawing.Size(60, 13);
            this.Lab_Description.TabIndex = 14;
            this.Lab_Description.Text = "Description";
            // 
            // TexBox_GUID
            // 
            this.TexBox_GUID.Location = new System.Drawing.Point(111, 13);
            this.TexBox_GUID.Name = "TexBox_GUID";
            this.TexBox_GUID.Size = new System.Drawing.Size(619, 20);
            this.TexBox_GUID.TabIndex = 13;
            // 
            // TexBox_Name
            // 
            this.TexBox_Name.Location = new System.Drawing.Point(111, 118);
            this.TexBox_Name.Name = "TexBox_Name";
            this.TexBox_Name.Size = new System.Drawing.Size(619, 20);
            this.TexBox_Name.TabIndex = 12;
            // 
            // TexBox_ImagePath
            // 
            this.TexBox_ImagePath.Location = new System.Drawing.Point(111, 166);
            this.TexBox_ImagePath.Name = "TexBox_ImagePath";
            this.TexBox_ImagePath.Size = new System.Drawing.Size(619, 20);
            this.TexBox_ImagePath.TabIndex = 11;
            // 
            // TexBox_GamePath
            // 
            this.TexBox_GamePath.Location = new System.Drawing.Point(111, 191);
            this.TexBox_GamePath.Name = "TexBox_GamePath";
            this.TexBox_GamePath.Size = new System.Drawing.Size(619, 20);
            this.TexBox_GamePath.TabIndex = 10;
            // 
            // TexBox_PlayTime
            // 
            this.TexBox_PlayTime.Location = new System.Drawing.Point(111, 217);
            this.TexBox_PlayTime.Name = "TexBox_PlayTime";
            this.TexBox_PlayTime.Size = new System.Drawing.Size(619, 20);
            this.TexBox_PlayTime.TabIndex = 9;
            this.TexBox_PlayTime.TextChanged += new System.EventHandler(this.TexBox_PlayTime_TextChanged);
            // 
            // TexBox_CostToPlay
            // 
            this.TexBox_CostToPlay.Location = new System.Drawing.Point(111, 92);
            this.TexBox_CostToPlay.Name = "TexBox_CostToPlay";
            this.TexBox_CostToPlay.Size = new System.Drawing.Size(619, 20);
            this.TexBox_CostToPlay.TabIndex = 8;
            this.TexBox_CostToPlay.TextChanged += new System.EventHandler(this.TexBox_CostToPlay_TextChanged);
            // 
            // TexBox_StartOptions
            // 
            this.TexBox_StartOptions.Location = new System.Drawing.Point(111, 141);
            this.TexBox_StartOptions.Name = "TexBox_StartOptions";
            this.TexBox_StartOptions.Size = new System.Drawing.Size(619, 20);
            this.TexBox_StartOptions.TabIndex = 7;
            // 
            // Lab_GameStartOptions
            // 
            this.Lab_GameStartOptions.AutoSize = true;
            this.Lab_GameStartOptions.Location = new System.Drawing.Point(6, 144);
            this.Lab_GameStartOptions.Name = "Lab_GameStartOptions";
            this.Lab_GameStartOptions.Size = new System.Drawing.Size(99, 13);
            this.Lab_GameStartOptions.TabIndex = 6;
            this.Lab_GameStartOptions.Text = "Game Start Options";
            // 
            // Lab_CostToPlay
            // 
            this.Lab_CostToPlay.AutoSize = true;
            this.Lab_CostToPlay.Location = new System.Drawing.Point(6, 92);
            this.Lab_CostToPlay.Name = "Lab_CostToPlay";
            this.Lab_CostToPlay.Size = new System.Drawing.Size(67, 13);
            this.Lab_CostToPlay.TabIndex = 5;
            this.Lab_CostToPlay.Text = "Cost To Play";
            // 
            // Lab_PlayTime
            // 
            this.Lab_PlayTime.AutoSize = true;
            this.Lab_PlayTime.Location = new System.Drawing.Point(6, 220);
            this.Lab_PlayTime.Name = "Lab_PlayTime";
            this.Lab_PlayTime.Size = new System.Drawing.Size(53, 13);
            this.Lab_PlayTime.TabIndex = 4;
            this.Lab_PlayTime.Text = "Play Time";
            // 
            // Lab_GamePath
            // 
            this.Lab_GamePath.AutoSize = true;
            this.Lab_GamePath.Location = new System.Drawing.Point(6, 194);
            this.Lab_GamePath.Name = "Lab_GamePath";
            this.Lab_GamePath.Size = new System.Drawing.Size(60, 13);
            this.Lab_GamePath.TabIndex = 3;
            this.Lab_GamePath.Text = "Game Path";
            // 
            // Lab_ImagePath
            // 
            this.Lab_ImagePath.AutoSize = true;
            this.Lab_ImagePath.Location = new System.Drawing.Point(6, 169);
            this.Lab_ImagePath.Name = "Lab_ImagePath";
            this.Lab_ImagePath.Size = new System.Drawing.Size(61, 13);
            this.Lab_ImagePath.TabIndex = 2;
            this.Lab_ImagePath.Text = "Image Path";
            // 
            // Lab_Name
            // 
            this.Lab_Name.AutoSize = true;
            this.Lab_Name.Location = new System.Drawing.Point(6, 121);
            this.Lab_Name.Name = "Lab_Name";
            this.Lab_Name.Size = new System.Drawing.Size(35, 13);
            this.Lab_Name.TabIndex = 1;
            this.Lab_Name.Text = "Name";
            // 
            // Lab_GUID
            // 
            this.Lab_GUID.AutoSize = true;
            this.Lab_GUID.Location = new System.Drawing.Point(6, 16);
            this.Lab_GUID.Name = "Lab_GUID";
            this.Lab_GUID.Size = new System.Drawing.Size(34, 13);
            this.Lab_GUID.TabIndex = 0;
            this.Lab_GUID.Text = "GUID";
            // 
            // GroBox_OptionalExesEditor
            // 
            this.GroBox_OptionalExesEditor.Controls.Add(this.TexBox_ExeGUID);
            this.GroBox_OptionalExesEditor.Controls.Add(this.Lab_ExeGUID);
            this.GroBox_OptionalExesEditor.Controls.Add(this.CheBox_IsDeletedOpExe);
            this.GroBox_OptionalExesEditor.Controls.Add(this.TexBox_ExeOpt);
            this.GroBox_OptionalExesEditor.Controls.Add(this.TexBox_Delay);
            this.GroBox_OptionalExesEditor.Controls.Add(this.Lab_ExeStartOptions);
            this.GroBox_OptionalExesEditor.Controls.Add(this.TexBox_ExePath);
            this.GroBox_OptionalExesEditor.Controls.Add(this.Lab_Delay);
            this.GroBox_OptionalExesEditor.Controls.Add(this.Lab_ExePath);
            this.GroBox_OptionalExesEditor.Location = new System.Drawing.Point(7, 876);
            this.GroBox_OptionalExesEditor.Name = "GroBox_OptionalExesEditor";
            this.GroBox_OptionalExesEditor.Size = new System.Drawing.Size(736, 158);
            this.GroBox_OptionalExesEditor.TabIndex = 7;
            this.GroBox_OptionalExesEditor.TabStop = false;
            this.GroBox_OptionalExesEditor.Text = "Optional Exes Editor";
            // 
            // TexBox_ExeOpt
            // 
            this.TexBox_ExeOpt.Location = new System.Drawing.Point(111, 82);
            this.TexBox_ExeOpt.Name = "TexBox_ExeOpt";
            this.TexBox_ExeOpt.Size = new System.Drawing.Size(619, 20);
            this.TexBox_ExeOpt.TabIndex = 18;
            // 
            // TexBox_Delay
            // 
            this.TexBox_Delay.Location = new System.Drawing.Point(111, 59);
            this.TexBox_Delay.Name = "TexBox_Delay";
            this.TexBox_Delay.Size = new System.Drawing.Size(619, 20);
            this.TexBox_Delay.TabIndex = 15;
            this.TexBox_Delay.TextChanged += new System.EventHandler(this.TexBox_Delay_TextChanged);
            // 
            // Lab_ExeStartOptions
            // 
            this.Lab_ExeStartOptions.AutoSize = true;
            this.Lab_ExeStartOptions.Location = new System.Drawing.Point(5, 85);
            this.Lab_ExeStartOptions.Name = "Lab_ExeStartOptions";
            this.Lab_ExeStartOptions.Size = new System.Drawing.Size(89, 13);
            this.Lab_ExeStartOptions.TabIndex = 17;
            this.Lab_ExeStartOptions.Text = "Exe Start Options";
            // 
            // TexBox_ExePath
            // 
            this.TexBox_ExePath.Location = new System.Drawing.Point(111, 37);
            this.TexBox_ExePath.Name = "TexBox_ExePath";
            this.TexBox_ExePath.Size = new System.Drawing.Size(619, 20);
            this.TexBox_ExePath.TabIndex = 14;
            // 
            // Lab_Delay
            // 
            this.Lab_Delay.AutoSize = true;
            this.Lab_Delay.Location = new System.Drawing.Point(6, 62);
            this.Lab_Delay.Name = "Lab_Delay";
            this.Lab_Delay.Size = new System.Drawing.Size(34, 13);
            this.Lab_Delay.TabIndex = 8;
            this.Lab_Delay.Text = "Delay";
            // 
            // Lab_ExePath
            // 
            this.Lab_ExePath.AutoSize = true;
            this.Lab_ExePath.Location = new System.Drawing.Point(6, 40);
            this.Lab_ExePath.Name = "Lab_ExePath";
            this.Lab_ExePath.Size = new System.Drawing.Size(50, 13);
            this.Lab_ExePath.TabIndex = 7;
            this.Lab_ExePath.Text = "Exe Path";
            // 
            // But_ReplaceSelectOptionalExe
            // 
            this.But_ReplaceSelectOptionalExe.Location = new System.Drawing.Point(137, 464);
            this.But_ReplaceSelectOptionalExe.Name = "But_ReplaceSelectOptionalExe";
            this.But_ReplaceSelectOptionalExe.Size = new System.Drawing.Size(118, 42);
            this.But_ReplaceSelectOptionalExe.TabIndex = 8;
            this.But_ReplaceSelectOptionalExe.Text = "Replace Selected Optional Exe";
            this.But_ReplaceSelectOptionalExe.UseVisualStyleBackColor = true;
            this.But_ReplaceSelectOptionalExe.Click += new System.EventHandler(this.But_ReplaceSelectOptionalExe_Click);
            // 
            // RicTexBox_Description
            // 
            this.RicTexBox_Description.Location = new System.Drawing.Point(111, 244);
            this.RicTexBox_Description.Name = "RicTexBox_Description";
            this.RicTexBox_Description.Size = new System.Drawing.Size(619, 85);
            this.RicTexBox_Description.TabIndex = 21;
            this.RicTexBox_Description.Text = "";
            // 
            // CheBox_IsDeletedOpExe
            // 
            this.CheBox_IsDeletedOpExe.AutoSize = true;
            this.CheBox_IsDeletedOpExe.Location = new System.Drawing.Point(9, 111);
            this.CheBox_IsDeletedOpExe.Name = "CheBox_IsDeletedOpExe";
            this.CheBox_IsDeletedOpExe.Size = new System.Drawing.Size(74, 17);
            this.CheBox_IsDeletedOpExe.TabIndex = 22;
            this.CheBox_IsDeletedOpExe.Text = "Is Deleted";
            this.CheBox_IsDeletedOpExe.UseVisualStyleBackColor = true;
            // 
            // TexBox_ExeGUID
            // 
            this.TexBox_ExeGUID.Location = new System.Drawing.Point(111, 11);
            this.TexBox_ExeGUID.Name = "TexBox_ExeGUID";
            this.TexBox_ExeGUID.Size = new System.Drawing.Size(619, 20);
            this.TexBox_ExeGUID.TabIndex = 24;
            // 
            // Lab_ExeGUID
            // 
            this.Lab_ExeGUID.AutoSize = true;
            this.Lab_ExeGUID.Location = new System.Drawing.Point(6, 14);
            this.Lab_ExeGUID.Name = "Lab_ExeGUID";
            this.Lab_ExeGUID.Size = new System.Drawing.Size(34, 13);
            this.Lab_ExeGUID.TabIndex = 23;
            this.Lab_ExeGUID.Text = "GUID";
            // 
            // GameConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 1040);
            this.Controls.Add(this.But_ReplaceSelectOptionalExe);
            this.Controls.Add(this.GroBox_OptionalExesEditor);
            this.Controls.Add(this.GroBox_GameEditor);
            this.Controls.Add(this.But_Save);
            this.Controls.Add(this.But_AddExeToSelectedGame);
            this.Controls.Add(this.But_AddGame);
            this.Controls.Add(this.But_DeleteSelected);
            this.Controls.Add(this.But_ReplaceSelectGame);
            this.Controls.Add(this.TreVie_GamesView);
            this.Name = "GameConfigEditor";
            this.Text = "GameConfigEditor";
            this.GroBox_GameEditor.ResumeLayout(false);
            this.GroBox_GameEditor.PerformLayout();
            this.GroBox_OptionalExesEditor.ResumeLayout(false);
            this.GroBox_OptionalExesEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreVie_GamesView;
        private System.Windows.Forms.Button But_ReplaceSelectGame;
        private System.Windows.Forms.Button But_DeleteSelected;
        private System.Windows.Forms.Button But_AddGame;
        private System.Windows.Forms.Button But_AddExeToSelectedGame;
        private System.Windows.Forms.Button But_Save;
        private System.Windows.Forms.GroupBox GroBox_GameEditor;
        private System.Windows.Forms.Label Lab_ImagePath;
        private System.Windows.Forms.Label Lab_Name;
        private System.Windows.Forms.Label Lab_GUID;
        private System.Windows.Forms.GroupBox GroBox_OptionalExesEditor;
        private System.Windows.Forms.Label Lab_GamePath;
        private System.Windows.Forms.Label Lab_PlayTime;
        private System.Windows.Forms.Label Lab_CostToPlay;
        private System.Windows.Forms.Label Lab_GameStartOptions;
        private System.Windows.Forms.Label Lab_Delay;
        private System.Windows.Forms.Label Lab_ExePath;
        private System.Windows.Forms.TextBox TexBox_GUID;
        private System.Windows.Forms.TextBox TexBox_Name;
        private System.Windows.Forms.TextBox TexBox_ImagePath;
        private System.Windows.Forms.TextBox TexBox_GamePath;
        private System.Windows.Forms.TextBox TexBox_PlayTime;
        private System.Windows.Forms.TextBox TexBox_CostToPlay;
        private System.Windows.Forms.TextBox TexBox_StartOptions;
        private System.Windows.Forms.TextBox TexBox_Delay;
        private System.Windows.Forms.TextBox TexBox_ExePath;
        private System.Windows.Forms.Button But_ReplaceSelectOptionalExe;
        private System.Windows.Forms.Label Lab_Description;
        private System.Windows.Forms.TextBox TexBox_ExeOpt;
        private System.Windows.Forms.Label Lab_ExeStartOptions;
        private System.Windows.Forms.CheckBox CheBox_IsDeleted;
        private System.Windows.Forms.CheckBox CheBox_IsActive;
        private System.Windows.Forms.TextBox TexBox_MinNumberOfShares;
        private System.Windows.Forms.Label Lab_MinNumberOfShares;
        private System.Windows.Forms.TextBox TexBox_MaxNumberOfShares;
        private System.Windows.Forms.Label Lab_MaxNumberOfShares;
        private System.Windows.Forms.RichTextBox RicTexBox_Description;
        private System.Windows.Forms.CheckBox CheBox_IsDeletedOpExe;
        private System.Windows.Forms.TextBox TexBox_ExeGUID;
        private System.Windows.Forms.Label Lab_ExeGUID;
    }
}