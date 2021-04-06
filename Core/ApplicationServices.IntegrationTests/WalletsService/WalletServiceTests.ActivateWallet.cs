using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.ApplicationServices.ApplicationServices;
using Core.ApplicationServices.ExternalInterfaces;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Exceptions;
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
        [TestMethod]
        public async Task PropertiesAfterActivateWallet()
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            await _walletService.BlockWallet(wallet.Id);
            //Act
            WalletDTO actWallet = await _walletService.ActivateWallet(wallet.Id);
            //Assert
            Assert.IsTrue(actWallet.Status == Domain.Entities.Enums.WalletStatus.ACTIVE);
        }

        [TestMethod]
        public void WalletNotFoundOnActivatingWallet()
        {
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() => _walletService.ActivateWallet(0).Wait());
        }

        [TestMethod]
        public async Task InvalidWalletStatusOnActivatingWallet()
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() => _walletService.ActivateWallet(wallet.Id).Wait());
        }
    }
}