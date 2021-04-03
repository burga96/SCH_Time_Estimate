using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.PaymentTransactionService
{
    public partial class PaymentTransactionServiceTests
    {
        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithFromAndToDate(double d1, double d2, double w)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();
            DateTime from = DateTime.Now;
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, withdrawal);
            DateTime to = DateTime.Now;
            //Act
            WalletDTO actWallet = await _paymentTransactionService.GetWalletWithFiltertedPaymentTransactionsForUser(uniqueMasterCitizenNumber, wallet.Password, from, to);

            //Assert
            Assert.AreEqual(actWallet.PaymentTransactions.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithFromDate(double d1, double d2, double w)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();
            DateTime from = DateTime.Now;
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            //Act
            WalletDTO actWallet = await _paymentTransactionService.GetWalletWithFiltertedPaymentTransactionsForUser(uniqueMasterCitizenNumber, wallet.Password, from, null);
            //Assert
            Assert.AreEqual(actWallet.PaymentTransactions.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithToDate(double d1, double d2, double w)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            DateTime to = DateTime.Now;

            //Act
            WalletDTO actWallet = await _paymentTransactionService.GetWalletWithFiltertedPaymentTransactionsForUser(uniqueMasterCitizenNumber, wallet.Password, null, to);
            //Assert
            Assert.AreEqual(actWallet.PaymentTransactions.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithoutDate(double d1, double d2, double w)
        {
            //Aramge
            string uniqueMasterCitizenNumber = "2108996781057";
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(uniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            //Act
            WalletDTO actWallet = await _paymentTransactionService.GetWalletWithFiltertedPaymentTransactionsForUser(uniqueMasterCitizenNumber, wallet.Password, null, null);
            //Assert
            Assert.AreEqual(actWallet.PaymentTransactions.Count(), 3);
        }
    }
}