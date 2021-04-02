using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class PasswordVM
    {
        public string Password { get; set; }
        public string Error { get; set; }
        public bool HasPassword { get; set; }

        public PasswordVM(string password, string error, bool hasPassword)
        {
            Password = password;
            Error = error;
            HasPassword = hasPassword;
        }

        public PasswordVM()
        {
        }
    }
}