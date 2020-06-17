using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BirthdayApp.Business;
using BirthdayApp.Data;
using System.Dynamic;
using System.Linq.Expressions;

namespace BirthdayApp
{
    class ViewConsole
    {
        public static void MainMenu()
        {
            Console.Clear();
            FeaturedText("Gerenciamento de Aniversários de Amigos", "Yellow");
            FeaturedText("\nAniversariante(s) do dia:", "Blue");
            ShowBirthdayToday();
            Console.WriteLine("\nSelecione uma das opções abaixo: ");
            Console.WriteLine("1 - Pesquisar pessoas ");
            Console.WriteLine("2 - Adicionar pessoas");
            Console.WriteLine("3 - Editar pessoas");
            Console.WriteLine("4 - Excluir pessoas");
            Console.WriteLine("5 - Sair");

            try
            {
                char optionSelected = Console.ReadLine().ToCharArray()[0];
                switch (optionSelected)
                {
                    case '1': Find(); break;
                    case '2': Add(); break;
                    case '3': Update(); break;
                    case '4': Delete(); break;
                    case '5':
                        //EXIT APP
                        Console.WriteLine("Bye bye");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Opção inexistente.");
                        PressAnyKey();
                        MainMenu();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Opção inexistente");
                MainMenu();
            }
        }

        static void Add()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome da pessoa que deseja adicionar:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome da pessoa que deseja adicionar:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Digite a data do nascimento no formato (dd/mm/aaaa): ");
            var birthday = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Os dados estão corretos? ");
            Console.WriteLine($"{firstName} {lastName}");
            Console.WriteLine(birthday.ToString("d"));
            Console.WriteLine("1 - Sim \n2 - Não");
            char option = Console.ReadLine().ToCharArray()[0];
            if (option == '1')
            {
                Console.Clear();
                var person = new Person(Db.GetSequenceId(), firstName, lastName, birthday);
                Db.SalvePerson(person);
                FeaturedText("Dados adicionados com sucesso!", "Green");
                PressAnyKey();
                MainMenu();
            }
            else if (option == '2')
            {
                Console.WriteLine("Insira os dados novamente, por favor:");
                PressAnyKey();
                Add();
            }
        }
        static void Find()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string name = Console.ReadLine();
            var peopleFilter = Db.GetAllPeople(name);
            if (peopleFilter != null && peopleFilter.GetEnumerator().MoveNext())
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontradas:");
                foreach (var person in peopleFilter)
                {
                    Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName}");
                }
                int optionId = int.Parse(Console.ReadLine());
                var chosenPerson = peopleFilter.First(person => person.Id.Equals(optionId));
                if (chosenPerson != null)
                {
                    Console.Clear();
                    FeaturedText("Dados da pessoa:", "Gray");
                    Console.WriteLine($"ID: {chosenPerson.Id}");
                    Console.WriteLine($"Nome Completo: {chosenPerson.FirstName} {chosenPerson.LastName}");
                    Console.WriteLine($"Data do Aniversário: {chosenPerson.Birthday.ToString("d")}");
                    if (chosenPerson.CalculateDays() == 0)
                    {
                        Console.WriteLine($"Hoje é Aniversário do(a) {chosenPerson.FirstName}! Dê os parabéns!\n");
                    }
                    else 
                    { 
                    Console.WriteLine($"Faltam {chosenPerson.CalculateDays()} dia(s) para esse aniversário.\n");
                    
                    }
                }
                else
                {
                    FeaturedText("Nenhuma pessoa localizada!", "Red");
                }
            }
            else
            {
                FeaturedText("Nenhuma cadastro no sistema. Favor cadastrar uma Pessoa.", "Red");
            }
            PressAnyKey();
            MainMenu();
        }

        static void Update()
        {
            Console.Clear();
            FeaturedText("Dados da pessoa:", "Gray");
            ShowAllPeople();
            Console.WriteLine("Selecione qual ID você deseja alterar:");
            try
            {
                int option = int.Parse(Console.ReadLine());
                var personGet = Db.GetPersonById(option);
                Person personSet = new Person();
                if (personGet == null)
                {
                    FeaturedText("Pessoa não encontrada para o Id forncecido", "Red");
                }
                else
                {
                    personSet.Id = personGet.Id;
                    Console.WriteLine("Digite o novo nome:");
                    string newFirstName = Console.ReadLine();
                    personSet.FirstName = newFirstName;

                    Console.WriteLine("Digite o novo sobrenome:");
                    string newLastName = Console.ReadLine();
                    personSet.LastName = newLastName;

                    Console.WriteLine("Digite a nova data do nascimento no formato dd/MM/yyyy: ");
                    var newBirthday = DateTime.Parse(Console.ReadLine());
                    personSet.Birthday = newBirthday;

                    Db.UpdatePerson(personGet, personSet);
                    FeaturedText("\nPessoa editada com sucesso!", "Green");
                }
            }
            catch
            {
                FeaturedText("Pessoa não encontrada para o Id forncecido", "Red");
            }
            PressAnyKey();
            MainMenu();
        }
        static void Delete()
        {
            Console.Clear();
            FeaturedText("Dados das pessoas:", "Gray");
            ShowAllPeople();
            Console.WriteLine("Selecione qual ID você deseja excluir:");
            try
            {
                int option = int.Parse(Console.ReadLine());
                var person = Db.GetPersonById(option);
                if (person != null)
                {
                    Db.DeletePerson(person);
                    FeaturedText("Pessoa excluída com sucesso!", "Green");
                }
                else
                {
                    FeaturedText("Pessoa não encontrada para o Id forncecido", "Red");
                }
            }
            catch
            {
                FeaturedText("Pessoa não encontrada para o Id forncecido", "Red");
            }
            PressAnyKey();
            MainMenu();
        }

        static void PressAnyKey()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }

        //CREATE A PROPERTY DATABASE 
        public static IPeopleRepository Db
        {
            get
            {
                return new PeopleRepositoryFile();
            }
        }
        static void ShowAllPeople()
        {
            foreach (var person in Db.GetAllPeople())
            {
                Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName} - {person.Birthday.ToString("d")}");
            }
        }

        static void ShowBirthdayToday()
        {
            var today = DateTime.Today;
            var select = Db.GetAllPeople().Where(person => person.Birthday.Day.Equals(today.Day) && person.Birthday.Month.Equals(today.Month));
            foreach (var person in select)
            {
                Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName} - {person.Birthday.ToString("d")}");
            }
        }

        public static void FeaturedText(string text, string color)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            switch (color)
            {
                case "Red": Console.BackgroundColor = ConsoleColor.Red; break;
                case "Yellow": Console.BackgroundColor = ConsoleColor.Yellow; break;
                case "Blue": Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                case "Green": Console.BackgroundColor = ConsoleColor.Green; break;
                case "Gray": Console.BackgroundColor = ConsoleColor.Gray; break;
                default: Console.ResetColor();break;
            }
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
