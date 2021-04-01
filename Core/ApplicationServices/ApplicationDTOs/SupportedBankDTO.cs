using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.ApplicationDTOs
{
    public class SupportedBankDTO
    {
        public SupportedBankDTO(SupportedBank supportedBank)
        {
            Id = supportedBank.Id;
            Name = supportedBank.Name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    public static partial class SupportedBankExtensionMethods
    {
        public static IEnumerable<SupportedBankDTO> ToSupportedBankDTOs(this IEnumerable<SupportedBank> supportedBanks)
        {
            return supportedBanks.Select(supportedBank => new SupportedBankDTO(supportedBank));
        }
    }
}