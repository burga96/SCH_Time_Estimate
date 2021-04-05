using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public abstract class InternalTransferPaymentTransaction : PaymentTransaction
    {
        public InternalTransferPaymentTransaction()
        {
        }

        public InternalTransferPaymentTransaction(Wallet wallet, Wallet secondWallet, decimal amount, PaymentTransactionType type, string internalTransferId) : base(wallet, amount, type)
        {
            SecondWallet = secondWallet;
            SecondWalletId = SecondWallet.Id;
            InternalTransferId = internalTransferId;
        }

        public string InternalTransferId { get; private set; }
        public int SecondWalletId { get; private set; }
        public Wallet SecondWallet { get; private set; }
    }
}