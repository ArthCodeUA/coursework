using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Other;
namespace Tickets
{
    public class Afisha : IEnumerable
    {
        protected readonly List<Performance> _afisha;

        protected Afisha()
        {
            _afisha = new List<Performance>();
        }

        public delegate void AfishaHandler(object sender, AfishaHandlerArgs handlerArgs);
        public event AfishaHandler Inform;

        protected void AfishaInform(AfishaHandlerArgs handlerArgs)
        {
            Inform?.Invoke(this, handlerArgs);
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