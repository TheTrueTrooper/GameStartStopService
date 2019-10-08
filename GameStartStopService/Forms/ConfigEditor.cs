using GameStartStopService.BasicConfig;
using GameStartStopService.ConfigEnums;
using GameStartStopService.ConfigLoaders;
using GameStartStopService.UtilitiesFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStartStopService
{
    public partial class ConfigEditor : Form
    {
        bool NeedVerdict = false;
        bool ShowingErrorAlready = false;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                if (!NeedVerdict)
                    return base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public ConfigEditor(bool NeedVerdict = false)
        {
            this.NeedVerdict = NeedVerdict;
            InitializeComponent();
        }

        static internal ConfigEditor ConfigEditorFactoryFromConfig(bool NeedVerdict = false)
        {
            ConfigEditor NewInstance = new ConfigEditor(NeedVerdict);
            NewInstance.TexBox_LocalLogOutput.Text = ArcadeGameStartAndStopService.MainConfig.MachineGUID;
            NewInstance.TexBox_LocalLogOutput.Text = ArcadeGameStartAndStopService.MainConfig.LocalLogOutput;
            NewInstance.TexBox_MachineName.Text = ArcadeGameStartAndStopService.MainConfig.MachineName;
            NewInstance.TexBox_MasterServerURL.Text = ArcadeGameStartAndStopService.MainConfig.MasterServerURL;
            NewInstance.TexBox_MasterStarterMasterLoc.Text = ArcadeGameStartAndStopService.MainConfig.MasterStarterMasterLoc;
            NewInstance.TexBox_Password.Text = ArcadeGameStartAndStopService.MainConfig.ServerCredential.Password;
            NewInstance.TexBox_UserName.Text = ArcadeGameStartAndStopService.MainConfig.ServerCredential.UserName;
            NewInstance.TexBox_ServerLogOutput.Text = ArcadeGameStartAndStopService.MainConfig.ServerLogOutput;
            //NewInstance.TexBox_DefualtGameGUID.Text = NewInstance.Config.DefualtGameGUID;
            NewInstance.LisBox_GameStarterMode.SelectedIndex = NewInstance.LisBox_GameStarterMode.FindStringExact(ArcadeGameStartAndStopService.MainConfig.StarterMode.ToString());
            NewInstance.LisBox_ServerMode.SelectedIndex = NewInstance.LisBox_ServerMode.FindStringExact(ArcadeGameStartAndStopService.MainConfig.ServerMode.ToString());
            NewInstance.LisBox_CardMode.SelectedIndex = NewInstance.LisBox_CardMode.FindStringExact(ArcadeGameStartAndStopService.MainConfig.CardModeMode.ToString());
            if (ArcadeGameStartAndStopService.MainConfig.StarterMode != GameStartMode.MultiSocketStarterSlave)
                DisableBox(NewInstance.TexBox_MasterStarterMasterLoc);
            return NewInstance;
        }
        
        static void DisableBox(TextBox TB)
        {
            TB.Enabled = false;
            TB.BackColor = Color.LightGray;
        }

        static void EnableBox(TextBox TB)
        {
            TB.Enabled = true;
            TB.BackColor = Color.White;
        }

        GameStartMode GetGameStartModeFromString(string StRep)
        {
            switch(StRep)
            {
                case "SingleGameStarter":
                    return GameStartMode.SingleGameStarter;
                case "MultiSocketStarterSlave":
                    return GameStartMode.MultiSocketStarterSlave;
                case "MultiSocketStarterMaster":
                    return GameStartMode.MultiSocketStarterMaster;
                case "AttendantChargeDeskOnly":
                    return GameStartMode.AttendantChargeDeskOnly;
            }
            return ArcadeGameStartAndStopService.MainConfig.StarterMode.Value;
        }

        CardModeMode GetCardModeFromString(string StRep)
        {
            switch (StRep)
            {
                case "NoCardNeededDemoMode":
                    return CardModeMode.NoCardNeededDemoMode;
                case "UseCard":
                    return CardModeMode.UseCard;
            }
            return ArcadeGameStartAndStopService.MainConfig.CardModeMode.Value;
        }

        ServerMode GetServerModeFromString(string StRep)
        {
            switch (StRep)
            {
                case "ConnectToServer":
                    return ServerMode.ConnectToServer;
                case "NoServerDemoMode":
                    return ServerMode.NoServerDemoMode;
            }
            return ArcadeGameStartAndStopService.MainConfig.ServerMode.Value;
        }

        private void But_Save_Click(object sender, EventArgs e)
        {
            if (TexBox_MachineGUID.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.MachineGUID = TexBox_MachineGUID.Text;
            if (TexBox_LocalLogOutput.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.LocalLogOutput = TexBox_LocalLogOutput.Text;
            if (TexBox_MachineName.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.MachineName = TexBox_MachineName.Text;
            if (TexBox_MasterServerURL.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.MasterServerURL = TexBox_MasterServerURL.Text;
            if (TexBox_MasterStarterMasterLoc.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.MasterStarterMasterLoc = TexBox_MasterStarterMasterLoc.Text;
            if (TexBox_Password.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.ServerCredential.Password = TexBox_Password.Text;
            if (TexBox_UserName.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.ServerCredential.UserName = TexBox_UserName.Text;
            if (TexBox_ServerLogOutput.Text.Trim() != "")
                ArcadeGameStartAndStopService.MainConfig.ServerLogOutput = TexBox_ServerLogOutput.Text;
            //if (TexBox_ServerLogOutput.Text.Trim() != "") //depreciated with game selector
            //    Config.DefualtGameGUID = TexBox_DefualtGameGUID.Text;
            ArcadeGameStartAndStopService.MainConfig.StarterMode = GetGameStartModeFromString(LisBox_GameStarterMode.Items[LisBox_GameStarterMode.SelectedIndex].ToString());

            ArcadeGameStartAndStopService.MainConfig.ServerMode = GetServerModeFromString(LisBox_GameStarterMode.Items[LisBox_GameStarterMode.SelectedIndex].ToString());

            ArcadeGameStartAndStopService.MainConfig.CardModeMode = GetCardModeFromString(LisBox_GameStarterMode.Items[LisBox_GameStarterMode.SelectedIndex].ToString());

            Dictionary<string, List<string>> Errors;
            if (!ConfigHelpers.ValidateConfig(ArcadeGameStartAndStopService.MainConfig, out Errors))
            {
                ShowingErrorAlready = true;
                MessageBox.Show(ConfigHelpers.ConvertErrorsToMessage(Errors));
                return;
            }
            ShowingErrorAlready = true;

            ArcadeGameStartAndStopService.Logger.WriteLog("'Arcade Game Start And Stop Service' has changed config.");

            JSONServiceConfig.SaveJSONServiceConfig(ArcadeGameStartAndStopService.MainConfig);
            Close();
        }

        private void LisBox_GameStarterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArcadeGameStartAndStopService.MainConfig.StarterMode = GetGameStartModeFromString(LisBox_GameStarterMode.Items[LisBox_GameStarterMode.SelectedIndex].ToString());
            if (ArcadeGameStartAndStopService.MainConfig.StarterMode != GameStartMode.MultiSocketStarterSlave)
            {
                DisableBox(TexBox_MasterStarterMasterLoc);
                ArcadeGameStartAndStopService.MainConfig.MasterStarterMasterLoc = null;
            }
            else
            {
                EnableBox(TexBox_MasterStarterMasterLoc);
                if (TexBox_MasterStarterMasterLoc.Text.Trim() != "")
                    ArcadeGameStartAndStopService.MainConfig.MasterStarterMasterLoc = TexBox_MasterStarterMasterLoc.Text;
                else
                    ArcadeGameStartAndStopService.MainConfig.MasterStarterMasterLoc = null;
            }
        }

        private void ConfigEditor_Leave(object sender, EventArgs e)
        {
            if (NeedVerdict && !ShowingErrorAlready)
            {
                TopMost = true;
                BringToFront();
                MessageBox.Show("Please condig the service to avoid any more errors.");
            }
        }

        private void ConfigEditor_Deactivate(object sender, EventArgs e)
        {
            if (NeedVerdict && !ShowingErrorAlready)
            {
                TopMost = true;
                BringToFront();
                MessageBox.Show("Please condig the service to avoid any more errors.");
            }
        }
    }
}
