using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ExternalInterfaces
{
    public interface IBankAPI
    {
        Task<bool> CheckStatus(string uniqueMasterCitizenNumber, string postalIndexNumber);
    }
}