using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServiceInterfaces
{
    public interface IWalletService
    {
        Task<WalletDTO> CreateNewWallet(string uniqueMasterCitizenNumber,
            string postalIndexNumber,
            int supportedBankId,
            string firstName,
            string lastName);

        Task<ResultsAndTotalCount<WalletDTO>> GetResultAndTotalCountWalletsAsync(string filter, OrderBySettings<Wallet> orderBySettings, int skip, int pageSize);

        Task<WalletDTO> GetWalletByUniqueMasterCitizenNumberAndPassword(string uniqueMasterCitizenNumber, string password);

        Task<WalletDTO> ChangePassword(string uniqueMasterCitizenNumber, string password, string newPassword);

        Task<WalletDTO> ActivateWallet(int walletId);

        Task<WalletDTO> BlockWallet(int walletId);
    }
}