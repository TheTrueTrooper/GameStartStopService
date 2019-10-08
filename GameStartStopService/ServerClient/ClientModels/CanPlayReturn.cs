using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public class CanPlayReturn
    {
        public bool CanPlay { get; set; }
        public double CurrentBalance { get; set; }
        public double NewBalance { get; set; }
        public int? NewCheckKey { get; set; }
    }
}
