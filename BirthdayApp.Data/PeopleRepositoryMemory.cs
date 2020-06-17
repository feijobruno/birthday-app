using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayApp.Business;
using System.Threading.Tasks;


namespace BirthdayApp.Data 
{
    public class PeopleRepositoryMemory : IPeopleRepository
    {
        private static List<Person> peopleList = new List<Person>();

        public void SalvePerson(Person person)
        {
            //if (ModelState.IsValid == false)
            //{
            //    return View();
            //}
                
            peopleList.Add(person);          
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return peopleList;
        }

        public IEnumerable<Person> GetAllPeople(string name)
        {
            /*
             Tips:
             - Where in Linq returns IEnumerable.
             - In queries prefer returns ienumerable.
            */
            return peopleList.Where(person => person.FirstName.Contains(name, StringComparison.InvariantCultureIgnoreCase)||person.LastName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }
        public Person GetPersonById(int id)
        {
            //return peopleList.Find(person => person.Id == id);
            throw new NotImplementedException();
        }

        public void DeletePerson(Person person)
        {
            peopleList.Remove(person);
        }

        public void UpdatePerson(Person personGet, Person personSet)
        {
            throw new NotImplementedException();
        }

        public int GetSequenceId()
        {
            throw new NotImplementedException();
        }     
    }
}
