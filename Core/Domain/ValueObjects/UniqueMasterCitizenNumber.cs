using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObjects
{
    public class UniqueMasterCitizenNumber : ValueObject<UniqueMasterCitizenNumber>
    {
        public UniqueMasterCitizenNumber(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
        public static int AdultAgeLimit = 18;

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new[] { Value };
        }

        public bool ValidForPlatform()
        {
            return IsAdult() && IsRepublicOfSerbiaCitizen();
        }

        public bool IsAdult()
        {
            var currentDate = DateTime.Now;
            string day = Value.Substring(0, 2);
            string month = Value.Substring(2, 2);
            string year = Value.Substring(4, 3);
            if (int.Parse(year) > 900)
            {
                year = "1" + year;
            }
            else
            {
                year = "2" + year;
            }
            string fullDate = year + "-" + month + "-" + day;
            var uniqueMasterCitizenNumberDate = DateTime.Parse(fullDate);
            bool adult = uniqueMasterCitizenNumberDate.AddYears(AdultAgeLimit) <= currentDate;
            return adult;
        }

        public bool IsRepublicOfSerbiaCitizen()
        {
            string region = Value.Substring(7, 2);
            if (region[0] != '7' && region[0] != '8' && region[0] != '9')
            {
                return false;
            }
            if (!(region[1] >= '0' && region[1] <= '9'))
            {
                return false;
            }
            return true;
        }
    }
}