using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class AdminPasswordVM
    {
        public string Password { get; set; }
        public string Error { get; set; }
        public bool HasAdminPassword { get; set; }

        public AdminPasswordVM(string password, string error, bool hasAdminPassword)
        {
            Password = password;
            Error = error;
            HasAdminPassword = hasAdminPassword;
        }

        public AdminPasswordVM()
        {
        }
    }
}