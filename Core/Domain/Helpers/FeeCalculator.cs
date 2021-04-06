using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Helpers
{
    public static class FeeCalculator
    {
        public static readonly decimal FixedFee = 100;
        public static readonly decimal MinimalFixedAmount = 10000;
        public static readonly int NumberOfDaysWithoutFee = 7;
        public static readonly decimal FeePercentage = 1;

        public static decimal CalculateFee(Wallet wallet, decimal amount)
        {
            var date = DateTime.Now;
            if (date.AddDays(-NumberOfDaysWithoutFee) < wallet.CreatedAt)
            {
                return 0;
            }
            if (wallet.NumberOfPaymentTransactionOnSpecificMonth(date) == 0)
            {
                return 0;
            }
            if (amount < MinimalFixedAmount)
            {
                return FixedFee;
            }
            return amount * FeePercentage / 100;
        }
    }
}