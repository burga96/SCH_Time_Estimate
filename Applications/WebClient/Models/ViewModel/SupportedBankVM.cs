using Core.ApplicationServices.ApplicationDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class SupportedBankVM
    {
        public SupportedBankVM(SupportedBankDTO supportedBank)
        {
            Id = supportedBank.Id;
            Name = supportedBank.Name;
        }

        public SupportedBankVM()
        {
        }

        public int Id { get; private set; }

        [DisplayName("Name")]
        public string Name { get; set; }
    }

    public static partial class SupportedBankDTOExtensionMethods
    {
        public static IEnumerable<SupportedBankVM> ToSupportedBankVMs(this IEnumerable<SupportedBankDTO> supportedBanks)
        {
            return supportedBanks.Select(supportedBank => new SupportedBankVM(supportedBank));
        }
    }
}