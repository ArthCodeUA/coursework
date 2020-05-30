namespace Tickets
{
    public class Ticket
    {
        private readonly int _price;
        private readonly Performance _performance;
        private TicketState _state;

        public Ticket(int price, Performance performance)
        {
            _price = price;
            _performance = performance;
            _state = TicketState.SELLING;
        }

        public int Price => _price;
        public Performance Performance => _performance;
        public TicketState State => _state;

        public Ticket ChangeState(TicketState state)
        {
            _state = state;
            return this;
        }

        public override string ToString()
        {
            return $"Ticket with price {Price} ₴";
        }
    }
}