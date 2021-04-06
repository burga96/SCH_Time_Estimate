using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static class PaymentTransactionExtensionMethods
    {
        public static PaymentTransactionDTO ToPaymentTransactionDTO(this PaymentTransaction paymentTransaction)
        {
            switch (paymentTransaction.Type)
            {
                case PaymentTransactionType.DEPOSIT:
                    return new DepositPaymentTransactionDTO((DepositPaymentTransaction)paymentTransaction);

                case PaymentTransactionType.WITHDRAWAL:
                    return new WithdrawalPaymentTransactionDTO((WithdrawalPaymentTransaction)paymentTransaction);

                case PaymentTransactionType.INTERNAL_TRANSFER_DEPOSIT:
                    return new DepositInternalTransferPaymentTransactionDTO((DepositInternalTransferPaymentTransaction)paymentTransaction);

                case PaymentTransactionType.INTERNAL_TRANSFER_WITHDRAWAL:
                    return new WithdrawalInternalTransferPaymentTransactionDTO((WithdrawalInternalTransferPaymentTransaction)paymentTransaction);

                case PaymentTransactionType.FEE:
                    return new FeeInternalTransferPaymentTransactionDTO((FeeInternalTransferPaymentTransaction)paymentTransaction);
            }

            return null;
        }

        public static IEnumerable<PaymentTransactionDTO> ToPaymentTransactionDTOs(this IEnumerable<PaymentTransaction> paymentTransactions)
        {
            return paymentTransactions.Select(PaymentTransaction => ToPaymentTransactionDTO(PaymentTransaction));
        }
    }
}