namespace HotelBookingApplication.Models
{
    public interface IGuestDetailsRepository
    {
        IEnumerable<GuestDetails> AllGuests { get; }
        void AddGuest(GuestDetails guestDetails);
        GuestDetails GetById(int guestId);
    }
}
