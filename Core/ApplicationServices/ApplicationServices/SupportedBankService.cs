using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServices
{
    public class SupportedBankService : ISupportedBankService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupportedBankService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SupportedBankDTO> CreateSupportedBankAsync(string supportedBankName)
        {
            var supportedBank = new SupportedBank(supportedBankName);
            await _unitOfWork.SupportedBankRepository.Insert(supportedBank);
            await _unitOfWork.SaveChangesAsync();
            return new SupportedBankDTO(supportedBank);
        }

        public async Task<SupportedBankDTO> UpdateSupportedBankAsync(int supportedBankId, string supportedBankName)
        {
            SupportedBank supportedBank = await _unitOfWork.SupportedBankRepository.GetById(supportedBankId);
            if (supportedBank == null)
            {
                throw new ArgumentException($"Supported bank with id {supportedBankId} does not exist");
            }
            supportedBank.UpdateName(supportedBankName);
            await _unitOfWork.SupportedBankRepository.Update(supportedBank);
            await _unitOfWork.SaveChangesAsync();
            return new SupportedBankDTO(supportedBank);
        }

        public async Task<IEnumerable<SupportedBankDTO>> GetAllSupportedBanksAsync()
        {
            IEnumerable<SupportedBank> supportedBanks = await _unitOfWork.SupportedBankRepository.GetAllList();
            return supportedBanks.ToSupportedBankDTOs();
        }

        public async Task<ResultsAndTotalCount<SupportedBankDTO>> GetResultAndTotalCountSupportedBanksAsync(
           string propertyValueContains,
           OrderBySettings<SupportedBank> orderBySettings = null,
           int skip = 0,
           int? take = null)
        {
            Expression<Func<SupportedBank, bool>> filterExpression;
            if (string.IsNullOrEmpty(propertyValueContains))
            {
                filterExpression = null;
            }
            else
            {
                string uppercasePropertyValueContains = propertyValueContains.ToUpper();
                filterExpression = SupportedBank
                    => SupportedBank.Name.ToUpper().Contains(uppercasePropertyValueContains);
            }

            ResultsAndTotalCount<SupportedBank> resultsAndTotalCount = await _unitOfWork
                .SupportedBankRepository
                .GetResultsAndTotalCountAsync(
                    filterExpression,
                    orderBySettings,
                    skip,
                    take
                );
            List<SupportedBankDTO> businessActivities = resultsAndTotalCount.Results.ToSupportedBankDTOs().ToList();
            return new ResultsAndTotalCount<SupportedBankDTO>(businessActivities, resultsAndTotalCount.TotalCount);
        }

        public async Task<SupportedBankDTO> GetSupportedBankByIdAsync(int supportedBankId)
        {
            SupportedBank supportedBank = await _unitOfWork.SupportedBankRepository.GetById(supportedBankId);
            if (supportedBank == null)
            {
                throw new ArgumentException($"Supported bank with id {supportedBankId} does not exist");
            }
            return new SupportedBankDTO(supportedBank);
        }

        public async Task<SupportedBankDTO> DeleteSupportedBankAsync(int supportedBankId)
        {
            SupportedBank supportedBank = await _unitOfWork.SupportedBankRepository.GetById(supportedBankId);
            if (supportedBank == null)
            {
                throw new ArgumentException($"Supported bank with id {supportedBankId} does not exist");
            }
            await _unitOfWork.SupportedBankRepository.Delete(supportedBank);
            return new SupportedBankDTO(supportedBank);
        }
    }
}