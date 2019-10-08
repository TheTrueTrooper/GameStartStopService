using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public class LoginReturn
    {
        public string phoneNumber { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string roleID { get; set; }
        public string roleName { get; set; }
        public string userID { get; set; }
        public bool isActive { get; set; }
        public bool isLockedEnabled { get; set; }
        public string tokenString { get; set; }
    }
}
