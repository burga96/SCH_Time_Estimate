using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.ApplicationExceptions
{
    public class NotValidUniqueMasterCitizenNumberException : Exception
    {
        public NotValidUniqueMasterCitizenNumberException(string message) : base(message)
        {
        }
    }
}