using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.SocketServer.SocketModels;
using ServicePipeLine;

namespace GameStartStopService.SocketServer
{
    class SlaveClient : IDisposable
    {
        internal static string GameGUID;

        JSONSocketClient Client;

        SlaveInfo Info;

        public SlaveClient(string IP, int Port = 9999)
        {
            Client = new JSONSocketClient(IP, Port);
            Info = new SlaveInfo { MachineName = $"{ArcadeGameStartAndStopService.MainConfig.MachineName}:{ArcadeGameStartAndStopService.MainConfig.MachineGUID}", IP = Client.AddressInfo.Address.ToString(), Port = Client.AddressInfo.Port.ToString() };
            Client.OnMessageReceived = OnMessageReceived;
            Client.OnDisconnect += Disconnected;
            Console.WriteLine("Connected");
        }

        private void Disconnected(JSONSocketClient DisconnectingEntity)
        {
            Console.WriteLine("Disconnected");
        }

        private JSONResponse<dynamic> OnMessageReceived(JSONAction<dynamic> Message)
        {
            Console.WriteLine($"Message Recived.\n{Message}");
            switch (Message.ActionName)
            {
                //ChangeGame
                case "ChangeGame":
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        GameGUID = Message.DynamicAutoCast<PlayGameModel>().ActionData.GUID;
                        ArcadeGameStartAndStopService.Logger.WriteLog($"ChangeGame from Master Server for {GameGUID}");
                        return Return;
                    }
                case "StopGame":
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        ArcadeGameStartAndStopService.Logger.WriteLog($"StopGame from Master Server");
                        ArcadeGameStartAndStopService.GameStarter.KillGame();
                        return Return;
                    }
                case "ClearCharges":
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        ArcadeGameStartAndStopService.Logger.WriteLog($"Clear Charge was called. Now clearing charges");
                        ArcadeGameStartAndStopService.LastCardGUID = null;
                        ArcadeGameStartAndStopService.LastCheckKey = null;
                        return Return;
                    }
                case "GetSlaveInfo":
                    {
                        JSONResponse<SlaveInfo> Return = new JSONResponse<SlaveInfo>() { ActionName = Message.ActionName, ActionData = Info, RequestStatus = JSONResponseStatus.Success };
                        GameGUID = Message.DynamicAutoCast<PlayGameModel>().ActionData.GUID;
                        return Return.ToDynamic();
                    }
                case "StartGame":
                    {
                        //AttendantConsole.ActivateForTappedCard(Message.DynamicAutoCast<SlaveInfo>().ActionData);
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        ArcadeGameStartAndStopService.Logger.WriteLog($"StartGameCalled from Master Server");
                        if (ArcadeGameStartAndStopService.LastCardGUID != null && ArcadeGameStartAndStopService.LastCheckKey != null)
                            ArcadeGameStartAndStopService.TheServerClient.PlayGame(Message.DynamicAutoCast<PlayGameModel>().ActionData.GUID, ArcadeGameStartAndStopService.LastCheckKey.Value, ArcadeGameStartAndStopService.LastCardGUID);
                        ArcadeGameStartAndStopService.GameStarter.StartGame(this, Message.DynamicAutoCast<PlayGameModel>().ActionData.GUID);
                        return Return;
                    }
                default:
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = Message.ActionData, Message = "The Client has failed to find the requested action.", RequestStatus = JSONResponseStatus.ActionNotFound };
                        ArcadeGameStartAndStopService.Logger.WriteLog($"No match fuction called returning with {Return}", UtilitiesFolder.LoggerWarringLevel.Warring);
                        return Return;
                    }
            }         
        }

        //CardTapped
        internal JSONResponse<object> NotifyServerOfTappedCard()
        {
            ArcadeGameStartAndStopService.Logger.WriteLog($"Sending Card Tapped");
            return Client.TransmitJSONCommand<SlaveInfo, object>(new JSONAction<SlaveInfo>() { ActionName = "CardTapped", ActionData = Info });
        }

        internal JSONResponse<object> NotifyServerOfGamesEnd()
        {
            ArcadeGameStartAndStopService.Logger.WriteLog($"Sending Notify Of Games End");
            return Client.TransmitJSONCommand<SlaveInfo, object>(new JSONAction<SlaveInfo>() { ActionName = "NotifyOfGamesEnd", ActionData = Info });
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
