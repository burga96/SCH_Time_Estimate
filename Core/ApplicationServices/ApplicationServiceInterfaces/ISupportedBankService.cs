using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServiceInterfaces
{
    public interface ISupportedBankService
    {
        Task<SupportedBankDTO> CreateSupportedBankAsync(string supportedBankName);

        Task<SupportedBankDTO> UpdateSupportedBankAsync(int supportedBankId, string supportedBankName);

        Task<IEnumerable<SupportedBankDTO>> GetAllSupportedBanksAsync();

        Task<ResultsAndTotalCount<SupportedBankDTO>> GetResultAndTotalCountSupportedBanksAsync(
           string propertyValueContains,
           OrderBySettings<SupportedBank> orderBySettings = null,
           int skip = 0,
           int? take = null);

        Task<SupportedBankDTO> GetSupportedBankByIdAsync(int supportedBankId);

        Task<SupportedBankDTO> DeleteSupportedBankAsync(int supportedBankId);
    }
}