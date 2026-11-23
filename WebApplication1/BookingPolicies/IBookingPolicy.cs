namespace WebApplication1.BookingPolicies
{
    public interface IBookingPolicy
    {
        DateTime CalculateDate(DateTime? from);
    }
}
