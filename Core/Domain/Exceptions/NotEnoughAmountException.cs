using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Exceptions
{
    public class NotEnoughAmountException : Exception
    {
        public NotEnoughAmountException() : base("Not enough amount")
        {
        }
    }
}