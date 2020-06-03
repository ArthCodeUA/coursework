using System;
using System.Collections.Generic;
using Tickets;
using Other;

namespace Customer
{
    public class Client
    {
        private readonly string _name;
        private readonly List<Ticket> _tickets;
        private int _balance;

        public Client()
        {
            _name = Environment.UserName;
            _tickets = new List<Ticket>();
            _balance = Constants.DefaultBalance;
        }

        public List<Ticket> BoughtTickets()
        {
            return _tickets.FindAll(ticket => ticket.State == TicketState.BOUGHT);
        }

        public List<Ticket> BookedTickets()
        {
            return _tickets.FindAll(ticket => ticket.State == TicketState.BOOKED);
        }

        public void ChargeClient(int amount)
        {
            _balance -= amount;
        }

        public bool CanAfford(Ticket ticket)
        {
            return _balance - ticket.Price >= 0;
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public int FindTicket(Ticket ticket)
        {
            return _tickets.FindIndex(i => i.Equals(ticket));
        }
        
        public void RemoveTicket(Ticket ticket)
        {
            _tickets.Remove(ticket);
        }

        public string Name => _name;
        public int Balance => _balance;

        public Ticket this[int index] => _tickets[index];
    }
}