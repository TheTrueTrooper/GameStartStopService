using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServicePipeLine;

namespace TestServerConsole
{
    class MasterServer : IDisposable
    {
        JSONSocketServer Server;

        internal static Dictionary<string, SlaveInfo> Connections = new Dictionary<string, SlaveInfo>();

        AttendantConsole AttendantConsole;

        public MasterServer(int Port = 9999)
        {
            AttendantConsole = new AttendantConsole(this);
            Server = new JSONSocketServer(Port);
            Server.ActionOnConnectHandle += NewConnection;
            Server.SocketMessageRecieved += OnMessageReceived;
            Server.OnDisconnect += OnDisconnect;
        }

        private JSONResponse<dynamic> OnMessageReceived(JSONAction<dynamic> Message)
        {
            switch (Message.ActionName)
            {
                
                case "NotifyOfGamesEnd":
                    {
                        SlaveInfo Data = Message.DynamicAutoCast<SlaveInfo>().ActionData;
                        AttendantConsole.MarkGameAsStoped(Data);
                        AttendantConsole.Log($"{Data} Has stopped its game.");
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return.ToDynamic();
                    }
                case "CardTapped":
                    {
                        SlaveInfo Data = Message.DynamicAutoCast<SlaveInfo>().ActionData;
                        AttendantConsole.ActivateForTappedCard(Message.DynamicAutoCast<SlaveInfo>().ActionData);
                        AttendantConsole.Log($"{Data} Has detected a card tap. Setting to active start.");
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return.ToDynamic();
                    }
                default:
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = Message.ActionData, Message = "The Client has failed to find the requested action.", RequestStatus = JSONResponseStatus.ActionNotFound };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return;
                    }

            }
        }

        Task t = Task.Run(() => {

        });
        TimeSpan ts = TimeSpan.FromMilliseconds(1500);

        private void OnDisconnect(JSONServerSocket DisconnectingEntity)
        {
            KeyValuePair<string, SlaveInfo> DisconnectedInfo;
            lock (Connections)
                DisconnectedInfo = Connections.First(x => x.Value.Connection == DisconnectingEntity);
            lock (Connections)
                Connections.Remove(DisconnectedInfo.Key.ToString());
            AttendantConsole.Log($"{DisconnectedInfo.Value} Has Disconnected.");
            AttendantConsole.RemoveFromMachineList(DisconnectedInfo.Value);
        }

        private void NewConnection(JSONServerSocket e)
        {
            JSONResponse<SlaveInfo> NewConnectionInfo = e.TransmitJSONCommand<PlayGameModel, SlaveInfo>(new JSONAction<PlayGameModel> { ActionName = "GetSlaveInfo", ActionData = new PlayGameModel { GUID = AttendantConsole.GUIDGameSelection } });
            Thread.Sleep(100);
            SlaveInfo Info = NewConnectionInfo.ActionData;
            Info.Connection = e;
            lock(Connections)
                Connections.Add(Info.ToString(), Info);
            AttendantConsole.Log($"{Info} Has Connected.");
            AttendantConsole.AddToMachineList(Info);
        }

        internal void StartGames(List<string> MachinesToStart, string GUID)
        {
            lock(Connections)
                foreach(string Machine in MachinesToStart)
                {
                    new Action(()=>
                    {
                        JSONResponse<object> Return = Connections[Machine].Connection.TransmitJSONCommand<PlayGameModel, object>(new JSONAction<PlayGameModel>() { ActionName = "StartGame", ActionData = new PlayGameModel() { GUID = GUID } });
                        if (Return.RequestStatus == JSONResponseStatus.Success)
                            AttendantConsole.Log($"{Machine} Has started");
                        else
                            AttendantConsole.Log($"{Machine} Has Failed to start with this message:\n{Return.Message}");
                    }).Invoke();
                }
        }

        internal void StopGames(List<string> MachinesToStop)
        {

            lock (Connections)
                foreach (string Machine in MachinesToStop)
                {
                    new Action(() =>
                    {
                        JSONResponse<object> Return = Connections[Machine].Connection.TransmitJSONCommand<object, object>(new JSONAction<object>() { ActionName = "StopGame", ActionData = new { } });
                        AttendantConsole.Log($"{Machine} Has stoped");
                    }).Invoke();
                }
        }

        internal void NotifyOfUncheck(string MachinesToClearChargingFrom)
        {
            JSONResponse<object> Return;
            lock (Connections)
                Return = Connections[MachinesToClearChargingFrom].Connection.TransmitJSONCommand<object, object>(new JSONAction<object>() { ActionName = "ClearCharges", ActionData = new { } });
        }


        public void ShowConsole()
        {
            if (AttendantConsole == null || AttendantConsole.IsDisposed)
                AttendantConsole = new AttendantConsole(this);
            AttendantConsole.Show();
        }

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}
