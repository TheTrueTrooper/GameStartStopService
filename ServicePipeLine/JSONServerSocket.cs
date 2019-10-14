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

    public delegate void ServerDisconnect(JSONServerSocket DisconnectingEntity);

    public class JSONServerSocket : IDisposable
    {
        Socket ClientSocket;

        Thread RxThread;

        JSONResponse<dynamic> LastResponse;

        public EndPoint AddressInfo { get => ClientSocket.RemoteEndPoint as IPEndPoint; }

        internal MessageReceived MessageReceivedHandle;

        internal ServerDisconnect DisconnectHandle;

        bool Disconnected;

        public TimeSpan TimeOut;

        internal JSONServerSocket(Socket ListerSocket, MessageReceived MessageReceivedHandle, ServerDisconnect DisconnectHandle)
        {
            TimeOut = TimeSpan.FromSeconds(15);

            ClientSocket = ListerSocket.Accept();

            this.MessageReceivedHandle = MessageReceivedHandle;
            this.DisconnectHandle = DisconnectHandle;

            RxThread = new Thread(new ParameterizedThreadStart(ReadThreadLoop));

            RxThread.Start(this);
        }

        private static void ReadThreadLoop(object obj)
        {
            const int BufferSize = 1024;

            JSONServerSocket This = (JSONServerSocket)obj;

            string CurrentMessage = "";
            while (!This.Disconnected)
            {
                byte[] ReceiveDataBuffer = new byte[BufferSize];
                int ReceivedDataLength = 0;

                try
                {
                    ReceivedDataLength = This.ClientSocket.Receive(ReceiveDataBuffer);
                }
                catch
                {
                    This?.ClientSocket?.Disconnect(false);
                    This.Disconnected = true;
                    This.DisconnectHandle?.Invoke(This);
                }

                CurrentMessage += Encoding.Default.GetString(ReceiveDataBuffer.Where(x => x != 0).ToArray());

                if (CurrentMessage.Count() > 0 && CurrentMessage.Last() == '\n')
                {
                    JSONResponse<dynamic> Response = JSONResponse<dynamic>.FromString(CurrentMessage);
                    lock (This)
                    {
                        if (Response.RequestStatus == JSONResponseStatus.NotPresent)
                        {
                            if (This.MessageReceivedHandle != null)
                                This.TransmitJSONResponse(This.MessageReceivedHandle?.Invoke(new JSONAction<dynamic>() { ActionName = Response.ActionName, ActionData = Response.ActionDataObj }));
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
            ClientSocket.Send(Encoding.ASCII.GetBytes($"{JSONAction.ToString()}\n"));
            Task Task = Task.Run(() => { while (LastResponse == null) ; });
            if (Task.Wait(TimeOut))
                Return = (JSONResponse<J>)LastResponse.ResponseDynamicAutoCast(typeof(J));
            else
                Return = (JSONResponse<J>)(new JSONResponse<dynamic> { ActionName = JSONAction.ActionName, ActionData = JSONAction.ActionData, RequestStatus = JSONResponseStatus.TimeOut, Message = $"The operation has timed out after {TimeOut}" }).ResponseDynamicAutoCast(typeof(J));
            LastResponse = null;
            return Return;
        }

        void TransmitJSONResponse<T>(JSONResponse<T> JSONResponse)
        {
            ClientSocket.Send(Encoding.ASCII.GetBytes($"{JSONResponse.ToString()}\n"));
        }

        public void Dispose()
        {
            Disconnected = true;
            ClientSocket.Dispose();
        }
    }
}
