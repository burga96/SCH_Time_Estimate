using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class WithdrawalInternalTransferPaymentTransaction : InternalTransferPaymentTransaction
    {
        public WithdrawalInternalTransferPaymentTransaction()
        {
        }

        public WithdrawalInternalTransferPaymentTransaction(Wallet wallet, Wallet secondWallet, decimal amount, string internalTransferId) : base(wallet, secondWallet, amount, PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL, internalTransferId)
        {
        }
    }
}