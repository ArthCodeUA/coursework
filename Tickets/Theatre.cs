using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Tickets
{
    public class Theatre : Afisha, IAfisha, IEnumerable
    {
        private List<Performance> _afisha;

        public Theatre(params Performance[] performances)
        {
            _afisha = new List<Performance>();
            foreach (Performance performance in performances)
            {
                _afisha.Add(performance);
            }
        }

        public Performance this[int index] => _afisha[index];

        public void SellTicket(Performance performance, Ticket ticket)
        {
            performance.RemoveTicket(ticket);
            StoreInform(
                new StoreHandlerArgs($"Ticket with price {ticket.Price} on" +
                                     $" performance {performance.Name} was sold!")
            );
        }

        public void AddPerformance(string name, string author, string genre, string date)
        {
            _afisha.Add(new Performance(name, author, genre, date));
        }

        public List<Performance> FilterByName(string name)
        {
            List<Performance> filtered = new List<Performance>();
            foreach (Performance performance in this)
            {
                if (performance.Name.Contains(name))
                {
                    filtered.Add(performance);
                }
            }

            return filtered;
        }

        public List<Performance> FilterByAuthor(string author)
        {
            List<Performance> filtered = new List<Performance>();
            foreach (Performance performance in this)
            {
                if (performance.Author.Contains(author))
                {
                    filtered.Add(performance);
                }
            }

            return filtered;
        }

        public List<Performance> FilterByGenre(string genre)
        {
            List<Performance> filtered = new List<Performance>();
            foreach (Performance performance in this)
            {
                if (performance.Genre.Contains(genre))
                {
                    filtered.Add(performance);
                }
            }

            return filtered;
        }

        public List<Performance> FilterByDate(string date)
        {
            List<Performance> filtered = new List<Performance>();
            foreach (Performance performance in this)
            {
                if (performance.Date.ToString(CultureInfo.CurrentCulture).Contains(date))
                {
                    filtered.Add(performance);
                }
            }

            return filtered;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _afisha.GetEnumerator();
        }
    }
}