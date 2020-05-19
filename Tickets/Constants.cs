namespace Tickets
{
    public static class Constants
    {
        public static readonly int DefaultTickets = 10;
        public static readonly int[] TicketPrices = {50, 100, 150, 200};
        public static readonly int BookTime = 30000;
        public static readonly string WrongChoice = "You did a wrong choice, please, enter a valid number.";
        public static readonly string NotFound = "Nothing found due to your search or there are not performances.";
        public static readonly string FormatException = "Wrong date format, use MM/DD/YYYY.";
    }
}