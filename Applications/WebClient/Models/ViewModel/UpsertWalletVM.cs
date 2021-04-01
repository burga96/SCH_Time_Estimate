using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Applications.WebClient.Models.ViewModel
{
    public class UpsertWalletVM
    {
        public UpsertWalletVM()
        {
        }

        public UpsertWalletVM(WalletVM wallet, IEnumerable<SupportedBankVM> supportedBanks)
        {
            Wallet = wallet;
            SupportedBanks = supportedBanks;
        }

        public WalletVM Wallet { get; set; }
        public IEnumerable<SupportedBankVM> SupportedBanks { get; set; }

        public IEnumerable<SelectListItem> SupportedBanksSelect
        {
            get
            {
                return SupportedBanks.Select(SupportedBank => new SelectListItem { Value = SupportedBank.Id.ToString(), Text = SupportedBank.Name });
            }
        }
    }
}