using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        public async Task PaymentTransactionsCountWithFromAndToDateByTwoWallets(double d1, double d2, double w)
        {
            //Aramge
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();

            DateTime from = DateTime.Now;
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, withdrawal);
            DateTime to = DateTime.Now;
            //Act
            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService.GetAllPaymentTransactions(from, to);

            //Assert
            Assert.AreEqual(paymentTransactionDTOs.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithFromDateByTwoWallets(double d1, double d2, double w)
        {
            //Aramge
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();

            DateTime from = DateTime.Now;
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, withdrawal);
            //Act
            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService.GetAllPaymentTransactions(from, null);

            //Assert
            Assert.AreEqual(paymentTransactionDTOs.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithToDateByTwoWallets(double d1, double d2, double w)
        {
            //Aramge
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();

            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, withdrawal);
            DateTime to = DateTime.Now;
            //Act
            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService.GetAllPaymentTransactions(null, to);

            //Assert
            Assert.AreEqual(paymentTransactionDTOs.Count(), 3);
        }

        [DataRow(10.0, 20.0, 10.0)]
        [DataRow(20.0, 10.0, 10.0)]
        [DataRow(30.0, 20.0, 10.0)]
        [DataRow(40.0, 30.0, 10.0)]
        [DataTestMethod]
        public async Task PaymentTransactionsCountWithoutFromAndToDateByTwoWallets(double d1, double d2, double w)
        {
            //Aramge
            decimal depositAmount1 = (decimal)d1;
            decimal depositAmount2 = (decimal)d2;
            decimal withdrawal = (decimal)w;
            WalletDTO wallet = await ArrangeWallet();

            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount1);
            await _paymentTransactionService.MakeDepositPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, depositAmount2);
            await _paymentTransactionService.MakeWithdrawalPaymentTransaction(wallet.UniqueMasterCitizenNumber, wallet.Password, withdrawal);
            //Act
            IEnumerable<PaymentTransactionDTO> paymentTransactionDTOs = await _paymentTransactionService.GetAllPaymentTransactions(null, null);

            //Assert
            Assert.AreEqual(paymentTransactionDTOs.Count(), 3);
        }
    }
}