using Core.ApplicationServices.ApplicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class WalletVM
    {
        public WalletVM(WalletDTO wallet)
        {
            Id = wallet.Id;
            UniqueMasterCitizenNumber = wallet.UniqueMasterCitizenNumber;
            Password = wallet.Password;
            FirstName = wallet.FirstName;
            LastName = wallet.LastName;
            FullName = wallet.FullName;
            SupportedBankId = wallet.SupportedBankId;
            SupportedBankName = wallet.SupportedBankName;
        }

        public WalletVM()
        {
        }

        public int Id { get; private set; }
        public string UniqueMasterCitizenNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int SupportedBankId { get; set; }
        public string SupportedBankName { get; set; }
        public string Password { get; set; }
    }

    public static partial class WalletExtensionMethods
    {
        public static IEnumerable<WalletVM> ToWalletVMs(this IEnumerable<WalletDTO> wallets)
        {
            return wallets.Select(wallet => new WalletVM(wallet));
        }
    }
}