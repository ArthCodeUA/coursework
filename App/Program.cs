using System;
using Tickets;

namespace CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Theatre theatre = new Theatre(
                    new Performance("Romeo and Juliet",
                        "Shakespeare", "Tragedy", "10/11/2020 14:00:00"),
                    new Performance("Hamlet",
                        "Shakespeare", "Tragedy", "10/12/2020 14:00:00"),
                    new Performance("A Raisin in the Sun",
                        "Lorraine Hansberry", "Domestic tragedy", "10/13/2020 14:00:00"),
                    new Performance("Hamilton",
                        "Lin-Manuel Miranda", "Musical", "10/14/2020 14:00:00"),
                    new Performance("A Doll's House",
                        "Henrik Ibsen", "Modern tragedy", "10/15/2020 14:00:00")
                );
            InitApp(theatre);
        }

        static void InitApp(Theatre theatre)
        {
            bool init = true;
            Console.WriteLine("Welcome to the Royal Theatre of Arthur the Great");
            while (init)
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Search for the performance");
                Console.WriteLine("2: Look all the performances");
                Console.WriteLine("3: Exit");
                Console.WriteLine();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("search...");
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("Here are all the performances in nearest time:");
                        int i = 1;
                        foreach (Performance performance in theatre)
                        {
                            Console.WriteLine($"{i}: {performance}");
                            i++;
                        }
                        Console.WriteLine("Which one do you want to choose?");
                        string perfChoice = Console.ReadLine();
                        Console.WriteLine($"You've chosen {theatre[Convert.ToInt32(perfChoice)-1].Name}");
                        foreach (Ticket ticket in theatre[Convert.ToInt32(perfChoice)-1])
                        {
                            Console.WriteLine(ticket);
                        }
                        break;
                    case "3":
                        init = false;
                        break;
                    default:
                        Console.WriteLine("You did a wrong choice, please, enter a valid number.");
                        break;
                }
            }
        }
    }
}