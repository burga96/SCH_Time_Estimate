using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Applications.WebClient.Helpers;
using Applications.WebClient.Models.ViewModel;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Applications.WebClient.Controllers
{
    public class WalletsController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ISupportedBankService _supportedBankService;
        private readonly string _adminPass;
        private const int PageSize = 10;

        public WalletsController(IWalletService walletService,
            ISupportedBankService supportedBankService,
            IConfiguration configuration)
        {
            _adminPass = configuration["Wallet:AdminPass"];
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
                IEnumerable<SupportedBankVM> supportedBankVMs = (await _supportedBankService.GetAllSupportedBanksAsync()).ToSupportedBankVMs();
                var pageVM = new UpsertWalletVM(wallet, supportedBankVMs);
                ViewData["Error"] = e.Message;
                return View(pageVM);
            }
        }

        public async Task<IActionResult> Index(string adminPass,
            string filter = null,
            string columnName = null,
            bool isDescending = false,
            int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Must be a positive integer.");
            }
            if (adminPass != _adminPass)
            {
                ViewData["AdminPasswordError"] = string.IsNullOrEmpty(adminPass) ? "" : "Wrong admin password";
                ViewBag.Filter = filter;
                ViewBag.ColumnName = columnName;
                ViewBag.PageNumber = pageNumber;
                ViewBag.IsDescendingString = isDescending.ToString().ToLower();
                ViewBag.PageCount = 0;
                ViewBag.AdminPassword = adminPass;
                return View(new List<WalletVM>());
            }
            OrderBySettings<Wallet> orderBySettings;
            if (string.IsNullOrEmpty(columnName))
            {
                orderBySettings = null;
            }
            else
            {
                Expression<Func<Wallet, object>> orderByExpression = columnName switch
                {
                    "FirstName" => _wallet => _wallet.PersonalData.FirstName,
                    "LastName" => _wallet => _wallet.PersonalData.LastName,

                    _ => throw new NotImplementedException($"{nameof(columnName)} = {columnName}"),
                };
                orderBySettings = new OrderBySettings<Wallet>(orderByExpression, !isDescending);
            }
            int skip = (pageNumber - 1) * PageSize;

            var resultsAndTotalCountSupportedBanks = await _walletService.GetResultAndTotalCountWalletsAsync(filter, orderBySettings, skip, PageSize);
            int pageCount = Utils.CalculatePageCount(resultsAndTotalCountSupportedBanks.TotalCount, PageSize);

            ViewBag.Filter = filter;
            ViewBag.ColumnName = columnName;
            ViewBag.PageNumber = pageNumber;
            ViewBag.IsDescendingString = isDescending.ToString().ToLower();
            ViewBag.PageCount = pageCount;
            ViewBag.AdminPassword = adminPass;
            IEnumerable<WalletVM> walletVMs = resultsAndTotalCountSupportedBanks.Results.ToWalletVMs();
            return View(walletVMs);
        }
    }
}