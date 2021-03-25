using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObjects
{
    public class PersonalData : ValueObject<PersonalData>
    {
        public PersonalData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new[] { FirstName, LastName };
        }
    }
}