using System;
using Tickets;

namespace Customer
{
    public class Client
    {
        private readonly string _name;
        private Inventory<Ticket> _bought;
        private Inventory<Ticket> _booked;

        public Client()
        {
            _name = Environment.UserName;
            _bought = new Inventory<Ticket>();
            _booked = new Inventory<Ticket>();
        }

        public string Name => _name;
        public Inventory<Ticket> Bought => _bought;
        public Inventory<Ticket> Booked => _booked;
    }
}