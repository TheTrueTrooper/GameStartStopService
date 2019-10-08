using GameInstancerNS;
using ServicePipeLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameSelectorWrapper
{


    public class GameMenu
    {
        const string ServerName = "GameStartAndStopServicePipe";

        object Controller;

        PipeRequestServer PipeServer;
        JSONRequestClientPipe ClientPipe;

        /// <summary>
        /// returns if the game is running or not (not 100%)
        /// </summary>
        public bool ShouldBeRunning { private set; get; }

        /// <summary>
        /// the optional 
        /// </summary>
        protected internal Process MenuGameExe { get; private set; }

        string ThePath = $"{Environment.CurrentDirectory}\\GameSelectorV0.7\\GameSelector.exe";

        /// <summary>
        /// creates an optional Exe Instance
        /// </summary>
        /// <param name="OptionalExe">based on OptionalExe</param>
        public GameMenu(object ServerController)
        {
            Controller = ServerController;
            this.MenuGameExe = new Process();
            this.MenuGameExe.StartInfo = new ProcessStartInfo()
            {
                FileName = ThePath,
                //UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(ThePath)
            };

            PipeServer = new PipeRequestServer(ServerName);
            PipeServer.AddProccess(Controller);

            MenuGameExe.Exited += Exited;
            MenuGameExe.EnableRaisingEvents = true;
        }

        /// <summary>
        /// starts the optional exe
        /// </summary>
        public virtual void StartMenu()
        {
            ShouldBeRunning = true;

            List<Process> GameProcessesTokill = Process.GetProcesses().Where(pr => pr.ProcessName == Path.GetFileName(ThePath).Replace(".exe", "")).ToList();
            foreach (Process process in GameProcessesTokill)
            {
                try
                {
                    process.Kill();
                }
                catch
                { }
            }

            MenuGameExe.Start();

            ClientPipe = new JSONRequestClientPipe("GameSelectorService");
        }

        private void Exited(object sender, EventArgs e)
        {
            if (ShouldBeRunning)
            {
                //Pipe.Dispose();
                //Pipe = null;
                MenuGameExe.Start();
                ClientPipe = new JSONRequestClientPipe("GameSelectorService");
            }
        }

        /// <summary>
        /// Kills the optional EXE
        /// </summary>
        public virtual void KillMenu()
        {
            PipeServer = null;
            if (ShouldBeRunning)
            {
                ShouldBeRunning = false;
                if (!MenuGameExe.HasExited)
                    MenuGameExe.Kill();
            }
        }

        public PipeJSONResponse<object> NotifyOfCardTap()
        {
            PipeJSONAction<object> Commandpackage = new PipeJSONAction<object>() { ActionName = "OnCardTap", ActionData = new { } };
            return ClientPipe.SendCommandRequest<object, object>(Commandpackage);
        }
    }
}
