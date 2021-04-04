using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Applications.WebClient.Helpers;
using Applications.WebClient.Models.ViewModel;
using Core.ApplicationServices.ApplicationDTOs;
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
        private readonly string _adminPassword;
        private const int PageSize = 10;

        public WalletsController(IWalletService walletService,
            ISupportedBankService supportedBankService,
            IConfiguration configuration)
        {
            _adminPassword = configuration["Wallet:AdminPassword"];
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
                WalletDTO createdWallet = await _walletService.CreateNewWallet(wallet.UniqueMasterCitizenNumber,
                    wallet.PostalIndexNumber,
                    wallet.SupportedBankId,
                    wallet.FirstName,
                    wallet.LastName);
                IEnumerable<SupportedBankVM> supportedBankVMs = (await _supportedBankService.GetAllSupportedBanksAsync()).ToSupportedBankVMs();
                var pageVM = new UpsertWalletVM(wallet, supportedBankVMs);
                ViewData["Success"] = $"Your password is {createdWallet.Password}";
                return View(pageVM);
            }
            catch (Exception e)
            {
                IEnumerable<SupportedBankVM> supportedBankVMs = (await _supportedBankService.GetAllSupportedBanksAsync()).ToSupportedBankVMs();
                var pageVM = new UpsertWalletVM(wallet, supportedBankVMs);
                ViewData["Error"] = e.Message;
                return View(pageVM);
            }
        }

        public async Task<IActionResult> Index(string password,
            string filter = null,
            string columnName = null,
            bool isDescending = false,
            int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Must be a positive integer.");
            }

            if (password != _adminPassword)
            {
                ViewData["AdminPasswordError"] = string.IsNullOrEmpty(password) ? "" : "Wrong admin password";
                ViewBag.Filter = filter;
                ViewBag.ColumnName = columnName;
                ViewBag.PageNumber = pageNumber;
                ViewBag.IsDescendingString = isDescending.ToString().ToLower();
                ViewBag.PageCount = 0;
                ViewBag.HasAdminPassword = false;
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
                    "First name" => _wallet => _wallet.PersonalData.FirstName,
                    "Last name" => _wallet => _wallet.PersonalData.LastName,
                    "Unique Master Citizen Number" => _wallet => _wallet.UniqueMasterCitizenNumber.Value,
                    "Supported bank" => _wallet => _wallet.SupportedBank.Name,
                    "Current amount" => _wallet => _wallet.CurrentAmount,

                    _ => throw new NotImplementedException($"{nameof(columnName)} = {columnName}"),
                };
                orderBySettings = new OrderBySettings<Wallet>(orderByExpression, !isDescending);
            }
            int skip = (pageNumber - 1) * PageSize;

            var resultsAndTotalCountSupportedBanks = await _walletService
                .GetResultAndTotalCountWalletsAsync(filter,
                    orderBySettings,
                    skip,
                    PageSize
                );

            int pageCount = Utils.CalculatePageCount(resultsAndTotalCountSupportedBanks.TotalCount, PageSize);

            ViewBag.Filter = filter;
            ViewBag.ColumnName = columnName;
            ViewBag.PageNumber = pageNumber;
            ViewBag.IsDescendingString = isDescending.ToString().ToLower();
            ViewBag.PageCount = pageCount;
            ViewBag.AdminPassword = password;
            ViewBag.HasAdminPassword = true;
            IEnumerable<WalletVM> walletVMs = resultsAndTotalCountSupportedBanks.Results.ToWalletVMs();
            return View(walletVMs);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string password, string uniqueMasterCitizenNumber)
        {
            ChangePasswordVM changePasswordVM;
            try
            {
                WalletDTO wallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, password);

                changePasswordVM = new ChangePasswordVM(uniqueMasterCitizenNumber, password, "", true, "");
                return View(changePasswordVM);
            }
            catch (Exception)
            {
                changePasswordVM = new ChangePasswordVM(uniqueMasterCitizenNumber, password, "Enter valid unique master citizen number and password", false, "");
                return View(changePasswordVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Info(string password, string uniqueMasterCitizenNumber)
        {
            WalletInfoVM walletInfo;
            try
            {
                WalletDTO walletDTO = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, password);
                var walletVM = new WalletVM(walletDTO);
                walletInfo = new WalletInfoVM(uniqueMasterCitizenNumber, password, "", true, walletVM);
                return View(walletInfo);
            }
            catch (Exception)
            {
                walletInfo = new WalletInfoVM(uniqueMasterCitizenNumber, password, "Enter valid unique master citizen number and password", false, new WalletVM());
                return View(walletInfo);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
        {
            try
            {
                await _walletService.ChangePassword(changePassword.UniqueMasterCitizenNumber, changePassword.Password, changePassword.NewPassword);
                ViewData["Success"] = "Successfully changed password";
                return View(changePassword);
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(changePassword);
            }
        }
    }
}