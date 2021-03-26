using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.ApplicationServices.ExternalInterfaces;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServices
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAPI _bankAPI;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_bankAPI = bankAPI;
        }

        public async Task<WalletDTO> CreateNewWallet(string uniqueMasterCitizenNumber,
            string postalIndexNumber,
            int supportedBankId,
            string firstName,
            string lastName)
        {
            return null;
        }
    }
}