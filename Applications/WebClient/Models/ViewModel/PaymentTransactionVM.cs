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

        public bool IsDeposit
        {
            get
            {
                return Type == PaymentTransactionType.DEPOSIT ||
                    Type == PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT;
            }
        }

        public bool IsWithdrawal
        {
            get
            {
                return Type == PaymentTransactionType.WITHDRAWAL ||
                    Type == PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL ||
                    Type == PaymentTransactionType.FEE;
            }
        }

        public bool IsInternal
        {
            get
            {
                return Type == PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT ||
                    Type == PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL ||
                    Type == PaymentTransactionType.FEE;
            }
        }
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

                case PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT:
                    return new DepositInternalTransferPaymentTransactionVM((DepositInternalTransferPaymentTransactionDTO)paymentTransaction);

                case PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL:
                    return new WithdrawalInternalTransferPaymentTransactionVM((WithdrawalInternalTransferPaymentTransactionDTO)paymentTransaction);

                case PaymentTransactionType.FEE:
                    return new FeeInternalTransferPaymentTransactionVM((FeeInternalTransferPaymentTransactionDTO)paymentTransaction);
            }

            return null;
        }

        public static IEnumerable<PaymentTransactionVM> ToPaymentTransactionVMs(this IEnumerable<PaymentTransactionDTO> paymentTransactions)
        {
            return paymentTransactions.Select(PaymentTransaction => ToPaymentTransactionVM(PaymentTransaction));
        }
    }
}