using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObjects
{
    public class InternalTransferPaymentTransactions
    {
        public InternalTransferPaymentTransactions(DepositInternalTransferPaymentTransaction deposit,
            WithdrawalInternalTransferPaymentTransaction withdrawal,
            FeeInternalTransferPaymentTransaction fee)
        {
            Deposit = deposit;
            Withdrawal = withdrawal;
            Fee = fee;
        }

        public DepositInternalTransferPaymentTransaction Deposit { get; private set; }
        public WithdrawalInternalTransferPaymentTransaction Withdrawal { get; private set; }
        public FeeInternalTransferPaymentTransaction Fee { get; private set; }

        public bool HasFee()
        {
            return Fee.Amount > 0;
        }
    }
}