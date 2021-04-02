using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Wrong password")
        {
        }
    }
}