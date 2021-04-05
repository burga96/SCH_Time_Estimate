using Core.Domain.Entities;
using Core.Infrastructure.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.Contexts
{
    public class TimeEstimateDBContext : DbContext
    {
        public TimeEstimateDBContext(DbContextOptions<TimeEstimateDBContext> options)
            : base(options)
        {
        }

        #region Entities

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<SupportedBank> SupportedBanks { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        #endregion Entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new SupportedBankConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new DepositPaymentTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawalPaymentTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new DepositInternalTransferPaymentTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawalInternalTransferPaymentTransactionConfiguration());
        }
    }
}