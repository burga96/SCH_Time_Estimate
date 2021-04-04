using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Exceptions
{
    public class WalletStatusException : Exception
    {
        public WalletStatusException(string message) : base(message)
        {
        }
    }
}