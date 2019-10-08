﻿using GameStartStopService.BasicConfig;
using GameStartStopService.ConfigLoaders;
using GameStartStopService.UtilitiesFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using GameInstancerNS;
using GameStartStopService.TheServerClient;
using GameStartStopService.TheServerClient.ClientModels;
using System.IO.Pipes;
using System.Threading;
using GameStartStopService.ConfigEnums;
using GameSelectorWrapper;
using System.Runtime.InteropServices;
using GameStartStopService.PipeServer.Events;
using RL8000_NFCReader;
using RL8000_NFCReader.MifareClassicEvents;

namespace GameStartStopService
{
    public partial class ArcadeGameStartAndStopService : ServiceBase
    {
        #region UngracefullShutDownC++toC#HandlerConstruct
        #region Kernel32ExitImport
        /// <summary>
        /// Kernal32 fuction import to allow us to grab our applications closing and respond to it as an event
        /// </summary>
        /// <param name="CloseEventHandler">the Event or funtion to call</param>
        /// <param name="add"></param>
        /// <returns></returns>
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(CloseEvent CloseEventHandler, bool add = true);

        /// <summary>
        /// the Control type pass back based on C++ enums
        /// </summary>
        public enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        #endregion
        #endregion

        /// <summary>
        /// the Event to tye to the sys 32 event is handled inside and passed out through event chaining
        /// </summary>
        /// <param name="sig">the systems pass back values</param>
        /// <returns></returns>
        private delegate bool CloseEvent(CtrlType sig);

        /// <summary>
        /// A instance of the event
        /// </summary>
        CloseEvent CloseEventHandle;

        const string ServerName = "GameStartAndStopServicePipe";

        static GameInstancer GameStarter;

        static GameMenu GameMenu;

        static PasswordCorrector PasswordCorrector;

        internal static GameServiceConfig MainConfig;
        internal static ServiceJSONGamesConfig GameConfig;
        internal static Logger Logger;

        static string GameGUID;

        static ConfigEditor ConfigEditorInstance;
        static GameConfigEditor GameConfigEditorInstance;
        static GameSelectorEditor GameSelectorEditorInstance;

        static ServerClient TheServerClient;

        static bool Disabled = false;

        //depreciated
        //static ACR122UManager CardManger = null;

        static RL8000_NFC NFCReader;

        static int MenuDisableLocation = 0;

        public bool GameRunning
        {
            get
            {
                return GameStarter.GameIsRunning;
            }
        }

        public bool MenuRunning
        {
            get
            {
                return GameMenu.ShouldBeRunning;
            }
        }

        //deprecated readonly static byte[][] AcceptedATRS = new byte[][] { new byte[]{ 0x3B, 0x8F, 0x80, 0x01, 0x80, 0x4F, 0x0C, 0xA0, 0x00, 0x00, 0x03, 0x06, 0x03, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x6A } };
        readonly static byte[] AuthenticationKeys = new byte[6] { 0xAF, 0xFA, 0x03, 0xF1, 0x0A, 0xA9 }; //Our key
        //readonly static byte[] AuthenticationKeys = new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }; //Blank key

        public ArcadeGameStartAndStopService()
        {
            InitializeComponent();
            MainConfig = JSONServiceConfig.GetJSONServiceConfig();
            Logger = new Logger();

            //GameServicePipeServer.Service = this;

            TheServerClient = new ServerClient(MainConfig);

            GameServicePipeServer ServerController = new GameServicePipeServer();
            ServerController.PlayGameEvent += StartGameRequested;

            GameMenu = new GameMenu(ServerController);

            #region depreciated
            //ACR122UManager.GlobalCardCheck = (e) =>
            //{
            //    return AcceptedATRS.Any(x =>
            //    {
            //        bool CeckSuccess = false;
            //        if (e.ATR.Length == x.Length)
            //        {
            //            CeckSuccess = true;
            //            for (int i = 0; i < e.ATR.Length; i++)
            //            {
            //                if (e.ATR[i] != x[i])
            //                {
            //                    CeckSuccess = false;
            //                    break;
            //                }
            //            }
            //        }
            //        return CeckSuccess;
            //    });
            //};

            //if (MainConfig.DefualtGameGUID == null || !GameStarter.Config.Games.Any(x => x.GUID == MainConfig.DefualtGameGUID))
            //{
            //    Logger.WriteLog("Game GUID doesnt exist. Will attempt revert to first game in list.", LoggerWarringLevel.Warring);
            //    MainConfig.DefualtGameGUID = GameStarter.Config.Games.FirstOrDefault(x=>true)?.GUID;
            //    if (MainConfig.DefualtGameGUID == null)
            //    {
            //        Logger.WriteLog("Game list seems to be empty. Please populate game list before starting service. Service will now Stop.", LoggerWarringLevel.Danger);
            //        MessageBox.Show("Game list seems to be empty.\nPlease populate game list before starting service.\nService will now Stop.");
            //        throw new Exception("Game list seems to be empty.\nPlease populate game list before starting service.\nService will now Stop.");
            //    }
            //}

            //GameGUID = MainConfig.DefualtGameGUID;

            #endregion

            #region Kernel32EventTieing
            CloseEventHandle += new CloseEvent(CloseEventAction);
            SetConsoleCtrlHandler(CloseEventHandle);
            #endregion

        }

        #region DebugExtras
#if DEBUG
        public void OnDebugStart()
        {
            OnStart(null);
        }

        public void OnDebugEnd()
        {
            OnStop();
        }
#endif
        #endregion

        #region ServiceStartStop
        protected override void OnStart(string[] args)
        {
            #region depreciated
            //if (MainConfig.CardModeMode != CardModeMode.NoCardNeededDemoMode)
            //{
            //    //while (CardManger == null || CardManger?.IsDisposed == true)
            //    //{
            //    //    try
            //    //    {
            //    //        List<string> Readers = ACR122UManager.GetACR122UReaders();
            //    //        CardManger = new ACR122UManager(Readers[0]);
            //    //    }
            //    //    catch
            //    //    {
            //    //        Logger.WriteLog("Error no card reader detected...Please Replugin a compatable cardreader", LoggerWarringLevel.Danger);
            //    //        MessageBox.Show("Error no card reader detected...\nPlease Replugin a compatable cardreader and click ok.");
            //    //    }
            //    //}


            //    //CardManger.CheckCard = true;

            //    //CardManger.CardDetected += CardDectected;
            //    //CardManger.CardRemoved += CardRemoved;
            //    //CardManger.AcceptedCardScaned += AcceptedCard;
            //    //CardManger.RejectedCardScaned += RejectedCard;
            //    //CardManger.CardStateChanged += CardStateChanged;

            //}
            #endregion

            if (MainConfig.ServerMode != ServerMode.NoServerDemoMode)
            {
                while (!TheServerClient.Connected)
                {
                    try
                    {
                        TheServerClient.Connect();
                        ResponseInfo<GetMachineGamesReturn, ResponseStatus> Return = TheServerClient.GetMachineGames(MainConfig.MachineGUID);
                        GameConfig = new ServiceJSONGamesConfig(Return.Data);
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Please provide valid credentials,either user name is invalid or password is invalid")
                        {
                            Logger.WriteLog("Access denied due to incorrect password.", LoggerWarringLevel.Danger);
                            MessageBox.Show("Access denied due to incorrect password. Please correct the password now.");
                            EditServerCredential_Click(this, new EventArgs());
                        }
                        else
                        {
                            Logger.WriteLog("Error - Server is not responding. Will try again in 20000ms.", LoggerWarringLevel.Danger);
                            Thread.Sleep(20000);
                        }
                    }
                }
            }
            else
                GameConfig = new ServiceJSONGamesConfig();



            GameStarter = new GameInstancer(GameConfig);

            if (!MenuRunning)
                GameMenu.StartMenu();

            GameStarter.GameIsStartingEvent += GameInstancerStartingGame;
            GameStarter.GameHasStartedEvent += GameInstancerGameHasStarted;
            GameStarter.GameHasEndedEvent += GameInstancerGameHasEnded;

            if (MainConfig.CardModeMode != CardModeMode.NoCardNeededDemoMode)
            {
                NFCReader = new RL8000_NFC();
                NFCReader.MifareClassicISO1443ACardDetectedEvent += CardDetected;
            }
            else
            {
                try
                {
                    NFCReader = new RL8000_NFC();
                    NFCReader.MifareClassicISO1443ACardDetectedEvent += CardDetected;
                }
                catch
                {}
            }

            NoIc_GameStarterStopper.ContextMenuStrip = BuildMenu();
            NoIc_GameStarterStopper.Visible = true;

            Logger.WriteLog($"'Arcade Game Start And Stop Service' has Started with the following parameters:\n\tCardModeMode:{MainConfig.CardModeMode}\n\tServerMode:{MainConfig.ServerMode}\n");
        }

        protected override void OnStop()
        {
            try
            {
                #region depreciated
                //CardManger?.Dispose();
                //CardManger = null;
                #endregion
                NFCReader?.Dispose();
                NFCReader = null;
            }
            catch { }

            if(GameRunning)
                GameStarter.KillGame();
            if (MenuRunning)
                GameMenu.KillMenu();

            Logger.WriteLog("'Arcade Game Start And Stop Service' has stopped.");
        }
        #endregion

        #region GameStartModes
        void StartGameAsSingleGameStarter(MifareClassicISO1443ACardDetectedEventArg e)
        {
            if (MainConfig.ServerMode == ServerMode.NoServerDemoMode || MainConfig.CardModeMode == CardModeMode.NoCardNeededDemoMode)
            {
                Logger.WriteLog($"\nCard Detected Serverless Demo Started {DateTime.UtcNow}\nCardInfo\n\tAir Protocol:{e.CardInfo.AirProtocalID}\n\tAntennaID:{e.CardInfo.AntennaID}\n\tTagID:{e.CardInfo.TagID}\n\tUID:{BitConverter.ToString(e.CardInfo.UID, 0, e.CardInfo.UIDlen)}\n\tDSFID:{e.CardInfo.DSFID}");
                GameMenu.NotifyOfCardTap();
            }
            else
            {
                try
                {
                    //CardManger.CheckCardTransactionWithServer(TheServerClient);
                    //CardManger.PlayGameTransactionWithServer(TheServerClient, GameGUID);

                    try
                    {
                        Logger.WriteLog("Game service starting Game");
                        GameStarter.StartGame(this, GameGUID);
                    }
                    catch
                    {
                        Logger.WriteLog("Game has failed to start\nPlease check your game config file.", LoggerWarringLevel.Danger);
                    }
                }
                catch
                {
                    try
                    {
                        //byte Data;
                        //CardManger.SetLEDandBuzzerControl(NFC_CardReader.ACR122U.ACR122U_LEDControl.AllOn, 20, 20, 5, NFC_CardReader.ACR122U.ACR122U_BuzzerControl.BuzzerOnT1And2Cycle, out Data);
                        //CardManger.DisconnectToCard();
                        Logger.WriteLog("Error reading card - Razing buzzer razed", LoggerWarringLevel.Warring);
                    }
                    catch
                    {
                        Logger.WriteLog("Error reading card - Razing buzzer razed", LoggerWarringLevel.Warring);
                    }
                }
            }
        }

        void StartGameAsMultiSocketStarterMaster()
        {
            throw new NotImplementedException();
        }

        void StartGameAsMultiSocketStarterSlave()
        {
            throw new NotImplementedException();
        }

        void StartGameAsAttendantChargeDeskOnly()
        {
            try
            {
                const string SuccessfullCharge = "Card was successfully charged.";
                //ResponseInfo<CheckCardReturn, ResponseStatus> Return1 = CardManger.CheckCardTransactionWithServer(TheServerClient);
                //ResponseInfo<PlayGameReturn, ResponseStatus> Return2 = CardManger.PlayGameTransactionWithServer(TheServerClient, GameGUID);
                //Logger.WriteLog($"{SuccessfullCharge}\n{Return2.Message}");
                MessageBox.Show(SuccessfullCharge);
            }
            catch
            {
                const string SuccessfullCharge = "Card was not charged please check card at nearest desk.";
                Logger.WriteLog(SuccessfullCharge, LoggerWarringLevel.Warring);
                MessageBox.Show(SuccessfullCharge);
            }
            
        }

        #region SpecialCardTaps
        #endregion
        #endregion

        #region CardReaderEvent
        private void CardDetected(object sender, MifareClassicISO1443ACardDetectedEventArg e)
        {
            if (!Disabled)
            {
                switch(MainConfig.StarterMode)
                {
                    case GameStartMode.SingleGameStarter:
                        StartGameAsSingleGameStarter(e);
                        break;
                }
            }
        }
        #endregion

        #region depreciated
        #region CardReaderEvents
        //private void AcceptedCard(object sender, ACRCardAcceptedCardScanEventArg e)
        //{
        //    if (!Disabled)
        //    {
        //        if (e.EventsACR122UManager.Card == null && !GameRunning)
        //        {
        //            #region depreciated
        //            //byte Data;
        //            //Logger.WriteLog("Accepted Card with ATR: " + e.ATRString);
        //            //try
        //            //{
        //            //    CardManger.ConnectToCard();
        //            //    CardManger.LoadAthenticationKeys(ACR122U_KeyMemories.Key1, AuthenticationKeys);
        //            //}
        //            //catch
        //            //{
        //            //    Logger.WriteLog("Unable to connect to card as card has been removed from reader. System is now aborting operations.", LoggerWarringLevel.Warring);
        //            //    return;
        //            //}
        //            //try
        //            //{
        //            //    CardManger.Athentication(8, ACR122U_Keys.KeyB, ACR122U_KeyMemories.Key1);
        //            //    Logger.WriteLog("Card success fully athenticated.");
        //            //}
        //            //catch
        //            //{
        //            //    Logger.WriteLog("Card Athentication failed.", LoggerWarringLevel.Warring);
        //            //    CardManger.SetLEDandBuzzerControl(NFC_CardReader.ACR122U.ACR122U_LEDControl.AllOn, 20, 20, 5, NFC_CardReader.ACR122U.ACR122U_BuzzerControl.BuzzerOnT1And2Cycle, out Data);
        //            //    return;
        //            //}

        //            //switch (MainConfig.StarterMode)
        //            //{
        //            //    case GameStartMode.SingleGameStarter:
        //            //        StartGameAsSingleGameStarter();
        //            //        break;
        //            //    case GameStartMode.AttendantChargeDeskOnly:
        //            //        StartGameAsAttendantChargeDeskOnly();
        //            //        break;
        //            //    case GameStartMode.MultiSocketStarterMaster:
        //            //        StartGameAsMultiSocketStarterMaster();
        //            //        break;
        //            //    case GameStartMode.MultiSocketStarterSlave:
        //            //        StartGameAsMultiSocketStarterSlave();
        //            //        break;
        //            //}
        //            #endregion
        //        }
        //    }
        //    else
        //        Logger.WriteLog("Accepted Card with ATR: " + e.ATRString + ". However the machine has been disabled.");
        //}

        //private void RejectedCard(object sender, ACRCardRejectedCardScanEventArg e)
        //{
        //    if (!Disabled)
        //    {
        //        Logger.WriteLog("Rejected Card with ATR: " + e.ATRString, LoggerWarringLevel.Warring);
        //        byte Data;
        //        try
        //        {
        //            e.EventsACR122UManager.ConnectToCard();
        //            e.EventsACR122UManager.SetLEDandBuzzerControl(NFC_CardReader.ACR122U.ACR122U_LEDControl.AllOn, 20, 20, 5, NFC_CardReader.ACR122U.ACR122U_BuzzerControl.BuzzerOnT1And2Cycle, out Data);
        //            e.EventsACR122UManager.DisconnectToCard();
        //            Logger.WriteLog("Razing buzzer razed");
        //        }
        //        catch
        //        {
        //            Logger.WriteLog("Attempted buzzer read, but card has been pulled. This may have resulted in a miss read. Buzzer aborted");
        //        }
        //    }
        //    else
        //        Logger.WriteLog("Rejected Card with ATR: " + e.ATRString + ". However the machine has been disabled.", LoggerWarringLevel.Warring);
        //}

        //private void CardRemoved(object sender, ACRCardRemovedEventArg e)
        //{
        //    Logger.WriteLog("Card Removed from Reader.");
        //    e.EventsACR122UManager.DisconnectToCard();
        //}

        //private void CardDectected(object sender, ACRCardDetectedEventArg e)
        //{
        //    if (e.EventsACR122UManager.Card == null)
        //    {
        //        Logger.WriteLog("Card Detected on Reader");
        //    }
        //}

        //private void CardStateChanged(object sender, ACRCardStateChangeEventArg e)
        //{
        //    Logger.WriteLog("Card readers state has changed. New State Enum Flags: " + e.EventState);
        //}
        #endregion
        #endregion

        #region GameEvents
        private void GameInstancerGameHasEnded(object sender, GameEndedEventArgs e)
        {
            Logger.WriteLog($"GameInstancer - Game has Ended. Time Ended {e.time} Reason: {e.Reason}" );
            GameMenu.StartMenu();
            Logger.WriteLog($"GameMenu - Game Menu has Started in response.");
        }

        private void GameInstancerGameHasStarted(object sender, GameStartedEventArgs e)
        {
            Logger.WriteLog($"GameInstancer - Game has started. Time started: {e.time} under mode {MainConfig.CardModeMode}");
        }

        private void GameInstancerStartingGame(object sender, GameStartingEventArgs e)
        {
            Logger.WriteLog($"GameInstancer - Game is starting. Time Starting {e.time} Game: {e.GameName}");
        }

        private void StartGameRequested(object sender, PlayGameEventArgs e)
        {
            GameMenu.KillMenu();
            Logger.WriteLog($"Ending Game Menue to start Game");
            GameStarter.StartGame(this, e.PlayInput.GameGUID);
        }
        #endregion

        #region ServiceEvents
        /// <summary>
        /// Editor Config button event. Makes config editor UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditConfig_Click(object sender, EventArgs e)
        {
            if (ConfigEditorInstance == null || ConfigEditorInstance.IsDisposed)
                ConfigEditorInstance = ConfigEditor.ConfigEditorFactoryFromConfig(false);

            if (!ConfigEditorInstance.Visible)
                ConfigEditorInstance.Show();
        }

        /// <summary>
        /// This disables and enables the service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleDisabledEnabled_Click(object sender, EventArgs e)
        {
            Disabled = !Disabled;
            if (Disabled)
                NoIc_GameStarterStopper.ContextMenuStrip.Items[MenuDisableLocation].Text = "Enable Machine";
            else
                NoIc_GameStarterStopper.ContextMenuStrip.Items[MenuDisableLocation].Text = "Disable Machine";
        }

        /// <summary>
        /// simply resets the service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, EventArgs e)
        {
            Logger.WriteLog("Reseting Service");
            OnStop();
            Thread.Sleep(1000);
            OnStart(null);
            MessageBox.Show("Service has been reset.");
        }

        private void EditGamesConfig_Click(object sender, EventArgs e)
        {
            if (GameConfigEditorInstance == null || GameConfigEditorInstance.IsDisposed)
            {
                GameConfigEditorInstance = new GameConfigEditor();
            }

            if (!GameConfigEditorInstance.Visible)
                GameConfigEditorInstance.Show();
        }

        private void SetActiveGame_Click(object sender, EventArgs e)
        {
            if (GameSelectorEditorInstance == null || GameSelectorEditorInstance.IsDisposed)
                GameSelectorEditorInstance = new GameSelectorEditor(GameStarter.Config, GUID => GameGUID = GUID);

            if (!GameSelectorEditorInstance.Visible)
                GameSelectorEditorInstance.Show();
        }

        private void EditServerCredential_Click(object sender, EventArgs e)
        {
            if (PasswordCorrector == null || PasswordCorrector.IsDisposed)
                PasswordCorrector = new PasswordCorrector();

            if (!PasswordCorrector.Visible)
                PasswordCorrector.Show();
        }

        private void OpenServiceLog_Click(object sender, EventArgs e)
        {
            Logger.OpenTextLog();
        }

        private void ClearServiceLog_Click(object sender, EventArgs e)
        {
            Logger.ClearTextLog();
        }

        private void DemoServiceWithOutCard_Click(object sender, EventArgs e)
        {
            Logger.WriteLog($"Demo Started {DateTime.UtcNow} ");
            GameMenu.NotifyOfCardTap();
        }
        #endregion

        #region Utilities
        ContextMenuStrip BuildMenu()
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            Menu.Items.Add("Set Active Game", null, new EventHandler(SetActiveGame_Click));
            Menu.Items.Add("Edit Server Credential", null, new EventHandler(EditServerCredential_Click));
            Menu.Items.Add("Config Service", null, new EventHandler(EditConfig_Click));
            Menu.Items.Add("Config Games", null, new EventHandler(EditGamesConfig_Click));
            MenuDisableLocation = Menu.Items.Count;
            Menu.Items.Add("Disable Machine", null, new EventHandler(ToggleDisabledEnabled_Click));
            Menu.Items.Add("Reset Machine Service", null, new EventHandler(Reset_Click));
            Menu.Items.Add("Open Service Log", null, new EventHandler(OpenServiceLog_Click));
            Menu.Items.Add("Clear Service Log", null, new EventHandler(ClearServiceLog_Click));
            if (MainConfig.CardModeMode == CardModeMode.NoCardNeededDemoMode || MainConfig.ServerMode == ServerMode.NoServerDemoMode)
                Menu.Items.Add("Demo/Test W/O Card", null, new EventHandler(DemoServiceWithOutCard_Click));
            return Menu;
        }



        #region Depreciated
        //void BeginTransaction()
        //{
        //    CardManger.Athentication(8, ACR122U_Keys.KeyB, ACR122U_KeyMemories.Key1);
        //    CardManger.CheckCardTransactionWithServer(TheServerClient);
        //    CardManger.PlayGameTransactionWithServer(TheServerClient, GameGUID);
        //}
        #endregion

        private bool CloseEventAction(CtrlType sig)
        {
            GameMenu.KillMenu();
            GameStarter.KillGame();
            return true;
        }
        #endregion

    }
}