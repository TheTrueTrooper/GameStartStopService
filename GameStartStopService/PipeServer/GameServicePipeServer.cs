using ServicePipeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.TheServerClient.ClientModels;
using GameStartStopService.PipeServer;
using GameStartStopService.BasicConfig;
using GameStartStopService.ConfigLoaders;
using GameStartStopService.UtilitiesFolder;
using GameStartStopService.ConfigEnums;
using GameInstancerNS;
using GameSelectorWrapper;
using System.Threading;
using GameStartStopService.PipeServer.Events;

namespace GameStartStopService
{
    public class GameServicePipeServer
    {
        //internal static ArcadeGameStartAndStopService Service = null;

        internal PlayGameEvent PlayGameEvent;

        [PipeFunction("PlayGame")]
        public JSONResponse<PlayGameReturn> PlayGame(JSONAction<PlayInput> This)
        {
            JSONResponse<PlayGameReturn> Return = new JSONResponse<PlayGameReturn>() { ActionName = This.ActionName, RequestStatus = JSONResponseStatus.Success };
            //if (ArcadeGameStartAndStopService.MainConfig.ServerMode == ServerMode.NoServerDemoMode && ArcadeGameStartAndStopService.MainConfig.CardModeMode == CardModeMode.NoCardNeededDemoMode)
            //{
                ArcadeGameStartAndStopService.Logger.WriteLog($"Play game called\nWith Data:\n{This}");
                Return.ActionData = new PlayGameReturn()
                {
                    ActivationDate = DateTime.Now,
                    Comments = "machine purchase",
                    //CheckNum = 0,
                    Number = "",
                    IsActive = true,
                    IsDeleted = false,
                    Customer = new CustomerData(),
                    LimitBalance = 0,
                    ExpiryDate = DateTime.Now,
                    ID = new Guid()
                };

                Task Task = new Task(() =>
                {
                    Thread.Sleep(2000);
                    this.PlayGameEvent?.Invoke(this, new PlayGameEventArgs(This.ActionData));
                });
                Task.Start();

            //}
            //else
            //{
            //    ArcadeGameStartAndStopService.Logger.WriteLog($"Play game called\nWith Data:\n{This}");
            //}

            

            ArcadeGameStartAndStopService.Logger.WriteLog($"Retuning with:\n{Return}\n");
            return Return;
        }

        [PipeFunction("CanPlayGame")]
        public JSONResponse<CanPlayReturn> CanPlayGame(JSONAction<PlayInput> This)
        {
            JSONResponse<CanPlayReturn> Return = new JSONResponse<CanPlayReturn>() { ActionName = This.ActionName, RequestStatus = JSONResponseStatus.Success };
            if (ArcadeGameStartAndStopService.MainConfig.ServerMode == ServerMode.NoServerDemoMode)
            {
                ArcadeGameStartAndStopService.Logger.WriteLog($"Can play game called\nWith Data:\n{This}\n");
                Return.ActionData = new CanPlayReturn()
                {
                    CanPlay = true,
                    CurrentBalance = 300.00,
                    NewBalance = 280.00,
                    //NewCheckKey = 0
                };
            }
            ArcadeGameStartAndStopService.Logger.WriteLog($"Retuning with:\n{Return}\n");
            return Return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="This">object should be null</param>
        /// <returns></returns>
        [PipeFunction("GetGames")]
        public JSONResponse<GetMachineGamesReturn> GetGames(JSONAction<object> This)
        {
            ArcadeGameStartAndStopService.Logger.WriteLog($"Get Games was called\nWith Data:\n{This}\n");
            JSONResponse<GetMachineGamesReturn> Return = new JSONResponse<GetMachineGamesReturn>() { ActionData = new GetMachineGamesReturn(), ActionName = This.ActionName, RequestStatus = JSONResponseStatus.Success };
            Return.ActionData.VRMachineGames = ArcadeGameStartAndStopService.GameConfig.GamesRaw;
            ArcadeGameStartAndStopService.Logger.WriteLog($"Retuning to get games with:\n{Return}\n");
            return Return;
        }
    }
}
