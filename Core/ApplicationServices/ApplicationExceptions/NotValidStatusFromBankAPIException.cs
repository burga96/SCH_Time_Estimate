using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class NotValidStatusFromBankAPIException : Exception
    {
        public NotValidStatusFromBankAPIException(string message) : base(message)
        {
        }
    }
}