using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Helpers
{
    public static class LimitPaymentTransactionCalculator
    {
        private static readonly decimal DepositLimitPaymentTransactionAmount = 1000000;
        private static readonly decimal WithdrawalLimitPaymentTransactionAmount = 1000000;

        public static bool IsLimitExceed(Wallet wallet, decimal newPaymentTransactionAmount)
        {
            return IsDepositLimitExceed(wallet, newPaymentTransactionAmount) || IsWithdrawalLimitExceed(wallet, newPaymentTransactionAmount);
        }

        public static bool IsDepositLimitExceed(Wallet wallet, decimal newPaymentTransactionAmount)
        {
            var date = DateTime.Now;
            return wallet.DepositPaymentTransactionsSum(date) + newPaymentTransactionAmount > DepositLimitPaymentTransactionAmount;
        }

        public static bool IsWithdrawalLimitExceed(Wallet wallet, decimal newPaymentTransactionAmount)
        {
            var date = DateTime.Now;
            return wallet.WithdrawalPaymentTransactionsSum(date) + newPaymentTransactionAmount > WithdrawalLimitPaymentTransactionAmount;
        }
    }
}