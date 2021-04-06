using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Exceptions
{
    public class InvalidNewPasswordException : Exception
    {
        public InvalidNewPasswordException() : base("Password must be exact 6 digits")
        {
        }
    }
}