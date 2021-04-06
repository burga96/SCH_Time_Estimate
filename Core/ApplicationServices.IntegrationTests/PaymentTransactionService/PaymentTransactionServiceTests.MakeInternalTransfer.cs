using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Entities.Enums;
using Core.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.PaymentTransactionService
{
    public partial class PaymentTransactionServiceTests
    {
        [DataRow(100.0, 10.0)]
        [DataRow(100.0, 20.0)]
        [DataRow(100.0, 30.0)]
        [DataRow(100.0, 40.0)]
        [DataRow(100.0, 50.0)]
        [DataTestMethod]
        public async Task PropertiesAfterMakeInternalTransfer(double d1, double it1)
        {
            //Arrange
            decimal depositAmount = (decimal)d1;
            decimal internalTransferAmount = (decimal)it1;
            WalletDTO wallet = await ArrangeWallet();
            WalletDTO secondWallet = await ArrangeSecondWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                wallet.Password,
                depositAmount);

            //Act
            InternalTransferPaymentTransactionsDTO paymentTransactions = await _paymentTransactionService
                .MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                    wallet.Password,
                    secondWallet.UniqueMasterCitizenNumber,
                    internalTransferAmount);

            //Assert
            Assert.AreEqual(paymentTransactions.Deposit.Amount, internalTransferAmount);
            Assert.AreEqual(paymentTransactions.Withdrawal.Amount, internalTransferAmount);
            Assert.AreEqual(paymentTransactions.Fee.Amount, 0);
            WalletDTO assertWallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(wallet.UniqueMasterCitizenNumber, wallet.Password);
            Assert.AreEqual(assertWallet.CurrentAmount, depositAmount - internalTransferAmount);
            WalletDTO assertSecondWallet = await _walletService.GetWalletByUniqueMasterCitizenNumberAndPassword(secondWallet.UniqueMasterCitizenNumber, secondWallet.Password);
            Assert.AreEqual(assertSecondWallet.CurrentAmount, internalTransferAmount);
            Assert.AreEqual(assertWallet.PaymentTransactions.Count(), 2);
            Assert.AreEqual(assertSecondWallet.PaymentTransactions.Count(), 1);
        }

        [DataRow(100.0, 10.0)]
        [DataRow(100.0, 20.0)]
        [DataRow(100.0, 30.0)]
        [DataRow(100.0, 40.0)]
        [DataRow(100.0, 50.0)]
        [DataTestMethod]
        public async Task MakeInternalTransactionWhenFirstWalletIsBlocked(double d1, double it1)
        {
            //Arrange
            decimal depositAmount = (decimal)d1;
            decimal internalTransferAmount = (decimal)it1;
            WalletDTO wallet = await ArrangeWallet();
            WalletDTO secondWallet = await ArrangeSecondWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                wallet.Password,
                depositAmount);
            await _walletService.BlockWallet(wallet.Id);

            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() =>
                _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                    wallet.Password,
                    secondWallet.UniqueMasterCitizenNumber,
                    internalTransferAmount)
                .Wait()
            );
        }

        [DataRow(100.0, 10.0)]
        [DataRow(100.0, 20.0)]
        [DataRow(100.0, 30.0)]
        [DataRow(100.0, 40.0)]
        [DataRow(100.0, 50.0)]
        [DataTestMethod]
        public async Task MakeInternalTransactionWhenSecondWalletIsBlocked(double d1, double it1)
        {
            //Arrange
            decimal depositAmount = (decimal)d1;
            decimal internalTransferAmount = (decimal)it1;
            WalletDTO wallet = await ArrangeWallet();
            WalletDTO secondWallet = await ArrangeSecondWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                wallet.Password,
                depositAmount);
            await _walletService.BlockWallet(secondWallet.Id);

            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() =>
                _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                    wallet.Password,
                    secondWallet.UniqueMasterCitizenNumber,
                    internalTransferAmount)
                .Wait()
            );
        }

        //ArgumentException

        [DataRow("2108997781057", "061111", 10.0)]
        [DataRow("2108997781057", "061211", 20.0)]
        [DataRow("2108996781057", "061112", 30.0)]
        [DataTestMethod]
        public void FirstWalletNotFoundArgumentExceptionOnMakingInternalTransfer(string uniqueMasterCitizenNumber, string password, double d)
        {
            //Arange
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() =>
               _paymentTransactionService.MakeInternalTransferPaymentTransaction(uniqueMasterCitizenNumber, password, "", amount).Wait()
            );
        }

        [DataRow("2108997781057", 10.0)]
        [DataRow("2108997781057", 20.0)]
        [DataRow("2108916781057", 30.0)]
        [DataTestMethod]
        public async Task SecondWalletNotFoundArgumentExceptionOnMakingInternalTransfer(string uniqueMasterCitizenNumber, double d)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() =>
               _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, uniqueMasterCitizenNumber, amount).Wait()
            );
        }

        [DataRow("061111", 10.0)]
        [DataRow("061211", 20.0)]
        [DataRow("061112", 30.0)]
        [DataTestMethod]
        public async Task WrongPasswordExceptionOnMakingInternalTransfer(string password, double d)
        {
            //Arange
            WalletDTO wallet = await ArrangeWallet();
            decimal amount = (decimal)d;
            //Act & Assert
            ExceptionAssert.Throws<WrongPasswordException>(() =>
               _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber, password, "", amount).Wait()
            );
        }

        [DataRow(15.0)]
        [DataRow(25.0)]
        [DataRow(35.0)]
        [DataRow(45.0)]
        [DataRow(55.0)]
        [DataRow(65.0)]
        [DataTestMethod]
        public async Task BankAPIExceptionOnMakingInternalTransfer(double i)
        {
            //Arrange

            WalletDTO wallet = await ArrangeWallet();
            WalletDTO secondWallet = await ArrangeSecondWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                wallet.Password,
                100);

            decimal internalTransferAmount = (decimal)i;
            //Act & Assert
            ExceptionAssert.Throws<BankAPIException>(() =>
               _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                    wallet.Password,
                    secondWallet.UniqueMasterCitizenNumber,
                    internalTransferAmount)
               .Wait()
            );
        }

        [TestMethod]
        public async Task DepositLimitExceededOnMakingInternalTransferPaymentTransaction()
        {
            //Arrange
            decimal amount1 = 500000.0m;
            decimal amount2 = 200000.0m;
            decimal amount3 = 200000.0m;
            decimal amount4 = 200000.0m;
            WalletDTO wallet = await ArrangeWallet();
            WalletDTO secondWallet = await ArrangeSecondWallet();

            await _paymentTransactionService.MakeDepositPaymentTransaction(secondWallet.UniqueMasterCitizenNumber, secondWallet.Password, amount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(secondWallet.UniqueMasterCitizenNumber, secondWallet.Password, amount2);
            await _paymentTransactionService.MakeDepositPaymentTransaction(secondWallet.UniqueMasterCitizenNumber, secondWallet.Password, amount3);

            //Act & Assert
            ExceptionAssert.Throws<LimitExceededException>(() =>
               _paymentTransactionService.MakeInternalTransferPaymentTransaction(wallet.UniqueMasterCitizenNumber,
                    wallet.Password,
                    secondWallet.UniqueMasterCitizenNumber,
                    amount4)
               .Wait()
            );
        }
    }
}