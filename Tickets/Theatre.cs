using System.Collections;
using System.Collections.Generic;

namespace Tickets
{
    public class Theatre : IEnumerable
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _afisha.GetEnumerator();
        }
    }
}