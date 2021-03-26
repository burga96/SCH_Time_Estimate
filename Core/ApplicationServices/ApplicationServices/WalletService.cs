﻿using BancaIntesaAPI;
using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ExternalInterfaces;
using Core.Domain.RepositoryInterfaces;
using Core.Domain.ValueObjects;
using Core.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
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
                throw new Exception("UMCN not valid for platform");
            }

            Wallet wallet = new Wallet(uniqueMasterCitizenNumberValue, supportedBank, firstName, lastName, "STEFAN");
            await _unitOfWork.WalletRepository.Insert(wallet);
            await _unitOfWork.SaveChangesAsync();
            return new WalletDTO(wallet);
        }

        private IBankAPI DeterminateBankAPI(SupportedBank supportedBank)
        {
            if (supportedBank.Name.Equals("Banca Intesa"))
            {
                return BancaIntesaAPIMockFactory.Create();
            }
            throw new NotImplementedException();
        }
    }
}