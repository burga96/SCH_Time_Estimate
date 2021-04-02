using BancaIntesaAPI;
using Core.ApplicationServices.ApplicationExceptions;
using Core.Domain.Entities;
using Core.Domain.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ExternalInterfaces
{
    public class BankAPIDeterminator : IBankAPIDeterminator
    {
        public IBankAPI DeterminateBankAPI(SupportedBank supportedBank)
        {
            if (supportedBank.Name.Equals("Banca Intesa"))
            {
                return BancaIntesaAPIMockFactory.Create();
            }
            throw new NotFoundBankAPIException("We don't have api from this bank");
        }
    }
}