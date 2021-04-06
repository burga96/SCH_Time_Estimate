using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class MakeDepositPaymentTransactionVM : AuthenticationVM
    {
        public MakeDepositPaymentTransactionVM()
        {
        }

        public MakeDepositPaymentTransactionVM(decimal currentAmount, string uniqueMasterCitizenNumber, string password, string error, bool authenticated) : base(uniqueMasterCitizenNumber, password, error, authenticated)
        {
            CurrentAmount = currentAmount;
        }

        [DisplayName("Current amount")]
        public decimal CurrentAmount { get; set; }

        public decimal Amount { get; set; }
    }
}