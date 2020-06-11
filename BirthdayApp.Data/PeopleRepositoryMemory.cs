using System;
using System.Collections.Generic;
using System.Text;
using BirthdayApp.Business;
using System.Linq;

namespace BirthdayApp.Data 
{
    public class PeopleRepositoryMemory : PeopleRepository
    {
        private static List<Person> peopleList = new List<Person>();

        public override void SalvePerson(Person person)
        {
            bool personAlreadyExist = false;
            foreach(var peopleFound in peopleList)
            {
                if(peopleFound == person)
                {
                    personAlreadyExist = true;
                    break;
                }
            }
            if(personAlreadyExist == false){
                peopleList.Add(person);
            }          
        }

        public override IEnumerable<Person> GetAllPeople()
        {
            return peopleList;
        }

        public override IEnumerable<Person> GetAllPeople(string name)
        {
            /*
             Tips:
             - Where in Linq returns IEnumerable.
             - In queries prefer returns ienumerable.
            */
            return peopleList.Where(person => person.FirstName.Contains(name, StringComparison.InvariantCultureIgnoreCase)||person.LastName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }
        public override Person GetPersonById(int id)
        {
            //return peopleList.Find(person => person.Id == id);
            throw new NotImplementedException();
        }

        public override void DeletePerson(Person person)
        {
            peopleList.Remove(person);
        }

        public override void UpdatePerson(Person personGet, Person personSet)
        {
            throw new NotImplementedException();
        }

        public override int GetSequenceId()
        {
            throw new NotImplementedException();
        }

        public override void ShowBirthdayToday()
        {
            throw new NotImplementedException();
        }       
    }
}
