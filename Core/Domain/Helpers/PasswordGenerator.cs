using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Helpers
{
    public static class PasswordGenerator
    {
        public static string WalletPassword()
        {
            var chars = "0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}