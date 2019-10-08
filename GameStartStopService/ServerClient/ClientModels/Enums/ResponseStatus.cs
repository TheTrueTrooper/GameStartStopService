using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public enum ResponseStatus
    {
        None = 0,
        Success = 200,
        Error = 500,
        NotFound = 404,
        Error403 = 403,
        BadRequest = 901
    }
}
