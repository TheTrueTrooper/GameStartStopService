using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStartStopService.SocketServer
{
    class MasterSocketServer
    {
        Socket ListenerSocket;

        Thread ListenerThread;

        List<MasterSocket> ClientConnections = new List<MasterSocket>();

        bool Listening = true;

        MasterSocketServer()
        {
            IPEndPoint IP = new IPEndPoint(IPAddress.Any, 9999);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(IP);

            ListenerThread = new Thread(new ParameterizedThreadStart(ListenLoop));
            ListenerThread.Start(this);
        }

        private static void ListenLoop(object obj)
        {
            MasterSocketServer This = (MasterSocketServer)obj;

            while (This.Listening)
            {
                lock(This.ListenerSocket)
                    This.ListenerSocket.Listen(10);
                lock (This)
                    This.ClientConnections.Add(new MasterSocket(This.ListenerSocket));
            }
        }
    }
}
