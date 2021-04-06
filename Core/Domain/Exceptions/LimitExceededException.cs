using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Exceptions
{
    public class LimitExceededException : Exception
    {
        public LimitExceededException() : base("Limit exceeded")
        {
        }
    }
}