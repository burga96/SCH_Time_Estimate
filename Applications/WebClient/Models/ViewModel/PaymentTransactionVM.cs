using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Applications.WebClient.Models.ViewModel
{
    public class PaymentTransactionVM
    {
        public PaymentTransactionVM()
        {
        }

        public PaymentTransactionVM(PaymentTransactionDTO paymentTransaction)
        {
            Id = paymentTransaction.Id;
            WalletId = paymentTransaction.WalletId;
            Wallet = paymentTransaction.Wallet != null ? new WalletVM(paymentTransaction.Wallet) : null;
            Amount = paymentTransaction.Amount;
            DateCreated = paymentTransaction.DateCreated;
            Type = paymentTransaction.Type;
        }

        public int Id { get; set; }
        public int WalletId { get; set; }
        public WalletVM Wallet { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public PaymentTransactionType Type { get; set; }
    }

    public static class PaymentTransactionExtensionMethods
    {
        public static PaymentTransactionVM ToPaymentTransactionVM(this PaymentTransactionDTO paymentTransaction)
        {
            switch (paymentTransaction.Type)
            {
                case PaymentTransactionType.DEPOSIT:
                    return new DepositPaymentTransactionVM((DepositPaymentTransactionDTO)paymentTransaction);

                case PaymentTransactionType.WITHDRAWAL:
                    return new WithdrawalPaymentTransactionVM((WithdrawalPaymentTransactionDTO)paymentTransaction);
            }

            return null;
        }

        public static IEnumerable<PaymentTransactionVM> ToPaymentTransactionVMs(this IEnumerable<PaymentTransactionDTO> paymentTransactions)
        {
            return paymentTransactions.Select(PaymentTransaction => ToPaymentTransactionVM(PaymentTransaction));
        }
    }
}