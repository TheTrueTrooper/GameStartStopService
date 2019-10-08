using System.IO.Pipes;
using System;
using System.IO;
using System.Text;

namespace ServicePipeLine
{
    // Cool to do's at a later date. add reflection leveraging with reflection attributes to remove action/func dictionary style for ease as a full lib
    public class JSONPipeServer : IDisposable
    {
        public string PipeServerName { get; private set; }

        NamedPipeServerStream ServicePipe;

        StreamReader Rx;
        StreamWriter Tx;

        public bool MessageIsWaiting
        {
            get
            {
                return Rx.Peek() == 0;
            }
        }

        public bool IsConnected
        {
            get
            {
                return ServicePipe.IsConnected;
            }
        }

        public JSONPipeServer(string PipeServerName)
        {
            ServicePipe = new NamedPipeServerStream(PipeServerName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            this.PipeServerName = PipeServerName;
            Rx = new StreamReader(ServicePipe);
            Tx = new StreamWriter(ServicePipe);
            //ServicePipe.WriteTimeout = 30000;
            //ServicePipe.ReadTimeout = 30000;
        }

        public void WaitForConnect()
        {
            ServicePipe.WaitForConnection();
            ServicePipe.ReadMode = PipeTransmissionMode.Message;
        }

        public void CloseConnection()
        {
            ServicePipe.Disconnect();
        }

        void SendCommandDataPackage<T>(PipeJSONAction<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            ServicePipe.WaitForPipeDrain();
        }

        internal void SendResponseDataPackage<T>(PipeJSONResponse<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            ServicePipe.WaitForPipeDrain();
        }

        internal PipeJSONAction<T> ListenForCommand<T>()
        {
            while (Rx.Peek() == 0);
            return GetCommand<T>();
        }

        PipeJSONAction<T> GetCommand<T>()
        {
            return PipeJSONAction<T>.FromString(Rx.ReadLine());
        }

        PipeJSONResponse<T> ListenForResponse<T>()
        {
            while (Rx.Peek() == 0);
            return GetResponse<T>();
        }

        PipeJSONResponse<T> GetResponse<T>()
        {
            return PipeJSONResponse<T>.FromString(Rx.ReadLine());
        }

        public PipeJSONResponse<TResult> SendCommandRequest<Tin, TResult>(PipeJSONAction<Tin> Package)
        {
            SendCommandDataPackage(Package);
            return ListenForResponse<TResult>();
        }

        public void ProcessRequest<Tin, TResult>(Func<PipeJSONAction<Tin>, PipeJSONResponse<TResult>> Process)
        {
            PipeJSONAction<Tin> Request = ListenForCommand<Tin>();
            PipeJSONResponse<TResult> Result = Process.Invoke(Request);
            SendResponseDataPackage(Result);
        }

        public void ProcessRequest(Func<PipeJSONAction<dynamic>, PipeJSONResponse<dynamic>> Process)
        {
            PipeJSONAction<dynamic> Request = ListenForCommand<dynamic>();
            PipeJSONResponse<dynamic> Result = Process.Invoke(Request);
            SendResponseDataPackage(Result);
        }

        public void Dispose()
        {
            ServicePipe.Disconnect();
            ServicePipe.Close();
            ServicePipe.Dispose();
        }
    }
}
