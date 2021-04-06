using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ExternalInterfaces
{
    public interface IBankAPI
    {
        Task<bool> CheckStatus(string uniqueMasterCitizenNumber, string postalIndexNumber);

        Task<bool> Withdraw(string uniqueMasterCitizenNumberValue, string postalIndexNumber, decimal amount);

        Task<bool> Deposit(string uniqueMasterCitizenNumberValue, string postalIndexNumber, decimal amount);
    }
}