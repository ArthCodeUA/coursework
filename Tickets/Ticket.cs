namespace Tickets
{
    public class Ticket
    {
        private readonly int _price;
        private readonly Performance _performance;

        public Ticket(int price, Performance performance)
        {
            _price = price;
            _performance = performance;
        }

        public int Price => _price;
        public Performance Performance => _performance;
        
        public override string ToString()
        {
            return $"Ticket with price {Price} ₴";
        }
    }
}