using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using BirthdayApp.Business;
using BirthdayApp.Data;
using System.Text.Encodings.Web;
using System.Threading;

namespace BirthdayApp.Data
{
    public class PeopleRepositoryFile : IPeopleRepository
    {
        private static List<Person> peopleList = new List<Person>();
        private static readonly List<int> sequence = new List<int>();
        public void SalvePerson(Person person)
        {
            bool personAlreadyExist = false;
            if (personAlreadyExist == false)
            {
                string fileName = GetNameFile();
                string format = $"{person.Id},{person.FirstName},{person.LastName},{person.Birthday};";
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

        public IEnumerable<Person> GetAllPeople()
        {
            peopleList.Clear();
            string fileName = GetNameFile();
            string result; 
            try
            {
                result = File.ReadAllText(fileName);
            }
            catch
            {
                File.AppendAllText(fileName, null);
                result = File.ReadAllText(fileName);
            }
            

            //Identify person
            string[] people = result.Split(';');
            //List<Person> peopleList = new List<Person>();
            for (int i = 0; i < people.Length - 1; i++)
            {
                string[] peopleData = people[i].Split(',');

                //Identify person's data
                string id = peopleData[0];
                string firstName = peopleData[1];
                string lastName = peopleData[2];
                DateTime birthday = Convert.ToDateTime(peopleData[3]);

                //Fill in person's class with this data
                Person person = new Person(int.Parse(id), firstName, lastName, birthday);

                //Add person into people's list
                peopleList.Add(person);
            }
            return peopleList;
        }

        public IEnumerable<Person> GetAllPeople(string name)
        {
            return GetAllPeople().Where(person => person.FirstName.Contains(name, StringComparison.InvariantCultureIgnoreCase) || person.LastName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }
        public Person GetPersonById(int id)
        {
            return peopleList.Find(person => person.Id.Equals(id));
            //throw new NotImplementedException();
        }

        public int GetSequenceId()
        {
            string fileName = GetNameFile();
            int newSequence;
            try
            {
                string result = File.ReadAllText(fileName);
                string[] seq = result.Split(';');
                for (int i = 0; i < seq.Length - 1; i++)
                {
                    string[] seqData = seq[i].Split(',');

                    //Identify person's data
                    int id = int.Parse(seqData[0]);

                    //Add sequence into sequence's list
                    sequence.Add(id);
                }
                newSequence = sequence.Max() + 1;
            }
            catch
            {
                newSequence = 1;
            }
            return newSequence;
        }

        public void DeletePerson(Person person)
        {
            peopleList.Remove(person);
            File.WriteAllText(GetNameFile(), "");
            foreach (var personInsert in peopleList)
            {
                string format = $"{personInsert.Id},{personInsert.FirstName},{personInsert.LastName},{personInsert.Birthday};";
                File.AppendAllText(GetNameFile(), format);
            }
        }

        public void UpdatePerson(Person personGet, Person personSet)
        {
            peopleList.Remove(personGet);
            peopleList.Add(personSet);
            File.WriteAllText(GetNameFile(), "");
            foreach (var personInsert in peopleList)
            {
                string format = $"{personInsert.Id},{personInsert.FirstName},{personInsert.LastName},{personInsert.Birthday};";
                File.AppendAllText(GetNameFile(), format);
            }
        }
    }
}
