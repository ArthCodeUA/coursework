using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Tickets
{
    public class Performance : IEnumerable
    {
        private string _name;
        private string _author;
        private string _genre;
        private DateTime _date;
        private List<Ticket> _tickets;

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
        }

        public string Name => _name;
        public string Author => _author;
        public string Genre => _genre;
        public DateTime Date => _date;

        public void AddTicket(int price)
        {
            _tickets.Add(new Ticket(price, this));
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