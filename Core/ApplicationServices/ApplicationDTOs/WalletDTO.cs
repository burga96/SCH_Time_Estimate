using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class WalletDTO
    {
        public WalletDTO(Wallet wallet)
        {
            Id = wallet.Id;
            UniqueMasterCitizenNumber = wallet.UniqueMasterCitizenNumber.Value;
            Password = wallet.Password;
            FirstName = wallet.PersonalData.FirstName;
            LastName = wallet.PersonalData.LastName;
            FullName = wallet.PersonalData.FullName;
            PostalIndexNumber = wallet.PostalIndexNumber;
            CurrentAmount = wallet.CurrentAmount;
            SupportedBankId = wallet.SupportedBankId;
            Status = wallet.Status;
            SupportedBankName = wallet.SupportedBank?.Name;
            PaymentTransactions = wallet.PaymentTransactions != null ? wallet.PaymentTransactions.ToPaymentTransactionDTOs() : new List<PaymentTransactionDTO>();
        }

        public WalletDTO(Wallet wallet, IEnumerable<PaymentTransaction> paymentTransactions)
        {
            Id = wallet.Id;
            UniqueMasterCitizenNumber = wallet.UniqueMasterCitizenNumber.Value;
            Password = wallet.Password;
            FirstName = wallet.PersonalData.FirstName;
            LastName = wallet.PersonalData.LastName;
            FullName = wallet.PersonalData.FullName;
            PostalIndexNumber = wallet.PostalIndexNumber;
            CurrentAmount = wallet.CurrentAmount;
            Status = wallet.Status;
            SupportedBankId = wallet.SupportedBankId;
            SupportedBankName = wallet.SupportedBank?.Name;
            PaymentTransactions = paymentTransactions.ToPaymentTransactionDTOs();
        }

        public int Id { get; private set; }
        public string UniqueMasterCitizenNumber { get; private set; }
        public decimal CurrentAmount { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public int SupportedBankId { get; private set; }
        public string SupportedBankName { get; private set; }
        public string Password { get; private set; }
        public string PostalIndexNumber { get; set; }
        public IEnumerable<PaymentTransactionDTO> PaymentTransactions { get; private set; }
        public WalletStatus Status { get; private set; }
    }

    public static partial class WalletExtensionMethods
    {
        public static IEnumerable<WalletDTO> ToWalletDTOs(this IEnumerable<Wallet> wallets)
        {
            return wallets.Select(wallet => new WalletDTO(wallet));
        }
    }
}