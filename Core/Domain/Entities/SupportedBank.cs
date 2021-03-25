using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class SupportedBank
    {
        public SupportedBank()
        {
        }

        public SupportedBank(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}