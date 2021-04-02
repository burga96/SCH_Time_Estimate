using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class BankAPIException : Exception
    {
        public BankAPIException(string message) : base(message)
        {
        }
    }
}