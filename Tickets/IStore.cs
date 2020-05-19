namespace Tickets
{
    public interface IStore
    {
        public Ticket SellTicket(Ticket ticket);
        public Ticket BookTicket(Ticket ticket);
    }
}