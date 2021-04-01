using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class ExistingWalletException : Exception
    {
        public ExistingWalletException(string message) : base(message)
        {
        }
    }
}