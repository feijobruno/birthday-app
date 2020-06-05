using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BirthdayApp.Business;
using BirthdayApp.Data;

namespace BirthdayApp.Data
{
    public class DatabaseFile
    {
        private static List<Person> peopleList = new List<Person>();

        public static void SalvePerson(Person person)
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
            if(personAlreadyExist == false)
            {
                string fileName = GetNameFile();
                string format = $"{person.FirstName}, {person.LastName}, {person.Birthday};";
                File.AppendAllText(fileName, format);
            }

            static string GetNameFile()
            {
                var folderDesktop = Environment.SpecialFolder.Desktop;
                string pathFolderDesktop = Environment.GetFolderPath(folderDesktop);
                string fileName = @"\peopleData.txt";

                return pathFolderDesktop + fileName;
            }
        }

        //public static List<Person> GetAllPeople()
        //{
        //    return peopleList;
        //}

        //public static IEnumerable<Person> GetAllPeople(string firstName)
        //{
        //    /*
        //     Tips:
        //     - Where in Linq returns IEnumerable.
        //     - In queries prefer returns ienumerable.
        //    */
        //    return peopleList.Where(person => person.FirstName.Contains(firstName, StringComparison.InvariantCultureIgnoreCase));
        //}
        //public static Person GetPersonById(int id)
        //{
        //    return peopleList.Find(person => person.Id == id);
        //}

        //public static void DeletePerson(Person person)
        //{
        //    peopleList.Remove(person);
        //}
    }
}
