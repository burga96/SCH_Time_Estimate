using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class InternalTransferPaymentTransactionDTO : PaymentTransactionDTO
    {
        public InternalTransferPaymentTransactionDTO(InternalTransferPaymentTransaction paymentTransaction) : base(paymentTransaction)
        {
            InternalTransferId = paymentTransaction.InternalTransferId;
            SecondWalletId = paymentTransaction.SecondWalletId;
            SecondWallet = paymentTransaction.SecondWallet != null ? new WalletDTO(paymentTransaction.SecondWallet) : null;
        }

        public string InternalTransferId { get; private set; }
        public int SecondWalletId { get; private set; }
        public WalletDTO SecondWallet { get; private set; }
    }
}