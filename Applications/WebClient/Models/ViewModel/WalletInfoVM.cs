using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class WalletInfoVM : AuthenticationVM
    {
        public WalletInfoVM()
        {
        }

        public WalletInfoVM(string uniqueMasterCitizenNumber, string password, string error, bool authenticated, WalletVM wallet) : base(uniqueMasterCitizenNumber, password, error, authenticated)
        {
            Wallet = wallet;
        }

        public WalletVM Wallet { get; set; }
    }
}