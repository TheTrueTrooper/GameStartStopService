using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;

namespace ServicePipeLine
{
    public class PipeRequestServer : IDisposable
    {
        Dictionary<string, InstancedAndMethodInfo> Processes = new Dictionary<string, InstancedAndMethodInfo>();

        JSONPipeServer Server;

        Thread Thread;

        bool Listening = true;

        public PipeRequestServer(string ServerName, int MaxNumberOfConnections = 1, bool ConstantListener = true)
        {
            Server = new JSONPipeServer(ServerName, MaxNumberOfConnections);
            if (ConstantListener)
            {
                Thread = new Thread(new ParameterizedThreadStart(ListenLoop));
                Thread.Start(this);
            }
            else
                Listening = false;
        }

        public void AddProccess(object Instance)
        {
            MethodInfo[] Methods = Instance.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(PipeFunctionAttribute), false).Length > 0).ToArray();

            foreach(MethodInfo M in Methods)
            {
                if (!M.ReturnType.IsSubclassOf(typeof(JSONResponseRoot)) || !M.ReturnType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IJSONResponse<>)))
                    throw new Exception("Method has a non-acceptable return.\nClass must be a a decendant of PipeJSONResponseRoot && implement the IPipeJSONResponse<> interface.\nTry using PipeJSONResponse<T> Templated to a class with the required data");
                if (M.GetParameters().Count() != 1 || !M.GetParameters()[0].ParameterType.IsSubclassOf(typeof(JSONActionRoot)) || !M.GetParameters()[0].ParameterType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IJSONAction<>)))
                    throw new Exception("Method has a non-acceptable input\nThere can't be more than one param.\nClass must be a a decendant of PipeJSONResponseRoot && implement the IPipeJSONResponse<> interface.\nTry using PipeJSONResponse<T> Templated to a class with the required data.");

                InstancedAndMethodInfo Process = new InstancedAndMethodInfo();

                string Name = ((PipeFunctionAttribute)M.GetCustomAttributes(typeof(PipeFunctionAttribute), false).FirstOrDefault()).ActionName;
                Process.Instance = Instance;
                Process.MethodInfo = M;
                Processes.Add(Name, Process);
            }
        }

        public void ProcessRequest()
        {
            Server.WaitForConnect();
            JSONAction<dynamic> Request = Server.ListenForCommand<dynamic>();

            if (Processes.ContainsKey(Request.ActionName))
            {
                InstancedAndMethodInfo MethodInfo = Processes[Request.ActionName];
                Type TemplatedType = MethodInfo.MethodInfo.GetParameters()[0].ParameterType.GetProperties().Where(x => x.Name == "ActionData").FirstOrDefault().PropertyType;
                object Result = MethodInfo.MethodInfo.Invoke(MethodInfo.Instance, new object[] { Request.ActionDynamicAutoCast(TemplatedType) });

                Type Return = MethodInfo.MethodInfo.ReturnType.GetProperties().Where(x => x.Name == "ActionData").FirstOrDefault().PropertyType;
                MethodInfo GenericMethod = typeof(JSONPipeServer).GetMethod("SendResponseDataPackage", BindingFlags.Instance | BindingFlags.NonPublic);
                MethodInfo SendResponseDataPackageMethod = GenericMethod.MakeGenericMethod(Return);

                if(Server.IsConnected)
                    SendResponseDataPackageMethod.Invoke(Server, new object[] { Result });
                if (Server.IsConnected)
                    Server.CloseConnection();
            }
            else
            {
                JSONResponse<dynamic> Response = new JSONResponse<dynamic>() { ActionData = new { }, RequestStatus = JSONResponseStatus.ActionNotFound, ActionName = Request.ActionName };
                if (Server.IsConnected)
                    Server.SendResponseDataPackage(Response);
                if (Server.IsConnected)
                    Server.CloseConnection();
            }
        }

        static void ListenLoop(object This)
        {
            PipeRequestServer Server = (PipeRequestServer)This;

            while (Server.Listening)
                lock(Server)
                    Server.ProcessRequest();
        }

        public void Dispose()
        {
            Listening = false;
            Server.Dispose();
            Thread?.Abort();
        }
    }
}
