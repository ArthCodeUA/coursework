using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets;
using Other;
using Customer;

namespace CW
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                Client client = new Client();
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
                        "Henrik Ibsen", "Modern tragedy", "10/15/2020 14:00:00"),
                    new Performance("Danylo and his Clowns",
                        "Shiron Gunawardana", "Modern realism", "10/16/2020 14:00:00")
                );
                theatre.Inform += AfishaMessageHandler;
                InitApp(theatre, client);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Constants.FormatException);
            }
        }

        private static void InitApp(Theatre theatre, Client client)
        {
            bool init = true;
            Console.WriteLine("Welcome to the Royal Theatre of Arthur the Great");
            while (init)
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Search for the performance");
                Console.WriteLine("2: Look all the performances");
                Console.WriteLine("3: Check all my tickets, that I've bought");
                Console.WriteLine("4: Check all my tickets, that I've booked");
                Console.WriteLine("5: Exit");
                Console.WriteLine();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine("Here are 4 criteria, that you can use to search for a performance.");
                        Console.WriteLine("1: Name");
                        Console.WriteLine("2: Author");
                        Console.WriteLine("3: Genre");
                        Console.WriteLine("4: Date");
                        Console.WriteLine();
                        string criteria = Console.ReadLine();
                        switch (criteria)
                        {
                            case "1":
                                Console.WriteLine("Enter name query:");
                                string name = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByName(name));
                                break;
                            case "2":
                                Console.WriteLine("Enter author query:");
                                string author = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByAuthor(author));
                                break;
                            case "3":
                                Console.WriteLine("Enter genre query:");
                                string genre = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByGenre(genre));
                                break;
                            case "4":
                                Console.WriteLine("Enter date query: (in format MM/DD/YYYY)");
                                string date = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByDate(date));
                                break;
                            default:
                                Console.WriteLine(Constants.WrongChoice);
                                break;
                        }

                        break;
                    case "2":
                        ProcessPerformances(theatre, client);
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("Here are tickets, that you already own:");
                        if (client.Bought.Count == 0)
                        {
                            Console.WriteLine(Constants.NoTicketsBought);
                        }
                        else
                        {
                            foreach (Ticket ticket in client.Bought)
                            {
                                Console.WriteLine(ticket + "\n" +
                                                  $"To the {ticket.Performance.Name}," +
                                                  $" that will be played on {ticket.Performance.Date}");
                            }
                        }

                        break;
                    case "4":
                        Console.WriteLine();
                        Console.WriteLine("Here are tickets, that you only booked:");
                        if (client.Bought.Count == 0)
                        {
                            Console.WriteLine(Constants.NoTicketsBooked);
                        }
                        else
                        {
                            foreach (Ticket ticket in client.Booked)
                            {
                                Console.WriteLine(ticket + "\n" +
                                                  $"To the {ticket.Performance.Name}," +
                                                  $" that will be played on {ticket.Performance.Date}");
                            }
                        }

                        break;
                    case "5":
                        init = false;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(Constants.WrongChoice);
                        break;
                }
            }
        }

        static void ProcessPerformances(Theatre theatre, Client client, List<Performance> performances = null)
        {
            Console.WriteLine();
            Console.WriteLine("Here are all the performances in nearest time:");
            int i = 1;
            if (performances == null)
            {
                foreach (Performance performance in theatre)
                {
                    Console.WriteLine($"{i}: {performance}");
                    i++;
                }
            }
            else
            {
                foreach (Performance performance in performances)
                {
                    Console.WriteLine($"{i}: {performance}");
                    i++;
                }
            }

            if (i != 1)
            {
                Console.WriteLine("Which one do you want to choose?");
                int chosen = Convert.ToInt32(Console.ReadLine());
                if (chosen < i && chosen >= 1)
                {
                    ProcessPerformance(theatre, performances == null ? theatre[chosen - 1] : performances[chosen - 1],
                        client);
                }
                else
                {
                    Console.WriteLine(Constants.WrongChoice);
                }
            }
            else
            {
                Console.WriteLine(Constants.NotFound);
            }
        }

        static void ProcessPerformance(Theatre theatre, Performance performance, Client client)
        {
            Console.WriteLine();
            Console.WriteLine($"You've chosen {performance.Name}");
            int i = 1;
            foreach (Ticket ticket in performance)
            {
                Console.WriteLine($"{i}: {ticket}");
                i++;
            }

            Console.WriteLine("Which ticket do you want to choose?");
            int chosen = Convert.ToInt32(Console.ReadLine());
            if (chosen < i && chosen >= 1)
            {
                ProcessTicket(theatre, performance, performance[chosen - 1], client);
            }
            else
            {
                Console.WriteLine(Constants.WrongChoice);
            }
        }

        private static void ProcessTicket(Theatre theatre, Performance performance, Ticket ticket, Client client)
        {
            Console.WriteLine();
            Console.WriteLine($"You've chosen ticket with price {ticket.Price} ₴");
            Console.WriteLine("Which action you want to do?");
            Console.WriteLine("1: Buy this ticket");
            Console.WriteLine("2: Book this ticket");
            string action = Console.ReadLine();
            if (action == "1")
            {
                theatre.SellTicket(performance, ticket);
                client.Bought.AddItem(ticket);
            }
            else if (action == "2")
            {
                theatre.SellTicket(performance, ticket);
                client.Booked.AddItem(ticket);
                Task.Run(async delegate
                {
                    await Task.Delay(Constants.BookTime);
                    client.Booked.RemoveItem(ticket);
                    client.Bought.AddItem(ticket);
                    Console.WriteLine($"Ticket with price {ticket.Price} ₴ on performance {ticket.Performance.Name}");
                    Console.WriteLine("Was moved from booked tickets to your bought tickets.");
                });
            }
        }

        private static void AfishaMessageHandler(object sender, AfishaHandlerArgs handlerArgs)
        {
            Console.WriteLine(handlerArgs.Message);
        }
    }
}