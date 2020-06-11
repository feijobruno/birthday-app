using System.Collections.Generic;
using BirthdayApp.Business;


namespace BirthdayApp.Data
{
    public abstract class PeopleRepository
    {
        public abstract void SalvePerson(Person person);

        public abstract void DeletePerson(Person person);

        public abstract void UpdatePerson(Person personGet, Person personSet);

        public abstract IEnumerable<Person> GetAllPeople();

        public abstract IEnumerable<Person> GetAllPeople(string name);

        public abstract Person GetPersonById(int id);

        public abstract int GetSequenceId();
    }
}
