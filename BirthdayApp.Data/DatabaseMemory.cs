using System;
using System.Collections.Generic;
using System.Text;
using BirthdayApp.Business;
using System.Linq;

namespace BirthdayApp.Data
{
    public class DatabaseMemory
    {
        private static List<Person> peopleList = new List<Person>();

        public static void AddNewPerson(Person person)
        {
            peopleList.Add(person);
        }

        public static List<Person> GetAllPeople()
        {
            return peopleList;
        }

        public static IEnumerable<Person> GetAllPeople(string firstName)
        {
            /*
             Tips:
             - Where in Linq returns IEnumerable.
             - In queries prefer returns ienumerable.
            */
            return peopleList.Where(person => person.FirstName.Contains(firstName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
