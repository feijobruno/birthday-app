using System.Collections.Generic;
using BirthdayApp.Business;


namespace BirthdayApp.Data
{
    public abstract class Database
    {
        public abstract void SalvePerson(Person person);

        public abstract void DeletePerson(Person person);

        public abstract IEnumerable<Person> GetAllPeople();

        public abstract IEnumerable<Person> GetAllPeople(string firstName);

        public abstract Person GetPersonById(int id);
    }
}
