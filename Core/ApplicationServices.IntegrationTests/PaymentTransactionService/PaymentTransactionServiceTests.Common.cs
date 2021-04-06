using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationExceptions;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.ApplicationServices.ApplicationServices;
using Core.ApplicationServices.ExternalInterfaces;
using Core.ApplicationServices.IntegrationTests.Common;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using Core.Domain.RepositoryInterfaces;
using Core.Infrastructure.DataAccess.Contexts;
using Core.Infrastructure.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.PaymentTransactionService
{
    [TestClass]
    public partial class PaymentTransactionServiceTests
    {
        private IUnitOfWork _unitOfWork;
        private TimeEstimateDBContext _context;
        private IWalletService _walletService;
        private IPaymentTransactionService _paymentTransactionService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            var bankAPIDeterminator = new BankAPIDeterminator();
            _context = dbContextFactory.CreateDbContext(new string[] { });
            _unitOfWork = new EfCoreUnitOfWork(_context);
            _walletService = new WalletService(_unitOfWork, bankAPIDeterminator);
            _paymentTransactionService = new ApplicationServices.PaymentTransactionService(_unitOfWork, bankAPIDeterminator);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await RemoveAllWalletsAndTransactions();
            await _context.DisposeAsync();
            _unitOfWork = null;
            _walletService = null;
            _paymentTransactionService = null;
        }

        public async Task RemoveAllWalletsAndTransactions()
        {
            _context.PaymentTransactions.RemoveRange(_context.PaymentTransactions.ToList());
            _context.Wallets.RemoveRange(_context.Wallets.ToList());
            await _context.SaveChangesAsync();
        }

        public async Task<WalletDTO> ArrangeWallet()
        {
            WalletDTO wallet = await _walletService.CreateNewWallet("2108996781057", "0612", 1, "Stefan", "Burgic");
            return wallet;
        }

        public async Task<WalletDTO> ArrangeSecondWallet()
        {
            WalletDTO wallet = await _walletService.CreateNewWallet("2108995781057", "0612", 1, "Vukoman", "Stojanovic");
            return wallet;
        }

        public async Task<WalletDTO> ArrangeWalletOnSpecificDateWithAmount(DateTime date, decimal amount)
        {
            SupportedBank bank = await _unitOfWork.SupportedBankRepository.GetById(1);
            Wallet wallet = new Wallet("2108996781057", bank, "Stefan", "Burgic", "061213", "0612");
            wallet.ChangeCreatedAtDate(date);
            wallet.AddAmount(amount);
            await _unitOfWork.WalletRepository.Insert(wallet);
            await _unitOfWork.SaveChangesAsync();
            return new WalletDTO(wallet);
        }
    }
}