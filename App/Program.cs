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
            Console.WriteLine($"Welcome to the Royal Theatre of Arthur the Great, {client.Name}");
            while (init)
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Search for the performance");
                Console.WriteLine("2: Look all the performances");
                Console.WriteLine("3: Check my balance");
                Console.WriteLine("4: Check all my tickets, that I've bought");
                Console.WriteLine("5: Check all my tickets, that I've booked");
                Console.WriteLine("6: Exit");
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
                                Console.WriteLine();
                                Console.WriteLine("Enter name query:");
                                Console.WriteLine();
                                string name = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByName(name));
                                break;
                            case "2":
                                Console.WriteLine();
                                Console.WriteLine("Enter author query:");
                                Console.WriteLine();
                                string author = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByAuthor(author));
                                break;
                            case "3":
                                Console.WriteLine();
                                Console.WriteLine("Enter genre query:");
                                Console.WriteLine();
                                string genre = Console.ReadLine();
                                ProcessPerformances(theatre, client, theatre.FilterByGenre(genre));
                                break;
                            case "4":
                                Console.WriteLine();
                                Console.WriteLine("Enter date query: (in format MM/DD/YYYY)");
                                Console.WriteLine();
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
                        Console.WriteLine($"Your balance is {client.Balance} ₴");
                        break;
                    case "4":
                        Console.WriteLine();
                        Console.WriteLine("Here are tickets, that you already own:");
                        if (client.BoughtTickets().Count == 0)
                        {
                            Console.WriteLine(Constants.NoTicketsBought);
                        }
                        else
                        {
                            foreach (Ticket ticket in client.BoughtTickets())
                            {
                                Console.WriteLine(ticket + "\n" +
                                                  $"To the {ticket.Performance.Name}," +
                                                  $" that will be played on {ticket.Performance.Date}");
                            }
                        }

                        break;
                    case "5":
                        Console.WriteLine();
                        Console.WriteLine("Here are tickets, that you only booked:");
                        if (client.BookedTickets().Count == 0)
                        {
                            Console.WriteLine(Constants.NoTicketsBooked);
                        }
                        else
                        {
                            foreach (Ticket ticket in client.BookedTickets())
                            {
                                Console.WriteLine(ticket + "\n" +
                                                  $"To the {ticket.Performance.Name}," +
                                                  $" that will be played on {ticket.Performance.Date}");
                            }
                        }

                        break;
                    case "6":
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
                Console.WriteLine();
                Console.WriteLine("Which one do you want to choose?");
                Console.WriteLine();
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

            Console.WriteLine();
            Console.WriteLine("Which ticket do you want to choose?");
            Console.WriteLine();
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
            Console.WriteLine();
            string action = Console.ReadLine();
            if (action == "1")
            {
                if (client.CanAfford(ticket))
                {
                    theatre.SellTicket(performance, ticket);
                    client.ChargeClient(ticket.Price);
                    client.AddTicket(ticket.ChangeState(TicketState.BOUGHT));
                    Console.WriteLine($"Ticket with price {ticket.Price} ₴ on performance {ticket.Performance.Name}");
                    Console.WriteLine("Was successfully bought.");
                }
                else
                {
                    Console.WriteLine(Constants.NotEnoughMoney);
                }
            }
            else if (action == "2")
            {
                theatre.SellTicket(performance, ticket);
                client.AddTicket(ticket.ChangeState(TicketState.BOOKED));
                Task.Run(async delegate
                {
                    await Task.Delay(Constants.BookTime);
                    if (client.CanAfford(ticket))
                    {
                        client.ChargeClient(ticket.Price);
                        client[client.FindTicket(ticket)].ChangeState(TicketState.BOUGHT);
                        Console.WriteLine(
                            $"Ticket with price {ticket.Price} ₴ on performance {ticket.Performance.Name}");
                        Console.WriteLine("Was moved from booked tickets to your bought tickets.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Ticket with price {ticket.Price} ₴ on performance {ticket.Performance.Name}");
                        Console.WriteLine("Wasn't moved from booked tickets to your bought tickets.");
                        performance.AddTicket(ticket.ChangeState(TicketState.SELLING));
                        Console.WriteLine(Constants.NotEnoughMoney);
                    }
                });
            }
            else
            {
                Console.WriteLine(Constants.WrongChoice);
            }
        }

        private static void AfishaMessageHandler(object sender, AfishaHandlerArgs handlerArgs)
        {
            Console.WriteLine(handlerArgs.Message);
        }
    }
}