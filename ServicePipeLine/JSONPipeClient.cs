using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;
using System.IO;
using System.Threading;
using System.Security.Principal;

namespace ServicePipeLine
{
    /// <summary>
    /// This is a class to allow the transfer of data through a pipe stream via JSON objects as text
    /// </summary>
    // Cool to do's at a later date. add reflection leveraging with reflection attributes action/func dictionary style for ease as a full lib
    public class JSONPipeClient : IDisposable
    {
        /// <summary>
        /// the server that we are connecting to 
        /// </summary>
        NamedPipeClientStream PipeClient;
        /// <summary>
        /// The read string 
        /// </summary>
        StreamReader Rx;
        /// <summary>
        /// the transation string 
        /// </summary>
        StreamWriter Tx;

        /// <summary>
        /// A property that tells us if there is data waiting 
        /// </summary>
        public bool MessageIsWaiting
        {
            get
            {
                return Rx.Peek() == 0;
            }
        }

        /// <summary>
        /// A property to tell us if we are still connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return PipeClient.IsConnected;
            }
        }

        /// <summary>
        /// A JSONPipeClient Constructor
        /// </summary>
        /// <param name="PipeServerName">The Servers name that we wish to connect to</param>
        /// <param name="URL"></param>
        public JSONPipeClient(string PipeServerName, string URL = ".")
        {
            PipeClient = new NamedPipeClientStream(URL, PipeServerName, PipeDirection.InOut);
            Rx = new StreamReader(PipeClient);
            Tx = new StreamWriter(PipeClient);
            //PipeClient.ReadMode = PipeTransmissionMode.Message;
            //PipeClient.WriteTimeout = 30000;
            //PipeClient.ReadTimeout = 30000;
        }

        /// <summary>
        /// A Method to connect to the server.
        /// </summary>
        public void Connect()
        {
            PipeClient.Connect();
            PipeClient.ReadMode = PipeTransmissionMode.Message;
        }

        /// <summary>
        /// A Method to close a connection
        /// </summary>
        public void CloseConnection()
        {
            PipeClient.Close();
        }

        /// <summary>
        /// A method to send a JSON command and data package
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Package"></param>
        void SendCommandDataPackage<T>(JSONAction<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            PipeClient.WaitForPipeDrain();
        }

        /// <summary>
        /// A method that is used to send a JSON response package
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Package"></param>
        void SendResponseDataPackage<T>(JSONResponse<T> Package)
        {
            Tx.WriteLine(Package.ToString());
            Tx.Flush();
            PipeClient.WaitForPipeDrain();
        }

        /// <summary>
        /// A command to start to Listen for a command Data Pakage run over the line 
        /// </summary>
        /// <typeparam name="T">A templated Returns Data Type</typeparam>
        /// <returns>A request run that has run over the line</returns>
        JSONAction<T> ListenForCommand<T>()
        {
            while (Rx.Peek() == 0);
            return GetCommand<T>();
        }

        /// <summary>
        /// A command to start to Listen for a command run over the line 
        /// </summary>
        /// <typeparam name="T">A templated Returns Data Type</typeparam>
        /// <returns>A Command has run over the line</returns>
        JSONAction<T> GetCommand<T>()
        {
            return JSONAction<T>.FromString(Rx.ReadLine());
        }

        /// <summary>
        /// A command to start to Listen for a response to a command run over the line 
        /// </summary>
        /// <typeparam name="T">A templated Returns Data Type</typeparam>
        /// <returns>A reponse has run over the line</returns>
        JSONResponse<T> ListenForResponse<T>()
        {
            while (Rx.Peek() == 0);
            return GetResponse<T>();
        }

        /// <summary>
        /// Get a response line from the server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        JSONResponse<T> GetResponse<T>()
        {
            string Line = Rx.ReadLine();
            return JSONResponse<T>.FromString(Line);
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

        public void ProcessRequest<Tin, TResult>(Func<IJSONAction, IJSONResponse> Processe)
        {
            JSONAction<Tin> Request = ListenForCommand<Tin>();
            JSONResponse<TResult> Result = (JSONResponse<TResult>)Processe.Invoke(Request);
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
            PipeClient.Dispose();
        }
    }
}