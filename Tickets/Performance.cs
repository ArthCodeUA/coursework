using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Tickets
{
    public class Performance : IEnumerable
    {
        private readonly string _name;
        private readonly string _author;
        private readonly string _genre;
        private readonly DateTime _date;
        private readonly List<Ticket> _tickets;

        public Performance(string name, string author, string genre, string date)
        {
            _name = name;
            _author = author;
            _genre = genre;
            _date = Convert.ToDateTime(date);
            _tickets = new List<Ticket>();
            for (int i = 0; i < Constants.DefaultTickets; i++)
            {
                AddTicket(Constants.TicketPrices[new Random().Next(0, Constants.TicketPrices.Length)]);
            }

            _tickets.Sort((t1, t2) => t1.Price.CompareTo(t2.Price));
        }

        public string Name => _name;
        public string Author => _author;
        public string Genre => _genre;
        public DateTime Date => _date;
        public int Count => _tickets.Count;

        public void AddTicket(int price)
        {
            _tickets.Add(new Ticket(price, this));
            _tickets.Sort((t1, t2) => t1.Price.CompareTo(t2.Price));
        }

        public void RemoveTicket(Ticket ticket)
        {
            _tickets.Remove(ticket);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tickets.GetEnumerator();
        }

        public Ticket this[int index] => _tickets[index];

        public override string ToString()
        {
            return $"Performance «{Name}», by {Author}, genre is {Genre} " +
                   $"will be played on {Date.ToString(CultureInfo.CurrentCulture)}\n" +
                   $"There are {_tickets.Count} tickets available!";
        }
    }
}