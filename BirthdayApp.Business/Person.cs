using System;

namespace BirthdayApp.Business
{
    public class Person
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Person(string firstName, string lastName, DateTime birthday)
        {
            Random num = new Random();
            Id = num.Next(1, 21);
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }
        public int CalculateDays()
        {
            var nextBirthday = new DateTime(DateTime.Today.Year, Birthday.Month, Birthday.Day);
            if (nextBirthday < DateTime.Today)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }
            return (nextBirthday - DateTime.Today).Days;
        }
        public Person()
        {
        }
    }
}
