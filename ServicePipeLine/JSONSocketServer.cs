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

    public class JSONSocketServer : IDisposable
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
        ServerDisconnect _OnDisconnect;

        public ServerDisconnect OnDisconnect
        {
            get
            {
                return _OnDisconnect;
            }
            set
            {
                _OnDisconnect = value;
                lock (ClientConnections)
                    foreach (JSONServerSocket S in ClientConnections)
                        S.DisconnectHandle = value;
                if (_OnDisconnect == null)
                    _OnDisconnect += OnDisconnectDefualt;
            }
        }

        public MessageReceived SocketMessageRecieved
        {
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

        public JSONSocketServer(int Port = 9999)
        {
            IPEndPoint IP = new IPEndPoint(IPAddress.Any, Port);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.Bind(IP);

            _OnDisconnect += OnDisconnectDefualt;

            ListenerThread = new Thread(new ParameterizedThreadStart(ListenLoop));
            ListenerThread.Start(this);
        }

        private void OnDisconnectDefualt(JSONServerSocket DisconnectingEntity)
        {
            lock (ClientConnections)
                ClientConnections.Remove(DisconnectingEntity);
        }

        static void ListenLoop(object obj)
        {
            JSONSocketServer This = (JSONSocketServer)obj;

            while (This.Listening)
            {
                lock(This.ListenerSocket)
                    This.ListenerSocket.Listen(10);
                lock (This)
                    This.ClientConnections.Add(new JSONServerSocket(This.ListenerSocket, This._SocketMessageRecieved, This._OnDisconnect));
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

        public void Dispose()
        {
            Listening = false;
            foreach(JSONServerSocket S in ClientConnections)
            {
                S.Dispose();
            }
        }
    }
}

