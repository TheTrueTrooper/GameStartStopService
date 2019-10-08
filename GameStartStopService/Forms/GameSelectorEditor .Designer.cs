namespace GameStartStopService
{
    partial class GameSelectorEditor
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
            this.But_SelectGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TreVie_GamesView
            // 
            this.TreVie_GamesView.Location = new System.Drawing.Point(12, 12);
            this.TreVie_GamesView.Name = "TreVie_GamesView";
            this.TreVie_GamesView.Size = new System.Drawing.Size(736, 372);
            this.TreVie_GamesView.TabIndex = 0;
            this.TreVie_GamesView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreVie_GamesView_AfterSelect);
            // 
            // But_SelectGame
            // 
            this.But_SelectGame.Location = new System.Drawing.Point(12, 390);
            this.But_SelectGame.Name = "But_SelectGame";
            this.But_SelectGame.Size = new System.Drawing.Size(736, 42);
            this.But_SelectGame.TabIndex = 1;
            this.But_SelectGame.Text = "Select Game";
            this.But_SelectGame.UseVisualStyleBackColor = true;
            this.But_SelectGame.Click += new System.EventHandler(this.But_SelectGame_Click);
            // 
            // GameSelectorEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 444);
            this.Controls.Add(this.But_SelectGame);
            this.Controls.Add(this.TreVie_GamesView);
            this.Name = "GameSelectorEditor";
            this.Text = "GameConfigEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreVie_GamesView;
        private System.Windows.Forms.Button But_SelectGame;
    }
}