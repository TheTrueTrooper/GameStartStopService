using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.PipeServer.Models
{
    class SlaveLocationModel
    {
        public string Name;
        public string LocalIP;
        public string PublicIP;

        public SlaveLocationModel()
        {
            ////IPAddress.Parse("2001:569:fa37:ca00:fdee:c9a1:8d0a:d0df").MapToIPv4().ToString();
            //string IP = IPAddress.Parse(new System.Net.WebClient().DownloadString("https://ipinfo.io/ip").Replace("\n", "")).ToString();
            IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
            LocalIP = Host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            Name = ArcadeGameStartAndStopService.MainConfig.MachineName;
            PublicIP = IPAddress.Parse(new WebClient().DownloadString("http://icanhazip.com").Replace("\n", "")).MapToIPv4().ToString();
        }
    }
}
