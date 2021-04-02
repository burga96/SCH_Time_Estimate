using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class DepositPaymentTransaction : PaymentTransaction
    {
        public DepositPaymentTransaction()
        {
        }

        public DepositPaymentTransaction(Wallet wallet, decimal amount) : base(wallet, amount, PaymentTransactionType.DEPOSIT)
        {
        }
    }
}