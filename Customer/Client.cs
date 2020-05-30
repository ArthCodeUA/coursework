using System;
using System.Linq;
using Other;
using Tickets;

namespace Customer
{
    public class Client
    {
        private readonly string _name;
        private readonly Inventory<Ticket> _tickets;
        private int _balance;

        public Client()
        {
            _name = Environment.UserName;
            _tickets = new Inventory<Ticket>();
            _balance = Constants.DefaultBalance;
        }

        public int BoughtTickets()
        {
            return _tickets.Cast<Ticket>().Count(ticket => ticket.State == TicketState.BOUGHT);
        }

        public int BookedTickets()
        {
            return _tickets.Cast<Ticket>().Count(ticket => ticket.State == TicketState.BOOKED);
        }

        public void ChargeClient(int amount)
        {
            if (_balance - amount >= 0)
            {
                _balance -= amount;
            }
            else
            {
                throw new NotEnoughMoneyException(Constants.NotEnoughMoney);
            }
        }

        public bool CanAfford(Ticket ticket)
        {
            return _balance - ticket.Price >= 0;
        }

        public string Name => _name;
        public int Balance => _balance;
        public Inventory<Ticket> Tickets => _tickets;
    }
}