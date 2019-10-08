using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartAndStopPipeClientNS
{
    public class PlayGameReturn
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

    public class CustomerData
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? StateID { get; set; }
        public int? CountryID { get; set; }
        public string Zip { get; set; }
        public Guid? VRStoreID { get; set; }
        public decimal Balance { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CardCount { get; set; }
    }
}
