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

        public JSONPipeServer(string PipeServerName, int MaxNumberOfConnections = 1)
        {
            ServicePipe = new NamedPipeServerStream(PipeServerName, PipeDirection.InOut, MaxNumberOfConnections, PipeTransmissionMode.Message);
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

        void SendCommandDataPackage<T>(JSONAction<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            ServicePipe.WaitForPipeDrain();
        }

        internal void SendResponseDataPackage<T>(JSONResponse<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            ServicePipe.WaitForPipeDrain();
        }

        internal JSONAction<T> ListenForCommand<T>()
        {
            while (Rx.Peek() == 0);
            return GetCommand<T>();
        }

        JSONAction<T> GetCommand<T>()
        {
            return JSONAction<T>.FromString(Rx.ReadLine());
        }

        JSONResponse<T> ListenForResponse<T>()
        {
            while (Rx.Peek() == 0);
            return GetResponse<T>();
        }

        JSONResponse<T> GetResponse<T>()
        {
            return JSONResponse<T>.FromString(Rx.ReadLine());
        }

        public JSONResponse<TResult> SendCommandRequest<Tin, TResult>(JSONAction<Tin> Package)
        {
            SendCommandDataPackage(Package);
            return ListenForResponse<TResult>();
        }

        public void ProcessRequest<Tin, TResult>(Func<JSONAction<Tin>, JSONResponse<TResult>> Process)
        {
            JSONAction<Tin> Request = ListenForCommand<Tin>();
            JSONResponse<TResult> Result = Process.Invoke(Request);
            SendResponseDataPackage(Result);
        }

        public void ProcessRequest(Func<JSONAction<dynamic>, JSONResponse<dynamic>> Process)
        {
            JSONAction<dynamic> Request = ListenForCommand<dynamic>();
            JSONResponse<dynamic> Result = Process.Invoke(Request);
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
