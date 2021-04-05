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

        public static bool IsLimitExceed(Wallet wallet)
        {
            return IsDepositLimitExceed(wallet) || IsWithdrawalLimitExceed(wallet);
        }

        public static bool IsDepositLimitExceed(Wallet wallet)
        {
            var date = DateTime.Now;
            return wallet.DepositPaymentTransactionsSum(date) > DepositLimitPaymentTransactionAmount;
        }

        public static bool IsWithdrawalLimitExceed(Wallet wallet)
        {
            var date = DateTime.Now;
            return wallet.WithdrawalPaymentTransactionsSum(date) > WithdrawalLimitPaymentTransactionAmount;
        }
    }
}