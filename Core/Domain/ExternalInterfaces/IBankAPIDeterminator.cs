using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ExternalInterfaces
{
    public interface IBankAPIDeterminator
    {
        IBankAPI DeterminateBankAPI(SupportedBank supportedBank);
    }
}