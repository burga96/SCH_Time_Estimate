using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.ApplicationServiceInterfaces
{
    public interface IPaymentTransactionService
    {
        Task MakeDepositPaymentTransaction(string uniqueMasterCitizenNumberValue,
            string postalIndexNumber,
            string password,
            decimal amount);
    }
}