using GameStartStopService.BasicConfig;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GameStartStopService.TheServerClient.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections.Generic;

namespace GameStartStopService.TheServerClient
{
    internal class ServerClient
    {
        const string ErrorMsgNotLogedin = "You have not called connet to login in yet";
        const string ErrorMsgUnknownError = "There was an unknow error";

        const string APILoginEndpoint = "/api/account/login";
        const string APICardCheckEndpoint = "/api/card/check/{0}/{1}";
        const string APITransactPlayGameEndpoint = "/api/transact/playgame/{0}/{1}/{2}";
        const string APITransactCanPlayGameEndpoint = "/api/transact/canplaygame/{0}/{1}/{2}";
        const string APIGameGetGamesEndpoint = "/api/game/{0}";

        HttpClient HttpClient;

        //[DataType(DataType.Password)]
        string Password;
        string UserName;
        string Token = null;
        string URL;

        public bool Connected { get; private set; } = false;

        internal ServerClient(GameServiceConfig GameServiceConfig)
        {
            Password = GameServiceConfig.ServerCredential.Password;
            UserName = GameServiceConfig.ServerCredential.UserName;
            URL = GameServiceConfig.MasterServerURL;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(URL);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ResponseInfo<LoginReturn, ResponseStatus> Connect()
        {
            var LoginCredential = new { password = Password, userName = UserName };

            ResponseInfo<LoginReturn, ResponseStatus> Return = Post<ResponseInfo<LoginReturn, ResponseStatus>>(APILoginEndpoint, LoginCredential);
            try
            {
                Token = Return.Data.tokenString;
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            catch(Exception e)
            {
                if (Return.Status == ResponseStatus.Error)
                    throw new Exception(Return.Message);
                throw e;
            }
            Connected = true;
            return Return;
        }

        public ResponseInfo<CheckCardReturn, ResponseStatus> CardCheck(string CardGUID, int CheckKey)
        {
            if (Token != null)
            {
                var CheckCardObj = new { cardNumber = CardGUID, checkKey = CheckKey };
                string APICardCheck = string.Format(APICardCheckEndpoint, CheckCardObj.cardNumber, CheckCardObj.checkKey);
                return Post<ResponseInfo<CheckCardReturn, ResponseStatus>>(APICardCheck, new { });
            }
            else throw new Exception(ErrorMsgNotLogedin);
        }

        public ResponseInfo<PlayGameReturn, ResponseStatus> PlayGame(string CardGUID, int CheckKey, string GUID)
        {
            if (Token != null)
            {
                var PlayGameObj = new { cardNumber = CardGUID, checkKey = CheckKey, machineGameID = GUID };
                string APITransactPlayGame = string.Format(APITransactPlayGameEndpoint, PlayGameObj.cardNumber, PlayGameObj.checkKey, PlayGameObj.machineGameID);
                return Post<ResponseInfo<PlayGameReturn, ResponseStatus>>(APITransactPlayGame, new { });
            }
            else throw new Exception(ErrorMsgNotLogedin);
        }

        public ResponseInfo<CanPlayReturn, ResponseStatus> CanPlayGame(string CardGUID, int CheckKey, string GUID)
        {
            if (Token != null)
            {
                var PlayGameObj = new { cardNumber = CardGUID, checkKey = CheckKey, machineGameID = GUID };
                string APITransactCanPlayGame = string.Format(APITransactCanPlayGameEndpoint, PlayGameObj.cardNumber, PlayGameObj.checkKey, PlayGameObj.machineGameID);
                return Post<ResponseInfo<CanPlayReturn, ResponseStatus>>(APITransactCanPlayGame, new { });
            }
            else throw new Exception(ErrorMsgNotLogedin);
        }

        public ResponseInfo<GetMachineGamesReturn, ResponseStatus> GetMachineGames(string MachineGUID)
        {
            if (Token != null)
            {
                string APIGameGetGames = string.Format(APIGameGetGamesEndpoint, MachineGUID);
                return Get<ResponseInfo<GetMachineGamesReturn, ResponseStatus>>(APIGameGetGames, new { });
            }
            else throw new Exception(ErrorMsgNotLogedin);
        }

        ByteArrayContent BuildContent(byte[] Data, bool UseSecurityToken = true)
        {
            ByteArrayContent Content = new ByteArrayContent(Data);
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Content;
        }

        T Get<T>(string Endpoint, object GetVars)
        {
            Endpoint += ObjectToGetVars(GetVars);
            HttpResponseMessage Response = HttpClient.GetAsync(Endpoint).Result;
            Response.EnsureSuccessStatusCode();
            string responseInfo = Response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(responseInfo))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseInfo);
            }
            else throw new Exception(ErrorMsgUnknownError);
        }

        T Post<T>(string Endpoint, object BodyModel)
        {
            byte[] Data = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(BodyModel));
            ByteArrayContent Content = BuildContent(Data);
            HttpResponseMessage Response = HttpClient.PostAsync(Endpoint, Content).Result;
            Response.EnsureSuccessStatusCode();
            string responseInfo = Response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(responseInfo))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseInfo);
            }
            else throw new Exception(ErrorMsgUnknownError);
        }

        T Put<T>(string Endpoint, object GetVars, object BodyModel)
        {
            Endpoint += ObjectToGetVars(GetVars);
            byte[] Data = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(BodyModel));
            ByteArrayContent Content = BuildContent(Data);
            HttpResponseMessage Response = HttpClient.PutAsync(Endpoint, Content).Result;
            Response.EnsureSuccessStatusCode();
            string responseInfo = Response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(responseInfo))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseInfo);
            }
            else throw new Exception(ErrorMsgUnknownError);
        }

        string ObjectToGetVars(object Obj)
        {
            Type ObjType = Obj.GetType();
            List<PropertyInfo> props = new List<PropertyInfo>(ObjType.GetProperties());
            string GetVars = "?";
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetType().IsPrimitive)
                {
                    object propValue = prop.GetValue(Obj, null);
                    GetVars += prop.Name + "=" + propValue + "&";
                }
                // Do something with propValue
            }
            return GetVars.Remove(GetVars.Length - 1);
        }
    }
}
