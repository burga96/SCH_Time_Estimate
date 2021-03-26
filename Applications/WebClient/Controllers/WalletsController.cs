using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.WebClient.Models.ViewModel;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Applications.WebClient.Controllers
{
    public class WalletsController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ISupportedBankService _supportedBankService;

        public WalletsController(IWalletService walletService, ISupportedBankService supportedBankService)
        {
            _walletService = walletService;
            _supportedBankService = supportedBankService;
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<SupportedBankVM> supportedBankVMs = (await _supportedBankService.GetAllSupportedBanksAsync()).ToSupportedBankVMs();
            WalletVM emptyWallet = new WalletVM();
            var pageVM = new UpsertWalletVM(emptyWallet, supportedBankVMs);
            return View(pageVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WalletVM wallet)
        {
            try
            {
                await _walletService.CreateNewWallet(wallet.UniqueMasterCitizenNumber,
                    wallet.PostalIndexNumber,
                    wallet.SupportedBankId,
                    wallet.FirstName,
                    wallet.LastName);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}