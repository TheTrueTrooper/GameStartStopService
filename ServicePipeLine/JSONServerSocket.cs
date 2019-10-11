using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public delegate JSONResponse<dynamic> MessageReceived(JSONAction<dynamic> Message);

    public class JSONServerSocket
    {
        Socket ClientSocket;

        Thread RxThread;

        JSONResponse<dynamic> LastResponse;

        public EndPoint AddressInfo { get => ClientSocket.RemoteEndPoint as IPEndPoint; }

        internal MessageReceived MessageReceivedHandle;

        internal JSONServerSocket(Socket ListerSocket, MessageReceived MessageReceivedHandle)
        {
            ClientSocket = ListerSocket.Accept();

            this.MessageReceivedHandle = MessageReceivedHandle;

            RxThread = new Thread(new ParameterizedThreadStart(ReadThreadLoop));

            RxThread.Start(this);
        }

        private static void ReadThreadLoop(object obj)
        {
            JSONServerSocket This = (JSONServerSocket)obj;
            using (MemoryStream MS = new MemoryStream())
            using (StreamReader Reader = new StreamReader(MS))
            {
                string CurrentMessage = "";
                while (true)
                {
                    byte[] ReceiveDataBuffer = new byte[24];
                    int ReceivedDataLength = 0;

                    ReceivedDataLength = This.ClientSocket.Receive(ReceiveDataBuffer);

                    // why bother to Deserialize?
                    // the last postion to use;
                    long LastPos = 0;
                    // a serailization fail reset point
                    //long CurrentTryPos = 0;
                    try
                    {
                         

                        LastPos = MS.Position;

                        MS.Seek(0, SeekOrigin.End);

                        MS.Write(ReceiveDataBuffer, 0, ReceivedDataLength);

                        MS.Position = LastPos;

                        CurrentMessage += Reader.ReadLine();
                    }
                    catch { }

                    if(CurrentMessage.Count() % 24 != 0 || CurrentMessage.Last() == '\n')
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
            ClientSocket.Send(Encoding.ASCII.GetBytes(JSONAction.ToString()));
            while (LastResponse == null);
            Return = (JSONResponse<J>)LastResponse.ResponseDynamicAutoCast(typeof(J));
            LastResponse = null;
            return Return;
        }

        void TransmitJSONResponse<T>(JSONResponse<T> JSONAction)
        {
            ClientSocket.Send(Encoding.ASCII.GetBytes(JSONAction.ToString()));
        }
    }
}
