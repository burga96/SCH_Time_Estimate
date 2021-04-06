using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.PaymentTransactionService
{
    public partial class PaymentTransactionServiceTests
    {
        [DataRow(10.0)]
        [DataRow(20.0)]
        [DataRow(30.0)]
        public async Task NotEnoughAmountExceptionOnMakingWithdrawalPaymentTransaction(double d)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal withdrawalAmount = (decimal)d;
            WalletDTO wallet = await ArrangeWallet();
            //Act & Assert
            ExceptionAssert.Throws<NotEnoughAmountException>(() =>
               _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, withdrawalAmount).Wait()
            );
        }

        [DataRow(10.0, 10.0)]
        [DataRow(20.0, 10.0)]
        [DataRow(30.0, 20.0)]
        [DataRow(40.0, 30.0)]
        [DataTestMethod]
        public async Task WalletPropertiesAfterMakeTwoPaymentTransaction(double d, double w)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount = (decimal)d;
            decimal withdrawalAmount = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();
            //Act
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, withdrawalAmount);

            //Assert
            WalletDTO wallet2 = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, wallet.Password);
            Assert.AreEqual(wallet2.CurrentAmount, depositAmount - withdrawalAmount);
            Assert.AreEqual(wallet2.PaymentTransactions.Count(), 2);
        }

        //ArgumentException

        [DataRow("2108997781057", "061111", 10.0)]
        [DataRow("2108997781057", "061211", 20.0)]
        [DataRow("2108996781057", "061112", 30.0)]
        [DataTestMethod]
        public void WalletNotFoundArgumentExceptionOnMakingWithdrawalPaymentTransactions(string uniqueMasterCitizenNumber, string password, double d)
        {
            //Arange
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() =>
               _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, password, amount).Wait()
            );
        }

        [DataRow("061111", 10.0)]
        [DataRow("061211", 20.0)]
        [DataRow("061112", 30.0)]
        [DataTestMethod]
        public async Task WrongPasswordExceptionOnMakingWithdrawalPaymentTransactions(string password, double w)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)w;

            //Act & Assert
            ExceptionAssert.Throws<WrongPasswordException>(() =>
               _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, password, amount).Wait()
            );
        }

        [DataRow(10.0)]
        [DataRow(20.0)]
        [DataRow(30.0)]
        [DataRow(40.0)]
        [DataTestMethod]
        public async Task MakeWithdrawalPaymentTransactionsWhenWalletIsBlocked(double d)
        {
            //Aramge
            decimal amount = (decimal)d;
            WalletDTO wallet = await ArrangeWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount);
            await _walletService.BlockWallet(wallet.Id);
            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() =>
              _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount).Wait()
           );
        }

        [DataRow(15.0)]
        [DataRow(25.0)]
        [DataRow(35.0)]
        [DataRow(45.0)]
        [DataRow(55.0)]
        [DataRow(65.0)]
        [DataTestMethod]
        public async Task BankAPIExceptionOnMakingWithdrawalPaymentTransactions(double w)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)w;

            //Act & Assert
            ExceptionAssert.Throws<BankAPIException>(() =>
               _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount).Wait()
            );
        }

        [DataRow()]
        [DataTestMethod]
        public async Task LimitExceededOnMakingWithdrawalPaymentTransaction()
        {
            //Arrange
            decimal amount1 = 500000.0m;
            decimal amount2 = 200000.0m;
            decimal amount3 = 200000.0m;
            decimal amount4 = 200000.0m;
            WalletDTO wallet = await ArrangeWallet();

            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount2);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount3);

            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount1);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount3);

            //Act & Assert
            ExceptionAssert.Throws<LimitExceededException>(() =>
               _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount4).Wait()
            );
        }
    }
}