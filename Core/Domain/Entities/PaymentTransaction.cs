using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public abstract class PaymentTransaction
    {
        public PaymentTransaction()
        {
        }

        public PaymentTransaction(Wallet wallet, decimal amount, PaymentTransactionType type)
        {
            Wallet = wallet;
            WalletId = Wallet.Id;
            Amount = amount;
            DateCreated = DateTime.Now;
            Type = type;
        }

        public int Id { get; private set; }
        public int WalletId { get; private set; }
        public Wallet Wallet { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DateCreated { get; private set; }
        public PaymentTransactionType Type { get; private set; }

        public bool IsInDateTimeScope(DateTime? from, DateTime? to)
        {
            return ((from != null && DateCreated >= from) || (from == null)) &&
                    ((to != null && DateCreated <= to) || (to == null));
        }
    }
}