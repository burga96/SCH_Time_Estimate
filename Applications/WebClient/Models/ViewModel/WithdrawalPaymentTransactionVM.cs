using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Applications.WebClient.Models.ViewModel
{
    public class WithdrawalPaymentTransactionVM : PaymentTransactionVM
    {
        public WithdrawalPaymentTransactionVM(WithdrawalPaymentTransactionDTO paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}