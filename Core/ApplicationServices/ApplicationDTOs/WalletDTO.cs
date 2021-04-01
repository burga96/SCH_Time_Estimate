﻿using Core.Domain.Entities;
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
            SupportedBankId = wallet.SupportedBankId;
            SupportedBankName = wallet.SupportedBank?.Name;
        }

        public int Id { get; private set; }
        public string UniqueMasterCitizenNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public int SupportedBankId { get; private set; }
        public string SupportedBankName { get; private set; }
        public string Password { get; private set; }
    }

    public static partial class WalletExtensionMethods
    {
        public static IEnumerable<WalletDTO> ToWalletDTOs(this IEnumerable<Wallet> wallets)
        {
            return wallets.Select(wallet => new WalletDTO(wallet));
        }
    }
}