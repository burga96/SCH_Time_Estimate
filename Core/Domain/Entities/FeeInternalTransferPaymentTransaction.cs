using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class FeeInternalTransferPaymentTransaction : InternalTransferPaymentTransaction
    {
        public FeeInternalTransferPaymentTransaction()
        {
        }

        public FeeInternalTransferPaymentTransaction(Wallet wallet, Wallet secondWallet, decimal amount, string internalTransferId) : base(wallet, secondWallet, amount, PaymentTransactionType.FEE, internalTransferId)
        {
        }
    }
}