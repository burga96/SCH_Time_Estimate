using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class InternalTransferPaymentTransactionsDTO
    {
        public InternalTransferPaymentTransactionsDTO(InternalTransferPaymentTransactions internalTransferPaymentTransactions)
        {
            Deposit = (DepositInternalTransferPaymentTransactionDTO)internalTransferPaymentTransactions.Deposit.ToPaymentTransactionDTO();
            Withdrawal = (WithdrawalInternalTransferPaymentTransactionDTO)internalTransferPaymentTransactions.Withdrawal.ToPaymentTransactionDTO();
            Fee = (FeeInternalTransferPaymentTransactionDTO)internalTransferPaymentTransactions.Fee.ToPaymentTransactionDTO();
        }

        public DepositInternalTransferPaymentTransactionDTO Deposit { get; private set; }
        public WithdrawalInternalTransferPaymentTransactionDTO Withdrawal { get; private set; }
        public FeeInternalTransferPaymentTransactionDTO Fee { get; private set; }
    }
}