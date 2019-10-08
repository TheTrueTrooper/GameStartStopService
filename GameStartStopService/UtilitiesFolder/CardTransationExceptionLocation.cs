using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.UtilitiesFolder
{
    public enum CardTransationExceptionLocation
    {
        Athentication,
        CardNumberReadFail,
        CardCheckValueReadFail,
        ServerCardCheckFail,
        ServerTransactionFail,
        CardCheckValueWriteFail
    }
}
