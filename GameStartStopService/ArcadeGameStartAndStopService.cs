using GameStartStopService.BasicConfig;
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
using GameStartStopService.PipeServer.Models;
using GameStartStopService.SocketServer;
using GameStartStopService.SocketServer.SocketModels;

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

        internal static GameInstancer GameStarter;

        internal static GameMenu GameMenu;

        static PasswordCorrector PasswordCorrector;

        internal static GameServiceConfig MainConfig;
        internal static ServiceJSONGamesConfig GameConfig;
        internal static Logger Logger;

        static string GameGUID;

        static ConfigEditor ConfigEditorInstance;
        static GameConfigEditor GameConfigEditorInstance;
        static GameSelectorEditor GameSelectorEditorInstance;

        internal static ServerClient TheServerClient;

        static MasterServer MasterServer;
        static SlaveClient SlaveClient;

        internal static string LastCardGUID;
        internal static int? LastCheckKey;

        static bool Disabled = false;
        bool exit = false;

        //depreciated
        //static ACR122UManager CardManger = null;

        internal static RL8000_NFC NFCReader;

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

            SlaveLocationModel slaveLocationModel = new SlaveLocationModel();

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

            if (MainConfig.StarterMode == GameStartMode.SingleGameStarter == !MenuRunning)
                GameMenu.StartMenu();

            GameStarter.GameIsStartingEvent += GameInstancerStartingGame;
            GameStarter.GameHasStartedEvent += GameInstancerGameHasStarted;
            GameStarter.GameHasEndedEvent += GameInstancerGameHasEnded;

            if (MainConfig.CardModeMode != CardModeMode.NoCardNeededDemoMode)
            {
                try
                {
                    NFCReader = new RL8000_NFC();
                    NFCReader.MifareClassicISO1443ACardDetectedEvent += CardDetected;
                }
                catch
                {
                    const string Error = "\nError - Cannot find card reader.\nPlease replug in the card reader.\nWill try again in 20000ms.\n";
                    Logger.WriteLog(Error, LoggerWarringLevel.Danger);
#if !DEBUG
                    DialogResult Result = MessageBox.Show(Error, "Error", MessageBoxButtons.RetryCancel);
                    if (Result == DialogResult.Cancel)
                        Application.Exit();
#endif
                    Thread.Sleep(20000);
                }
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

            if (MainConfig.StarterMode == GameStartMode.MultiSocketStarterMaster)
            {
                while (MasterServer == null)
                {
                    try
                    {
                        MasterServer = new MasterServer(MainConfig.PortNumber);
                        MasterServer.ShowConsole();
                    }
                    catch
                    {
                        const string Error = "\nError - Master Server can not open port.\nPlease Check for Port usage or your config file.\nWill try again in 20000ms.";
                        Logger.WriteLog(Error, LoggerWarringLevel.Danger);
#if !DEBUG
                    DialogResult Result = MessageBox.Show(Error, "Error", MessageBoxButtons.RetryCancel);
                    if (Result == DialogResult.Cancel)
                        Application.Exit();
#endif
                        Thread.Sleep(20000);
                    }
                }
            }

            if(MainConfig.StarterMode == GameStartMode.MultiSocketStarterSlave)
            {
                while (SlaveClient == null)
                {
                    try
                    {
                        SlaveClient = new SlaveClient(MainConfig.MasterStarterMasterLoc, MainConfig.PortNumber);
                    }
                    catch
                    {
                        const string Error = "\nError - Slaves Master Server is not responding.\nPlease Check the server, its endpoint, or your config file.\nWill try again in 20000ms.\n";
                        Logger.WriteLog(Error, LoggerWarringLevel.Danger);
#if !DEBUG
                    DialogResult Result = MessageBox.Show(Error, "Error", MessageBoxButtons.RetryCancel);
                    if (Result == DialogResult.Cancel)
                        Application.Exit();
#endif
                        Thread.Sleep(20000);
                    }
                }
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
        void CardTapAsSingleGameStarter(MifareClassicISO1443ACardDetectedEventArg e)
        {
            if (MainConfig.ServerMode == ServerMode.NoServerDemoMode || MainConfig.CardModeMode == CardModeMode.NoCardNeededDemoMode)
            {
                Logger.WriteLog($"\nCard Detected starting in {MainConfig.ServerMode}:{MainConfig.CardModeMode} mode. Serverless Demo Started {DateTime.UtcNow}\nCardInfo\n\tAir Protocol:{e.CardInfo.AirProtocalID}\n\tAntennaID:{e.CardInfo.AntennaID}\n\tTagID:{e.CardInfo.TagID}\n\tUID:{BitConverter.ToString(e.CardInfo.UID, 0, e.CardInfo.UIDlen)}\n\tDSFID:{e.CardInfo.DSFID}");
                GameMenu.NotifyOfCardTap();
            }
            else
            {
                GameMenu.NotifyOfCardTap();
            }
        }

        void CardTapAsMultiSocketStarterMaster(MifareClassicISO1443ACardDetectedEventArg e)
        {
            Logger.WriteLog("Card tap detected but in MultiSocketStarterMaster. Tap has been ignored");
        }

        void CardTapAsMultiSocketStarterSlave(MifareClassicISO1443ACardDetectedEventArg e)
        {
            if (MainConfig.ServerMode == ServerMode.NoServerDemoMode || MainConfig.CardModeMode == CardModeMode.NoCardNeededDemoMode)
            {
                Logger.WriteLog($"\nCard Detected starting in {MainConfig.ServerMode}:{MainConfig.CardModeMode} mode. Serverless Demo Started {DateTime.UtcNow}\nCardInfo\n\tAir Protocol:{e.CardInfo.AirProtocalID}\n\tAntennaID:{e.CardInfo.AntennaID}\n\tTagID:{e.CardInfo.TagID}\n\tUID:{BitConverter.ToString(e.CardInfo.UID, 0, e.CardInfo.UIDlen)}\n\tDSFID:{e.CardInfo.DSFID}");
                SlaveClient.NotifyServerOfTappedCard();
            }
            else
            {
                CanPlayTransactionReturn Data = ArcadeGameStartAndStopService.NFCReader.Card.CanPlayGameTransactionWithServer(SlaveClient.GameGUID).Data;
                if (Data.CanPlay)
                {
                    Logger.WriteLog($"\nCard Detected starting in {MainConfig.ServerMode}:{MainConfig.CardModeMode} mode. Serverless Demo Started {DateTime.UtcNow}\nCardInfo\n\tAir Protocol:{e.CardInfo.AirProtocalID}\n\tAntennaID:{e.CardInfo.AntennaID}\n\tTagID:{e.CardInfo.TagID}\n\tUID:{BitConverter.ToString(e.CardInfo.UID, 0, e.CardInfo.UIDlen)}\n\tDSFID:{e.CardInfo.DSFID}");
                    ArcadeGameStartAndStopService.LastCardGUID = Data.CardGUID;
                    ArcadeGameStartAndStopService.LastCheckKey = Data.NewCheckKey;
                    SlaveClient.NotifyServerOfTappedCard();
                }
                else
                {
                    ArcadeGameStartAndStopService.LastCardGUID = null;
                    ArcadeGameStartAndStopService.LastCheckKey = null;
                }
            }
        }

        void CardTapAsAttendantChargeDeskOnly()
        {
            Logger.WriteLog("Card tap detected but in AttendantChargeDeskOnly. Not Implemented Tap has been ignored");
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
                        CardTapAsSingleGameStarter(e);
                        break;
                    case GameStartMode.MultiSocketStarterSlave:
                        CardTapAsMultiSocketStarterSlave(e);
                        break;
                    case GameStartMode.MultiSocketStarterMaster:
                        CardTapAsMultiSocketStarterMaster(e);
                        break;
                    case GameStartMode.AttendantChargeDeskOnly:
                        CardTapAsAttendantChargeDeskOnly();
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
            if (!exit)
            {
                Logger.WriteLog($"GameInstancer - Game has Ended. Time Ended {e.time} Reason: {e.Reason}");
                if (MainConfig.StarterMode == GameStartMode.SingleGameStarter)
                {
                    GameMenu.StartMenu();
                    Logger.WriteLog($"GameMenu - Game Menu has Started in response.");
                }
                else if (MainConfig.StarterMode == GameStartMode.MultiSocketStarterSlave)
                {
                    SlaveClient.NotifyServerOfGamesEnd();
                    Logger.WriteLog($"GameMenu - Master Server has been notified.");
                }
            }
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
            {
                NoIc_GameStarterStopper.ContextMenuStrip.Items[MenuDisableLocation].Text = "Enable Machine";
                GameMenu.KillMenu();
                GameMenu.StartMenu(StartModes.DisabledMessage);
            }
            else
            {
                NoIc_GameStarterStopper.ContextMenuStrip.Items[MenuDisableLocation].Text = "Disable Machine";
                GameMenu.KillMenu();
                GameMenu.StartMenu();
            }
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

        private void OpenAttendantConsole_Click(object sender, EventArgs e)
        {
            MasterServer.ShowConsole();
        }
        #endregion

        #region Utilities
        ContextMenuStrip BuildMenu()
        {
            ContextMenuStrip Menu = new ContextMenuStrip();
            if (MainConfig.StarterMode == GameStartMode.MultiSocketStarterMaster)
                Menu.Items.Add("Open Attendant Console", null, new EventHandler(OpenAttendantConsole_Click));
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
            exit = true;
            GameMenu.KillMenu();
            GameStarter.KillGame();
            return exit;
        }
        #endregion

    }
}
