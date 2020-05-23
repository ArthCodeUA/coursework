using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Other;

namespace Tickets
{
    public class Theatre : Afisha, IAfisha, IEnumerable
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
                new AfishaHandlerArgs($"Ticket with price {ticket.Price} ₴ on" +
                                      $" performance {performance.Name} was sold!")
            );
            if (performance.Count != 0) return;
            RemovePerformance(performance);
            AfishaInform(
                new AfishaHandlerArgs($"Tickets on performance {performance.Name} was sold completely!")
            );
        }

        public void AddPerformance(string name, string author, string genre, string date)
        {
            _afisha.Add(new Performance(name, author, genre, date));
        }

        public void RemovePerformance(Performance performance)
        {
            _afisha.Remove(performance);
        }

        public List<Performance> FilterByName(string name)
        {
            List<Performance> filtered = new List<Performance>();
            foreach (Performance performance in this)
            {
                if (performance.Name.ToLower().Contains(name.ToLower()))
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
                if (performance.Author.ToLower().Contains(author.ToLower()))
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
                if (performance.Genre.ToLower().Contains(genre.ToLower()))
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