using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.ApplicationServices.ApplicationServices;
using Core.ApplicationServices.ExternalInterfaces;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.RepositoryInterfaces;
using Core.Infrastructure.DataAccess.Contexts;
using Core.Infrastructure.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.WalletsService
{
    public partial class WalletServiceTests
    {
        [DataRow("2108996781057", "0612", "Stefan", "Burgic", 1)]
        [DataRow("2008996781057", "0613", "Vukoman", "Stojanovic", 1)]
        [DataRow("1908996781057", "1213", "Branko", "Djakovic", 1)]
        [DataTestMethod]
        public async Task PropertiesAfterCreateWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Act
            WalletDTO wallet = await _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName);
            //Assert
            Assert.IsTrue(wallet.SupportedBankId == supportedBankId);
            Assert.IsTrue(wallet.UniqueMasterCitizenNumber == uniqueMasterCitizenNumber);
            Assert.IsTrue(wallet.FirstName == firstName);
            Assert.IsTrue(wallet.LastName == lastName);
            Assert.IsTrue(wallet.PostalIndexNumber == postalIndexNumber);
        }

        [DataRow("2108996781057", "0612", "Stefan", "Burgic", 10)]
        [DataRow("2008996781057", "0613", "Vukoman", "Stojanovic", 10)]
        [DataRow("1908996781057", "1213", "Branko", "Djakovic", 10)]
        [DataTestMethod]
        public void SupportedBankNotFoundExceptionOnCreatingWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Act && Assert
            ExceptionAssert.Throws<ArgumentException>(() => _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName).Wait());
        }

        [DataRow("2108996781057", "0612", "Stefan", "Burgic", 2)]
        [DataRow("2008996781057", "0613", "Vukoman", "Stojanovic", 2)]
        [DataRow("1908996781057", "1213", "Branko", "Djakovic", 2)]
        [DataTestMethod]
        public void NotFoundBankAPIExceptionOnCreatingWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Act && Assert
            ExceptionAssert.Throws<NotFoundBankAPIException>(() => _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName).Wait());
        }

        [DataRow("2108004781057", "0612", "Stefan", "Burgic", 1)]
        [DataTestMethod]
        public void NotValidUniqueMasterCitizenNumberExceptionOnCreatingWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Act && Assert
            ExceptionAssert.Throws<NotValidUniqueMasterCitizenNumberException>(() => _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName).Wait());
        }

        [DataRow("2108996781057", "0610", "Stefan", "Burgic", 1)]
        [DataRow("2108996781051", "0612", "Stefan", "Burgic", 1)]
        [DataTestMethod]
        public void NotValidStatusOnCreatingWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Act && Assert
            ExceptionAssert.Throws<BankAPIException>(() => _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName).Wait());
        }

        [DataRow("2108996781057", "0612", "Stefan", "Burgic", 1)]
        [DataRow("2008996781057", "0613", "Vukoman", "Stojanovic", 1)]
        [DataRow("1908996781057", "1213", "Branko", "Djakovic", 1)]
        [DataTestMethod]
        public async Task ExistingWalletExceptionOnCreatingWallet(string uniqueMasterCitizenNumber, string postalIndexNumber, string firstName, string lastName, int supportedBankId)
        {
            //Arrange
            WalletDTO wallet = await _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName);
            //Act && Assert
            ExceptionAssert.Throws<ExistingWalletException>(() => _walletService.CreateNewWallet(uniqueMasterCitizenNumber, postalIndexNumber, supportedBankId, firstName, lastName).Wait());
        }
    }
}