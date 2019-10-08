using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public class CheckCardReturn : ICardCheck
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public int? CheckNum { get; set; }
        public Guid? CustomerID { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? LimitBalance { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public decimal CustomerBalance { get; set; }
        public CustomerData Customer { get; set; }
    }
}
