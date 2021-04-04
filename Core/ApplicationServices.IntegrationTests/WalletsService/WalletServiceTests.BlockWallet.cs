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
        public async Task PropertiesAfterBlockWallet()
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            //Act
            WalletDTO actWallet = await _walletService.BlockWallet(wallet.Id);
            //Assert
            Assert.IsTrue(actWallet.Status == Domain.Entities.Enums.WalletStatus.BLOCKED);
        }

        [TestMethod]
        public void WalletNotFoundOnBlockingWallet()
        {
            //Act & Assert
            ExceptionAssert.Throws<ArgumentException>(() => _walletService.BlockWallet(0).Wait());
        }

        [TestMethod]
        public async Task InvalidWalletStatusOnBlockingWallet()
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            WalletDTO actWallet = await _walletService.BlockWallet(wallet.Id);
            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() => _walletService.BlockWallet(wallet.Id).Wait());
        }
    }
}