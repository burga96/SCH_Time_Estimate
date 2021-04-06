using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class DepositInternalTransferPaymentTransaction : InternalTransferPaymentTransaction
    {
        public DepositInternalTransferPaymentTransaction()
        {
        }

        public DepositInternalTransferPaymentTransaction(Wallet wallet, Wallet secondWallet, decimal amount, string internalTransferId) : base(wallet, secondWallet, amount, PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT, internalTransferId)
        {
        }
    }
}