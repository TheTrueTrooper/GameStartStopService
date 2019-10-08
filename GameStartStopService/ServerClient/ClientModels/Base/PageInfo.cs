using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public long TotalRecords { get; set; }
        public int NoOfPages { get; set; }
    }
}
