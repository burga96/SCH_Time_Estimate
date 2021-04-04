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
        [DataRow("061213")]
        [DataRow("061214")]
        [DataRow("061215")]
        [DataTestMethod]
        public async Task PropertiesAfterChangedPassword(string newPassword)
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            //Act
            WalletDTO actWallet = await _walletService.ChangePassword(wallet.UniqueMasterCitizenNumber, wallet.Password, newPassword);
            //Assert
            Assert.IsTrue(actWallet.Password == newPassword);
        }

        [DataRow("0612134")]
        [DataRow("0612")]
        [DataRow("061215a")]
        [DataRow("06121a")]
        [DataTestMethod]
        public async Task InvalidNewPassword(string newPassword)
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            //Act & Assert
            ExceptionAssert.Throws<InvalidNewPasswordException>(() => _walletService.ChangePassword(wallet.UniqueMasterCitizenNumber, wallet.Password, newPassword).Wait());
        }

        [DataRow("061213")]
        [DataRow("061214")]
        [DataRow("061215")]
        [DataTestMethod]
        public async Task ChangedPasswordWhenWalletIsBlocked(string newPassword)
        {
            //Arrange
            WalletDTO wallet = await ArrangeWallet();
            await _walletService.BlockWallet(wallet.Id);

            //Act & Assert
            ExceptionAssert.Throws<WalletStatusException>(() =>
              _walletService.ChangePassword(wallet.UniqueMasterCitizenNumber, wallet.Password, newPassword).Wait()
           );
        }
    }
}