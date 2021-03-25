using Core.ApplicationServices.ApplicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class SupportedBankVM
    {
        public SupportedBankVM(SupportedBankDTO supportedBank)
        {
            Id = supportedBank.Id;
            Name = supportedBank.Name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
    }

    public static partial class SupportedBankDTOExtensionMethods
    {
        public static IEnumerable<WalletVM> ToSupportedBankVMs(this IEnumerable<WalletDTO> wallets)
        {
            return wallets.Select(wallet => new WalletVM(wallet));
        }
    }
}