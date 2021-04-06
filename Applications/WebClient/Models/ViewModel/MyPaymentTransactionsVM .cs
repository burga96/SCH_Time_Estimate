using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class MyPaymentTransactionsVM : AuthenticationVM
    {
        public MyPaymentTransactionsVM()
        {
        }

        public MyPaymentTransactionsVM(decimal currentAmount,
            string uniqueMasterCitizenNumber,
            string password,
            string error,
            bool authenticated,
            IEnumerable<PaymentTransactionVM> paymentTransactions,
            DateTime? from,
            DateTime? to) : base(uniqueMasterCitizenNumber, password, error, authenticated)
        {
            PaymentTransactions = paymentTransactions;
            From = from;
            To = to;
            CurrentAmount = currentAmount;
        }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        [DisplayName("Current amount")]
        public decimal CurrentAmount { get; set; }

        public IEnumerable<PaymentTransactionVM> PaymentTransactions { get; set; }
    }
}