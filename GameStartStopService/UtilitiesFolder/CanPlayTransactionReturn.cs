using GameStartStopService.TheServerClient.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.UtilitiesFolder
{
    class CanPlayTransactionReturn : CanPlayReturn
    {
        public string CardGUID { get; set; }
    }
}
