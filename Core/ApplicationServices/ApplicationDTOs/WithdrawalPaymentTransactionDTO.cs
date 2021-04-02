using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class WithdrawalPaymentTransactionDTO : PaymentTransactionDTO
    {
        public WithdrawalPaymentTransactionDTO(WithdrawalPaymentTransaction paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}