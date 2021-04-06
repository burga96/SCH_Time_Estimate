using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class WithdrawalInternalTransferPaymentTransactionDTO : InternalTransferPaymentTransactionDTO
    {
        public WithdrawalInternalTransferPaymentTransactionDTO(WithdrawalInternalTransferPaymentTransaction paymentTransaction) : base(paymentTransaction)
        {
        }
    }
}