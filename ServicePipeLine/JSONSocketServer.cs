using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public delegate void ActionOnConnectLocal(JSONServerSocket e);
    public delegate void ActionOnConnectGlobal(JSONSocketServer e);

    public class JSONSocketServer
    {
        Socket ListenerSocket;

        Thread ListenerThread;

        public List<JSONServerSocket> ClientConnections = new List<JSONServerSocket>();

        public JSONServerSocket this[int Index]
        {
            get
            {
                return ClientConnections[Index];
            }
        }

        public ActionOnConnectLocal ActionOnConnectHandle;
        public ActionOnConnectGlobal GlobalActionOnConnectHandle;

        MessageReceived _SocketMessageRecieved;

        public MessageReceived SocketMessageRecieved {
            get
            {
                return _SocketMessageRecieved;
            }
            set
            {
                _SocketMessageRecieved = value;
                lock (ClientConnections)
                    foreach (JSONServerSocket S in ClientConnections)
                        S.MessageReceivedHandle = value;
            }
        }

        bool Listening = true;

        public JSONSocketServer()
        {
            IPEndPoint IP = new IPEndPoint(IPAddress.Any, 9999);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(IP);

            ListenerThread = new Thread(new ParameterizedThreadStart(ListenLoop));
            ListenerThread.Start(this);
        }

        static void ListenLoop(object obj)
        {
            JSONSocketServer This = (JSONSocketServer)obj;

            while (This.Listening)
            {
                lock(This.ListenerSocket)
                    This.ListenerSocket.Listen(10);
                lock (This)
                    This.ClientConnections.Add(new JSONServerSocket(This.ListenerSocket, This._SocketMessageRecieved));
                lock (This)
                    if(This.ActionOnConnectHandle != null)
                        This.ActionOnConnectHandle?.Invoke(This.ClientConnections.Last());
                lock (This)
                    if(This.GlobalActionOnConnectHandle != null)
                            This.GlobalActionOnConnectHandle?.Invoke(This);
            }
        }

        public void WaitForFirstConnection()
        {
            while(ClientConnections.Count() == 0);
        }
    }

    public class JSONSocketServer<T> : JSONSocketServer
    {

    }
}

