using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace GameInstancerNS
{
    /// <summary>
    /// a class that handles the games starting and stoping
    /// </summary>
    public class GameInstancer
    {

        /// <summary>
        /// the Config to use
        /// </summary>
        public IGameConfig Config = null; 
        
        /// <summary>
        /// Gets a list of your games to work with
        /// </summary>
        public List<IGameModel> Games { get { return Config.Games; } }

        /// <summary>
        /// The last instance runngng on the machine.
        /// </summary>
        InstanceGame RunningGame = null;

        /// <summary>
        /// The event to tie to this games end.
        /// </summary>
        public event GameEndedEventHandler GameHasEndedEvent;

        /// <summary>
        /// The Event to tie to the game after the start
        /// </summary>
        public event GameStartedEventHandler GameHasStartedEvent;

        /// <summary>
        /// The Event to tie to the game durring start
        /// </summary>
        public event GameStartingEventHandler GameIsStartingEvent;

        /// <summary>
        /// Builds an instancer with the kernel32's event catch
        /// </summary>
        public GameInstancer(IGameConfig Config , bool AddCloseEvent = false)
        {
            this.Config = Config;

            if (Config == null)
                throw new Exception("Not a valid config. Please set to a valid IGameConfig Interface.");
        }

        /// <summary>
        /// Returns the name of the game running or a sting stating no games are running
        /// </summary>
        public string RunningGameName
        {
            get
            {
                if (RunningGame == null)
                    return "No Games are currently running";
                return RunningGame.Info.FileName;
            }
        }

        /// <summary>
        /// returns if the game is running or not
        /// </summary>
        public bool GameIsRunning
        {
            get
            {
                if (RunningGame != null)
                {
                    lock (RunningGame)
                    {
                        return RunningGame.IsAlive;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Instances A game  by index with a defualt of the first game avaible
        /// </summary>
        /// <param name="GameNumberToStart">The index of the game in the config</param>
        public void StartGame(object StartRequester, string GameGUIDToStart)
        {
            GameIsStartingEvent?.Invoke(this, new GameStartingEventArgs { GameName = Config[GameGUIDToStart].Name, RequestingObj = StartRequester });
            RunningGame = new InstanceGame(Config[GameGUIDToStart]);
            RunningGame.GameHasEndedEvent += GameEndedEventChain;
            RunningGame.StartGame();
            GameHasStartedEvent?.Invoke(this, new GameStartedEventArgs { GameName = Config[GameGUIDToStart].Name });
        }

        /// <summary>
        /// Instances A game by its name
        /// </summary>
        /// <param name="GameNumberToStart">The index of the game in the config</param>
        public void StartGameByName(object StartRequester, string GameNameToStart)
        {
            IGameModel gameConfig = Config.GetGameByName(GameNameToStart);
            GameIsStartingEvent?.Invoke(this, new GameStartingEventArgs { GameName = gameConfig.Name, RequestingObj = StartRequester });
            RunningGame = new InstanceGame(gameConfig);
            RunningGame.GameHasEndedEvent += GameEndedEventChain;
            RunningGame.StartGame();
            GameHasStartedEvent?.Invoke(this, new GameStartedEventArgs { GameName = gameConfig.Name });
        }

        /// <summary>
        /// simply an internal invoke to chain events to the outside
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEndedEventChain(object sender, GameEndedEventArgs e)
        {
            try
            {
                GameHasEndedEvent?.Invoke(sender, e);
            }
            catch
            {}
        }

        /// <summary>
        /// Kills the game and removes it from currentRunning
        /// </summary>
        public void KillGame()
        {
            try
            {
                lock (RunningGame)
                RunningGame?.Kill();
                RunningGame = null;
            }
            catch
            { }
        }

    }
}
