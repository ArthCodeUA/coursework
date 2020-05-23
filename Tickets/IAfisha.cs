namespace Tickets
{
    public interface IAfisha
    {
        public void SellTicket(Performance performance, Ticket ticket);
        public void AddPerformance(string name, string author, string genre, string date);
        public void RemovePerformance(Performance performance);
    }
}