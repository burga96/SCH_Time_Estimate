using System;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWalletRepository WalletRepository { get; }
        ISupportedBankRepository SupportedBankRepository { get; }

        Task SaveChangesAsync();
    }
}