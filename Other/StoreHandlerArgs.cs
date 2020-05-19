namespace Tickets
{
    public class StoreHandlerArgs
    {
        public string Message { get; }

        public StoreHandlerArgs(string message)
        {
            Message = message;
        }
    }
}