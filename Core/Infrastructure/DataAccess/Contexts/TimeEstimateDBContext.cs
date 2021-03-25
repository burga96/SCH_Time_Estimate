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

        #endregion Entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
        }
    }
}