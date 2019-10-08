namespace GameStartStopService
{
    partial class PasswordCorrector
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
            this.Lab_UserName = new System.Windows.Forms.Label();
            this.Lab_Password = new System.Windows.Forms.Label();
            this.But_Save = new System.Windows.Forms.Button();
            this.TexBox_UserName = new System.Windows.Forms.TextBox();
            this.TexBox_Password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Lab_UserName
            // 
            this.Lab_UserName.AutoSize = true;
            this.Lab_UserName.Location = new System.Drawing.Point(12, 12);
            this.Lab_UserName.Name = "Lab_UserName";
            this.Lab_UserName.Size = new System.Drawing.Size(60, 13);
            this.Lab_UserName.TabIndex = 0;
            this.Lab_UserName.Text = "User Name";
            // 
            // Lab_Password
            // 
            this.Lab_Password.AutoSize = true;
            this.Lab_Password.Location = new System.Drawing.Point(12, 37);
            this.Lab_Password.Name = "Lab_Password";
            this.Lab_Password.Size = new System.Drawing.Size(53, 13);
            this.Lab_Password.TabIndex = 1;
            this.Lab_Password.Text = "Password";
            // 
            // But_Save
            // 
            this.But_Save.Location = new System.Drawing.Point(15, 60);
            this.But_Save.Name = "But_Save";
            this.But_Save.Size = new System.Drawing.Size(642, 23);
            this.But_Save.TabIndex = 2;
            this.But_Save.Text = "Save";
            this.But_Save.UseVisualStyleBackColor = true;
            this.But_Save.Click += new System.EventHandler(this.But_Save_Click);
            // 
            // TexBox_UserName
            // 
            this.TexBox_UserName.Location = new System.Drawing.Point(78, 9);
            this.TexBox_UserName.Name = "TexBox_UserName";
            this.TexBox_UserName.Size = new System.Drawing.Size(579, 20);
            this.TexBox_UserName.TabIndex = 3;
            // 
            // TexBox_Password
            // 
            this.TexBox_Password.Location = new System.Drawing.Point(78, 34);
            this.TexBox_Password.Name = "TexBox_Password";
            this.TexBox_Password.Size = new System.Drawing.Size(579, 20);
            this.TexBox_Password.TabIndex = 4;
            // 
            // PasswordCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 102);
            this.Controls.Add(this.TexBox_Password);
            this.Controls.Add(this.TexBox_UserName);
            this.Controls.Add(this.But_Save);
            this.Controls.Add(this.Lab_Password);
            this.Controls.Add(this.Lab_UserName);
            this.Name = "PasswordCorrector";
            this.Text = "PasswordCorrector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_UserName;
        private System.Windows.Forms.Label Lab_Password;
        private System.Windows.Forms.Button But_Save;
        private System.Windows.Forms.TextBox TexBox_UserName;
        private System.Windows.Forms.TextBox TexBox_Password;
    }
}