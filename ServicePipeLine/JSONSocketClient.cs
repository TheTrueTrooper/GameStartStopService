using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ServicePipeLine
{
    public delegate void ClientDisconnect(JSONSocketClient DisconnectingEntity);

    public class JSONSocketClient : IDisposable
    {
        IPEndPoint IP;

        Socket ServerSocket;

        Thread RxThread;

        JSONResponse<dynamic> LastResponse;

        public MessageReceived OnMessageReceived;
        public ClientDisconnect OnDisconnect;

        bool Disconnected = false;

        public IPEndPoint AddressInfo { get => ServerSocket.LocalEndPoint as IPEndPoint; }

        //"127.0.0.1"
        public JSONSocketClient(string ServerLoc, int Port = 9999)
        {
            IP = new IPEndPoint(IPAddress.Parse(ServerLoc), 9999);
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            RxThread = new Thread(new ParameterizedThreadStart(ReadThreadLoop));

            ServerSocket.Connect(IP);

            RxThread.Start(this);
        }

        private static void ReadThreadLoop(object obj)
        {
            const int BufferSize = 1024;

            JSONSocketClient This = (JSONSocketClient)obj;

            string CurrentMessage = "";
            while (!This.Disconnected)
            {
                byte[] ReceiveDataBuffer = new byte[BufferSize];
                int ReceivedDataLength = 0;

                try
                {
                    ReceivedDataLength = This.ServerSocket.Receive(ReceiveDataBuffer);
                }
                catch
                {
                    This.ServerSocket.Disconnect(false);
                    This.Disconnected = true;
                    This.OnDisconnect?.Invoke(This);
                }

                CurrentMessage += Encoding.Default.GetString(ReceiveDataBuffer.Where(x => x != 0).ToArray());

                if (CurrentMessage.Count() > 0 && CurrentMessage.Last() == '\n')
                {
                    JSONResponse<dynamic> Response = JSONResponse<dynamic>.FromString(CurrentMessage);
                    lock (This)
                    {
                        if (Response.RequestStatus == JSONResponseStatus.NotPresent)
                        {
                            if (This.OnMessageReceived != null)
                                This.TransmitJSONResponse(This.OnMessageReceived?.Invoke(new JSONAction<dynamic>() { ActionName = Response.ActionName, ActionData = Response.ActionDataObj }));
                        }
                        else
                            lock (This)
                                This.LastResponse = Response;
                        CurrentMessage = "";
                    }
                }
            }

        }

        public JSONResponse<J> TransmitJSONCommand<T, J>(JSONAction<T> JSONAction)
        {
            JSONResponse<J> Return;
            ServerSocket.Send(Encoding.ASCII.GetBytes($"{JSONAction}\n"));
            while (LastResponse == null) ;
            Return = (JSONResponse<J>)LastResponse.ResponseDynamicAutoCast(typeof(J));
            LastResponse = null;
            return Return;
        }

        void TransmitJSONResponse<T>(JSONResponse<T> JSONResponse)
        {
            ServerSocket.Send(Encoding.ASCII.GetBytes($"{JSONResponse}\n"));
        }

        public void Dispose()
        {
            Disconnected = true;
            ServerSocket.Dispose();
        }
    }
}
