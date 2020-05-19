namespace Tickets
{
    public class Store
    {
        public delegate void StackHandler(object sender, StoreHandlerArgs handlerArgs);
        public event StackHandler Inform;

        protected void StoreInform(StoreHandlerArgs handlerArgs)
        {
            Inform?.Invoke(this, handlerArgs);
        }
    }
}