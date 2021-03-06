using Core.ApplicationServices.ApplicationDTOs;
using Core.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class WalletVM
    {
        public WalletVM(WalletDTO wallet)
        {
            Id = wallet.Id;
            UniqueMasterCitizenNumber = wallet.UniqueMasterCitizenNumber;
            Password = wallet.Password;
            FirstName = wallet.FirstName;
            LastName = wallet.LastName;
            FullName = wallet.FullName;
            CurrentAmount = wallet.CurrentAmount;
            SupportedBankId = wallet.SupportedBankId;
            SupportedBankName = wallet.SupportedBankName;
            Status = wallet.Status;
            CreatedAt = wallet.CreatedAt;
        }

        public WalletVM()
        {
        }

        public int Id { get; private set; }

        [Required]
        [DisplayName("Unique Master Citizen Number")]
        public string UniqueMasterCitizenNumber { get; set; }

        [DisplayName("Current amount")]
        public decimal CurrentAmount { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public string FullName { get; set; }
        [DisplayName("Created at")]
        public DateTime CreatedAt { get; set; }

        
        [Required]
        [DisplayName("Supported bank")]
        public int SupportedBankId { get; set; }

        [Required]
        [DisplayName("Postal index number")]
        public string PostalIndexNumber { get; set; }

        [DisplayName("Supported bank")]
        public string SupportedBankName { get; set; }

        public string Password { get; set; }
        public WalletStatus Status { get; set; }
    }

    public static partial class WalletExtensionMethods
    {
        public static IEnumerable<WalletVM> ToWalletVMs(this IEnumerable<WalletDTO> wallets)
        {
            return wallets.Select(wallet => new WalletVM(wallet));
        }
    }
}