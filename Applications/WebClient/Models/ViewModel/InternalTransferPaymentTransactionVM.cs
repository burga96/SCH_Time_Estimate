using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Applications.WebClient.Models.ViewModel
{
    public class InternalTransferPaymentTransactionVM : PaymentTransactionVM
    {
        public InternalTransferPaymentTransactionVM(InternalTransferPaymentTransactionDTO paymentTransaction) : base(paymentTransaction)
        {
            InternalTransferId = paymentTransaction.InternalTransferId;
            SecondWalletId = paymentTransaction.SecondWalletId;
            SecondWallet = paymentTransaction.SecondWallet != null ? new WalletVM(paymentTransaction.SecondWallet) : null;
        }

        public string InternalTransferId { get; private set; }
        public int SecondWalletId { get; private set; }
        public WalletVM SecondWallet { get; private set; }
    }
}