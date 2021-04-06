using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class PaymentTransactionsByAdminVM : PasswordVM
    {
        public PaymentTransactionsByAdminVM()
        {
        }

        public PaymentTransactionsByAdminVM(
            string password,
            string error,
            bool authenticated,
            IEnumerable<PaymentTransactionVM> paymentTransactions,
            DateTime? from,
            DateTime? to) : base(password, error, authenticated)
        {
            PaymentTransactions = paymentTransactions;
            From = from;
            To = to;
        }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public IEnumerable<PaymentTransactionVM> PaymentTransactions { get; set; }
    }
}