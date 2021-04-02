using Core.Domain.RepositoryInterfaces;
using Core.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.Infrastructure.DataAccess.Repositories
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly TimeEstimateDBContext Context;

        #endregion Fields

        #region Constructors

        public EfCoreUnitOfWork(TimeEstimateDBContext context)
        {
            Context = context;
            WalletRepository = new WalletRepository(context);
            SupportedBankRepository = new SupportedBankRepository(context);
            PaymentTransactionRepository = new PaymentTransactionRepository(context);
        }

        #endregion Constructors

        #region Properties

        public IWalletRepository WalletRepository { get; }
        public ISupportedBankRepository SupportedBankRepository { get; }
        public IPaymentTransactionRepository PaymentTransactionRepository { get; }

        #endregion Properties

        #region Methods

        public async Task SaveChangesAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException duce)
            {
                throw duce;
            }
            catch (DbUpdateException due)
            {
                throw due;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Methods

        #region IDisposable implementation

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }
            if (disposing)
            {
                Context.Dispose();
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable implementation
    }
}