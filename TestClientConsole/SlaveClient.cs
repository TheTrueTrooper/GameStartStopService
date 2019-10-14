using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePipeLine;

namespace TestClientConsole
{
    class SlaveClient : IDisposable
    {
        static int Debug = -1;

        JSONSocketClient Client;

        SlaveInfo Info;

        public SlaveClient(string IP, int Port = 9999)
        {
            Debug += 1;
            Client = new JSONSocketClient(IP, Port);
            Info = new SlaveInfo { MachineName = $"Test Machine{Debug}", IP = Client.AddressInfo.Address.ToString(), Port = Client.AddressInfo.Port.ToString() };
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
                case "StopGame":
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return;
                    }
                case "ClearCharges":
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return;
                    }
                case "GetSlaveInfo":
                    {
                        JSONResponse<SlaveInfo> Return = new JSONResponse<SlaveInfo>() { ActionName = Message.ActionName, ActionData = Info, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return.ToDynamic();
                    }
                case "StartGame":
                    {
                        //AttendantConsole.ActivateForTappedCard(Message.DynamicAutoCast<SlaveInfo>().ActionData);
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = new { }, RequestStatus = JSONResponseStatus.Success };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return;
                    }
                default:
                    {
                        JSONResponse<dynamic> Return = new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = Message.ActionData, Message = "The Client has failed to find the requested action.", RequestStatus = JSONResponseStatus.ActionNotFound };
                        Console.WriteLine($"Returing with:\n{Return}");
                        return Return;
                    }
            }
        }

        //CardTapped
        internal JSONResponse<object> NotifyServerOfTappedCard()
        {
            Console.WriteLine($"Sending Card Tapped");
            return Client.TransmitJSONCommand<SlaveInfo, object>(new JSONAction<SlaveInfo>() { ActionName = "CardTapped", ActionData = Info });
        }

        internal JSONResponse<object> NotifyServerOfGamesEnd()
        {
            Console.WriteLine($"Sending Notify Of Games End");
            return Client.TransmitJSONCommand<SlaveInfo, object>(new JSONAction<SlaveInfo>() { ActionName = "NotifyOfGamesEnd", ActionData = Info });
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
