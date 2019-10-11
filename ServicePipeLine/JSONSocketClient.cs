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
    public class JSONSocketClient
    {
        IPEndPoint IP;

        Socket ServerSocket;

        Thread RxThread;

        JSONResponse<dynamic> LastResponse;

        public MessageReceived MessageReceivedHandle;

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
            JSONSocketClient This = (JSONSocketClient)obj;
            using (MemoryStream MS = new MemoryStream())
            using (StreamReader Reader = new StreamReader(MS))
            {
                string CurrentMessage = "";
                while (true)
                {
                    byte[] ReceiveDataBuffer = new byte[24];
                    int ReceivedDataLength = 0;

                    ReceivedDataLength = This.ServerSocket.Receive(ReceiveDataBuffer);

                    long LastPos = 0;
                    try
                    {
                        LastPos = MS.Position;

                        MS.Seek(0, SeekOrigin.End);

                        MS.Write(ReceiveDataBuffer, 0, ReceivedDataLength);

                        MS.Position = LastPos;

                        CurrentMessage += Reader.ReadLine();
                    }
                    catch { }

                    if (CurrentMessage.Count() % 24 != 0 || CurrentMessage.Last() == '\n')
                    {
                        JSONResponse<dynamic> Response = JSONResponse<dynamic>.FromString(CurrentMessage);
                        lock (This)
                        {
                            if (This.MessageReceivedHandle != null)
                            {
                                if (Response.RequestStatus == JSONResponseStatus.NotPresent)
                                    This.TransmitJSONResponse(This.MessageReceivedHandle?.Invoke(new JSONAction<dynamic>() { ActionName = Response.ActionName, ActionData = Response.ActionDataObj }));
                                else
                                    lock (This)
                                        This.LastResponse = Response;
                            }
                        }
                        CurrentMessage = "";
                    }
                }
            }
        }

        public JSONResponse<J> TransmitJSONCommand<T, J>(JSONAction<T> JSONAction)
        {
            JSONResponse<J> Return;
            ServerSocket.Send(Encoding.ASCII.GetBytes(JSONAction.ToString()));
            while (LastResponse == null) ;
            Return = (JSONResponse<J>)LastResponse.ResponseDynamicAutoCast(typeof(J));
            LastResponse = null;
            return Return;
        }

        void TransmitJSONResponse<T>(JSONResponse<T> JSONAction)
        {
            ServerSocket.Send(Encoding.ASCII.GetBytes(JSONAction.ToString()));
        }
    }
}
