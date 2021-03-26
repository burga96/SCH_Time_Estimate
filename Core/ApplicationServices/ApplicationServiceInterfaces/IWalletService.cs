using Core.ApplicationServices.ApplicationDTOs;
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
    }
}