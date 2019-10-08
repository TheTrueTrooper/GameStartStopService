using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.ConfigEnums;
using GameStartStopService.ConfigLoaders;

namespace GameStartStopService.BasicConfig
{
    static class ConfigHelpers
    {
        public static void Save(GameServiceConfig Settings)
        {
            JSONServiceConfig.SaveJSONServiceConfig(Settings);
        }

        public static string ConvertErrorsToMessage(Dictionary<string, List<string>> Errors)
        {
            string Message = "";
            foreach (KeyValuePair<string, List<string>> ErrorList in Errors)
            {
                Message += ErrorList.Key + ":\n";
                foreach (string Error in ErrorList.Value)
                {
                    Message += Error;
                }
            }
            return Message;
        }

        public static bool ValidateConfig(GameServiceConfig ConfigToTest, out Dictionary<string, List<string>> Errors)
        {
            const string MachineGUID = "MachineGUID";
            const string MasterServerURL = "MasterServerURL";
            const string MachineName = "MachineName";
            const string StarterMode = "StarterMode";
            const string ServerCredentialPassword = "ServerCredential.Password";
            const string ServerCredentialUserName = "ServerCredential.UserName";
            const string MasterStarterMasterLoc = "MasterStarterMasterLoc";
            Errors = new Dictionary<string, List<string>>();
            if (ConfigToTest.MachineGUID == null)
            {
                if (!Errors.Keys.Contains(MachineGUID))
                    Errors.Add(MachineGUID, new List<string>());
                Errors[MachineGUID].Add("'Master Server URL' has been left null or empty.\n");
            }
            if (ConfigToTest.MasterServerURL == null)
            {
                if (!Errors.Keys.Contains(MasterServerURL))
                    Errors.Add(MasterServerURL, new List<string>());
                Errors[MasterServerURL].Add("'Master Server URL' has been left null or empty.\n");
            }
            if (ConfigToTest.MachineName == null)
            {
                if (!Errors.Keys.Contains(MachineName))
                    Errors.Add(MachineName, new List<string>());
                Errors[MachineName].Add("'Machine Name' has been left null or empty.\n");
            }
            if (ConfigToTest.StarterMode == null || !Enum.IsDefined(typeof(GameStartMode), ConfigToTest.StarterMode))
            {
                if (!Errors.Keys.Contains(StarterMode))
                    Errors.Add(StarterMode, new List<string>());
                Errors[StarterMode].Add("'Starter Mode' has been left null, empty, or not a valid value.\n");
            }
            if (ConfigToTest?.ServerCredential?.Password == null)
            {
                if (!Errors.Keys.Contains(ServerCredentialPassword))
                    Errors.Add(ServerCredentialPassword, new List<string>());
                Errors[ServerCredentialPassword].Add("'Password' in the 'Server Credential' has been left null or empty.\n");
            }
            if (ConfigToTest?.ServerCredential?.UserName == null)
            {
                if (!Errors.Keys.Contains(ServerCredentialUserName))
                    Errors.Add(ServerCredentialUserName, new List<string>());
                Errors[ServerCredentialUserName].Add("'UserName' in the 'Server Credential' has been left null or empty.\n");
            }
            if (ConfigToTest.StarterMode == GameStartMode.MultiSocketStarterSlave && ConfigToTest.MasterStarterMasterLoc == null)
            {
                if (!Errors.Keys.Contains(MasterStarterMasterLoc))
                    Errors.Add(MasterStarterMasterLoc, new List<string>());
                Errors[MasterStarterMasterLoc].Add("'Master Starter Master IP or URL' has been left null or empty with a 'Multi Socket Starter Slave' config.\n");
            }

            if (Errors.Count() > 0)
                return false;
            return true;
        }
    }
}
