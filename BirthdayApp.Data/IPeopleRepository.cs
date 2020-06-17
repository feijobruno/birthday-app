using BirthdayApp.Business;
using System.Collections.Generic;

namespace BirthdayApp.Data
{
    public interface IPeopleRepository
    {
        void DeletePerson(Person person);
        IEnumerable<Person> GetAllPeople();
        IEnumerable<Person> GetAllPeople(string name);
        Person GetPersonById(int id);
        int GetSequenceId();
        void SalvePerson(Person person);
        void UpdatePerson(Person personGet, Person personSet);
    }
}