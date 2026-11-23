namespace WebApplication1.BookingPolicies
{
    public class WinterBookingPolicy : IBookingPolicy
    {
        public DateTime CalculateDate(DateTime? from)
        {
            if (from == null)
                return DateTime.Now.AddDays(7);
            else return from.Value.AddDays(7);
        }
    }
}
