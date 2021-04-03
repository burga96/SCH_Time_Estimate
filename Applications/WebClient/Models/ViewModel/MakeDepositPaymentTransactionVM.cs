using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class ChangePasswordVM : AuthenticationVM
    {
        public ChangePasswordVM()
        {
        }

        public ChangePasswordVM(string uniqueMasterCitizenNumber, string password, string error, bool authenticated, string newPassword) : base(uniqueMasterCitizenNumber, password, error, authenticated)
        {
            NewPassword = newPassword;
        }

        public string NewPassword { get; set; }
    }
}