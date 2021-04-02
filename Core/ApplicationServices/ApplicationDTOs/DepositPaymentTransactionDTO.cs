using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class DepositPaymentTransactionDTO : PaymentTransactionDTO
    {
        public DepositPaymentTransactionDTO(DepositPaymentTransaction paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}