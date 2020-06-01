using Other;

namespace Tickets
{
    public class Theatre : Afisha, IAfisha
    {
        public Theatre(params Performance[] performances)
        {
            foreach (Performance performance in performances)
            {
                _afisha.Add(performance);
            }
        }

        public Performance this[int index] => _afisha[index];

        public void SellTicket(Performance performance, Ticket ticket)
        {
            performance.RemoveTicket(ticket);
            AfishaInform(
                this, new AfishaHandlerArgs($"Ticket with price {ticket.Price} ₴ on" +
                                            $" performance {performance.Name} was sold!")
            );
            if (performance.Count != 0) return;
            AfishaInform(
                this, new AfishaHandlerArgs($"Tickets on performance {performance.Name}" +
                                            " was sold completely!")
            );
            RemovePerformance(performance);
        }

        public void AddPerformance(string name, string author, string genre, string date)
        {
            _afisha.Add(new Performance(name, author, genre, date));
            AfishaInform(
                this, new AfishaHandlerArgs($"Performance {name} by {author} was added to Afisha!")
            );
        }

        public void RemovePerformance(Performance performance)
        {
            _afisha.Remove(performance);
            AfishaInform(
                this, new AfishaHandlerArgs($"Performance {performance.Name} " +
                                            $"by {performance.Author} was removed!")
            );
        }
    }
}