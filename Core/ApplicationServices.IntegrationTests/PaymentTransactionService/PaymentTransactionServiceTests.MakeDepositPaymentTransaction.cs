using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Entities.Enums;
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
        [DataRow(40.0)]
        [DataTestMethod]
        public async Task PropertiesAfterMakeDepositPaymentTransaction(double d)
        {
            //Aramge
            decimal depositAmount = (decimal)d;
            WalletDTO wallet = await ArrangeWallet();
            //Act
            DepositPaymentTransactionDTO depositPaymentTransactionDTO = await _paymentTransactionService.MakeDepositPaymentTransaction("2108996781057", wallet.Password, depositAmount);
            //Assert
            Assert.AreEqual(depositPaymentTransactionDTO.Amount, depositAmount);
            Assert.AreEqual(depositPaymentTransactionDTO.Type, PaymentTransactionType.DEPOSIT);
            Assert.AreEqual(depositPaymentTransactionDTO.WalletId, wallet.Id);
        }

        [DataRow(10.0, 10.0)]
        [DataRow(20.0, 10.0)]
        [DataRow(30.0, 20.0)]
        [DataRow(40.0, 30.0)]
        [DataTestMethod]
        public async Task WalletPropertiesAfterMakeTwoDepositPaymentTransaction(double d1, double d2)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            WalletDTO wallet = await ArrangeWallet();
            //Act
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount2);

            //Assert
            WalletDTO wallet2 = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(uniqueMasterCitizenNumber, wallet.Password);
            Assert.AreEqual(wallet2.CurrentAmount, depositAmount1 + depositAmount2);
            Assert.AreEqual(wallet2.PaymentTransactions.Count(), 2);
        }

        [DataRow(10.0)]
        [DataRow(20.0)]
        [DataRow(30.0)]
        [DataRow(40.0)]
        [DataTestMethod]
        public async Task MakeDepositPaymentTransactionsWhenWalletIsBlocked(double d)
        {
            //Aramge
            decimal amount = (decimal)d;
            WalletDTO wallet = await ArrangeWallet();
            await _walletService.BlockWallet(wallet.Id);
            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() =>
              _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount).Wait()
           );
        }

        //ArgumentException

        [DataRow("2108997781057", "061111", 10.0)]
        [DataRow("2108997781057", "061211", 20.0)]
        [DataRow("2108996781057", "061112", 30.0)]
        [DataTestMethod]
        public void WalletNotFoundArgumentExceptionOnMakingDepositPaymentTransactions(string uniqueMasterCitizenNumber, string password, double d)
        {
            //Arange
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() =>
               _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, password, amount).Wait()
            );
        }

        [DataRow("061111", 10.0)]
        [DataRow("061211", 20.0)]
        [DataRow("061112", 30.0)]
        [DataTestMethod]
        public async Task WrongPasswordExceptionOnMakingDepositPaymentTransactions(string password, double d)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<WrongPasswordException>(() =>
               _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, password, amount).Wait()
            );
        }

        [DataRow(15.0)]
        [DataRow(25.0)]
        [DataRow(35.0)]
        [DataRow(45.0)]
        [DataRow(55.0)]
        [DataRow(65.0)]
        [DataTestMethod]
        public async Task BankAPIExceptionOnMakingDepositPaymentTransactions(double d)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)d;

            //Act & Assert
            ExceptionAssert.Throws<BankAPIException>(() =>
               _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount).Wait()
            );
        }

        [TestMethod]
        public async Task LimitExceededOnMakingDepoistPaymentTransaction()
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

            //Act & Assert
            ExceptionAssert.Throws<LimitExceededException>(() =>
               _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, amount4).Wait()
            );
        }
    }
}