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
            ContrastColor("Yellow");
            Console.WriteLine("Gerenciamento de Aniversários de Amigos");
            Console.ResetColor();
            ContrastColor("Blue");
            Console.WriteLine("\nAniversariante(s) do dia:");
            Console.ResetColor();
            Db.ShowBirthdayToday();
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
                ContrastColor("Green");
                Console.WriteLine("Dados adicionados com sucesso!");
                Console.ResetColor();
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
                    ContrastColor("Yellow");
                    Console.WriteLine("Dados da pessoa:");
                    Console.ResetColor();
                    Console.WriteLine($"ID: {chosenPerson.Id}");
                    Console.WriteLine($"Nome Completo: {chosenPerson.FirstName} {chosenPerson.LastName}");
                    Console.WriteLine($"Data do Aniversário: {chosenPerson.Birthday.ToString("d")}");
                    Console.WriteLine($"Faltam {chosenPerson.CalculateDays()} dia(s) para esse aniversário.\n");
                }
                else
                {
                    ContrastColor("Red");
                    Console.WriteLine("Nenhuma pessoa localizada!");

                    Console.ResetColor();
                }
            }
            else
            {
                ContrastColor("Red");
                Console.WriteLine("Nenhuma cadastro no sistema. Favor cadastrar uma Pessoa.");
                Console.ResetColor();
            }
            PressAnyKey();
            MainMenu();
        }

        static void Update()
        {
            Console.Clear();
            ContrastColor("Yellow");
            Console.WriteLine("Dados das pessoas:");
            Console.ResetColor();
            ShowAllPeople();
            Console.WriteLine("Selecione qual ID você deseja alterar:");
            try
            {
                int option = int.Parse(Console.ReadLine());
                var personGet = Db.GetPersonById(option);
                Person personSet = new Person();
                if (personGet == null)
                {
                    ContrastColor("Red");
                    Console.WriteLine("Pessoa não encontrada para o Id forncecido");
                    Console.ResetColor();
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
                    ContrastColor("Green");
                    Console.WriteLine("Dados editados com sucesso!");
                    Console.ResetColor();
                }
            }
            catch
            {
                ContrastColor("Red");
                Console.WriteLine("Pessoa não encontrada para o Id forncecido");
                Console.ResetColor();
            }
            PressAnyKey();
            MainMenu();

        }
        static void Delete()
        {
            Console.Clear();
            Console.WriteLine("Dados das pessoas:");
            ShowAllPeople();
            Console.WriteLine("Selecione qual ID você deseja excluir:");
            try
            {
                int option = int.Parse(Console.ReadLine());
                var person = Db.GetPersonById(option);
                if (person != null)
                {
                    Db.DeletePerson(person);
                    ContrastColor("Green");
                    Console.WriteLine("Pessoa excluída com sucesso!");
                    Console.ResetColor();
                }
                else
                {
                    ContrastColor("Red");
                    Console.WriteLine("Pessoa não encontrada para o Id forncecido");
                    Console.ResetColor();
                }
            }
            catch
            {
                ContrastColor("Red");
                Console.WriteLine("Pessoa não encontrada para o Id forncecido");
                Console.ResetColor();
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
        public static PeopleRepository Db
        {
            get
            {
                return new PeopleRepositoryFile();
            }
        }
        public static void ShowAllPeople()
        {
            foreach (var person in Db.GetAllPeople())
            {
                Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName} - {person.Birthday.ToString("d")}");
            }
        }

        public static void ContrastColor(string color)
        {
            switch (color)
            {
                case "Red": Console.BackgroundColor = ConsoleColor.Red; break;
                case "Yellow": Console.BackgroundColor = ConsoleColor.Yellow; break;
                case "Green": Console.BackgroundColor = ConsoleColor.Green; break;
                case "Blue": Console.BackgroundColor = ConsoleColor.Blue; break;
                default: Console.BackgroundColor = ConsoleColor.Black; break;
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
