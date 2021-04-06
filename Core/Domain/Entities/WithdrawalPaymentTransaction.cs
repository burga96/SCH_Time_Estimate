using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class WithdrawalPaymentTransaction : PaymentTransaction
    {
        public WithdrawalPaymentTransaction()
        {
        }

        public WithdrawalPaymentTransaction(Wallet wallet, decimal amount) : base(wallet, amount, PaymentTransactionType.WITHDRAWAL)
        {
        }
    }
}