using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public class JSONRequestClientPipe
    {
        public string PipeServerName { get; private set; }
        public string URL { get; private set; }

        JSONPipeClient Client;

        public TimeSpan StandardTimeout { get; set; } = new TimeSpan(0, 0, 150);

        public JSONRequestClientPipe(string PipeServerName, string URL = ".")
        {
            this.PipeServerName = PipeServerName;
            this.URL = URL;
        }

        public void CloseConnection()
        {
            Client.CloseConnection();
        }

        public PipeJSONResponse<TResult> SendCommandRequest<Tin, TResult>(PipeJSONAction<Tin> Package, TimeSpan Timeout)
        {
            PipeJSONResponse<TResult> Result = new PipeJSONResponse<TResult>() { ActionName = Package.ActionName, RequestStatus = PipeJSONResponseStatus.TimeOut, Message = "Client has timed out o its request to the server." };
            Client = new JSONPipeClient(PipeServerName, URL);
            Client.Connect();
            Task task = Task.Factory.StartNew(() =>
            {
                Result = Client.SendCommandRequest<Tin, TResult>(Package);
                Client.CloseConnection();
                return Result;
            });

            task.Wait(Timeout);

            if(Client.IsConnected)
                Client.CloseConnection();

            return Result;
        }

        public PipeJSONResponse<TResult> SendCommandRequest<Tin, TResult>(PipeJSONAction<Tin> Package)
        {
            return SendCommandRequest<Tin, TResult>(Package, StandardTimeout);
        }
    }
}
