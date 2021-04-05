using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities.Enums
{
    public enum PaymentTransactionType
    {
        DEPOSIT, WITHDRAWAL, INTERNAL_TRANSFER_DEPOSIT, INTERNAL_TRANSFER_WITHDRAWAL, FEE
    }
}