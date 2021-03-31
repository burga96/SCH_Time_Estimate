using BancaIntesaAPI;
using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ExternalInterfaces;
using Core.Domain.Factories;
using Core.Domain.RepositoryInterfaces;
using Core.Domain.ValueObjects;
using Core.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServices
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WalletDTO> CreateNewWallet(string uniqueMasterCitizenNumberValue,
            string postalIndexNumber,
            int supportedBankId,
            string firstName,
            string lastName)
        {
            SupportedBank supportedBank = await _unitOfWork.SupportedBankRepository.GetById(supportedBankId);
            if (supportedBank == null)
            {
                throw new Exception($"Supported bank with id {supportedBankId} does not exist");
            }
            IBankAPI bankAPI = DeterminateBankAPI(supportedBank);
            bool validStatus = await bankAPI.CheckStatus(uniqueMasterCitizenNumberValue, postalIndexNumber);
            if (!validStatus)
            {
                throw new Exception("Bank api - invalid status");
            }
            Wallet existingWallet = await _unitOfWork.WalletRepository.GetFirstOrDefault(Wallet =>
                Wallet.UniqueMasterCitizenNumber.Value == uniqueMasterCitizenNumberValue
            );
            if (existingWallet != null)
            {
                throw new Exception("Wallet already exist with same UMCN");
            }
            UniqueMasterCitizenNumber uniqueMasterCitizenNumber = new UniqueMasterCitizenNumber(uniqueMasterCitizenNumberValue);
            if (!uniqueMasterCitizenNumber.ValidForPlatform())
            {
                throw new Exception("Unique master citizen number not valid for platform");
            }
            string walletPassword = PasswordGenerator.WalletPassword();
            Wallet wallet = new Wallet(uniqueMasterCitizenNumberValue, supportedBank, firstName, lastName, walletPassword);
            await _unitOfWork.WalletRepository.Insert(wallet);
            await _unitOfWork.SaveChangesAsync();
            return new WalletDTO(wallet);
        }

        public async Task<ResultsAndTotalCount<WalletDTO>> GetResultAndTotalCountWalletsAsync(string propertyValueContains,
            OrderBySettings<Wallet> orderBySettings,
            int skip, int take)
        {
            Expression<Func<Wallet, bool>> filterExpression;
            if (string.IsNullOrEmpty(propertyValueContains))
            {
                filterExpression = null;
            }
            else
            {
                string uppercasePropertyValueContains = propertyValueContains.ToUpper();
                filterExpression = Wallet
                    => Wallet.PersonalData.FirstName.ToUpper().Contains(uppercasePropertyValueContains)
                    || Wallet.PersonalData.LastName.ToUpper().Contains(uppercasePropertyValueContains)
                    || Wallet.UniqueMasterCitizenNumber.Value.ToUpper().Contains(uppercasePropertyValueContains)
                    || Wallet.SupportedBank.Name.ToUpper().Contains(uppercasePropertyValueContains);
            }

            ResultsAndTotalCount<Wallet> resultsAndTotalCount = await _unitOfWork
                .WalletRepository
                .GetResultsAndTotalCountAsync(
                    filterExpression,
                    orderBySettings,
                    skip,
                    take,
                    Wallet => Wallet.SupportedBank
                );
            List<WalletDTO> wallets = resultsAndTotalCount.Results.ToWalletDTOs().ToList();
            return new ResultsAndTotalCount<WalletDTO>(wallets, resultsAndTotalCount.TotalCount);
        }

        private IBankAPI DeterminateBankAPI(SupportedBank supportedBank)
        {
            if (supportedBank.Name.Equals("Banca Intesa"))
            {
                return BancaIntesaAPIMockFactory.Create();
            }
            throw new Exception("We don't have api from this bank");
        }
    }
}