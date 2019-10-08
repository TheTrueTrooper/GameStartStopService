using System;
using System.Drawing;

namespace GameStartStopService
{
    partial class ArcadeGameStartAndStopService
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
            this.NoIc_GameStarterStopper = new System.Windows.Forms.NotifyIcon(this.components);
            // 
            // NoIc_GameStarterStopper
            // 
            this.NoIc_GameStarterStopper.Text = "GameStarterStopper";
            this.NoIc_GameStarterStopper.Visible = false;
            this.NoIc_GameStarterStopper.Icon = new Icon("VRRcadeServiceIcon.ico");
            // 
            // Service1
            // 
            this.ServiceName = "VRArcadeGameStarterStopperService";
        }

        #endregion

        private System.Windows.Forms.NotifyIcon NoIc_GameStarterStopper;
    }
}
