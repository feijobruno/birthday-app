using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BirthdayApp.Business;
using BirthdayApp.Data;

namespace BirthdayApp
{
    class ViewConsole
    {
        public static void MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nGerenciador de aniversários");
            Console.ResetColor();
            Console.WriteLine("Selecione uma das opções abaixo: ");
            Console.WriteLine("1 - Pesquisar pessoas ");
            Console.WriteLine("2 - Adicionar pessoas");
            Console.WriteLine("3 - Sair");

            char optionSelected = Console.ReadLine().ToCharArray()[0];
            switch (optionSelected)
            {
                case '1': FindPerson(); break;
                case '2': NewPerson(); break;
                case '3':
                    //Exit App
                    Console.WriteLine("Bye bye");
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Opção inexistente");
                    MainMenu();
                    break;
            }
        }

        static void NewPerson()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome da pessoa que deseja adicionar:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome da pessoa que deseja adicionar:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Digite a data do nascimento no formato dd/MM/yyyy: ");
            var birthday = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Os dados estão corretos? ");
            Console.WriteLine($"{firstName} {lastName}");
            Console.WriteLine(birthday.ToString("d"));
            Console.WriteLine("1 - Sim \n2 - Não");
            char option = Console.ReadLine().ToCharArray()[0];

            if (option == '1')
            {
                Console.Clear();
                var person = new Person(firstName, lastName, birthday);
                DatabaseMemory.AddNewPerson(person);
                Console.WriteLine("Dados adicionados com sucesso!");
                PressAnyKey();
                MainMenu();
            }
            if (option == '2')
            {
                Console.WriteLine("Insira os dados novamente, por favor:");
                PressAnyKey();
                NewPerson();
            }
        }
        static void FindPerson()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome, ou parte do nome, da pessoa que deseja encontrar:");
            string name = Console.ReadLine();
            Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontradas:");
            var people = DatabaseMemory.GetAllPeople(name).ToList();
            int peopleCount = people.Count();

            if (peopleCount > 0)
            {
                foreach (var person in people)
                {
                    Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName}");
                }
                int optionId = int.Parse(Console.ReadLine());
                var chosenPerson = people.Single(pessoa => pessoa.Id.Equals(optionId));

                Console.WriteLine("Dados da pessoa:");
                Console.WriteLine($"Nome Completo: {chosenPerson.FirstName} {chosenPerson.LastName}");
                Console.WriteLine($"Data do Aniversário: {chosenPerson.Birthday.ToString("d")}");
                Console.WriteLine($"Faltam {chosenPerson.CalculateDays()} dia(s) para esse aniversário.\n");
                PressAnyKey();
                MainMenu();
            }
            else
            {
                Console.WriteLine("Nenhuma pessoa encontrada para o nome: " + name);
                PressAnyKey();
                MainMenu();
            }
        }
        static void PressAnyKey()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
