using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class PaymentTransactionDTO
    {
        public PaymentTransactionDTO(PaymentTransaction paymentTransaction)
        {
            Id = paymentTransaction.Id;
            WalletId = paymentTransaction.WalletId;
            Wallet = paymentTransaction.Wallet != null ? new WalletDTO(paymentTransaction.Wallet) : null;
            Amount = paymentTransaction.Amount;
            DateCreated = paymentTransaction.DateCreated;
            Type = paymentTransaction.Type;
        }

        public int Id { get; private set; }
        public int WalletId { get; private set; }
        public WalletDTO Wallet { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DateCreated { get; private set; }
        public PaymentTransactionType Type { get; private set; }
    }
}