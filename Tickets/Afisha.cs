namespace Tickets
{
    public class Afisha
    {
        public delegate void StackHandler(object sender, StoreHandlerArgs handlerArgs);
        public event StackHandler Inform;

        protected void StoreInform(StoreHandlerArgs handlerArgs)
        {
            Inform?.Invoke(this, handlerArgs);
        }
    }
}