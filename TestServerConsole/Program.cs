using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePipeLine;

namespace TestServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //MasterSocketServer Server = new MasterSocketServer();

            JSONSocketServer Server = new JSONSocketServer();
            Server.ActionOnConnectHandle += OnConnected;
            Server.SocketMessageRecieved += Test;
        }

        private static void OnConnected(JSONServerSocket e)
        {
            Console.WriteLine($"ClientConnected:{e.AddressInfo}\nSending hand shake");
            Console.WriteLine(e.TransmitJSONCommand<string, string>(new JSONAction<string>() { ActionData = "TestConnection", ActionName = "TestConnection" }));
        }

        private static JSONResponse<dynamic> Test(JSONAction<dynamic> Message)
        {
            Console.WriteLine($"MessageReceived:{Message}");
            return new JSONResponse<dynamic>() { ActionName = Message.ActionName, ActionData = "Test", RequestStatus = JSONResponseStatus.Success };
        }

        
    }
}
