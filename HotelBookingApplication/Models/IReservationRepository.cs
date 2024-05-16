namespace HotelBookingApplication.Models
{
    public interface IReservationRepository 
    {
        IEnumerable<Reservation> AllReservations { get; }
        Reservation GetById(int resId);
        Reservation GetByName(string name);
    }
}
