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
    [TestClass]
    public partial class WalletServiceTests
    {
        private IUnitOfWork _unitOfWork;
        private TimeEstimateDBContext _context;
        private IWalletService _walletService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            var bankAPIDeterminator = new BankAPIDeterminator();
            _context = dbContextFactory.CreateDbContext(new string[] { });
            _unitOfWork = new EfCoreUnitOfWork(_context);
            _walletService = new WalletService(_unitOfWork, bankAPIDeterminator);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await RemoveAllWallets();
            await _context.DisposeAsync();
            _unitOfWork = null;
            _walletService = null;
        }

        public async Task RemoveAllWallets()
        {
            _context.Wallets.RemoveRange(_context.Wallets.ToList());
            await _context.SaveChangesAsync();
        }

        public async Task<WalletDTO> ArrangeWallet()
        {
            WalletDTO wallet = await _walletService.CreateNewWallet("2108996781057", "0612", 1, "Stefan", "Burgic");
            return wallet;
        }
    }
}