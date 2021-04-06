using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class MakeInternalPaymentTransactionVM : AuthenticationVM
    {
        public MakeInternalPaymentTransactionVM()
        {
        }

        public MakeInternalPaymentTransactionVM(decimal currentAmount,
            string uniqueMasterCitizenNumber,
            string password,
            string error,
            bool authenticated,
            string toUniqueMasterCitizenNumber) : base(uniqueMasterCitizenNumber, password, error, authenticated)
        {
            CurrentAmount = currentAmount;
            ToUniqueMasterCitizenNumber = toUniqueMasterCitizenNumber;
        }

        [DisplayName("Current amount")]
        public decimal CurrentAmount { get; set; }

        public decimal Amount { get; set; }

        [DisplayName("To unique master citizen number")]
        public string ToUniqueMasterCitizenNumber { get; set; }
    }
}