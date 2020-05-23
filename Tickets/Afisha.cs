using System.Collections.Generic;
using Other;
namespace Tickets
{
    public class Afisha
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
    }
}