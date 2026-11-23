
namespace WebApplication1.BookingPolicies
{
    public class SummerBookingPolicy : IBookingPolicy
    {
        public DateTime CalculateDate(DateTime? from)
        {
            if (from == null)
                return DateTime.Now.AddDays(15);
            else return from.Value.AddDays(15);
        }
    }
}
