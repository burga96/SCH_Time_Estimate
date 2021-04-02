using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class AuthenticationVM
    {
        public string Password { get; set; }
        public string Error { get; set; }
        public string UniqueMasterCitizenNumber { get; set; }
        public bool Authenticated { get; set; }

        public AuthenticationVM(string uniqueMasterCitizenNumber, string password, string error, bool authenticated)
        {
            Password = password;
            Error = error;
            UniqueMasterCitizenNumber = uniqueMasterCitizenNumber;
            Authenticated = authenticated;
        }

        public AuthenticationVM()
        {
        }
    }
}