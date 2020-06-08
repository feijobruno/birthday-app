using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BirthdayApp.Business;
using BirthdayApp.Data;


namespace BirthdayApp.Data
{
    public class DatabaseFile : Database
    {
        public override void SalvePerson(Person person) 
        {
            bool personAlreadyExist = false;
            if (personAlreadyExist == false)
            {
                string fileName = GetNameFile();
                string format = $"{person.FirstName}, {person.LastName}, {person.Birthday};";
                File.AppendAllText(fileName, format);
            }
        }

        private static string GetNameFile()
        {
            var folderDesktop = Environment.SpecialFolder.Desktop;
            string pathFolderDesktop = Environment.GetFolderPath(folderDesktop);
            string fileName = @"\peopleData.txt";

            return pathFolderDesktop + fileName;
        }

        public override IEnumerable<Person> GetAllPeople()
        {
            string fileName = GetNameFile();
            string result = File.ReadAllText(fileName);

            //Identify person
            string[] people = result.Split(';');
            List<Person> peopleList = new List<Person>();

            for (int i = 0; i < people.Length - 1; i++)
            {
                string[] peopleData = people[i].Split(',');

                //Identify person's data
                string firstName = peopleData[0];
                string lastName = peopleData[1];
                DateTime birthday = Convert.ToDateTime(peopleData[2]);

                //Fill in person's class with this data
                Person person = new Person(firstName, lastName, birthday);

                //Add person into people's list
                peopleList.Add(person);
            }
            return peopleList;
        }

        public override IEnumerable<Person> GetAllPeople(string firstName)
        {
            var peopleList = GetAllPeople();
            return peopleList.Where(person => person.FirstName.Contains(firstName, StringComparison.InvariantCultureIgnoreCase));
        }
        public override Person GetPersonById(int id)
        {
            throw new NotImplementedException();
        }

        public override void DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
