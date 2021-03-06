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
        private readonly string _adminPassword;

        public PaymentTransactionsController(IPaymentTransactionService paymentTransactionService,
            IWalletService walletService,
            IConfiguration configuration)
        {
            _adminPassword = configuration["Wallet:AdminPassword"];
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
                return RedirectToAction(nameof(MyPaymentTransactions),
                    new { password = makeDepositPaymentTransaction.Password, uniqueMasterCitizenNumber = makeDepositPaymentTransaction.UniqueMasterCitizenNumber });
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

                return RedirectToAction(nameof(MyPaymentTransactions),
                    new { password = withdrawalPaymentTransaction.Password, uniqueMasterCitizenNumber = withdrawalPaymentTransaction.UniqueMasterCitizenNumber });
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(withdrawalPaymentTransaction);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MakeInternal(string password, string uniqueMasterCitizenNumber)
        {
            MakeInternalPaymentTransactionVM makeInternalPaymentTransactionVM;
            try
            {
                WalletDTO wallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, password);

                makeInternalPaymentTransactionVM = new MakeInternalPaymentTransactionVM(wallet.CurrentAmount, uniqueMasterCitizenNumber, password, "", true, "");
                return View(makeInternalPaymentTransactionVM);
            }
            catch (Exception)
            {
                makeInternalPaymentTransactionVM = new MakeInternalPaymentTransactionVM(0, uniqueMasterCitizenNumber, password, "Enter valid unique master citizen number and password", false, "");
                return View(makeInternalPaymentTransactionVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeInternal(MakeInternalPaymentTransactionVM makeInternalPaymentTransaction)
        {
            try
            {
                await _paymentTransactionService.MakeInternalTransferPaymentTransaction(makeInternalPaymentTransaction.UniqueMasterCitizenNumber,
                    makeInternalPaymentTransaction.Password, makeInternalPaymentTransaction.ToUniqueMasterCitizenNumber, makeInternalPaymentTransaction.Amount);

                return RedirectToAction(nameof(MyPaymentTransactions),
                    new { password = makeInternalPaymentTransaction.Password, uniqueMasterCitizenNumber = makeInternalPaymentTransaction.UniqueMasterCitizenNumber });
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(makeInternalPaymentTransaction);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyPaymentTransactions(string password,
            string uniqueMasterCitizenNumber)
        {
            MyPaymentTransactionsVM myPaymentTransactionsVM;
            try
            {
                WalletDTO wallet = await _paymentTransactionService.GetWalletWithFiltertedPaymentTransactionsForUser(uniqueMasterCitizenNumber, password, null, null);

                myPaymentTransactionsVM = new MyPaymentTransactionsVM(wallet.CurrentAmount,
                    uniqueMasterCitizenNumber,
                    password,
                    "",
                    true,
                    wallet.PaymentTransactions.ToPaymentTransactionVMs(),
                    null,
                    null)
                    ;
                return View(myPaymentTransactionsVM);
            }
            catch (Exception)
            {
                myPaymentTransactionsVM = new MyPaymentTransactionsVM(0,
                    uniqueMasterCitizenNumber,
                    password,
                    "Enter valid unique master citizen number and password",
                    false,
                    new List<PaymentTransactionVM>(),
                    null,
                    null);
                return View(myPaymentTransactionsVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MyPaymentTransactions(MyPaymentTransactionsVM myPaymentTransaction)
        {
            try
            {
                WalletDTO wallet = await _paymentTransactionService
                    .GetWalletWithFiltertedPaymentTransactionsForUser(myPaymentTransaction.UniqueMasterCitizenNumber,
                    myPaymentTransaction.Password,
                    myPaymentTransaction.From,
                    myPaymentTransaction.To);

                var viewModel = new MyPaymentTransactionsVM(wallet.CurrentAmount,
                   myPaymentTransaction.UniqueMasterCitizenNumber,
                   myPaymentTransaction.Password,
                   "",
                   true,
                   wallet.PaymentTransactions.ToPaymentTransactionVMs(),
                   myPaymentTransaction.From,
                    myPaymentTransaction.To);
                return View(viewModel);
            }
            catch (Exception)
            {
                var viewModel = new MyPaymentTransactionsVM(0,
                    myPaymentTransaction.UniqueMasterCitizenNumber,
                    myPaymentTransaction.Password,
                    "Enter valid unique master citizen number and password",
                    false,
                    new List<PaymentTransactionVM>(),
                    null,
                    null);
                return View(viewModel);
            }
        }

        public async Task<IActionResult> GetPaymentTransactionsByAdmin(string password,
            DateTime? from,
            DateTime? to)
        {
            PaymentTransactionsByAdminVM paymentTransactionsByAdmin;
            if (password != _adminPassword)
            {
                paymentTransactionsByAdmin = new PaymentTransactionsByAdminVM(password, "Enter valid admin password", false, new List<PaymentTransactionVM>(), from, to);
                return View(paymentTransactionsByAdmin);
            }

            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService
                .GetAllPaymentTransactions(from, to);

            IEnumerable<PaymentTransactionVM> paymentTransactionVMs = paymentTransactionDTOs.ToPaymentTransactionVMs();
            paymentTransactionsByAdmin = new PaymentTransactionsByAdminVM(password, "", true, paymentTransactionVMs, from, to);
            return View(paymentTransactionsByAdmin);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaymentTransactionsByAdmin(PaymentTransactionsByAdminVM paymentTransactionsByAdmin)
        {
            if (paymentTransactionsByAdmin.Password != _adminPassword)
            {
                paymentTransactionsByAdmin.PaymentTransactions = new List<PaymentTransactionVM>();
                paymentTransactionsByAdmin.Error = string.IsNullOrEmpty(paymentTransactionsByAdmin.Password) ? "" : "Enter valid admin password";
                paymentTransactionsByAdmin.HasPassword = false;
                return View(paymentTransactionsByAdmin);
            }
            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService
                .GetAllPaymentTransactions(paymentTransactionsByAdmin.From, paymentTransactionsByAdmin.To);

            IEnumerable<PaymentTransactionVM> paymentTransactionVMs = paymentTransactionDTOs.ToPaymentTransactionVMs();
            paymentTransactionsByAdmin.PaymentTransactions = paymentTransactionVMs;
            paymentTransactionsByAdmin.HasPassword = true;
            return View(paymentTransactionsByAdmin);
        }
    }
}