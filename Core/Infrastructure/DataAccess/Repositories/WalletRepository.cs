using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Core.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.Repositories
{
    public class WalletRepository : EfCoreBaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(TimeEstimateDBContext context) : base(context)
        {
        }
    }
}