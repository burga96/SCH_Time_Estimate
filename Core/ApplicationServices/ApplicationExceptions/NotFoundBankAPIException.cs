using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class NotFoundBankAPIException : Exception
    {
        public NotFoundBankAPIException(string message) : base(message)
        {
        }
    }
}