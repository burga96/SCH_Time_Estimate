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
    public class PaymentTransactionsController : Controller
    {
        private readonly IPaymentTransactionService _paymentTransactionService;
        private readonly IWalletService _walletService;

        public PaymentTransactionsController(IPaymentTransactionService paymentTransactionService,
            IWalletService walletService)
        {
            _walletService = walletService;
            _paymentTransactionService = paymentTransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> MakeDeposit(string password, string uniqueMasterCitizenNumber)
        {
            MakeDepositPaymentTransactionVM makeDepositPaymentTransactionVM;
            try
            {
                WalletDTO wallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, password);

                makeDepositPaymentTransactionVM = new MakeDepositPaymentTransactionVM(wallet.CurrentAmount, uniqueMasterCitizenNumber, password, "", true);
                return View(makeDepositPaymentTransactionVM);
            }
            catch (Exception)
            {
                makeDepositPaymentTransactionVM = new MakeDepositPaymentTransactionVM(0, uniqueMasterCitizenNumber, password, "Enter valid unique master citizen number and password", false);
                return View(makeDepositPaymentTransactionVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeDeposit(MakeDepositPaymentTransactionVM makeDepositPaymentTransaction)
        {
            try
            {
                await _paymentTransactionService.MakeDepositPaymentTransaction(makeDepositPaymentTransaction.UniqueMasterCitizenNumber, makeDepositPaymentTransaction.Password, makeDepositPaymentTransaction.Amount);
                return View();
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(makeDepositPaymentTransaction);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MakeWithdrawal(string password, string uniqueMasterCitizenNumber)
        {
            MakeWithdrawalPaymentTransactionVM makeWithdrawalPaymentTransactionVM;
            try
            {
                WalletDTO wallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, password);

                makeWithdrawalPaymentTransactionVM = new MakeWithdrawalPaymentTransactionVM(wallet.CurrentAmount, uniqueMasterCitizenNumber, password, "", true);
                return View(makeWithdrawalPaymentTransactionVM);
            }
            catch (Exception)
            {
                makeWithdrawalPaymentTransactionVM = new MakeWithdrawalPaymentTransactionVM(0, uniqueMasterCitizenNumber, password, "Enter valid unique master citizen number and password", false);
                return View(makeWithdrawalPaymentTransactionVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeWithdrawal(MakeWithdrawalPaymentTransactionVM withdrawalPaymentTransaction)
        {
            try
            {
                await _paymentTransactionService.MakeWithdrawalPaymentTransaction(withdrawalPaymentTransaction.UniqueMasterCitizenNumber, withdrawalPaymentTransaction.Password, withdrawalPaymentTransaction.Amount);

                return View();
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(withdrawalPaymentTransaction);
            }
        }
    }
}